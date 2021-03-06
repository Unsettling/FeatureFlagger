namespace FeatureFlagger.ConfigurationReaders
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Xml;

    using FeatureFlagger.Domain;

    public class FeaturesSection
    {
        /*
        public object Create(object parent, object configContext, XmlNode section)
        {
            var features = new List<Feature>();

            foreach (XmlNode childNode in section.ChildNodes)
            {
                var flag = EnabledFlag(childNode.Attributes);
                var feature = new Feature { Name = childNode.Attributes.GetNamedItem("name").InnerText };
                feature.Flags.Add(flag);

                if (!childNode.HasChildNodes)
                {
                    continue;
                }

                foreach (XmlNode node in childNode.ChildNodes)
                {
                    if (node.Attributes == null)
                    {
                        continue;
                    }

                    var attrs =
                        node.Attributes.Cast<XmlAttribute>()
                        .ToDictionary(attr => attr.Name, attr => attr.InnerText);

                    // title case is required when looking up Behaviours.
                    var info = new CultureInfo("en-US").TextInfo;
                    // NOTE: this was ToTitleCase ...
                    var name = info.ToLower(node.Name);

                    feature.Flags.Add(
                        new Flag
                            {
                               Name = name,
                               Properties = attrs
                            });
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
                throw new Exception("TODO");
            }

            var flag = new Flag
                {
                    Name = "Enabled",
                    Properties = new Dictionary<string, string>
                        {
                            { "enabled", enabled }
                        }
                };

            return flag;
        }
 */
    }
}