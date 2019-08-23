namespace FeatureFlagger.Tests
{
    using Shouldly;
    using Xunit;

    public class FeatureFlaggerTests
    {
        [Fact]
        public void ShouldUseBasicEnabledFlag()
        {
            var featureFlag = new ExampleFeatureFlagger();
            featureFlag.IsEnabled().ShouldBeTrue();
        }

        [Fact]
        public void ShouldFailDisabledFlag()
        {
            var featureFlag = new DisabledFeatureFlagger();
            featureFlag.IsEnabled().ShouldBeFalse();
        }

        [Fact]
        public void ShouldUseFromFlag()
        {
            var featureFlag = new FromFeatureFlagger();
            featureFlag.IsEnabled().ShouldBeTrue();
        }

        [Fact]
        public void ShouldUseUserFlag()
        {
            var featureFlag = new UserFeatureFlagger();
            featureFlag.IsEnabled().ShouldBeTrue();
        }

        [Fact]
        public void ShouldFailUserNotListedFlag()
        {
            var featureFlag = new UnuserFeatureFlagger();
            featureFlag.IsEnabled().ShouldBeFalse();
        }
    }
}
