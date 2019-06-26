namespace FeatureFlagger.Behaviours
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Security.Cryptography;

    [Export(typeof(IBehaviour))]
    public class DistributionBehaviour : IBehaviour
    {
        private static readonly Random Random = new Random();
        private static readonly object Synclock = new object();
        private readonly IUser user;
        private readonly IBucket bucket;

        [ImportingConstructor]
        public DistributionBehaviour(
            [Import(AllowDefault = true)] IUser user,
            [Import(AllowDefault = true)] IBucket bucket)
        {
            this.user = user ?? new User();
            this.bucket = bucket ?? new Bucket();
        }

        // <distribution [percentage="0.125"] />
        public Func<Dictionary<string, string>, bool> Behaviour()
        {
            const string ControlSubstring = Constants.VariantSeparator + Constants.Control;

            return x =>
            {
                var featureName = x[Constants.Feature];
                var userName = user.UserName;

                // Debug.WriteLine($"UserName: {userName}, FeatureName: {featureName}");

                // check the bucket.
                var isInBucket = bucket.Check(featureName, userName);
                if (isInBucket)
                {
                    // this user is already using this variant.
                    return true;
                }

                var percentage = GetPercentage(x);

                // is it an A/B test or an ABCD test?
                // we do this on the naming conventions where
                // an ABCD test contains a variant name.
                if (featureName.Contains(Constants.VariantSeparator))
                {
                    // ABCD.
                    if (bucket.CheckVariants(featureName, userName))
                    {
                        return false;
                    }

                    if (featureName.IndexOf(ControlSubstring, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // _Control variant must be checked last in code flow
                        // which places an onus on the ordering of variants.

                        bucket.Add(featureName, userName);

                        // this user is now in the split test's control group.
                        return true;
                    }

                    // ReSharper disable once InvertIf : it's clearer this way.
                    if (RollDice(percentage))
                    {
                        bucket.Add(featureName, userName);

                        // this user is now using this variant.
                        return true;
                    }

                    // so, not this variant.
                    return false;
                }

                // A/B.
                var control = featureName + ControlSubstring;
                isInBucket = bucket.Check(control, userName);
                if (isInBucket)
                {
                    // this user is already in the control group.
                    return false;
                }

                if (RollDice(percentage))
                {
                    bucket.Add(featureName, userName);

                    // in an A/B test this user will get the functionality.
                    return true;
                }

                // control.
                bucket.Add(control, userName);

                return false;
            };
        }

        private static decimal GetPercentage(IReadOnlyDictionary<string, string> x)
        {
            // percentage is optional: we don't use it for the control group.
            string percent =
                x.TryGetValue(Constants.Percentage, out percent)
                    ? percent
                    : "0";

            var percentage = Convert.ToDecimal(percent);

            // want percentages as decimals, i.e. 0.125 is 12.5%.
            if (percentage > 1)
            {
                throw new ArgumentException("Distribution percentage must be expressed as a decimal.");
            }

            return percentage;
        }

        private static bool RollDice(decimal percentage)
        {
            lock (Synclock)
            {
                var randomNumber = new byte[1];
                var crytoService = new RNGCryptoServiceProvider();
                crytoService.GetBytes(randomNumber);
                var diceRoll = (randomNumber[0] % 100) + 1;

                return diceRoll <= percentage * 100;
            }
        }
    }
}
