﻿namespace FeatureFlagger.ConfigurationReaders
{
    using System;
    using System.Collections.Generic;
    using System.Composition;
    using System.Linq;
    using FeatureFlagger.Domain;

    [Export(typeof(IConfigurationReader))]
    [ExportMetadata("Reader", "TEST")]
    public class TestReader : IConfigurationReader
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
            List<Feature> features = new List<Feature>();

            var properties =
                new Dictionary<string, string> { { "enabled", "true" } };
            var flag = new Flag("Enabled", properties);
            var feature = new Feature("example", new List<Flag> { flag });

            features.Add(feature);

            return features;
        }
    }
}
