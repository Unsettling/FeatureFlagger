namespace FeatureFlagger
{
    using System;

    public class EnabledBehaviour : IBehaviour
    {
        public Func<string, bool> Behaviour()
        {
            return Convert.ToBoolean;
        }
    }
}