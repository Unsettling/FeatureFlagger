namespace FeatureFlagger
{
    using System;
    using System.Linq;
    using System.Reflection;

    public static class ToggleExtensions
    {
        public static bool IsEnabled(this IToggle toggle, IConfigurationReader reader)
        {
            // use type name to look up feature flag.
            var dictionary = reader.Read(toggle.GetType().Name);

            // we're going to create an instance of a behaviour type in the assembly.
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyName = assembly.GetName().Name;

            // instantiate a behaviour that matches each property in the config then test it with the property value.
            // TODO: exception handling.
            return !(from pair in dictionary
                        let type = assembly.GetType(string.Format("{0}.{1}Behaviour", assemblyName, pair.Key))
                        let behaviour = (IBehaviour)Activator.CreateInstance(type)
                        let func = behaviour.Behaviour()
                        where func(pair.Value) == false
                        select pair).Any();
        }
    }
}