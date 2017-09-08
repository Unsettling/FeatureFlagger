namespace FeatureFlagger.Behaviours
{
    using System;
    using System.Collections.Generic;
    using System.Composition;
    using System.Globalization;

    // idea lifted from Togglz.
    [Export(typeof(IBehaviour))]
    public class RolloutBehaviour : IBehaviour, IUser
    {
        public Func<Dictionary<string, string>, bool> Behaviour()
        {
            return s =>
                {
                    int hashCode = string.Format("{0}:{1}", this.Basis(s["basis"]), s["name"]).GetHashCode();
                    return Math.Abs(hashCode) % 100 < Convert.ToInt32(s["percentage"]);
                };
        }

        public string Basis(string basis)
        {
            if (basis.ToLowerInvariant().Equals("user"))
            {
                return this.Username();
            }

            throw new ArgumentException("'basis' was not understood.", "basis");
        }

        public string Username()
        {
            var r = new Random();
            var rand = r.Next().ToString(CultureInfo.InvariantCulture);
            return "username" + rand;
        }
    }
}