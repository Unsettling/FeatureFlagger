namespace FeatureFlagger.Domain
{
    using System.Collections.Generic;

    public class Feature
    {
        public Feature(string name, string description = "")
        {
            this.Flags = new List<Flag>();
            this.Name = name;
            this.Description = description;
        }

        public Feature(string name, List<Flag> flags, string description = "")
        {
            this.Flags = flags;
            this.Name = name;
            this.Description = description;
        }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public List<Flag> Flags { get; }

        public Feature Add(Flag flag)
        {
            this.Flags.Add(flag);
            return this;
        }
    }
}
