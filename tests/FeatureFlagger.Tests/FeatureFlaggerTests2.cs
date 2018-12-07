namespace RoyalLondon.IntermediaryManagement.Api.FeatureFlagger
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FeatureFlaggerTests
    {
        [TestMethod]
        public void ShouldUseBasicEnabledFlag()
        {
            var featureFlag = new ExampleFeatureFlagger();

            var isEnabled = featureFlag.IsEnabled();

            Assert.IsTrue(isEnabled);
        }

        [TestMethod]
        public void ShouldFailDisabledFlag()
        {
            var featureFlag = new DisabledFeatureFlagger();

            var isEnabled = featureFlag.IsEnabled();

            Assert.IsFalse(isEnabled);
        }

        [TestMethod]
        public void ShouldUseFromFlag()
        {
            var featureFlag = new FromFeatureFlagger();

            var isEnabled = featureFlag.IsEnabled();

            Assert.IsTrue(isEnabled);
        }

        [TestMethod]
        public void ShouldUseUserFlag()
        {
            var featureFlag = new UserFeatureFlagger();

            var isEnabled = featureFlag.IsEnabled();

            Assert.IsTrue(isEnabled);
        }

        [TestMethod]
        public void ShouldFailUserNotListedFlag()
        {
            var featureFlag = new UnuserFeatureFlagger();

            var isEnabled = featureFlag.IsEnabled();

            Assert.IsFalse(isEnabled);
        }
    }
}
