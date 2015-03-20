namespace FeatureFlagger
{
    using System;

    public class FromBehaviour : IBehaviour
    {
        public Func<string, bool> Behaviour()
        {
            return x => Convert.ToDateTime(x).Date >= DateTime.UtcNow.Date;
        }
    }
}