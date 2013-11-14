using System;
using System.Xml;
using ApplicationConfigSettings;

namespace UpdateConfigCustomAction.Setters
{
    [Setter(SettingType.AppSetting)]
    public class AppSettingSetter : configUpdater
    {
        public AppSettingSetter(string configFileName, Setting setting)
            : base(configFileName, setting)
        {
        }

        public override void Invoke()
        {
            XmlNode node = ConfigFile.SelectSingleNode("//appSettings");

            if (node == null)
                throw new InvalidOperationException("configuration section not found");

            XmlElement element =
                    (XmlElement)
                            node.SelectSingleNode(string.Format("//add[@key='{0}']", Setting.SettingKey));
            if (element == null)
            {
                element = ConfigFile.CreateElement("add");
                element.SetAttribute("key", Setting.SettingKey);
                node.AppendChild(element);
            }
            
            element.SetAttribute("value", Setting.SettingValue);
        }
    }
}