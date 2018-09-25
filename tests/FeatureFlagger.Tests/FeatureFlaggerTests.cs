namespace RoyalLondon.IntermediaryManagement.Api.FeatureFlagger
{
    using System.Collections.ObjectModel;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Security.Principal;
    using System.Web.Http;
    using System.Web.Http.Controllers;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using RoyalLondon.IntermediaryManagement.Api.FeatureFlagger.CustomAttributes;

    [TestClass]
    public class FeatureFlaggerTests
    {
        [TestMethod]
        public void ShouldUseBasicEnabledFlag()
        {
            var featureFlag = new ExampleFeatureFlagger();

            var isEnabled = featureFlag.IsEnabled();

            Assert.IsTrue(isEnabled);
        }

        [TestMethod]
        public void ShouldFailDisabledFlag()
        {
            var featureFlag = new DisabledFeatureFlagger();

            var isEnabled = featureFlag.IsEnabled();

            Assert.IsFalse(isEnabled);
        }

        [TestMethod]
        public void ShouldUseFromFlag()
        {
            var featureFlag = new FromFeatureFlagger();

            var isEnabled = featureFlag.IsEnabled();

            Assert.IsTrue(isEnabled);
        }

        [TestMethod]
        public void ShouldUseUserFlag()
        {
            var featureFlag = new UserFeatureFlagger();

            var isEnabled = featureFlag.IsEnabled();

            Assert.IsTrue(isEnabled);
        }

        [TestMethod]
        public void ShouldFailUserNotListedFlag()
        {
            var featureFlag = new UnuserFeatureFlagger();

            var isEnabled = featureFlag.IsEnabled();

            Assert.IsFalse(isEnabled);
        }

        [TestMethod]
        public void ShouldPassCustomAttribute()
        {
            var attribute =
                new UserAuthorisationAttribute("test")
                    {
                        Lookup = "users",
                        UsersList = "dummy"
                    };

            var mockActionContext = ContextUtil();

            attribute.OnAuthorization(mockActionContext);

            Assert.AreEqual(
                System.Net.HttpStatusCode.OK,
                mockActionContext.Response.StatusCode);
        }

        [TestMethod]
        public void ShouldNotPassCustomAttribute()
        {
            var attribute =
                new UserAuthorisationAttribute("test")
                    {
                        Lookup = "users"
                    };

            var mockActionContext = ContextUtil();

            attribute.OnAuthorization(mockActionContext);

            Assert.AreEqual(
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
