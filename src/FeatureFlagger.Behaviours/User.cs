namespace Unsettling.FeatureFlagger.Behaviours
{
    using System;
    using System.ComponentModel.Composition;
    using System.Configuration;
    using System.Data.SqlClient;

    [Export(typeof(IUser))]
    public class User : IUser
    {
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
            {
                using (var command = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        hasFeature = reader.GetInt32(0);
                    }

                    reader.Close();
                }
            }

            return Convert.ToBoolean(hasFeature);
        }

        public string UserName()
        {
            // TODO: get the authorised user from SSO.
            return "dummy";
        }
    }
}
