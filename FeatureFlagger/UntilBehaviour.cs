namespace FeatureFlagger
{
    using System;

    public class UntilBehaviour : IBehaviour
    {
        public Func<string[], bool> Behaviour()
        {
            return x => DateTime.UtcNow.Date <= DateTime.Parse(x[0]).Date;
        }
    }
}