namespace FeatureFlagger.Behaviours
{
    using System;
    using System.Collections.Generic;
    using System.Composition;

    [Export(typeof(IBehaviour))]
    public class EnabledBehaviour : IBehaviour
    {
        public Func<Dictionary<string, string>, bool> Behaviour()
        {
            return x => Convert.ToBoolean(x["enabled"]);
        }
    }
}