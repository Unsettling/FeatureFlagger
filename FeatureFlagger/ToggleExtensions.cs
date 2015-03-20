namespace FeatureFlagger
{
    using System;
    using System.Reflection;

    public static class ToggleExtensions
    {
        public static bool IsEnabled(this IToggle toggle, IConfigurationReader reader)
        {
            // use type name to look up flag.
            var dictionary = reader.Read(toggle.GetType().Name);

            foreach (var pair in dictionary)
            {
                var assembly = Assembly.GetExecutingAssembly();
                var type = assembly.GetType(pair.Key + "Behaviour");
                var behaviour = (IBehaviour)Activator.CreateInstance(type);
                var func = behaviour.Behaviour();
                if (func(pair.Value) == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}