using System;
using System.Collections.Generic;
using System.IO;
using System.Preparations;
using System.Xml;
using ApplicationConfigSettings;
using Microsoft.Deployment.WindowsInstaller;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Generated;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UpdateConfigCustomAction
{
    [TestClass]
    [PexClass(typeof(CustomActions))]

    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class CustomActionsTest
    {
        public XmlDocument TestConfigFile { get; set; }
        private string tempPath { get; set; }
        private TestContext testContextInstance;
        private string FullFilePath { get; set; }

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestInitialize()]
        public void MyTestInitialize()
        {
            TestConfigFile = CreateConfigFile.CreateXmlConfigFile();
            tempPath = Path.GetTempPath();
            FullFilePath = Path.Combine(tempPath, "web.config");
            TestConfigFile.Save(FullFilePath);

        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            File.Delete(FullFilePath);
        }




        [PexMethod]
        public ActionResult ConfigureApplicationSettings(Session session)
        {
            ActionResult result = CustomActions.ConfigureApplicationSettings(session);
            return result;
        }

        [PexMethod(MaxConditions = 1000, MaxBranches = 20000)]
        public List<Setting> GetApplicationSettings(Action<string> sessionLog, EnvironmentType environment)
        {
            List<Setting> result = CustomActions.GetApplicationSettings(sessionLog, environment);
            return result;
        }

        [TestMethod]
        [Ignore]
        [PexDescription("this test requires to run under the Pex profiler in order to reproduce")]
        [PexNotReproducible]
        [HostType("Moles")]
        public void GetApplicationSettingsThrowsMoleNotImplementedException91()
        {
            List<Setting> list;
            AppDomainPreparation.Prepare();
            list = this.GetApplicationSettings
                       ((Action<string>)null, (EnvironmentType)0);
        }
    }
}

