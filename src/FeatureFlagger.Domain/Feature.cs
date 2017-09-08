namespace FeatureFlagger.Domain
{
    using System.Collections.Generic;

    public class Feature
    {
        public Feature()
        {
            this.Flags = new List<Flag>();
        }

        public string Name { get; set; }

        public List<Flag> Flags { get; set; }
    }
}