namespace FeatureFlagger
{
    using System;
    using System.Globalization;

    // idea lifted from Togglz.
    public class RolloutBehaviour : IBehaviour, IUser
    {
        public Func<string[], bool> Behaviour()
        {
            // s[0] is the 'basis', s[1] is the feature flag's name and s[2] is the 'percentage'.
            return s =>
                {
                    int hashCode = string.Format("{0}:{1}", this.Basis(s[0]), s[1]).GetHashCode();
                    return Math.Abs(hashCode) % 100 < Convert.ToInt32(s[2]);
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