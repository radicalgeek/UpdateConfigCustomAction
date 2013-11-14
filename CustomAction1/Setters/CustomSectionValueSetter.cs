using System;
using System.Xml;
using ApplicationConfigSettings;

namespace UpdateConfigCustomAction.Setters
{
    [Setter(SettingType.ConfigurationValue)]
    public class CustomSectionValueSetter : configUpdater
    {
        public CustomSectionValueSetter(string configFileName, Setting setting) : base(configFileName, setting)
        {
        }

        public override void Invoke()
        {
            XmlNode node = ConfigFile.SelectSingleNode(string.Format("//configuration/{0}", Setting.SettingName));

            if (node == null)
                throw new InvalidOperationException(string.Format("{0} section not found", Setting.SettingName));

            XmlElement element =
                    (XmlElement)
                            node.SelectSingleNode(string.Format("*/setting[@name='{0}']/value", Setting.SettingKey));
            if (element == null)
                throw new XmlException(string.Format("A custom config section with the name {0} could not be found",
                        Setting.SettingName));
            element.InnerText = Setting.SettingValue;
        }
    }
}