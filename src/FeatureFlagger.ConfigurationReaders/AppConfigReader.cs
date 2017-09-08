namespace FeatureFlagger.ConfigurationReaders
{
    using System;
    using System.Collections.Generic;
    using System.Composition;
    using System.Configuration;

    using FeatureFlagger.Domain;

    [Export(typeof(IConfigurationReader))]
    public class AppConfigReader : IConfigurationReader
    {
        public Feature Read(string name)
        {
            // read from App.config.
            var features = (List<Feature>)ConfigurationManager.GetSection("features");

            // TODO: either set up naming convention or warn about incorrectly named feature flag markers.
            name = name.Replace("FeatureFlag", string.Empty);

            return features.Find(f => f.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}