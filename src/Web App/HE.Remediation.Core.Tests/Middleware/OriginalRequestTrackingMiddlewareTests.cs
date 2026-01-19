using System.Diagnostics;
using HE.Remediation.Core.Middleware;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Moq;
using Xunit;

namespace HE.Remediation.Core.Tests.Middleware;

public class OriginalRequestTrackingMiddlewareTests : IDisposable
{
    private readonly Mock<RequestDelegate> _nextMock;
    private readonly DefaultHttpContext _httpContext;
    private readonly ActivitySource _activitySource;
    private readonly ActivityListener _activityListener;

    public OriginalRequestTrackingMiddlewareTests()
    {
        _nextMock = new Mock<RequestDelegate>();
        _httpContext = new DefaultHttpContext();
        
        // Create an activity source and listener for testing
        _activitySource = new ActivitySource("TestSource");
        
        // Set up activity listener to enable activity creation
        _activityListener = new ActivityListener
        {
            ShouldListenTo = source => source.Name == "TestSource",
            Sample = (ref ActivityCreationOptions<ActivityContext> options) => ActivitySamplingResult.AllDataAndRecorded
        };
        ActivitySource.AddActivityListener(_activityListener);
    }
    
    private Activity StartActivity()
    {
        var activity = _activitySource.StartActivity("TestActivity");
        return activity!;
    }

    [Fact]
    public async Task InvokeAsync_WithActivity_SetsOriginalPathTags()
    {
        // Arrange
        using var activity = StartActivity();
        _httpContext.Request.Path = "/application/submit";
        _httpContext.Request.Method = "POST";
        _httpContext.Request.QueryString = new QueryString("?id=123&status=pending");

        var middleware = new OriginalRequestTrackingMiddleware(_nextMock.Object);

        // Act
        await middleware.InvokeAsync(_httpContext);

        // Assert
        Assert.Contains(activity.Tags, t => t.Key == "http.original_path" && t.Value == "/application/submit");
        Assert.Contains(activity.Tags, t => t.Key == "http.original_method" && t.Value == "POST");
        Assert.Contains(activity.Tags, t => t.Key == "http.original_query_string" && t.Value == "?id=123&status=pending");
        
        _nextMock.Verify(next => next(_httpContext), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_WithoutQueryString_DoesNotSetQueryStringTag()
    {
        // Arrange
        using var activity = StartActivity();
        _httpContext.Request.Path = "/application/home";
        _httpContext.Request.Method = "GET";
        _httpContext.Request.QueryString = QueryString.Empty;

        var middleware = new OriginalRequestTrackingMiddleware(_nextMock.Object);

        // Act
        await middleware.InvokeAsync(_httpContext);

        // Assert
        Assert.Contains(activity.Tags, t => t.Key == "http.original_path" && t.Value == "/application/home");
        Assert.Contains(activity.Tags, t => t.Key == "http.original_method" && t.Value == "GET");
        Assert.DoesNotContain(activity.Tags, t => t.Key == "http.original_query_string");
    }

    [Fact]
    public async Task InvokeAsync_SetsBaggageForPropagation()
    {
        // Arrange
        using var activity = StartActivity();
        _httpContext.Request.Path = "/application/validate";
        _httpContext.Request.Method = "POST";

        var middleware = new OriginalRequestTrackingMiddleware(_nextMock.Object);

        // Act
        await middleware.InvokeAsync(_httpContext);

        // Assert
        Assert.Equal("/application/validate", activity.GetBaggageItem("original.path"));
        Assert.Equal("POST", activity.GetBaggageItem("original.method"));
    }

    [Fact]
    public async Task InvokeAsync_ErrorEndpoint_CapturesReferer()
    {
        // Arrange
        using var activity = StartActivity();
        _httpContext.Request.Path = "/error";
        _httpContext.Request.Method = "GET";
        _httpContext.Request.Headers["Referer"] = "https://app.example.com/application/form";

        var middleware = new OriginalRequestTrackingMiddleware(_nextMock.Object);

        // Act
        await middleware.InvokeAsync(_httpContext);

        // Assert
        Assert.Contains(activity.Tags, t => t.Key == "error.referer_url" && t.Value == "https://app.example.com/application/form");
    }

    [Fact]
    public async Task InvokeAsync_ErrorEndpointWithException_CapturesExceptionDetails()
    {
        // Arrange
        using var activity = StartActivity();
        _httpContext.Request.Path = "/error/500";
        _httpContext.Request.Method = "GET";

        var exception = new InvalidOperationException("Test error message");
        var exceptionFeature = new Mock<IExceptionHandlerFeature>();
        exceptionFeature.Setup(e => e.Error).Returns(exception);
        exceptionFeature.Setup(e => e.Path).Returns("/application/submit");

        _httpContext.Features.Set(exceptionFeature.Object);

        var middleware = new OriginalRequestTrackingMiddleware(_nextMock.Object);

        // Act
        await middleware.InvokeAsync(_httpContext);

        // Assert
        Assert.Contains(activity.Tags, t => t.Key == "error.exception_type" && t.Value == "System.InvalidOperationException");
        // Note: error.exception_message is no longer captured to avoid exposing sensitive information
        Assert.Contains(activity.Tags, t => t.Key == "error.original_failing_path" && t.Value == "/application/submit");
    }

    [Fact]
    public async Task InvokeAsync_ErrorEndpointWithExceptionSource_CapturesSource()
    {
        // Arrange
        using var activity = StartActivity();
        _httpContext.Request.Path = "/error";
        _httpContext.Request.Method = "GET";

        var exception = new NullReferenceException("Object reference not set")
        {
            Source = "HE.Remediation.WebApp"
        };
        
        var exceptionFeature = new Mock<IExceptionHandlerFeature>();
        exceptionFeature.Setup(e => e.Error).Returns(exception);
        exceptionFeature.Setup(e => e.Path).Returns("/application/details");

        _httpContext.Features.Set(exceptionFeature.Object);

        var middleware = new OriginalRequestTrackingMiddleware(_nextMock.Object);

        // Act
        await middleware.InvokeAsync(_httpContext);

        // Assert
        Assert.Contains(activity.Tags, t => t.Key == "error.exception_source" && t.Value == "HE.Remediation.WebApp");
    }

    [Fact]
    public async Task InvokeAsync_ErrorEndpointCaseInsensitive_DetectsError()
    {
        // Arrange
        using var activity = StartActivity();
        _httpContext.Request.Path = "/Error/HandleError/500";
        _httpContext.Request.Method = "GET";
        _httpContext.Request.Headers["Referer"] = "https://app.example.com/test";

        var middleware = new OriginalRequestTrackingMiddleware(_nextMock.Object);

        // Act
        await middleware.InvokeAsync(_httpContext);

        // Assert
        Assert.Contains(activity.Tags, t => t.Key == "error.referer_url");
    }

    [Fact]
    public async Task InvokeAsync_WhenExceptionThrown_CapturesCaughtExceptionDetails()
    {
        // Arrange
        using var activity = StartActivity();
        _httpContext.Request.Path = "/application/submit";
        _httpContext.Request.Method = "POST";

        var expectedException = new ArgumentException("Invalid application ID");
        _nextMock.Setup(next => next(_httpContext)).ThrowsAsync(expectedException);

        var middleware = new OriginalRequestTrackingMiddleware(_nextMock.Object);

        // Act & Assert
        var thrownException = await Assert.ThrowsAsync<ArgumentException>(
            () => middleware.InvokeAsync(_httpContext));

        Assert.Equal(expectedException, thrownException);
        Assert.Contains(activity.Tags, t => t.Key == "error.caught_exception_type" && t.Value == "System.ArgumentException");
        // Note: error.caught_exception_message is no longer captured to avoid exposing sensitive information
        Assert.Contains(activity.Tags, t => t.Key == "error.caught_at_path" && t.Value == "/application/submit");
    }

    [Fact]
    public async Task InvokeAsync_WithoutActivity_DoesNotThrow()
    {
        // Arrange
        Activity.Current = null;
        _httpContext.Request.Path = "/application/home";
        _httpContext.Request.Method = "GET";

        var middleware = new OriginalRequestTrackingMiddleware(_nextMock.Object);

        // Act & Assert - should not throw
        await middleware.InvokeAsync(_httpContext);
        
        _nextMock.Verify(next => next(_httpContext), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_ErrorEndpointWithoutException_OnlyCapturesReferer()
    {
        // Arrange
        using var activity = StartActivity();
        _httpContext.Request.Path = "/error";
        _httpContext.Request.Method = "GET";
        _httpContext.Request.Headers["Referer"] = "https://app.example.com/previous";

        // No exception feature set
        var middleware = new OriginalRequestTrackingMiddleware(_nextMock.Object);

        // Act
        await middleware.InvokeAsync(_httpContext);

        // Assert
        Assert.Contains(activity.Tags, t => t.Key == "error.referer_url" && t.Value == "https://app.example.com/previous");
        Assert.DoesNotContain(activity.Tags, t => t.Key == "error.exception_type");
        Assert.DoesNotContain(activity.Tags, t => t.Key == "error.exception_message");
    }

    [Fact]
    public async Task InvokeAsync_ErrorEndpointWithEmptyReferer_DoesNotSetRefererTag()
    {
        // Arrange
        using var activity = StartActivity();
        _httpContext.Request.Path = "/error";
        _httpContext.Request.Method = "GET";
        // Empty referer

        var middleware = new OriginalRequestTrackingMiddleware(_nextMock.Object);

        // Act
        await middleware.InvokeAsync(_httpContext);

        // Assert
        Assert.DoesNotContain(activity.Tags, t => t.Key == "error.referer_url");
    }

    [Fact]
    public async Task InvokeAsync_NormalRequest_CallsNextDelegate()
    {
        // Arrange
        using var activity = StartActivity();
        _httpContext.Request.Path = "/home/index";
        _httpContext.Request.Method = "GET";

        var middleware = new OriginalRequestTrackingMiddleware(_nextMock.Object);

        // Act
        await middleware.InvokeAsync(_httpContext);

        // Assert
        _nextMock.Verify(next => next(_httpContext), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_ComplexPath_CapturesCorrectly()
    {
        // Arrange
        using var activity = StartActivity();
        _httpContext.Request.Path = "/Application/WorksPackage/Submit/Details";
        _httpContext.Request.Method = "PUT";
        _httpContext.Request.QueryString = new QueryString("?applicationId=abc-123&workPackageId=wp-456");

        var middleware = new OriginalRequestTrackingMiddleware(_nextMock.Object);

        // Act
        await middleware.InvokeAsync(_httpContext);

        // Assert
        Assert.Contains(activity.Tags, t => t.Key == "http.original_path" && t.Value == "/Application/WorksPackage/Submit/Details");
        Assert.Contains(activity.Tags, t => t.Key == "http.original_method" && t.Value == "PUT");
        Assert.Contains(activity.Tags, t => t.Key == "http.original_query_string" && t.Value == "?applicationId=abc-123&workPackageId=wp-456");
    }

    [Fact]
    public async Task InvokeAsync_ExceptionWithNullPath_DoesNotThrow()
    {
        // Arrange
        using var activity = StartActivity();
        _httpContext.Request.Path = "/error";
        _httpContext.Request.Method = "GET";

        var exception = new Exception("Test error");
        var exceptionFeature = new Mock<IExceptionHandlerFeature>();
        exceptionFeature.Setup(e => e.Error).Returns(exception);
        exceptionFeature.Setup(e => e.Path).Returns((string)null);

        _httpContext.Features.Set(exceptionFeature.Object);

        var middleware = new OriginalRequestTrackingMiddleware(_nextMock.Object);

        // Act & Assert - should not throw
        await middleware.InvokeAsync(_httpContext);
        
        Assert.Contains(activity.Tags, t => t.Key == "error.exception_type");
        Assert.DoesNotContain(activity.Tags, t => t.Key == "error.original_failing_path");
    }

    public void Dispose()
    {
        _activityListener?.Dispose();
        _activitySource?.Dispose();
    }
}
