using System;
using System.Xml;
using ApplicationConfigSettings;

namespace UpdateConfigCustomAction.Setters
{
    [Setter(SettingType.ConfigurationSetting)]
    public class CustomSectionAttributeSetter : configUpdater
    {
        public CustomSectionAttributeSetter(string configFileName, Setting setting) : base(configFileName, setting)
        {
        }

        public override void Invoke()
        {
            XmlNode node = ConfigFile.SelectSingleNode("//configuration");

            if (node == null)
                throw new InvalidOperationException(string.Format("configuration section was not found"));

            XmlElement element = (XmlElement)node.SelectSingleNode(string.Format("//{0}", Setting.SettingName));
            if (element == null)
                throw new XmlException(string.Format("A custom config section with the name {0} could not be found",
                        Setting.SettingName));

            element.SetAttribute(Setting.SettingKey, Setting.SettingValue);
        }
    }
}