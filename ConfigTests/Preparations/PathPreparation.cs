using System;
using Microsoft.Pex.Framework;
using System.IO.Moles;
using System.IO;

namespace System.IO.Preparations
{
    /// <summary>Contains a method to prepare the type Path</summary>
    public static partial class PathPreparation
    {
        /// <summary>Prepares the environment (and the moles) before executing any method of the prepared type Path</summary>
        [PexPreparationMethod(typeof(Path))]
        public static void Prepare()
        {
            MPath.BehaveAsCurrent();
            // TODO: use Moles to replace API that call into the environment
        }
    }
}
