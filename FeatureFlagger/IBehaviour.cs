namespace FeatureFlagger
{
    using System;

    public interface IBehaviour
    {
        Func<string, bool> Behaviour();
    }
}