using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationConfigSettings
{
    /// <summary>
    /// Different types of setting that the InstallerSettingsService can 
    /// serve and the UpdateConfigAustomAction can use
    /// </summary>
    public enum SettingType
    {
        ConnectionString=1, 
        AppSetting=2,
        ServiceEndpoint=3,
        ConfigurationSetting=4,
        ServiceBaseAddress=5,
        ClientEndpoint=6,
        XMLNodeValue=7,
        DefaultProxy=8,
        ConfigurationValue=9,
        XMLNodeAttribute=10
    };
}
