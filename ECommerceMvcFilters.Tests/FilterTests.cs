using ECommerceMvcFilters.Filters;
using ECommerceMvcFilters.Models;
using ECommerceMvcFilters.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;

namespace ECommerceMvcFilters.Tests
{
    public class FilterTests
    {
        [Fact]
        public void AuthenticationFilter_ShouldRedirect_WhenUserIsNotLoggedIn()
        {
            Mock<IAuthService> authServiceMock = new Mock<IAuthService>();
            Mock<IAppLogger> loggerMock = new Mock<IAppLogger>();

            authServiceMock
                .Setup(service => service.IsUserLoggedIn())
                .Returns(false);

            CustomAuthenticationFilter filter = new CustomAuthenticationFilter(
                authServiceMock.Object,
                loggerMock.Object);

            DefaultHttpContext httpContext = new DefaultHttpContext();

            AuthorizationFilterContext context = new AuthorizationFilterContext(
                new ActionContext(
                    httpContext,
                    new RouteData(),
                    new ActionDescriptor()),
                new List<IFilterMetadata>());

            filter.OnAuthorizationAsync(context).Wait();

            Assert.NotNull(context.Result);
            Assert.IsType<RedirectToActionResult>(context.Result);

            RedirectToActionResult result = (RedirectToActionResult)context.Result;

            Assert.Equal("Login", result.ActionName);
            Assert.Equal("Account", result.ControllerName);
        }

        [Fact]
        public void AuthenticationFilter_ShouldAllowAccess_WhenUserIsLoggedIn()
        {
            Mock<IAuthService> authServiceMock = new Mock<IAuthService>();
            Mock<IAppLogger> loggerMock = new Mock<IAppLogger>();

            authServiceMock
                .Setup(service => service.IsUserLoggedIn())
                .Returns(true);

            CustomAuthenticationFilter filter = new CustomAuthenticationFilter(
                authServiceMock.Object,
                loggerMock.Object);

            DefaultHttpContext httpContext = new DefaultHttpContext();

            AuthorizationFilterContext context = new AuthorizationFilterContext(
                new ActionContext(
                    httpContext,
                    new RouteData(),
                    new ActionDescriptor()),
                new List<IFilterMetadata>());

            filter.OnAuthorizationAsync(context).Wait();

            Assert.Null(context.Result);
        }

        [Fact]
        public void GlobalExceptionFilter_ShouldHandleException_AndReturnErrorView()
        {
            Mock<IAppLogger> loggerMock = new Mock<IAppLogger>();

            GlobalExceptionFilter filter = new GlobalExceptionFilter(loggerMock.Object);

            DefaultHttpContext httpContext = new DefaultHttpContext();

            ExceptionContext context = new ExceptionContext(
                new ActionContext(
                    httpContext,
                    new RouteData(),
                    new ActionDescriptor()),
                new List<IFilterMetadata>())
            {
                Exception = new Exception("Test exception")
            };

            filter.OnException(context);

            Assert.True(context.ExceptionHandled);
            Assert.NotNull(context.Result);
            Assert.IsType<ViewResult>(context.Result);

            ViewResult result = (ViewResult)context.Result;

            Assert.Equal("Error", result.ViewName);
            Assert.IsType<ErrorViewModel>(result.Model);
        }
    }
}
