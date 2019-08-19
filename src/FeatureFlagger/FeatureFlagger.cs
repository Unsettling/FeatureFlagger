namespace FeatureFlagger
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
    using System.Composition;
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

        public static IEnumerable<Feature> Features { get; private set; }

        public static IConfigurationReader Reader { get; private set; }

        public static IConfigurationWriter Writer { get; private set; }

        [ImportMany]
        private static IEnumerable<IConfigurationReader> Readers { get; set; }

        [ImportMany]
        private static IEnumerable<IConfigurationWriter> Writers { get; set; }

        private static void SetFeatures()
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
            using (var catalog = new AggregateCatalog())
            {
                catalog.Catalogs.Add(
                    new AssemblyCatalog(
                        typeof(IBehaviour).GetTypeInfo().Assembly));
                using (var container = new CompositionContainer(catalog))
                {
                    container.SatisfyImportsOnce(this);
                    Behaviours = container.GetExportedValues<IBehaviour>();
                    Readers = container.GetExportedValues<IConfigurationReader>();
                    // Writers ...
                }
            }
        }
    }
}
