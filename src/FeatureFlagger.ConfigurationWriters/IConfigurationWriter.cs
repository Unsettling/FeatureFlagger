namespace FeatureFlagger.ConfigurationWriters
{
    using System;
    using FeatureFlagger.Domain;

    public interface IConfigurationWriter
    {
        /// <summary>
        /// The Name of the implementation.
        /// </summary>
        string Name { get; }

        void Create(Feature feature);

        void Update(Feature feature);

        void Delete(string featureName);
    }
}
