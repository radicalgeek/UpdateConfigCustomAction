// <auto-generated>
// This file contains automatically generated unit tests.
// Do NOT modify this file manually.
// 
// When Pex is invoked again,
// it might remove or update any previously generated unit tests.
// 
// If the contents of this file becomes outdated, e.g. if it does not
// compile anymore, you may delete this file and invoke Pex again.
// </auto-generated>
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Pex.Framework.Generated;
using Microsoft.Pex.Framework;
using System.Collections.Generic;
using System.Xml;

namespace UpdateConfigCustomAction
{
    public partial class configUpdaterTest
    {

        [TestMethod]
        public void SetAppSettingValueTest()
        {
            List<string> TestLog = new List<string>();
            configUpdater target = new configUpdater(TestConfigFile);
            const string key = "CommIdea.AccountId";
            const string value = "10076061";
            bool actual = target.SetAppSettingValue(key, value, TestLog.Add);
            Assert.AreEqual(true, actual);

            XmlNode node = TestConfigFile.SelectSingleNode("//appSettings");
            XmlElement Elem = (XmlElement)node.SelectSingleNode(String.Format("//add[@key='{0}']", key));
            string attribute = Elem.GetAttribute("value");
            Assert.AreEqual(value, attribute);
        }
    }
}
