﻿namespace FeatureFlagger.Core
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
            Reader = SetReader();
            Writer = SetWriter();
            Features = Reader.ReadAll();
        }

        [ImportMany]
        public static IEnumerable<IBehaviour> Behaviours { get; private set; }

        [ImportMany]
        private static IEnumerable<ExportFactory<IConfigurationReader, ExportReaderAttribute>> Readers { get; set; }

        [ImportMany]
        private static IEnumerable<ExportFactory<IConfigurationWriter, ExportWriterAttribute>> Writers { get; set; }

        public static IEnumerable<Feature> Features { get; private set; }

        public static IConfigurationReader Reader { get; private set; }

        public static IConfigurationWriter Writer { get; private set; }

        private static IConfigurationReader SetReader()
        {
            // set the configuration reader based on an AppSetting.
            var source =
                ConfigurationManager.AppSettings["FeatureFlagger.Reader"]
                ?? Constants.Config;

            var reader =
                Readers.ToList()
                .Find(
                    f =>
                    f.Metadata.Reader.Equals(
                        source,
                        StringComparison.OrdinalIgnoreCase))
                    .CreateExport()
                    .Value;

            return reader;
        }

        private static IConfigurationWriter SetWriter()
        {
            // set the configuration writer based on an AppSetting.
            var source =
                ConfigurationManager.AppSettings["FeatureFlagger.Writer"];

            // if a writer isn't required then don't proceed.
            if (string.IsNullOrEmpty(source))
            {
                return null;
            }

            var writers =
                Writers.ToList()
                .Find(
                    f =>
                    f.Metadata.Writer.Equals(
                        source,
                        StringComparison.OrdinalIgnoreCase))
                    .CreateExport()
                    .Value;

            return writers;
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
            Readers = container.GetExports<ExportFactory<IConfigurationReader, ExportReaderAttribute>>();
            Writers = container.GetExports<ExportFactory<IConfigurationWriter, ExportWriterAttribute>>();
        }
    }
}
