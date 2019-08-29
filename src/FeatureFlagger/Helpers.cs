namespace FeatureFlagger
{
    using System;
    using System.Linq;

    public class Helpers
    {
        public static Domain.Feature Read(string featureName)
        {
            return
                FeatureFlagger.Features.ToList()
                    .Find(
                        f =>
                        f.Name.Equals(
                            featureName,
                            StringComparison.OrdinalIgnoreCase));
        }
    }
}
