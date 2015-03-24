namespace FeatureFlagger
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using FeatureFlagger.ConfigurationReaders;
    using FeatureFlagger.Domain;

    public static class ToggleExtensions
    {
//        public IEnumerable<FeatureFlagger.Behaviours.IBehaviour> Behaviours { get; set; } 

        public static bool IsEnabled(this IToggle toggle)
        {
            var reader = new AppConfigReader();

            // use type name to look up feature flags.
            var feature = reader.Read(toggle.GetType().Name);

            // instantiate a behaviour that matches each flag then test it with the flag's properties.
            // TODO: exception handling.
/*
            return !(from flag in feature.Flags
                        let type = assembly.GetType(string.Format("{0}.{1}Behaviour", assemblyName, flag.Name))
                        let behaviour = (IBehaviour)Activator.CreateInstance(type)
                        let func = behaviour.Behaviour()
                        where func(flag.Properties) == false
                        select flag).Any();
*/
            return true;
        }
    }
}