using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Configuration;
using Microsoft.Deployment.WindowsInstaller;
using System.Diagnostics;

namespace UpdateConfigCustomAction
{
    /// <summary>
    /// Class to update web.config and app.config settings during or post deployment.  
    /// </summary>
    public class configUpdater : AppSettingsReader
    {
        public string docName = String.Empty;
        private XmlDocument _configFile;
        public XmlDocument configFile 
        {
            get
            {
                return _configFile;
            }
            set
            {
                if (_configFile == null)
                {
                    _configFile = value;
                }
            }
        }
        private XmlNode node;

        public configUpdater(XmlDocument config)
        {
            if (config == null)
                throw new ArgumentNullException("xml Config file must be supplied");
                configFile = config;
        }

        public configUpdater()
        {
            configFile = new XmlDocument();
        }

        /// <summary>
        /// Sets the endpoint address for a WCF service in a web.config, or app.config file
        /// </summary>
        /// <param name="endpointName">The name of the endpoint to update</param>
        /// <param name="endpointAddress">The new address for the endpoint being updated</param>
        /// <param name="sessionLog">Reference to the logfile</param>
        /// <returns>returns true if successful</returns>
        public bool SetClientEndpointValue(string endpointName, string endpointAddress, Action<string> sessionLog)
        {
            if (sessionLog == (Action<string>)null)
                throw new ArgumentNullException("sessionLog");

            try
            {
                endpointAddress = ReplaceKeywordWithServername(endpointAddress);
                loadConfigDoc(configFile);
                node = configFile.SelectSingleNode("//client");

                if (node == null)
                {
                    throw new InvalidOperationException("client section not found");
                }

                XmlElement addElem = (XmlElement)node.SelectSingleNode(String.Format("//endpoint[@name='{0}']", endpointName));
                if (addElem != null)
                {
                    addElem.SetAttribute("address", endpointAddress);
                }
                else
                {
                    throw new XmlException(String.Format("An endpoint with the name {0} could not be found", endpointName));
                }
                saveConfigDoc(configFile, docName, sessionLog);
                return true;
            }
            catch(Exception ex)
            {
                sessionLog(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Sets the Service endpoint address for a WCF service in a web.config, or app.config file
        /// </summary>
        /// <param name="endpointName">The name of the endpoint to update</param>
        /// <param name="endpointAddress">The new address for the endpoint being updated</param>
        /// <param name="sessionLog">Reference to the logfile</param>
        /// <returns>returns true if successful</returns>
        public bool SetServiceEndPointValue(string configSectionName, string attributeName, string attributeValue, Action<string> sessionLog)
        {
            if (sessionLog == (Action<string>)null)
                throw new ArgumentNullException("sessionLog");

            try
            {
                attributeValue = ReplaceKeywordWithServername(attributeValue);
                loadConfigDoc(configFile);
                node = configFile.SelectSingleNode(string.Format("//services/service[@name='{0}']", configSectionName));

                if (node == null)
                {
                    throw new InvalidOperationException("Service Endpoint section not found");
                }

                XmlElement addElem = (XmlElement)node.SelectSingleNode(string.Format("//services/service[@name='{0}']/endpoint", configSectionName));
                if (addElem != null)
                {
                    addElem.SetAttribute(attributeName, attributeValue);
                }
                else
                {
                    throw new XmlException(String.Format("An endpoint for the service {0} could not be found", configSectionName));
                }
                saveConfigDoc(configFile, docName, sessionLog);
                return true;
            }
            catch (Exception ex)
            {
                sessionLog(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Updates or inserts a setting in the AppSettings section of a web.config or app.config file
        /// </summary>
        /// <param name="key">The name of the key to be updated or added</param>
        /// <param name="value">The new value for the key being updated or inserted</param>
        /// <param name="sessionLog">Reference to the logfile</param>
        /// <returns>returns true if successful</returns>
        public bool SetAppSettingValue(string key, string value, Action<string> sessionLog)
        {

            if (sessionLog == (Action<string>)null)
                throw new ArgumentNullException("sessionLog");

            try
            {
                value = ReplaceKeywordWithServername(value);
                loadConfigDoc(configFile);
                node = configFile.SelectSingleNode("//appSettings");

                if (node == null)
                {
                    throw new InvalidOperationException("configuration section not found");
                }

                XmlElement addElem = (XmlElement)node.SelectSingleNode(String.Format("//add[@key='{0}']", key));
                if (addElem != null)
                {
                    addElem.SetAttribute("value", value);
                }
                else
                {
                    XmlElement entry = configFile.CreateElement("add");
                    entry.SetAttribute("key", key);
                    entry.SetAttribute("value", value);
                    node.AppendChild(entry);
                }
                saveConfigDoc(configFile, docName, sessionLog);
                return true;
            }
            catch (Exception ex)
            {
                sessionLog(ex.Message);
                return false;
            }

        }

        /// <summary>
        /// updates a custom configuration section attribute in a web.config or app.config file 
        /// </summary>
        /// <param name="configSectionName">The name of the config section. This must already be defined in your .config file</param>
        /// <param name="attributeName">The name of the attribute that is to be updated</param>
        /// <param name="attributeValue">The new value of the attribute being updated</param>
        /// <param name="sessionLog">Reference to the logfile</param>
        /// <returns>returns true if successful</returns>
        public bool SetCustomConfigSectionAttribute(string configSectionName, string attributeName, string attributeValue, Action<string> sessionLog)
        {

            if (sessionLog == (Action<string>)null)
                throw new ArgumentNullException("sessionLog");
            try
            {
                attributeValue = ReplaceKeywordWithServername(attributeValue);
                loadConfigDoc(configFile);
                node = configFile.SelectSingleNode("//configuration");

                if (node == null)
                {
                    throw new InvalidOperationException("configuration section not found");
                }

                XmlElement addElem = (XmlElement)node.SelectSingleNode("//" + configSectionName);
                if (addElem != null)
                {
                    addElem.SetAttribute(attributeName, attributeValue);
                }
                else
                {
                    throw new XmlException(String.Format("A custom config section with the name {0} could not be found", configSectionName));

                }
                saveConfigDoc(configFile, docName, sessionLog);
                return true;
            }
            catch (Exception ex)
            {
                sessionLog(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Updates a custom configuration section value in a web.config or app.config file
        /// </summary>
        /// <param name="configSectionName">The name of the config section. This must already be defined in your .config file</param>
        /// <param name="attributeName">The name of the attribute that is to be updated</param>
        /// <param name="attributeValue">The new value of the attribute being updated</param>
        /// <param name="sessionLog">Reference to the logfile</param>
        /// <returns>returns true if successful</returns>
        public bool SetCustomConfigSectionValue(string configSectionName, string attributeName, string attributeValue, Action<string> sessionLog)
        {

            if (sessionLog == (Action<string>)null)
                throw new ArgumentNullException("sessionLog");
            try
            {
                attributeValue = ReplaceKeywordWithServername(attributeValue);
                loadConfigDoc(configFile);
                node = configFile.SelectSingleNode("//configuration/" + configSectionName);

                if (node == null)
                {
                    throw new InvalidOperationException(configSectionName + " section not found");
                }


                XmlElement addElem = (XmlElement)node.SelectSingleNode(String.Format("*/setting[@name='{0}']/value", attributeName));
                if (addElem != null)
                {
                    addElem.InnerText =  attributeValue;
                }
                else
                {
                    throw new XmlException(String.Format("A custom config section with the name {0} could not be found", configSectionName));

                }
                saveConfigDoc(configFile, docName, sessionLog);
                return true;
            }
            catch (Exception ex)
            {
                sessionLog(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// updates a service base address in a web.config or app.config file
        /// </summary>
        /// <param name="configSectionName">The name of the config section. This must already be defined in your .config file</param>
        /// <param name="attributeName">The name of the attribute that is to be update</param>
        /// <param name="attributeValue">The new value of the attribute being updated</param>
        /// <param name="sessionLog">Reference to the logfile</param>
        /// <returns>returns true if successful</returns>
        public bool SetServiceBaseAddress(string configSectionName, string attributeName, string attributeValue, Action<string> sessionLog)
        {
            if (sessionLog == (Action<string>)null)
                throw new ArgumentNullException("sessionLog");

            try
            {
                attributeValue = ReplaceKeywordWithServername(attributeValue);
                loadConfigDoc(configFile);
                node = configFile.SelectSingleNode(string.Format("//service[@name='{0}']", configSectionName));

                if (node == null)
                {
                    throw new InvalidOperationException("configuration section not found");
                }

                XmlElement addElem = (XmlElement)node.SelectSingleNode("//host//baseAddresses//add");
                if (addElem != null)
                {
                    addElem.SetAttribute(attributeName, attributeValue);
                }
                else
                {
                    throw new XmlException(String.Format("A custom config section with the name {0} could not be found", configSectionName));

                }
                saveConfigDoc(configFile, docName, sessionLog);
                return true;
            }
            catch (Exception ex)
            {
                sessionLog(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Updates or inserts a connection string in to a web.config or app.config file
        /// </summary>
        /// <param name="connectionStringName">The name of the connection string to update or insert</param>
        /// <param name="encryptedConnectionstringValue">An encrypted connection string to update or insert</param>
        /// <param name="sessionLog">Reference to the logfile</param>
        /// <returns>returns true if successful</returns>
        public bool SetConnectionStringValue(string connectionStringName, string encryptedConnectionstringValue, Action<string> sessionLog)
        {
            if (sessionLog == (Action<string>)null)
                throw new ArgumentNullException("sessionLog");
            try
            {
                encryptedConnectionstringValue = ReplaceKeywordWithServername(encryptedConnectionstringValue);
                loadConfigDoc(configFile);
                node = configFile.SelectSingleNode("//connectionStrings");

                if (node == null)
                {
                    throw new InvalidOperationException("Connection strings section not found");
                }

                XmlElement addElem = (XmlElement)node.SelectSingleNode(String.Format("//add[@name='{0}']", connectionStringName));
                if (addElem != null)
                {
                    addElem.SetAttribute("connectionString", encryptedConnectionstringValue);
                }
                else
                {
                    XmlElement entry = configFile.CreateElement("add");
                    entry.SetAttribute("name", connectionStringName);
                    entry.SetAttribute("connectionString", encryptedConnectionstringValue);
                    node.AppendChild(entry);
                }
                saveConfigDoc(configFile, docName, sessionLog);
                return true;
            }
            catch(Exception ex)
            {
                sessionLog(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// update any attribute of any node
        /// </summary>
        /// <param name="nodePath">the xpath expression to navigate to the node to change</param>
        /// <param name="attributeName">the name of the attribute to be updated</param>
        /// <param name="attributeValue">the value of the attribute to be updated</param>
        /// <param name="sessionLog">the session to write logs back to</param>
        /// <returns>Returns true for success</returns>
        public bool SetXMLNodeAttributeValue(string nodePath, string attributeName, string attributeValue, Action<string> sessionLog)
        {
            if (sessionLog == (Action<string>)null)
                throw new ArgumentNullException("sessionLog");

            try
            {
                loadConfigDoc(configFile);
            }
            catch (Exception ex)
            {
                sessionLog(String.Format("Unable to locate file: {0}\r\n{1}", configFile, ex.Message));
                return false;
            }

            try
            {
                attributeValue = ReplaceKeywordWithServername(attributeValue);
                XmlElement addElem = (XmlElement)configFile.SelectSingleNode(nodePath);
                if (addElem != null)
                {
                    addElem.SetAttribute(attributeName, attributeValue);
                }
                else
                {
                    throw new InvalidOperationException("Could not locate the specified Xml Node");
                }
                saveConfigDoc(configFile, docName, sessionLog);
                return true;
            }
            catch (Exception ex)
            {
                sessionLog(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Set a generic XML node value
        /// </summary>
        /// <param name="nodePath">the xpath to the node to be updated</param>
        /// <param name="nodeValue">the value to update the node with</param>
        /// <param name="sessionLog">the session log for writing events to the msi log</param>
        /// <returns>true for success and false for failure</returns>
        public bool SetXMLNodeValue(string nodePath, string nodeValue, Action<string> sessionLog)
        {
            if (sessionLog == (Action<string>)null)
                throw new ArgumentNullException("sessionLog");

            try
            {
                loadConfigDoc(configFile);
            }
            catch (Exception ex)
            {
                sessionLog(String.Format("Unable to locate file: {0}\r\n{1}", configFile, ex.Message));
                return false;
            }

            try
            {
                nodeValue = ReplaceKeywordWithServername(nodeValue);

                XmlElement addElem = (XmlElement)configFile.SelectSingleNode(nodePath);
                if (addElem != null)
                {
                    addElem.InnerText = nodeValue;
                }
                else
                {
                    throw new InvalidOperationException("Could not locate the specified Xml Node");
                }
                saveConfigDoc(configFile, docName, sessionLog);
                return true;

            }
            catch (Exception ex)
            {
                sessionLog(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Set the default proxy in a web.config or app.config file
        /// </summary>
        /// <param name="ProxyAddress">the http address of the proxy</param>
        /// <param name="sessionLog">the session log for writing events to the msi log</param>
        /// <returns>returns true if successful</returns>
        public bool SetDefaultProxy(string ProxyAddress, Action<string> sessionLog)
        {

            if (sessionLog == (Action<string>)null)
                throw new ArgumentNullException("sessionLog");

            try
            {
                loadConfigDoc(configFile);

                XmlElement addElem = (XmlElement)configFile.SelectSingleNode("//configuration/system.net/defaultProxy/proxy");
                if (addElem != null)
                {
                    addElem.SetAttribute("proxyaddress", ProxyAddress);
                }
                else
                {
                    throw new InvalidOperationException("Could not locate the Proxy Address Element");
                }
                saveConfigDoc(configFile, docName, sessionLog);
                return true;

            }
            catch (Exception ex)
            {
                sessionLog(ex.Message);
                return false;
            }
                
            
        }

        /// <summary>
        /// Saves a web.config or app.config file
        /// </summary>
        /// <param name="configFile">The config file to be saved</param>
        /// <param name="configPath">The file path to save the file too</param>
        /// <param name="sessionLog">Reference to the logfile</param>
        private static void saveConfigDoc(XmlDocument configFile, string configPath, Action<string> sessionLog)
        {
            try
            {
                if (configPath != "")
                {
                    using (XmlTextWriter xmlWriter = new XmlTextWriter(configPath, null) { Formatting = Formatting.Indented })
                    {
                        configFile.WriteTo(xmlWriter);
                        xmlWriter.Flush();
                        xmlWriter.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                sessionLog(ex.Message);
            }
        }

        /// <summary>
        /// Loads the web.config or app.config file that was specified when this class was instantiated  
        /// </summary>
        /// <param name="configFile">an empty XmlDocument to load the config file in to</param>
        /// <returns>returns a config file ready for editing</returns>
        private XmlDocument loadConfigDoc(XmlDocument config)
        {
            if (!config.HasChildNodes)
            {
                docName = docName.Replace(@"\\", @"\");
                config.Load(docName);
            }
            
            return config;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        internal string ReplaceKeywordWithServername(string inputString)
        {
            string outputString;
            var machineName = Environment.MachineName;
            const string keywordOne = "PRODUCTIONSERVER";
            const string keywordTwo = "PRODUCTIONTILL";

            if (inputString.ToUpper().Contains(keywordOne))
                outputString = inputString.Replace(keywordOne, machineName);
            else if (inputString.ToUpper().Contains(keywordTwo))
                outputString = inputString.Replace(keywordTwo, string.Format("{0}SVR",machineName.Substring(0,4)));
            else
                outputString = inputString;

            return outputString;
        }
    }
}
