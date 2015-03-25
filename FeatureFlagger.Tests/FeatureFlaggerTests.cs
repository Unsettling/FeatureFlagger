namespace FeatureFlagger.Tests
{
    using System.Collections.Generic;

    using global::FeatureFlagger.Domain;

    using NSubstitute;

    using Should;

    public class FeatureFlaggerTests
    {
        public void ShouldUseToggle()
        {
            var toggle = Substitute.For<IToggle>();
//            var reader = Substitute.For<IConfigurationReader>();
            var properties = new Dictionary<string, string> { { "enabled", "true" } };
            var flag = new Flag { Name = "Enabled", Properties = properties };
            var feature = new Feature { Name = "example", Flags = new List<Flag> { flag } };
//            reader.Read(null).ReturnsForAnyArgs(feature);
            toggle.IsEnabled().ShouldBeTrue();
        }
    }
}