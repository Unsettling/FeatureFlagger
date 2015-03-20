namespace FeatureFlagger
{
    using System;
    using System.Collections.Generic;

    public class AppConfigReader : IConfigurationReader
    {
        public Dictionary<string, string[]> Read(string name)
        {
            var properties = new Dictionary<string, string[]>();

            // read from App.config.
            // if the name is not found then throw an exception (misconfiguration).
            // dummy.
            // TODO: yield?
            properties.Add("Enabled", new[] { "true" });
            properties.Add("From", new[] { DateTime.UtcNow.Date.ToShortDateString() });
            properties.Add("Rollout", new[] { "user", name, "15" });

            return properties;
        }
    }
}