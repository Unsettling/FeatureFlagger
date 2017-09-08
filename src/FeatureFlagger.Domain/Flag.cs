namespace FeatureFlagger.Domain
{
    using System.Collections.Generic;

    public class Flag
    {
        public Flag()
        {
            this.Properties = new Dictionary<string, string>();
        }

        public string Name { get; set; }

        public Dictionary<string, string> Properties { get; set; } 
    }
}