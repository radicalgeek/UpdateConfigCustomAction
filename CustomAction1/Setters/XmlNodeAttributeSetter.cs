using System;
using System.Xml;
using ApplicationConfigSettings;

namespace UpdateConfigCustomAction.Setters
{
    [Setter(SettingType.XMLNodeAttribute)]
    public class XmlNodeAttributeSetter : configUpdater
    {
        public XmlNodeAttributeSetter(string configFileName, Setting setting) : base(configFileName, setting)
        {
        }

        public override void Invoke()
        {
            XmlElement element = (XmlElement)ConfigFile.SelectSingleNode(Setting.SettingName);
            if (element == null)
                throw new InvalidOperationException("Could not locate the specified Xml Node");
            element.SetAttribute(Setting.SettingKey, Setting.SettingValue);
        }
    }
}