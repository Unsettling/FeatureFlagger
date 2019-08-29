namespace FeatureFlagger.ConfigurationReaders
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Configuration;

    using FeatureFlagger.Domain;

    [Export(typeof(IConfigurationReader))]
    [ExportMetadata("Reader", Constants.Config)]
    public class ConfigReader : IConfigurationReader
    {
        public IEnumerable<Feature> ReadAll()
        {
            return (List<Feature>)ConfigurationManager.GetSection("features");
        }
    }
}
