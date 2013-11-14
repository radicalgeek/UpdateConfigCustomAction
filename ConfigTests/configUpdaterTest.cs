using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UpdateConfigCustomAction;
using System.Xml;

namespace UpdateConfigCustomAction
{
    [TestClass]
    [PexClass(typeof(configUpdater))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class configUpdaterTest
    {
        public TestContext TestContext { get; set; }
        public XmlDocument TestConfigFile { get; set; }
        public XmlDocument TestXML { get; set; }

        [TestInitialize()]
        public void TestInitialize()
        {
            TestConfigFile = CreateConfigFile.CreateXmlConfigFile();
            TestXML = CreateXMLFile.CreateXMLDocument();
        }

        [TestCleanup()]
        public void TestCleanup()
        {
            TestConfigFile = null;
            TestXML = null;
        }

        [PexMethod]
        public bool SetClientEndpointValue([PexAssumeUnderTest]configUpdater target, string endpointName, string endpointAddress, Action<string> sessionLog)
        {
            bool result = target.SetClientEndpointValue(endpointName, endpointAddress, sessionLog);
            return result;
        }

        [PexMethod]
        public configUpdater Constructor(XmlDocument config)
        {
            configUpdater target = new configUpdater(config);
            return target;
        }

        [PexMethod]
        public configUpdater Constructor01()
        {
            configUpdater target = new configUpdater();
            return target;

        }

        [PexMethod]
        public bool SetAppSettingValue([PexAssumeUnderTest]configUpdater target, string key, string value,Action<string> sessionLog)
        {
            bool result = target.SetAppSettingValue(key, value, sessionLog);
            return result;
        }

        [PexMethod]
        public bool SetConnectionStringValue([PexAssumeUnderTest]configUpdater target, string connectionStringName, string encryptedConnectionstringValue, Action<string> sessionLog)
        {
            bool result = target.SetConnectionStringValue
                              (connectionStringName, encryptedConnectionstringValue, sessionLog);
            return result;
        }

        [PexMethod]
        public bool SetCustomConfigSectionAttribute([PexAssumeUnderTest]configUpdater target, string configSectionName, string attributeName, string attributeValue, Action<string> sessionLog)
        {
            bool result = target.SetCustomConfigSectionAttribute
                              (configSectionName, attributeName, attributeValue, sessionLog);
            return result;
        }

        [PexMethod]
        public bool SetCustomConfigSectionValue([PexAssumeUnderTest]configUpdater target,  string configSectionName, string attributeName, string attributeValue, Action<string> sessionLog)
        {
            bool result =
                         target.SetCustomConfigSectionValue(configSectionName, attributeName, attributeValue, sessionLog);
            return result;
        }

        [PexMethod]
        public bool SetDefaultProxy([PexAssumeUnderTest]configUpdater target, string ProxyAddress, Action<string> sessionLog)
        {
            bool result = target.SetDefaultProxy(ProxyAddress, sessionLog);
            return result;
        }

        [PexMethod]
        public bool SetServiceBaseAddress([PexAssumeUnderTest]configUpdater target, string configSectionName, string attributeName, string attributeValue,Action<string> sessionLog)
        {
            bool result
               = target.SetServiceBaseAddress(configSectionName, attributeName, attributeValue, sessionLog);
            return result;
        }

        [PexMethod]
        public bool SetServiceEndPointValue( [PexAssumeUnderTest]configUpdater target, string configSectionName, string attributeName, string attributeValue,  Action<string> sessionLog)
        {
            bool result
               = target.SetServiceEndPointValue(configSectionName, attributeName, attributeValue, sessionLog);
            return result;
        }

        [PexMethod]
        public bool SetXMLNodeValue( [PexAssumeUnderTest]configUpdater target, string nodePath,string nodeValue, Action<string> sessionLog )
        {
            bool result = target.SetXMLNodeValue(nodePath, nodeValue, sessionLog);
            return result;
        }

        [PexMethod]
        public XmlDocument configFileGet([PexAssumeUnderTest]configUpdater target)
        {
            XmlDocument result = target.configFile;
            return result;
        }

    }
}
