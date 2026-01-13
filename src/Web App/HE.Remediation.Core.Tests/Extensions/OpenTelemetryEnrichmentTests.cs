using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Xunit;

namespace HE.Remediation.Core.Tests.Extensions;

public class OpenTelemetryEnrichmentTests : TestBase
{
    [Fact]
    public void HttpRequestEnrichment_WithReferer_AddsRefererTag()
    {
        // Arrange
        var activity = new Activity("test");
        activity.Start();
        
        var context = new DefaultHttpContext();
        context.Request.Headers["Referer"] = "https://example.com/previous-page";

        // Act
        EnrichWithHttpRequest(activity, context.Request);

        // Assert
        Assert.Contains(activity.Tags, tag => tag.Key == "http.referer" && tag.Value == "https://example.com/previous-page");
        activity.Stop();
    }

    [Fact]
    public void HttpRequestEnrichment_WithoutReferer_DoesNotAddRefererTag()
    {
        // Arrange
        var activity = new Activity("test");
        activity.Start();
        
        var context = new DefaultHttpContext();

        // Act
        EnrichWithHttpRequest(activity, context.Request);

        // Assert
        Assert.DoesNotContain(activity.Tags, tag => tag.Key == "http.referer");
        activity.Stop();
    }

    [Fact]
    public void HttpRequestEnrichment_WithOrigin_AddsOriginTag()
    {
        // Arrange
        var activity = new Activity("test");
        activity.Start();
        
        var context = new DefaultHttpContext();
        context.Request.Headers["Origin"] = "https://app.example.com";

        // Act
        EnrichWithHttpRequest(activity, context.Request);

        // Assert
        Assert.Contains(activity.Tags, tag => tag.Key == "http.origin" && tag.Value == "https://app.example.com");
        activity.Stop();
    }

    [Fact]
    public void HttpRequestEnrichment_WithUserAgent_AddsUserAgentTag()
    {
        // Arrange
        var activity = new Activity("test");
        activity.Start();
        
        var context = new DefaultHttpContext();
        context.Request.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64)";

        // Act
        EnrichWithHttpRequest(activity, context.Request);

        // Assert
        Assert.Contains(activity.Tags, tag => tag.Key == "http.user_agent" && tag.Value == "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
        activity.Stop();
    }

    [Fact]
    public void HttpRequestEnrichment_WithQueryString_AddsQueryStringTag()
    {
        // Arrange
        var activity = new Activity("test");
        activity.Start();
        
        var context = new DefaultHttpContext();
        context.Request.QueryString = new QueryString("?id=123&type=test");

        // Act
        EnrichWithHttpRequest(activity, context.Request);

        // Assert
        Assert.Contains(activity.Tags, tag => tag.Key == "http.query_string" && tag.Value == "?id=123&type=test");
        activity.Stop();
    }

    [Fact]
    public void HttpRequestEnrichment_WithoutQueryString_DoesNotAddQueryStringTag()
    {
        // Arrange
        var activity = new Activity("test");
        activity.Start();
        
        var context = new DefaultHttpContext();

        // Act
        EnrichWithHttpRequest(activity, context.Request);

        // Assert
        Assert.DoesNotContain(activity.Tags, tag => tag.Key == "http.query_string");
        activity.Stop();
    }

    [Fact]
    public void HttpRequestEnrichment_WithContentType_AddsContentTypeTag()
    {
        // Arrange
        var activity = new Activity("test");
        activity.Start();
        
        var context = new DefaultHttpContext();
        context.Request.ContentType = "application/json";

        // Act
        EnrichWithHttpRequest(activity, context.Request);

        // Assert
        Assert.Contains(activity.Tags, tag => tag.Key == "http.request.content_type" && tag.Value == "application/json");
        activity.Stop();
    }

    [Fact]
    public void HttpRequestEnrichment_WithRemoteIpAddress_AddsClientIpTag()
    {
        // Arrange
        var activity = new Activity("test");
        activity.Start();
        
        var context = new DefaultHttpContext();
        context.Connection.RemoteIpAddress = System.Net.IPAddress.Parse("192.168.1.100");

        // Act
        EnrichWithHttpRequest(activity, context.Request);

        // Assert
        Assert.Contains(activity.Tags, tag => tag.Key == "client.ip" && tag.Value == "192.168.1.100");
        activity.Stop();
    }

    [Fact]
    public void HttpRequestEnrichment_WithAuthenticatedUser_AddsUserIdTag()
    {
        // Arrange
        var activity = new Activity("test");
        activity.Start();
        
        var context = new DefaultHttpContext();
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, "user-123"),
            new Claim(ClaimTypes.Name, "John Doe")
        };
        var identity = new ClaimsIdentity(claims, "TestAuth");
        context.User = new ClaimsPrincipal(identity);

        // Act
        EnrichWithHttpRequest(activity, context.Request);

        // Assert
        Assert.Contains(activity.Tags, tag => tag.Key == "user.id" && tag.Value == "user-123");
        Assert.Contains(activity.Tags, tag => tag.Key == "user.name" && tag.Value == "John Doe");
        activity.Stop();
    }

    [Fact]
    public void HttpRequestEnrichment_WithUnauthenticatedUser_DoesNotAddUserTags()
    {
        // Arrange
        var activity = new Activity("test");
        activity.Start();
        
        var context = new DefaultHttpContext();

        // Act
        EnrichWithHttpRequest(activity, context.Request);

        // Assert
        Assert.DoesNotContain(activity.Tags, tag => tag.Key == "user.id");
        Assert.DoesNotContain(activity.Tags, tag => tag.Key == "user.name");
        activity.Stop();
    }

    [Fact]
    public void HttpRequestEnrichment_WithRouteEndpoint_AddsRouteTag()
    {
        // Arrange
        var activity = new Activity("test");
        activity.Start();
        
        var context = new DefaultHttpContext();
        
        // Create a mock RouteEndpoint with route pattern
        var routeValues = new RouteValueDictionary();
        var endpointMetadata = new EndpointMetadataCollection(
            new RouteEndpoint(
                context => Task.CompletedTask,
                Microsoft.AspNetCore.Routing.Patterns.RoutePatternFactory.Parse("/BankAccount/{action}"),
                0,
                null,
                null)
        );
        
        var endpoint = new RouteEndpoint(
            context => Task.CompletedTask,
            Microsoft.AspNetCore.Routing.Patterns.RoutePatternFactory.Parse("/BankAccount/{action}"),
            0,
            endpointMetadata,
            null);
        
        context.SetEndpoint(endpoint);

        // Act
        EnrichWithHttpRequest(activity, context.Request);

        // Assert
        Assert.Contains(activity.Tags, tag => tag.Key == "http.route" && tag.Value == "/BankAccount/{action}");
        activity.Stop();
    }

    [Fact]
    public void HttpRequestEnrichment_WithoutRouteEndpoint_DoesNotAddRouteTag()
    {
        // Arrange
        var activity = new Activity("test");
        activity.Start();
        
        var context = new DefaultHttpContext();

        // Act
        EnrichWithHttpRequest(activity, context.Request);

        // Assert
        Assert.DoesNotContain(activity.Tags, tag => tag.Key == "http.route");
        activity.Stop();
    }

    [Fact]
    public void HttpResponseEnrichment_AddsResponseTags()
    {
        // Arrange
        var activity = new Activity("test");
        activity.Start();
        
        var context = new DefaultHttpContext();
        context.Response.ContentType = "text/html";
        context.Response.ContentLength = 1024;

        // Act
        EnrichWithHttpResponse(activity, context.Response);

        // Assert
        var tags = activity.Tags.ToList();
        Assert.Contains(tags, tag => tag.Key == "http.response.content_type" && tag.Value == "text/html");
        Assert.Contains(tags, tag => tag.Key == "http.response.content_length" && tag.Value == "1024");
        activity.Stop();
    }

    [Fact]
    public void HttpResponseEnrichment_WithNullContentLength_AddsZeroValue()
    {
        // Arrange
        var activity = new Activity("test");
        activity.Start();
        
        var context = new DefaultHttpContext();
        context.Response.ContentType = "application/json";

        // Act
        EnrichWithHttpResponse(activity, context.Response);

        // Assert
        var tags = activity.Tags.ToList();
        Assert.Contains(tags, tag => tag.Key == "http.response.content_length" && tag.Value == "0");
        activity.Stop();
    }

    [Fact]
    public void HttpRequestEnrichment_WithMultipleHeaders_AddsAllTags()
    {
        // Arrange
        var activity = new Activity("test");
        activity.Start();
        
        var context = new DefaultHttpContext();
        context.Request.Headers["Referer"] = "https://example.com/page";
        context.Request.Headers["Origin"] = "https://example.com";
        context.Request.Headers["User-Agent"] = "TestBrowser/1.0";
        context.Request.ContentType = "application/x-www-form-urlencoded";
        context.Request.QueryString = new QueryString("?test=value");
        context.Connection.RemoteIpAddress = System.Net.IPAddress.Parse("10.0.0.1");

        // Act
        EnrichWithHttpRequest(activity, context.Request);

        // Assert
        Assert.Contains(activity.Tags, tag => tag.Key == "http.referer");
        Assert.Contains(activity.Tags, tag => tag.Key == "http.origin");
        Assert.Contains(activity.Tags, tag => tag.Key == "http.user_agent");
        Assert.Contains(activity.Tags, tag => tag.Key == "http.request.content_type");
        Assert.Contains(activity.Tags, tag => tag.Key == "http.query_string");
        Assert.Contains(activity.Tags, tag => tag.Key == "client.ip");
        activity.Stop();
    }

    [Fact]
    public void HttpClientRequestEnrichment_WithRequestMessage_AddsMethodTag()
    {
        // Arrange
        var activity = new Activity("test");
        activity.Start();
        
        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.example.com/data");

        // Act
        EnrichWithHttpRequestMessage(activity, request);

        // Assert
        Assert.Contains(activity.Tags, tag => tag.Key == "http.request.method" && tag.Value == "POST");
        activity.Stop();
    }

    [Fact]
    public void HttpClientRequestEnrichment_WithContent_AddsContentTags()
    {
        // Arrange
        var activity = new Activity("test");
        activity.Start();
        
        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.example.com/data")
        {
            Content = new StringContent("{\"test\":\"data\"}", System.Text.Encoding.UTF8, "application/json")
        };

        // Act
        EnrichWithHttpRequestMessage(activity, request);

        // Assert
        var tags = activity.Tags.ToList();
        Assert.Contains(tags, tag => tag.Key == "http.request.has_content" && tag.Value == "True");
        Assert.Contains(tags, tag => tag.Key == "http.request.content_type" && tag.Value?.Contains("application/json") == true);
        activity.Stop();
    }

    [Fact]
    public void HttpClientRequestEnrichment_WithoutContent_DoesNotAddContentTags()
    {
        // Arrange
        var activity = new Activity("test");
        activity.Start();
        
        var request = new HttpRequestMessage(HttpMethod.Get, "https://api.example.com/data");

        // Act
        EnrichWithHttpRequestMessage(activity, request);

        // Assert
        Assert.DoesNotContain(activity.Tags, tag => tag.Key == "http.request.has_content");
        activity.Stop();
    }

    [Fact]
    public void HttpClientResponseEnrichment_AddsResponseTags()
    {
        // Arrange
        var activity = new Activity("test");
        activity.Start();
        
        var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
        {
            ReasonPhrase = "OK",
            Content = new StringContent("response data")
        };

        // Act
        EnrichWithHttpResponseMessage(activity, response);

        // Assert
        var tags = activity.Tags.ToList();
        Assert.Contains(tags, tag => tag.Key == "http.response.reason_phrase" && tag.Value == "OK");
        Assert.Contains(tags, tag => tag.Key == "http.response.content_length");
        activity.Stop();
    }

    // Helper methods that simulate the actual enrichment callbacks
    private void EnrichWithHttpRequest(Activity activity, HttpRequest httpRequest)
    {
        var referer = httpRequest.Headers["Referer"].ToString();
        if (!string.IsNullOrEmpty(referer))
        {
            activity.SetTag("http.referer", referer);
        }

        var origin = httpRequest.Headers["Origin"].ToString();
        if (!string.IsNullOrEmpty(origin))
        {
            activity.SetTag("http.origin", origin);
        }

        var userAgent = httpRequest.Headers["User-Agent"].ToString();
        if (!string.IsNullOrEmpty(userAgent))
        {
            activity.SetTag("http.user_agent", userAgent);
        }

        var contentType = httpRequest.ContentType;
        if (!string.IsNullOrEmpty(contentType))
        {
            activity.SetTag("http.request.content_type", contentType);
        }

        if (httpRequest.QueryString.HasValue)
        {
            activity.SetTag("http.query_string", httpRequest.QueryString.Value);
        }

        var clientIp = httpRequest.HttpContext.Connection.RemoteIpAddress?.ToString();
        if (!string.IsNullOrEmpty(clientIp))
        {
            activity.SetTag("client.ip", clientIp);
        }

        if (httpRequest.HttpContext.User?.Identity?.IsAuthenticated == true)
        {
            var userId = httpRequest.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                activity.SetTag("user.id", userId);
            }

            var userName = httpRequest.HttpContext.User.Identity.Name;
            if (!string.IsNullOrEmpty(userName))
            {
                activity.SetTag("user.name", userName);
            }
        }

        var endpoint = httpRequest.HttpContext.GetEndpoint();
        if (endpoint != null)
        {
            var routePattern = endpoint.Metadata.GetMetadata<RouteEndpoint>()?.RoutePattern?.RawText;
            if (!string.IsNullOrEmpty(routePattern))
            {
                activity.SetTag("http.route", routePattern);
            }
        }
    }

    private void EnrichWithHttpResponse(Activity activity, HttpResponse httpResponse)
    {
        if (!string.IsNullOrEmpty(httpResponse.ContentType))
        {
            activity.SetTag("http.response.content_type", httpResponse.ContentType);
        }
        activity.SetTag("http.response.content_length", (httpResponse.ContentLength ?? 0).ToString());
    }

    private void EnrichWithHttpRequestMessage(Activity activity, HttpRequestMessage httpRequestMessage)
    {
        activity.SetTag("http.request.method", httpRequestMessage.Method.ToString());
        if (httpRequestMessage.Content != null)
        {
            activity.SetTag("http.request.has_content", "True");
            var contentType = httpRequestMessage.Content.Headers.ContentType?.ToString();
            if (!string.IsNullOrEmpty(contentType))
            {
                activity.SetTag("http.request.content_type", contentType);
            }
        }
    }

    private void EnrichWithHttpResponseMessage(Activity activity, HttpResponseMessage httpResponseMessage)
    {
        if (!string.IsNullOrEmpty(httpResponseMessage.ReasonPhrase))
        {
            activity.SetTag("http.response.reason_phrase", httpResponseMessage.ReasonPhrase);
        }
        if (httpResponseMessage.Content != null)
        {
            var contentLength = httpResponseMessage.Content.Headers.ContentLength;
            if (contentLength.HasValue)
            {
                activity.SetTag("http.response.content_length", contentLength.Value.ToString());
            }
        }
    }
}
