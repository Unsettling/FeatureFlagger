namespace FeatureFlagger
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.Configuration;
    using System.Linq;

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
            Reader = SetReader();
            Writer = SetWriter();
            Features = Reader.ReadAll();
        }

        [ImportMany]
        public static IEnumerable<Lazy<IBehaviour>> Behaviours { get; private set; }

        [ImportMany]
        private static IEnumerable<Lazy<IConfigurationReader, IReaderData>> Readers { get; set; }

        [ImportMany]
        private static IEnumerable<Lazy<IConfigurationWriter, IWriterData>> Writers { get; set; }

        public static IEnumerable<Feature> Features { get; private set; }

        public static IConfigurationReader Reader { get; private set; }

        public static IConfigurationWriter Writer { get; private set; }

        private static IConfigurationReader SetReader()
        {
            // set the configuation reader based on an AppSetting.
            var source =
                ConfigurationManager.AppSettings["FeatureFlaggerSource"]
                ?? Constants.Config;

            var reader =
                Readers.ToList()
                .Find(
                    f =>
                    f.Metadata.Reader.Equals(
                        source,
                        StringComparison.OrdinalIgnoreCase))
                    .Value;

            return reader;
        }

        private static IConfigurationWriter SetWriter()
        {
            // set the configuation writer based on an AppSetting.
            var source =
                ConfigurationManager.AppSettings["FeatureFlaggerSource"]
                ?? Constants.Config;

            var writer =
                Writers.ToList()
                .Find(
                    f =>
                    f.Metadata.Writer.Equals(
                        source,
                        StringComparison.OrdinalIgnoreCase))
                    .Value;

            return writer;
        }

        private void SetImports()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(IBehaviour).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(IConfigurationReader).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(IConfigurationWriter).Assembly));

            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
            Readers = container.GetExports<IConfigurationReader, IReaderData>();
            Writers = container.GetExports<IConfigurationWriter, IWriterData>();
            Behaviours = container.GetExports<IBehaviour>();
        }
    }
}
