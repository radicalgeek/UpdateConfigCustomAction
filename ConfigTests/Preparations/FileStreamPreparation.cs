using System;
using Microsoft.Pex.Framework;
using System.IO.Moles;
using System.IO;

namespace System.IO.Preparations
{
    /// <summary>Contains a method to prepare the type FileStream</summary>
    public static partial class FileStreamPreparation
    {
        /// <summary>Prepares the environment (and the moles) before executing any method of the prepared type FileStream</summary>
        [PexPreparationMethod(typeof(FileStream))]
        public static void Prepare()
        {
            MFileStream.BehaveAsCurrent();
            // TODO: use Moles to replace API that call into the environment
        }
    }
}
