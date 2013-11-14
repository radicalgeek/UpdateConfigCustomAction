using System;
using ApplicationConfigSettings;

namespace UpdateConfigCustomAction
{
    public class SetterAttribute : Attribute
    {
        public SetterAttribute(SettingType settingType)
        {
            SettingType = settingType;
        }

        public SettingType SettingType {get; private set;}
    }
}