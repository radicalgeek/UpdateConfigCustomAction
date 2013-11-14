using System;
using Microsoft.Pex.Framework;
using System.Moles;

namespace System.Preparations
{
    /// <summary>Contains a method to prepare the type AppDomain</summary>
    public static partial class AppDomainPreparation
    {
        /// <summary>Prepares the environment (and the moles) before executing any method of the prepared type AppDomain</summary>
        [PexPreparationMethod(typeof(AppDomain))]
        public static void Prepare()
        {
            MAppDomain.BehaveAsCurrent();
            // TODO: use Moles to replace API that call into the environment
        }
    }
}
