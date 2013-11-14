using System;
using System.Xml;
using ApplicationConfigSettings;

namespace UpdateConfigCustomAction.Setters
{
    [Setter(SettingType.ConnectionString)]
    public class ConnectionStringValueSetter : configUpdater
    {
        public ConnectionStringValueSetter(string configFileName, Setting setting)
            : base(configFileName, setting)
        {
        }

        public override void Invoke()
        {
            XmlNode node = ConfigFile.SelectSingleNode("//connectionStrings");

            if (node == null)
                throw new InvalidOperationException("Connection strings section not found");

            XmlElement element =
                    (XmlElement)node.SelectSingleNode(String.Format("//add[@name='{0}']", Setting.SettingKey));
            if (element == null)
            {
                element = ConfigFile.CreateElement("add");
                element.SetAttribute("name", Setting.SettingKey);
                node.AppendChild(element);
            }
        
            element.SetAttribute("connectionString", Setting.SettingValue);
        }
    }
}