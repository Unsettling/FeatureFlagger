namespace FeatureFlagger.Behaviours
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    [Export(typeof(IUser))]
    public class User : IUser
    {
        public string UserName()
        {
            throw new NotImplementedException();
        }

        bool IUser.UserHasFeature(string userName, string featureName)
        {
            DbContext context = null;
            try
            {
                context = DbContextFactory.Create();
                /*
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
*/

                return true;
            }
            finally
            {
                context?.Dispose();
            }
        }

        public class DbContext
        {
            internal readonly List<User> Users;

            internal void Dispose()
            {
                throw new NotImplementedException();
            }
        }

        public class DbContextFactory
        {
            internal static DbContext Create()
            {
                throw new NotImplementedException();
            }
        }
    }
}