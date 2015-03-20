namespace FeatureFlagger
{
    using System;

    public class FromBehaviour : IBehaviour
    {
        public Func<string[], bool> Behaviour()
        {
            return x => DateTime.UtcNow.Date >= DateTime.Parse(x[0]).Date;
        }
    }
}