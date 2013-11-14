using System;
using System.Xml;
using ApplicationConfigSettings;

namespace UpdateConfigCustomAction.Setters
{
    [Setter(SettingType.ServiceBaseAddress)]
    public class ServiceBaseAddressSetter : configUpdater
    {
        public ServiceBaseAddressSetter(string configFileName, Setting setting) : base(configFileName, setting)
        {
        }

        public override void Invoke()
        {
            XmlNode node = ConfigFile.SelectSingleNode(string.Format("//service[@name='{0}']", Setting.SettingName));

            if (node == null)
                throw new InvalidOperationException(string.Format("{0} section not found", Setting.SettingName));

            XmlElement element = (XmlElement)node.SelectSingleNode("//host//baseAddresses//add");
            if (element == null)
                throw new XmlException(String.Format("A custom config section with the name {0} could not be found",
                        Setting.SettingName));

            element.SetAttribute(Setting.SettingKey, Setting.SettingValue);
        }
    }
}