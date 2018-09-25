namespace FeatureFlagger.ConfigurationReaders
{
    using System;
    using System.Collections.Generic;
    using System.Composition;

    using FeatureFlagger.Domain;

    [Export(typeof(IConfigurationReader))]
    public class ConfigReader : IConfigurationReader
    {
        private ConfigReader()
        {
            Name = "CONFIG";
        }

        public string Name { get; }

        public Feature Read(string featureName)
        {
            return
                FeatureFlagger.Features.ToList()
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
