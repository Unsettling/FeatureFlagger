namespace FeatureFlagger.Behaviours
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Bucket : IBucket
    {
        // NOTE: this *really* shouldn't be in memory!
        // dictionary has key = bucketName, value = userName list.
        private readonly Dictionary<string, List<string>> buckets;

        public Bucket()
        {
            buckets = new Dictionary<string, List<string>>();
        }

        public bool Check(string bucketName, string userName)
        {
            bucketName = bucketName.ToUpperInvariant();
            userName = userName.ToUpperInvariant();
            List<string> users;
            if (!buckets.ContainsKey(bucketName)
                || !buckets.TryGetValue(bucketName, out users))
            {
                return false;
            }

            return users.Contains(userName);
        }

        public bool CheckVariants(string featureName, string userName)
        {
            // get the base feature name.
            featureName =
                featureName.Remove(
                    featureName.IndexOf(
                        Constants.VariantSeparator,
                        StringComparison.OrdinalIgnoreCase));

            // find all the buckets for the group of variants.
            var featureBuckets =
                buckets.Keys.ToList()
                .FindAll(
                    x =>
                    x.StartsWith(featureName, StringComparison.OrdinalIgnoreCase));

            // check all the variant buckets for the given user.
            return featureBuckets.Any(b => Check(b, userName));
        }

        public void Add(string bucketName, string userName)
        {
            bucketName = bucketName.ToUpperInvariant();
            userName = userName.ToUpperInvariant();
            if (buckets.ContainsKey(bucketName))
            {
                List<string> users;
                // ReSharper disable once InvertIf
                if (buckets.TryGetValue(bucketName, out users))
                {
                    if (!users.Contains(userName))
                    {
                        users.Add(userName);
                    }
                }
            }
            else
            {
                buckets.Add(bucketName, new List<string> { userName });
            }
        }
    }
}