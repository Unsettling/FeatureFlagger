namespace FeatureFlagger.Domain
{
    using System.Collections.Generic;

    public class Feature
    {
        public Feature(string name)
        {
            this.Flags = new List<Flag>();
            this.Name = name;
        }

        public string Name { get; private set; }

        public List<Flag> Flags { get; private set; }

        public Feature Add(Flag flag)
        {
            this.Flags.Add(flag);
            return this;
        }
    }
}