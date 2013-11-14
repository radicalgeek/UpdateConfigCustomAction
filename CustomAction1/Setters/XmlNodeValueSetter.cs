using System;
using System.Xml;
using ApplicationConfigSettings;

namespace UpdateConfigCustomAction.Setters
{
    [Setter(SettingType.XMLNodeValue)]
    public class XmlNodeValueSetter : configUpdater
    {
        public XmlNodeValueSetter(string configFileName, Setting setting) : base(configFileName, setting)
        {
        }

        public override void Invoke()
        {
            XmlElement element = (XmlElement)ConfigFile.SelectSingleNode(Setting.SettingKey);
            if (element == null)
                throw new InvalidOperationException("Could not locate the specified Xml Node");
            element.InnerText = Setting.SettingKey;
        }
    }
}