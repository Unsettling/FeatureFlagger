namespace RoyalLondon.IntermediaryManagement.Api.FeatureFlagger
{
    using System.Collections.Generic;

    public class Flag
    {
        public Flag(string name)
        {
            this.Properties = new Dictionary<string, string>();
            this.Name = name;
        }

        public Flag(string name, Dictionary<string, string> properties)
        {
            this.Properties = properties;
            this.Name = name;
        }

        public string Name { get; private set; }

        public Dictionary<string, string> Properties { get; }

        public Flag Add(string key, string value)
        {
            this.Properties.Add(key, value);
            return this;
        }
    }
}