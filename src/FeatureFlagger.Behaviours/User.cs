namespace FeatureFlagger.Behaviours
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    [Export(typeof(IUser))]
    public class User : IUser
    {
        /*
                // TODO: get the authorised user from SSO.
        public string UserName => UserTemp.UserName;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "SQL parameters don't come from user input.")]
        public bool UserHasFeature(string userName, string featureName)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["IntermediaryManagement_DB"].ConnectionString;

            var queryString =
                "SELECT 1 " +
                "FROM Users " +
                "JOIN UserFeature ON Users.Id = UserFeature.Users_Id " +
                "JOIN Features ON UserFeature.Features_Id = Features.Id " +
                $"WHERE Users.Name = '{userName}' and Features.Name = '{featureName}'";

            var hasFeature = 0;

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(queryString, connection))
            {
                // ToDo Code changes required to resolve the Veracode issue below...
                //Avoid dynamically constructing SQL queries.Instead, use parameterized prepared statements to prevent the
                //    database from interpreting the contents of bind variables as part of the query.Always validate untrusted input to
                //    ensure that it conforms to the expected format, using centralized data validation routines when possible.
                //connection.Open();
                //var reader = command.ExecuteReader();
                //while (reader.Read())
                //{
                //    hasFeature = reader.GetInt32(0);
                //}

                //reader.Close();
            }

            return Convert.ToBoolean(hasFeature);

         */
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