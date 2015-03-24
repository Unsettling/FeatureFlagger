namespace FeatureFlagger
{
    using System;
    using System.Collections.Generic;
    using System.Composition;
    using System.Composition.Hosting;
    using System.Reflection;

    using FeatureFlagger.Behaviours;
    using FeatureFlagger.ConfigurationReaders;

    // TODO: maybe factory isn't useful and it should be 'InitialiseFF' class ...
    public class FeatureFlagFactory
    {
        // TODO: is this an abuse of the factory? Having a constructor?
        public FeatureFlagFactory()
        {
            var configuration =
                new ContainerConfiguration()
                .WithAssemblies(
                    new[]
                    {
                        typeof(IBehaviour).GetTypeInfo().Assembly,
                        typeof(IConfigurationReader).GetTypeInfo().Assembly
                    });

            var compositionHost = configuration.CreateContainer();
            compositionHost.SatisfyImports(this);
        }

        // TODO: that this is static is a bit smelly ...
        [ImportMany]
        public static IEnumerable<IBehaviour> Behaviours { get; set; }

        [Import]
        public static IConfigurationReader Reader { get; set; }
        
        public IToggle New(Func<IToggle> toggle)
        {
            return toggle();
        }
    }
}