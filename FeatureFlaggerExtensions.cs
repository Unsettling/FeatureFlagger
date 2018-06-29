namespace RoyalLondon.IntermediaryManagement.Api.FeatureFlagger
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class FeatureFlaggerExtensions
    {
        public static bool IsEnabled(this IFeatureFlagger featureFlagger)
        {
            var behaviours = FeatureFlagger.Behaviours.ToList();
            var flags = GetFlags(featureFlagger);

            return !(from flag in flags
                     let behaviour =
                         behaviours.FirstOrDefault(
                             b =>
                             b.GetType()
                                 .Name.ToUpperInvariant()
                                 .Contains(flag.Name.ToUpperInvariant()))
                     let func = behaviour.Behaviour()
                     where func(flag.Properties) == false
                     select flag).Any();
        }

        private static IEnumerable<Flag> GetFlags(IFeatureFlagger featureFlagger)
        {
            var featureName =
                featureFlagger.GetType().Name
                .Replace("FeatureFlagger", string.Empty);
            var feature =
                FeatureFlagger.Features.ToList()
                    .Find(
                        f =>
                        f.Name.Equals(
                            featureName,
                            StringComparison.OrdinalIgnoreCase));

            // always add the feature name to each flag as a property.
            feature.Flags.ForEach(f => f.Add("feature", feature.Name));

            return feature.Flags;
        }
    }
}