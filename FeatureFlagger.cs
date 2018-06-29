namespace RoyalLondon.IntermediaryManagement.Api.FeatureFlagger
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.Configuration;
    using System.Reflection;

    using RoyalLondon.IntermediaryManagement.Api.FeatureFlagger.Behaviours;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces", Justification = "Naming is hard")]
    public sealed class FeatureFlagger
    {
        static FeatureFlagger()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new FeatureFlagger();
        }

        private FeatureFlagger()
        {
            SetBehaviours();
            SetFeatures();
        }

        [ImportMany]
        public static IEnumerable<IBehaviour> Behaviours { get; private set; }

        public static IEnumerable<Feature> Features { get; private set; }

        private static void SetFeatures()
        {
            Features =
                (List<Feature>)ConfigurationManager.GetSection("features");
        }

        private void SetBehaviours()
        {
            // wouldn't it be nice if Microsoft.Composition was available?
            var catalog = new AggregateCatalog();
            try
            {
                catalog.Catalogs.Add(
                    new AssemblyCatalog(
                        typeof(IBehaviour).GetTypeInfo().Assembly));
                var container = new CompositionContainer(catalog);
                try
                {
                    container.SatisfyImportsOnce(this);
                    Behaviours = container.GetExportedValues<IBehaviour>();
                }
                finally
                {
                    container.Dispose();
                }
            }
            finally
            {
                catalog.Dispose();
            }
        }
    }
}