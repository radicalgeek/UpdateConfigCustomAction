using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace UpdateConfigCustomAction
{
    class CreateConfigFile
    {
        /// <summary>
        /// Method to create a .config file for testing
        /// </summary>
        public static XmlDocument CreateXmlConfigFile()
        {
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(docNode);

            XmlNode configurationNode = doc.CreateElement("configuration");
            doc.AppendChild(configurationNode);

            XmlNode configSectionsNode = doc.CreateElement("configSections");
            configurationNode.AppendChild(configSectionsNode);

            XmlNode sectionNode = doc.CreateElement("section");
            XmlAttribute sectionAttributeName = doc.CreateAttribute("name");
            sectionAttributeName.Value = "environmentSetting";
            XmlAttribute sectionAttributeType = doc.CreateAttribute("type");
            sectionAttributeType.Value = "ReferenceImplimentation.Service.EnvironmentConfiguration, CalcService";
            sectionNode.Attributes.Append(sectionAttributeName);
            sectionNode.Attributes.Append(sectionAttributeType);
            configSectionsNode.AppendChild(sectionNode);

            XmlNode environmentSettingNode = doc.CreateElement("environmentSetting");
            XmlAttribute environmentAttribute = doc.CreateAttribute("environment");
            environmentAttribute.Value = "UAT";
            environmentSettingNode.Attributes.Append(environmentAttribute);
            configurationNode.AppendChild(environmentSettingNode);

            XmlNode aNewNode = doc.CreateElement("someotherNode");
            environmentSettingNode.AppendChild(aNewNode);

            XmlNode custSetting = doc.CreateElement("setting");
            XmlAttribute settingAttribute = doc.CreateAttribute("name");
            settingAttribute.Value = "mySetting";
            custSetting.Attributes.Append(settingAttribute);
            aNewNode.AppendChild(custSetting);

            XmlNode custValue = doc.CreateElement("value");
            custValue.InnerText = "QA";
            custSetting.AppendChild(custValue);

            XmlNode systemServiceModelNode = doc.CreateElement("system.serviceModel");
            configurationNode.AppendChild(systemServiceModelNode);
            XmlNode clientNode = doc.CreateElement("client");
            systemServiceModelNode.AppendChild(clientNode);
            XmlNode endpointNode = doc.CreateElement("endpoint");
            XmlAttribute endpointAttributeAddress = doc.CreateAttribute("address");
            endpointAttributeAddress.Value = "http://localhost:9000/CalculatorService.svc";
            endpointNode.Attributes.Append(endpointAttributeAddress);
            XmlAttribute endpointAttributebinding = doc.CreateAttribute("binding");
            endpointAttributebinding.Value = "basicHttpBinding";
            endpointNode.Attributes.Append(endpointAttributebinding);
            XmlAttribute endpointAttributeBindingConfig = doc.CreateAttribute("bindingConfiguration");
            endpointAttributeBindingConfig.Value = "BasicHttpBinding_ICalculator";
            endpointNode.Attributes.Append(endpointAttributeBindingConfig);
            XmlAttribute endpointAttributeContract = doc.CreateAttribute("contract");
            endpointAttributeContract.Value = "Calculator.ICalculator";
            endpointNode.Attributes.Append(endpointAttributeContract);
            XmlAttribute endpointAttributeName = doc.CreateAttribute("name");
            endpointAttributeName.Value = "BasicHttpBinding_ICalculator";
            endpointNode.Attributes.Append(endpointAttributeName);
            clientNode.AppendChild(endpointNode);

            XmlNode services = doc.CreateElement("services");
            XmlNode service = doc.CreateElement("service");
            XmlAttribute serviceName = doc.CreateAttribute("name");
            serviceName.Value = "DebtService.DebtService";
            XmlAttribute serviceBehav = doc.CreateAttribute("behaviorConfiguration");
            serviceBehav.Value = "ServiceBehavior";

            service.Attributes.Append(serviceName);
            service.Attributes.Append(serviceBehav);
            XmlNode host = doc.CreateElement("host");
            XmlNode baseAddress = doc.CreateElement("baseAddresses");
            XmlNode addressAdd = doc.CreateElement("add");
            XmlAttribute bAddress = doc.CreateAttribute("baseAddress");
            bAddress.Value = "http://localhost:8742/";
            addressAdd.Attributes.Append(bAddress);
            baseAddress.AppendChild(addressAdd);
            host.AppendChild(baseAddress);
            service.AppendChild(host);
            services.AppendChild(service);
            systemServiceModelNode.AppendChild(services);

            XmlNode endpoint = doc.CreateElement("endpoint");
            
            XmlAttribute address = doc.CreateAttribute("address");
            address.Value = "net.msmq://ukuatefcoesb01/private/debtcollection.svc";
            endpoint.Attributes.Append(address);
            service.AppendChild(endpoint);

            XmlNode appSettingsNode = doc.CreateElement("appSettings");
            configurationNode.AppendChild(appSettingsNode);
            XmlNode addNode = doc.CreateElement("add");
            XmlAttribute keyAttribute = doc.CreateAttribute("key");
            keyAttribute.Value = "CommIdea.AccountId";
            addNode.Attributes.Append(keyAttribute);
            XmlAttribute valueAttribute = doc.CreateAttribute("value");
            valueAttribute.Value = "10006751";
            addNode.Attributes.Append(valueAttribute);
            appSettingsNode.AppendChild(addNode);

            XmlNode connectionStringsNode = doc.CreateElement("connectionStrings");
            configurationNode.AppendChild(connectionStringsNode);
            XmlNode addConNode = doc.CreateElement("add");
            XmlAttribute conName = doc.CreateAttribute("name");
            conName.Value = "constring1";
            addConNode.Attributes.Append(conName);
            XmlAttribute addConString = doc.CreateAttribute("connectionString");
            addConString.Value = "2MWqsaSKofHF+hxPbdxNvBubUBQaW+yrzW0LusEGS6/xOLgzjOD8BRVaBMmz4PhmgfCbt2mNu8m/AYK6+uKRa+/ZZKe6tZFcwi/cElsJTY/o3BvEwk4yTkv7FSWuCNRe";
            addConNode.Attributes.Append(addConString);
            connectionStringsNode.AppendChild(addConNode);

            XmlNode systemnet = doc.CreateElement("system.net");
            configurationNode.AppendChild(systemnet);

            XmlNode defaultProxy = doc.CreateElement("defaultProxy");
            XmlAttribute enabled = doc.CreateAttribute("enabled");
            enabled.Value = "true";
            defaultProxy.Attributes.Append(enabled);
            XmlAttribute useDefaultCredentials = doc.CreateAttribute("useDefaultCredentials");
            useDefaultCredentials.Value = "true";
            defaultProxy.Attributes.Append(useDefaultCredentials);

            XmlNode Proxy = doc.CreateElement("proxy");
            XmlAttribute autoDetect = doc.CreateAttribute("autoDetect");
            autoDetect.Value = "true";
            Proxy.Attributes.Append(autoDetect);

            XmlAttribute proxyAddress = doc.CreateAttribute("proxyaddress");
            proxyAddress.Value = "http://bloxx.dfguk.com:8080";
            Proxy.Attributes.Append(proxyAddress);

            defaultProxy.AppendChild(Proxy);
            systemnet.AppendChild(defaultProxy);
            return doc;
        }
    }
}
