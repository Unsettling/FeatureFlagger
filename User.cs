namespace RoyalLondon.IntermediaryManagement.Api.FeatureFlagger.Behaviours
{
    using System;
    using System.ComponentModel.Composition;
    using System.Linq;

    using RoyalLondon.IntermediaryManagement.Api.Entities;

    [Export(typeof(IUser))]
    public class User : IUser
    {
        public bool UserHasFeature(string userName, string featureName)
        {
            IntermediaryManagement_DB context = null;
            try
            {
                context = IntermediariesContainerFactory.Create();
                var user =
                    context.Users.FirstOrDefault(
                        u =>
                        u.Name.Equals(
                            userName,
                            StringComparison.OrdinalIgnoreCase));
                var hasFeature =
                    user?.Features.Any(
                        f =>
                        f.Name.Equals(
                            featureName,
                            StringComparison.OrdinalIgnoreCase));

                return hasFeature == true;
            }
            finally
            {
                context?.Dispose();
            }
        }

        public string UserName()
        {
            // TODO: get the authorised user from SSO.
            return "dummy";
        }
    }
}