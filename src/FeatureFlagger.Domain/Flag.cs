namespace FeatureFlagger.Domain
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

        public Dictionary<string, string> Properties { get; private set; }

        public Flag Add(string key, string val)
        {
            this.Properties.Add(key, val);
            return this;
        }
    }
}