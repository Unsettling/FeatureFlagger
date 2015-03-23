namespace FeatureFlagger
{
    using System;
    using System.Collections.Generic;

    public class UntilBehaviour : IBehaviour
    {
        public Func<Dictionary<string, string>, bool> Behaviour()
        {
            return x => DateTime.UtcNow.Date <= DateTime.Parse(x["date"]).Date;
        }
    }
}