namespace FeatureFlagger.Tests
{
    using Xunit;

    public class FeatureFlaggerTests
    {
        public void ShouldUseBasicEnabledFlag()
        {
            var featureFlag = new ExampleFeatureFlagger();

            var isEnabled = featureFlag.IsEnabled();

            Assert.True(isEnabled);
        }

        public void ShouldFailDisabledFlag()
        {
            var featureFlag = new DisabledFeatureFlagger();

            var isEnabled = featureFlag.IsEnabled();

            Assert.False(isEnabled);
        }

        public void ShouldUseFromFlag()
        {
            var featureFlag = new FromFeatureFlagger();

            var isEnabled = featureFlag.IsEnabled();

            Assert.True(isEnabled);
        }

        public void ShouldUseUserFlag()
        {
            var featureFlag = new UserFeatureFlagger();

            var isEnabled = featureFlag.IsEnabled();

            Assert.True(isEnabled);
        }

        public void ShouldFailUserNotListedFlag()
        {
            var featureFlag = new UnuserFeatureFlagger();

            var isEnabled = featureFlag.IsEnabled();

            Assert.False(isEnabled);
        }
    }
}
