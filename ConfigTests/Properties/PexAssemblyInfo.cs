using Microsoft.Pex.Framework.Coverage;
using Microsoft.Pex.Framework.Creatable;
using Microsoft.Pex.Framework.Instrumentation;
using Microsoft.Pex.Framework.Moles;
using Microsoft.Pex.Framework.Settings;
using Microsoft.Pex.Framework.Validation;
using Microsoft.Pex.Linq;
using System;
using System.Text;
using System.IO;

// Microsoft.Pex.Framework.Settings
[assembly: PexAssemblySettings(TestFramework = "VisualStudioUnitTest")]

// Microsoft.Pex.Framework.Instrumentation
[assembly: PexAssemblyUnderTest("UpdateConfigCustomAction")]
[assembly: PexInstrumentAssembly("Microsoft.Deployment.WindowsInstaller")]
[assembly: PexInstrumentAssembly("System.Core")]
[assembly: PexInstrumentAssembly("System.ServiceModel")]
[assembly: PexInstrumentAssembly("ApplicationConfigSettings")]

// Microsoft.Pex.Framework.Creatable
[assembly: PexCreatableFactoryForDelegates]

// Microsoft.Pex.Framework.Validation
[assembly: PexAllowedContractRequiresFailureAtTypeUnderTestSurface]
[assembly: PexAllowedXmlDocumentedException]

// Microsoft.Pex.Framework.Coverage
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "Microsoft.Deployment.WindowsInstaller")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "System.Core")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "System.ServiceModel")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "ApplicationConfigSettings")]

// Microsoft.Pex.Framework.Moles
[assembly: PexAssumeContractEnsuresFailureAtBehavedSurface]
[assembly: PexChooseAsBehavedCurrentBehavior]

// Microsoft.Pex.Linq
[assembly: PexLinqPackage]

[assembly: PexInstrumentType("mscorlib", "System.Globalization.CodePageDataItem")]
[assembly: PexInstrumentAssembly("System.Runtime.Serialization")]
[assembly: PexInstrumentAssembly("System.Configuration")]
[assembly: PexInstrumentAssembly("System.Runtime.DurableInstancing")]
[assembly: PexInstrumentAssembly("System.Xml")]
[assembly: PexInstrumentType(typeof(Attribute))]
[assembly: PexInstrumentType(typeof(EncoderReplacementFallback))]
[assembly: PexInstrumentType(typeof(DecoderReplacementFallback))]
[assembly: PexInstrumentType(typeof(EncoderFallback))]
[assembly: PexInstrumentType(typeof(DecoderFallback))]
[assembly: PexInstrumentType("mscorlib", "System.Globalization.EncodingTable")]
[assembly: PexInstrumentType(typeof(Decoder))]
[assembly: PexInstrumentType("mscorlib", "System.Text.UTF8Encoding+UTF8Decoder")]
[assembly: PexInstrumentType(typeof(AppDomain))]
[assembly: PexInstrumentType(typeof(Path))]
[assembly: PexInstrumentType(typeof(FileStream))]
[assembly: PexInstrumentAssembly("System.Configuration")]
