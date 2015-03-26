namespace FeatureFlagger
{
    using System.Collections.Generic;
    using System.Composition;
    using System.Composition.Hosting;
    using System.Reflection;

    using global::FeatureFlagger.Behaviours;
    using global::FeatureFlagger.ConfigurationReaders;

    public class FeatureFlagger
    {
        static FeatureFlagger()
        {
            // fancy footwork here helps to do the MEF composition
            // without requiring the user to instantiate the class themselves.
            // (thanks, Gary).
            var ff = new FeatureFlagger();
        }
            
        public FeatureFlagger()
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

        [ImportMany]
        public static IEnumerable<IBehaviour> Behaviours { get; set; }

        [Import]
        public static IConfigurationReader Reader { get; set; }
    }
}