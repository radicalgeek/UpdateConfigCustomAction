using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationConfigSettings
{
    /// <summary>
    /// this class provides a structure for the settings that are served from the InstallerSettingsService
    /// </summary>
    public class Setting
    {
        public int settingID { get; set; }
        public string settingName { get; set; }
        public string settingKey { get; set; }
        public string settingValue { get; set; }
        public SettingType settingType { get; set; }
        public EnvironmentType environmentType { get; set; }
        public string configFile { get; set; }
    }
}
