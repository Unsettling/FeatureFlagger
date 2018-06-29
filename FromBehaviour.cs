namespace RoyalLondon.IntermediaryManagement.Api.FeatureFlagger.Behaviours
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    [Export(typeof(IBehaviour))]
    public class FromBehaviour : IBehaviour
    {
        public Func<Dictionary<string, string>, bool> Behaviour()
        {
            return x => DateTime.UtcNow.Date >= DateTime.Parse(x["date"]).Date;
        }
    }
}