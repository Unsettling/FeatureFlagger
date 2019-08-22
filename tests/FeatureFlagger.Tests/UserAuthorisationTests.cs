namespace FeatureFlagger.Tests
{
    using System.Collections.ObjectModel;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Security.Principal;
    using System.Web.Http;
    using System.Web.Http.Controllers;

    using Moq;
    using Xunit;

    public class UserAuthorisationTests
    {
        public void ShouldPassCustomAttribute()
        {
            var attribute =
                new Authorisation.UserAuthorisationAttribute("test")
                    {
                        Lookup = "users",
                        UsersList = "dummy"
                    };

            var mockActionContext = ContextUtil();

            attribute.OnAuthorization(mockActionContext);

            Assert.Equal(
                System.Net.HttpStatusCode.OK,
                mockActionContext.Response.StatusCode);
        }

        public void ShouldNotPassCustomAttribute()
        {
            var attribute =
                new Authorisation.UserAuthorisationAttribute("test")
                    {
                        Lookup = "users"
                    };

            var mockActionContext = ContextUtil();

            attribute.OnAuthorization(mockActionContext);

            Assert.Equal(
                System.Net.HttpStatusCode.Unauthorized,
                mockActionContext.Response.StatusCode);
        }

        private static HttpActionContext ContextUtil()
        {
            IPrincipal principal =
                new GenericPrincipal(
                    new GenericIdentity("TestName"),
                    new[] { "TestRole" });

            var config = new HttpConfiguration();

            HttpActionContext mockActionContext;
            try
            {
                mockActionContext =
                    new HttpActionContext
                        {
                            ControllerContext =
                                new HttpControllerContext
                                    {
                                        Request = new HttpRequestMessage(),
                                        RequestContext =
                                            new HttpRequestContext
                                                {
                                                    Principal = principal
                                                },
                                        ControllerDescriptor =
                                            CreateControllerDescriptor()
                                    },
                            ActionArguments = { { "SomeArgument", "null" } },
                            ActionDescriptor = CreateActionDescriptor(),
                            Response = new HttpResponseMessage()
                        };

                mockActionContext.ControllerContext.Configuration = config;
                mockActionContext.ControllerContext.Configuration.Formatters
                    .Add(new JsonMediaTypeFormatter());
            }
            finally
            {
                config.Dispose();
            }

            return mockActionContext;
        }

        private static HttpActionDescriptor CreateActionDescriptor()
        {
            var mock = new Mock<HttpActionDescriptor> { CallBase = true };
            mock.SetupGet(d => d.ActionName).Returns("Foo");

            return mock.Object;
        }

        private static HttpControllerDescriptor CreateControllerDescriptor()
        {
            var mock = new Mock<HttpControllerDescriptor> { CallBase = true };
            mock
                .Setup(d => d.GetCustomAttributes<AllowAnonymousAttribute>())
                .Returns(new Collection<AllowAnonymousAttribute>());

            return mock.Object;
        }
    }
}
