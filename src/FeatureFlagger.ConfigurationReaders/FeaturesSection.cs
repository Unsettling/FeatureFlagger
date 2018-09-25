namespace Unsettling.FeatureFlagger.ConfigurationReaders
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;
    using System.Linq;
    using System.Xml;

    public class FeaturesSection : IConfigurationSectionHandler
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
                        childNode.Attributes?.GetNamedItem("name").InnerText.Trim().ToUpperInvariant());
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
                                attr => attr.Name.Trim().ToUpperInvariant(),
                                attr => attr.InnerText.Trim().ToUpperInvariant());

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
                    Constants.Enabled,
                    new Dictionary<string, string>(
                        StringComparer.OrdinalIgnoreCase)
                    {
                        { Constants.Enabled, enabled }
                    });

            return flag;
        }
    }
}
