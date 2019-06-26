namespace FeatureFlagger.Behaviours
{
    public interface IBucket
    {
        bool Check(string bucketName, string userName);

        bool CheckVariants(string featureName, string userName);

        void Add(string bucketName, string userName);
    }

}