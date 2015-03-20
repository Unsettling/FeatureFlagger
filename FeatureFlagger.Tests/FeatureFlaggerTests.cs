namespace FeatureFlagger.Tests
{
    using System.Collections.Generic;

    using NSubstitute;

    using Should;

    public class FeatureFlaggerTests
    {
        public void ShouldUseToggle()
        {
//            var toggle = Substitute.For<IToggle>();
            var toggle = new ExampleFeatureFlag();
            var reader = new AppConfigReader();
            var properties = new Dictionary<string, string> { { "Enabled", "true" } };
//            reader.Read(null).Returns(properties);
            toggle.IsEnabled(reader).ShouldBeTrue();
        }
    }
}