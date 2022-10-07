namespace FeatureFlagger.ConfigurationWriters
{
    using FeatureFlagger.Domain;

    public interface IConfigurationWriter
    {
        void Create(Feature feature);

        void Update(Feature feature);

        void Delete(string featureName);
    }
}
