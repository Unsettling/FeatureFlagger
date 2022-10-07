namespace FeatureFlagger.ConfigurationReaders
{
    using System;
    using System.Collections.Generic;
    using System.Composition;
    using System.Configuration;
    using System.Linq;
    using FeatureFlagger.Domain;

    [Export(typeof(IConfigurationReader))]
    [ExportMetadata("Reader", Constants.Config)]
    public class ConfigReader : IConfigurationReader
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
            Configuration config =
                ConfigurationManager.OpenExeConfiguration(
                    ConfigurationUserLevel.None);

            var sectionName = "features";

            FeaturesSection section = config.GetSection(sectionName) as FeaturesSection;

            return null;
        }
    }
}
