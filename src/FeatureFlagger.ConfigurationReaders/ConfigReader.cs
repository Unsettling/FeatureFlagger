namespace FeatureFlagger.ConfigurationReaders
{
    using System.Collections.Generic;
    using System.Composition;
    using System.Configuration;

    using FeatureFlagger.Domain;

    [Export(typeof(IConfigurationReader)),
        ExportMetadata(Constants.Reader, Constants.Config)]
    public class ConfigReader : IConfigurationReader
    {
        public IEnumerable<Feature> ReadAll()
        {
            return (List<Feature>)ConfigurationManager.GetSection("features");
        }
    }
}
