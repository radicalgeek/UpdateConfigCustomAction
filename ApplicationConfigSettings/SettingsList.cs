using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ApplicationConfigSettings
{
    /// <summary>
    /// XML Serializable class to define a list of settings objects. 
    /// </summary>
    [Serializable]
    [XmlRoot()]
    public class SettingsList
    {
        [XmlElement()]
        public List<Setting> settings { get; set; }
    }
}
