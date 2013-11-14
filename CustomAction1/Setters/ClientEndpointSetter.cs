using System;
using System.Xml;
using ApplicationConfigSettings;

namespace UpdateConfigCustomAction.Setters
{
    [Setter(SettingType.ClientEndpoint)]
    public class ClientEndpointSetter : configUpdater
    {
        public ClientEndpointSetter(string configFileName, Setting setting) : base(configFileName, setting)
        {
        }

        public override void Invoke()
        {
            XmlNode node = ConfigFile.SelectSingleNode("//client");

            if (node == null)
                throw new InvalidOperationException("client section not found");

            XmlElement element =
                    (XmlElement)
                            node.SelectSingleNode(string.Format("//endpoint[@name='{0}']",
                                    Setting.SettingKey));
            if (element == null)
                throw new XmlException(String.Format("An endpoint with the name {0} could not be found",
                        Setting.SettingKey));

            element.SetAttribute("address", Setting.SettingValue);
        }
    }
}