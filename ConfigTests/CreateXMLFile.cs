using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace UpdateConfigCustomAction
{
    class CreateXMLFile
    {

        public static XmlDocument CreateXMLDocument()
        {
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(docNode);

            XmlNode configurationNode = doc.CreateElement("Configuration");
            doc.AppendChild(configurationNode);

            XmlNode hostNode = doc.CreateNode(XmlNodeType.Element, "host", "");
            hostNode.InnerText = "https://testserver.datacash.com/Transaction";
            XmlNode timeoutNode = doc.CreateNode(XmlNodeType.Element, "timeout", "");
            timeoutNode.InnerText = "500";
            XmlNode proxyNode = doc.CreateNode(XmlNodeType.Element, "proxy", "");
            proxyNode.InnerText = "http://bloxx.dfguk.com:8080";
            XmlNode logfileNode = doc.CreateNode(XmlNodeType.Element, "logfile", "");
            logfileNode.InnerText = @"C:\Inetpub\wwwroot\WSDataCash\log.txt";
            XmlNode loggingNode = doc.CreateNode(XmlNodeType.Element, "logging", "");
            loggingNode.InnerText = "1";

            configurationNode.AppendChild(hostNode);
            configurationNode.AppendChild(timeoutNode);
            configurationNode.AppendChild(proxyNode);
            configurationNode.AppendChild(logfileNode);
            configurationNode.AppendChild(loggingNode);

            return doc;
        }
    }
}


