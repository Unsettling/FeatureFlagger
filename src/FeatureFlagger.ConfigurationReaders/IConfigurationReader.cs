﻿namespace FeatureFlagger.ConfigurationReaders
{
    using System.Collections.Generic;

    using FeatureFlagger.Domain;

    public interface IConfigurationReader
    {
        /// <summary>
        /// The Name of the implementation.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Read a feature from the configuration.
        /// </summary>
        /// <param name="featureName">The name of the feature.</param>
        /// <returns>A Feature.</returns>
        Feature Read(string featureName);

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
