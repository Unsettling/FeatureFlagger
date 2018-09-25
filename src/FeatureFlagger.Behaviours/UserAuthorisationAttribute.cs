namespace Settling.FeatureFlagger.CustomAttributes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Controllers;

    // [UserFeatureFlag("endpoints-get", [UserName = "jenny"], [Lookup = "users"], [UsersList = "dummy,jenny"])]
    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Method, AllowMultiple = true)]
    public sealed class UserAuthorisationAttribute : AuthorizeAttribute
    {
        public UserAuthorisationAttribute(string featureName)
        {
            this.FeatureName = featureName;
        }

        public string FeatureName { get; }

        public string Lookup { get; set; }

        public string UserName { get; set; }

        public string UsersList { get; set; }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var behaviours = FeatureFlagger.Behaviours.ToList();
            var userBehaviour =
                behaviours
                    .FirstOrDefault(
                        u =>
                        u.GetType().Name.ToUpperInvariant().Contains(Constants.User));
            var behaviour = userBehaviour?.Behaviour();
            var properties =
                new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                    {
                        { Constants.Feature, this.FeatureName },
                        { Constants.Lookup, this.Lookup },
                        { Constants.Name, this.UserName },
                        { Constants.Users, this.UsersList }
                    };
            var isEnabled = behaviour != null && behaviour(properties);

            return isEnabled;
        }
    }
}
