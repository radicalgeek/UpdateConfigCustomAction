using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationConfigSettings
{
    /// <summary>
    /// Different environment types that can be used in the configuration service
    /// </summary>
    public enum EnvironmentType
    {
        DEV = 1,
        QA = 2,
        UAT = 3,
        CAP = 4,
        INT = 5,
        PROD = 6,
        STG = 7
    };
}
