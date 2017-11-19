namespace FeatureFlagger.Tests
{
    using System.Collections.Generic;
    using ConfigurationReaders;
    using Domain;
    using NSubstitute;
    using Shouldly;
    using Xunit;

    public class FeatureFlaggerTests
    {
        [Fact]
        public void ShouldUseFlag()
        {
            var featureFlag = Substitute.For<IFeatureFlag>();
            var reader = Substitute.For<IConfigurationReader>();

            var properties =
                new Dictionary<string, string> { { "enabled", "true" } };
            var flag = new Flag("Enabled", properties);
            var feature = new Feature("example", new List<Flag> { flag });

            FeatureFlagger.Reader = reader;
            reader.Read(string.Empty).ReturnsForAnyArgs(feature);

            featureFlag.IsEnabled().ShouldBeTrue();
        }
    }
}