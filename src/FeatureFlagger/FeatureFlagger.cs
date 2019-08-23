namespace FeatureFlagger
{
    using System;
    using System.Collections.Generic;
    using System.Composition;
    using System.Composition.Hosting;
    using System.Configuration;
    using System.Linq;
    using System.Reflection;
    using Behaviours;
    using ConfigurationReaders;
    using ConfigurationWriters;
    using Domain;

    public sealed class FeatureFlagger
    {
        static FeatureFlagger()
        {
            new FeatureFlagger();
        }

        private FeatureFlagger()
        {
            SetImports();
            SetReader();
            SetFeatures();
            SetWriter();
        }

        [ImportMany]
        public static IEnumerable<IBehaviour> Behaviours { get; private set; }

        [ImportMany(typeof(IConfigurationReader))]
        private static IEnumerable<IConfigurationReader> Readers { get; set; }

        [ImportMany]
        private static IEnumerable<IConfigurationWriter> Writers { get; set; }

        public static IEnumerable<Feature> Features { get; private set; }

        public static IConfigurationReader Reader { get; private set; }

        public static IConfigurationWriter Writer { get; private set; }

        public static void SetFeatures()
        {
            Features = Reader.ReadAll();
        }

        private static void SetReader()
        {
            // set the configuation reader based on an AppSetting.
            var source =
                ConfigurationManager.AppSettings["FeatureFlaggerSource"]
                ?? Constants.Config;
            Reader =
                Readers.ToList()
                    .Find(
                        f =>
                        f.Name.Equals(
                            source,
                            StringComparison.OrdinalIgnoreCase));
        }

        private static void SetWriter()
        {
            // set the configuation writer based on an AppSetting.
            var source =
                ConfigurationManager.AppSettings["FeatureFlaggerSource"]
                ?? Constants.Config;
            Writer =
                Writers.ToList()
                    .Find(
                        f =>
                        f.Name.Equals(
                            source,
                            StringComparison.OrdinalIgnoreCase));
        }

        private void SetImports()
        {
            var configuration =
                new ContainerConfiguration().WithAssemblies(
                    new List<Assembly> {
                        typeof(IBehaviour).Assembly,
                        typeof(IConfigurationReader).Assembly,
                        typeof(IConfigurationWriter).Assembly });
            var container = configuration.CreateContainer();
            container.SatisfyImports(this);
            Behaviours = container.GetExports<IBehaviour>();
            Readers = container.GetExports<IConfigurationReader>();
            Writers = container.GetExports<IConfigurationWriter>();
        }
    }
}
