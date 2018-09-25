namespace FeatureFlagger
{
    using System.Collections.Generic;
    using System.Composition;
    using System.Composition.Hosting;
    using System.Reflection;

    using Behaviours;
    using ConfigurationReaders;

    public sealed class FeatureFlagger
    {
        static FeatureFlagger()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new FeatureFlagger();
        }

        private FeatureFlagger()
        {
            SetImports();
            SetReader();
            SetFeatures();
        }

        [ImportMany]
        public static IEnumerable<IBehaviour> Behaviours { get; private set; }

        public static IEnumerable<Feature> Features { get; private set; }

        public static IConfigurationReader Reader { get; private set; }

        [ImportMany]
        private static IEnumerable<IConfigurationReader> Readers { get; set; }

        private static void SetFeatures()
        {
            Features = Reader.ReadAll();
        }

        private static void SetReader()
        {
            // set the configuation reader based on an appSetting.
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

        private void SetImports()
        {
            // wouldn't it be nice if Microsoft.Composition was available?
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
                }
            }
        }
    }
}
