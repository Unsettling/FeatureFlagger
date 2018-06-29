namespace RoyalLondon.IntermediaryManagement.Api.FeatureFlagger
{
    using System.Collections.Generic;

    public class Feature
    {
        public Feature(string name)
        {
            this.Flags = new List<Flag>();
            this.Name = name;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Not inherited")]
        public Feature(string name, List<Flag> flags)
        {
            this.Flags = flags;
            this.Name = name;
        }

        public string Name { get; private set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Not inherited")]
        public List<Flag> Flags { get; }

        public Feature Add(Flag flag)
        {
            this.Flags.Add(flag);
            return this;
        }
    }
}