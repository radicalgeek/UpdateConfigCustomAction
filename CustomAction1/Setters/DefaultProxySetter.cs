using System;
using System.Xml;
using ApplicationConfigSettings;

namespace UpdateConfigCustomAction.Setters
{
    [Setter(SettingType.DefaultProxy)]
    public class DefaultProxySetter : configUpdater
    {
        public DefaultProxySetter(string configFileName, Setting setting) : base(configFileName, setting)
        {
        }

        public override void Invoke()
        {
            XmlElement element =
                    (XmlElement)ConfigFile.SelectSingleNode("//configuration/system.net/defaultProxy/proxy");
            if (element == null)
                throw new InvalidOperationException("Could not locate the Proxy Address Element");
            element.SetAttribute("proxyaddress", Setting.SettingValue);
        }
    }
}