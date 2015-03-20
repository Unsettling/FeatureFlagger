namespace FeatureFlagger
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class AppConfigReader : IConfigurationReader
    {
        public Dictionary<string, string> Read(string name)
        {
            var properties = new Dictionary<string, string>();

            // read from App.config. 
            // dummy.
            // TODO: yield?
            properties.Add("Enabled", "true");
            properties.Add("From", DateTime.UtcNow.Date.ToString(CultureInfo.InvariantCulture));

            return properties;
        }
    }
}