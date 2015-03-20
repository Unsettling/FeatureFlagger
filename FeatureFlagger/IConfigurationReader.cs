namespace FeatureFlagger
{
    using System.Collections.Generic;

    public interface IConfigurationReader
    {
        /// <summary>
        /// Read the configuration.
        /// </summary>
        /// <param name="name">The name of the feature flag.</param>
        /// <returns>
        /// A dictionary where the key is a property name and the value is the property's value.
        /// </returns>
        Dictionary<string, string> Read(string name);
    }
}