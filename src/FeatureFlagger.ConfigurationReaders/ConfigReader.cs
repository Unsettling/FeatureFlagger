namespace FeatureFlagger.ConfigurationReaders
{
    using System;
    using System.Collections.Generic;
    using System.Composition;
    using System.Configuration;
    using System.Linq;
    using FeatureFlagger.Domain;

    [Export(typeof(IConfigurationReader))]
    public class ConfigReader : IConfigurationReader
    {
        private ConfigReader()
        {
            Name = "CONFIG";
        }

        public string Name { get; }

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
            return (List<Feature>)ConfigurationManager.GetSection("features");
        }
    }
}
