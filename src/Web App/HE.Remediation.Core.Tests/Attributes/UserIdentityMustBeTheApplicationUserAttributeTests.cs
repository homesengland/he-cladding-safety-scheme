using HE.Remediation.Core.Attributes;
using HE.Remediation.Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using System.Security.Claims;

namespace HE.Remediation.Core.Tests.Attributes;

public class UserIdentityMustBeTheApplicationUserAttributeTests
{
    [Fact]
    public void OnAuthorization_UserIdentityMatches_ContinuesExecution()
    {
        // Arrange
        var applicationDataProviderMock = new Mock<IApplicationDataProvider>();
        applicationDataProviderMock.Setup(x => x.GetApplicationEmailAddress()).Returns("user@example.com");
        var routeData = new RouteData();
        var actionDescriptor = new Mock<ActionDescriptor>().Object;

        var contextMock = new AuthorizationFilterContext(
            new ActionContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, "user@example.com")
                    }))
                },
                RouteData = routeData,
                ActionDescriptor = actionDescriptor
            }, new List<IFilterMetadata>());

        var attribute = new UserIdentityMustBeTheApplicationUserAttribute.ApplicationUserIdentityAttribute(applicationDataProviderMock.Object);

        // Act
        attribute.OnAuthorization(contextMock);

        // Assert
        Assert.Null(contextMock.Result);
    }

    [Fact]
    public void OnAuthorization_UserIdentityDoesNotMatch_SetsUnauthorizedResult()
    {
        // Arrange
        var applicationDataProviderMock = new Mock<IApplicationDataProvider>();
        applicationDataProviderMock.Setup(x => x.GetApplicationEmailAddress()).Returns("user@example.com");
        var routeData = new RouteData();
        var actionDescriptor = new Mock<ActionDescriptor>().Object;

        var contextMock = new AuthorizationFilterContext(
            new ActionContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, "differentuser@example.com")
                    }))
                },
                RouteData = routeData,
                ActionDescriptor = actionDescriptor
            }, new List<IFilterMetadata>());

        var attribute = new UserIdentityMustBeTheApplicationUserAttribute.ApplicationUserIdentityAttribute(applicationDataProviderMock.Object);

        // Act
        attribute.OnAuthorization(contextMock);

        // Assert
        Assert.IsType<UnauthorizedResult>(contextMock.Result);
    }
}
