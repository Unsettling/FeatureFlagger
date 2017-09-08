namespace FeatureFlagger.Behaviours
{
    using System;
    using System.Collections.Generic;

    public interface IBehaviour
    {
        Func<Dictionary<string, string>, bool> Behaviour();
    }
}