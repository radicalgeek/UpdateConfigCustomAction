using System;
using System.Xml;
using ApplicationConfigSettings;

namespace UpdateConfigCustomAction.Setters
{
    [Setter(SettingType.ServiceEndpoint)]
    public class ServiceEndpointSetter : configUpdater
    {
        public ServiceEndpointSetter(string configFileName, Setting setting) : base(configFileName, setting)
        {
        }

        public override void Invoke()
        {
            XmlNode node =
                    ConfigFile.SelectSingleNode(string.Format("//services/service[@name='{0}']",
                            Setting.SettingName));

            if (node == null)
                throw new InvalidOperationException("Service Endpoint section not found");

            XmlElement element =
                    (XmlElement)
                            node.SelectSingleNode(string.Format("//services/service[@name='{0}']/endpoint",
                                    Setting.SettingName));
            if (element == null)
                throw new XmlException(String.Format("An endpoint for the service {0} could not be found",
                        Setting.SettingName));

            element.SetAttribute(Setting.SettingName, Setting.SettingValue);
        }
    }
}