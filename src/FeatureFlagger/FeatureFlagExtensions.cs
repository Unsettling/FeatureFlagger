namespace FeatureFlagger
{
    using System.Linq;

    public static class FeatureFlagExtensions
    {
        public static bool IsEnabled(
            this IFeatureFlag featureFlag)
        {
            var behaviours = FeatureFlagger.Behaviours.ToList();
            var feature    = FeatureFlagger.Reader.Read(featureFlag.GetType().Name);

            // TODO: exception handling.
            return !(from flag in feature.Flags
                        let behaviour = behaviours.FirstOrDefault(b => b.GetType().Name.Contains(flag.Name))
                        let func = behaviour.Behaviour()
                        where func(flag.Properties) == false
                        select flag).Any();
        }
    }
}