namespace FeatureFlagger
{
    using System;
    using System.Collections.Generic;

    public class EnabledBehaviour : IBehaviour
    {
        public Func<Dictionary<string, string>, bool> Behaviour()
        {
            return x => Convert.ToBoolean(x["enabled"]);
        }
    }
}