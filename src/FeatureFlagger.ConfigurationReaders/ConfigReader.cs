namespace FeatureFlagger.ConfigurationReaders
{
    using System.Collections.Generic;
    using System.Composition;
    using System.Configuration;

    using FeatureFlagger.Domain;

    [Export(typeof(IConfigurationReader))]
    [ExportMetadata("Reader", Constants.Config)]
    public class ConfigReader : IConfigurationReader
    {
        public IEnumerable<Feature> ReadAll()
        {
            var features = (List<Feature>)ConfigurationManager.GetSection("features");

            return features;
        }
    }
}
