namespace FeatureFlagger
{
    public interface IConfigurationReader
    {
        /// <summary>
        /// Read the configuration.
        /// </summary>
        /// <param name="name">The name of the feature flag.</param>
        /// <returns>
        /// A dictionary where the key is a property name and the values are
        /// the property's values ordered alphabetically by attribute name.
        /// </returns>
        Feature Read(string name);
    }
}