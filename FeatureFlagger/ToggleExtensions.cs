namespace FeatureFlagger
{
    using System.Linq;

    public static class ToggleExtensions
    {
        public static bool IsEnabled(
            this IToggle toggle)
        {
            var behaviours = FeatureFlagFactory.Behaviours.ToList();
            var feature    = FeatureFlagFactory.Reader.Read(toggle.GetType().Name);

            // TODO: exception handling.
            return !(from flag in feature.Flags
                        let behaviour = behaviours.FirstOrDefault(b => b.GetType().Name.Contains(flag.Name))
                        let func = behaviour.Behaviour()
                        where func(flag.Properties) == false
                        select flag).Any();
        }
    }
}