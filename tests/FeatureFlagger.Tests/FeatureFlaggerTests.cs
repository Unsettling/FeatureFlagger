namespace FeatureFlagger.Tests
{
    using System.Collections.Generic;

    using ConfigurationReaders;
    using Domain;

    using NSubstitute;

    using Should;
    using Xunit;

    public class FeatureFlaggerTests
    {
        [Fact]
        public void ShouldUseFlag()
        {
            var featureFlag = Substitute.For<IFeatureFlag>();
            var reader = Substitute.For<IConfigurationReader>();

            var properties = new Dictionary<string, string> { { "enabled", "true" } };
            var flag = new Flag { Name = "Enabled", Properties = properties };
            var feature = new Feature { Name = "example", Flags = new List<Flag> { flag } };

            FeatureFlagger.Reader = reader;
            reader.Read(string.Empty).ReturnsForAnyArgs(feature);

            featureFlag.IsEnabled().ShouldBeTrue();
        }
    }
}