namespace FeatureFlagger.ConfigurationReaders
{
    using System.Collections.Generic;
    using System.Configuration;

    using FeatureFlagger.Domain;

    [ExportReader(Reader = Constants.Config)]
    public class ConfigReader : IConfigurationReader
    {
        public IEnumerable<Feature> ReadAll()
        {
            return (List<Feature>)ConfigurationManager.GetSection("features");
        }
    }
}
