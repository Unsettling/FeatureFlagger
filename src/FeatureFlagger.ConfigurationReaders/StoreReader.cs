namespace FeatureFlagger.ConfigurationReaders
{
    using System;
    using System.Collections.Generic;
    using System.Composition;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Linq;

    using FeatureFlagger.Domain;

    [Export(typeof(IConfigurationReader))]
    [ExportMetadata("Reader", Constants.Store)]
    public class StoreReader : IConfigurationReader
    {
        public Feature Read(string featureName, IEnumerable<Feature> features)
        {
            return
                features.ToList()
                    .Find(
                        f =>
                        f.Name.Equals(
                            featureName,
                            StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Feature> ReadAll()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["FeatureFlagger"].ConnectionString;

            const string QueryString =
                "SELECT Features.Name, Flags.Name, FlagProperties.PropertyKey, FlagProperties.PropertyValue "
               + "FROM Features "
               + "JOIN Flags ON Features.Id = Flags.FeatureId "
               + "JOIN FlagProperties ON Flags.Id = FlagProperties.FlagId "
               + "ORDER BY Features.Name, Flags.Name, FlagProperties.PropertyKey";

            var features = new List<Feature>();

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(QueryString, connection))
            {
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var featureName = reader.GetString(0).Trim().ToUpperInvariant();
                    var flagName = reader.GetString(1).Trim().ToUpperInvariant();
                    var propertyKey = reader.GetString(2).Trim().ToUpperInvariant();
                    var propertyValue = reader.GetString(3).Trim().ToUpperInvariant();

                    Feature feature;
                    if (!features.Exists(l => l.Name.Equals(featureName)))
                    {
                        feature = new Feature(featureName);
                        features.Add(feature);
                    }
                    else
                    {
                        feature = features.First(l => l.Name.Equals(featureName));
                    }

                    Flag flag;
                    if (!feature.Flags.Exists(l => l.Name.Equals(flagName)))
                    {
                        flag = new Flag(flagName);
                        feature.Flags.Add(flag);
                    }
                    else
                    {
                        flag = feature.Flags.First(l => l.Name.Equals(flagName));
                    }

                    flag.Properties.Add(propertyKey, propertyValue);
                }

                reader.Close();
            }

            return features;
        }
    }
}
