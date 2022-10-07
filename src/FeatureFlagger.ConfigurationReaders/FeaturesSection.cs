namespace FeatureFlagger.ConfigurationReaders
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;
    using System.Linq;
    using System.Xml;

    using FeatureFlagger.Domain;

    public sealed class FeaturesSection : ConfigurationSection
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1059:MembersShouldNotExposeCertainConcreteTypes", MessageId = "System.Xml.XmlNode", Justification = "Standard method")]
        public object Create(
            object parent,
            object configContext,
            XmlNode section)
        {
            var features = new List<Feature>();

            foreach (XmlNode childNode in section.ChildNodes)
            {
                var flag = EnabledFlag(childNode.Attributes);
                var feature =
                    new Feature(
                        childNode.Attributes?.GetNamedItem("name").InnerText);
                feature.Flags.Add(flag);

                foreach (XmlNode node in childNode.ChildNodes)
                {
                    if (node.Attributes == null)
                    {
                        continue;
                    }

                    var attrs =
                        node.Attributes.Cast<XmlAttribute>()
                            .ToDictionary(
                                attr => attr.Name,
                                attr => attr.InnerText);

                    // title case is required when looking up Behaviours.
                    var info = new CultureInfo("en-US").TextInfo;
                    var name = info.ToLower(node.Name);

                    feature.Flags.Add(new Flag(name, attrs));
                }

                features.Add(feature);
            }

            return features;
        }

        private static Flag EnabledFlag(XmlAttributeCollection attributes)
        {
            string enabled = null;
            if (attributes != null)
            {
                enabled = attributes.GetNamedItem("enabled").InnerText;
            }

            if (string.IsNullOrWhiteSpace(enabled))
            {
                throw new ArgumentException("enabled attribute is required");
            }

            var flag =
                new Flag(
                    "Enabled",
                    new Dictionary<string, string> { { "enabled", enabled } });

            return flag;
        }
    }
}