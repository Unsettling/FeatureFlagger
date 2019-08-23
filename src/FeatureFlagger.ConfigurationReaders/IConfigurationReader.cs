namespace FeatureFlagger.ConfigurationReaders
{
    using System.Collections.Generic;

    using FeatureFlagger.Domain;

    public interface IConfigurationReader
    {
        /// <summary>
        /// Read a feature from the configuration.
        /// </summary>
        /// <param name="featureName">The name of the feature.</param>
        /// <param name="features">The list of features.</param>
        /// <returns>A Feature.</returns>
        Feature Read(string featureName, IEnumerable<Feature> features);

        /// <summary>
        /// Read the configuration.
        /// </summary>
        /// <returns>
        /// A dictionary where the key is a property name and the values are
        /// the property's values ordered alphabetically by attribute name.
        /// </returns>
        IEnumerable<Feature> ReadAll();
    }
}
