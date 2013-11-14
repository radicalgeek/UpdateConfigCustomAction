using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.ServiceModel;
using Microsoft.Deployment.WindowsInstaller;
using System.Xml.Serialization;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace UpdateConfigCustomAction
{
    /// <summary>
    /// Custom Installer actions to configure applications from web service.
    /// </summary>
    public class CustomActions
    {
        private static string settingXMLPath;

        /// <summary>
        /// Retrieves Application configuration information from Installer Settings Service
        /// </summary>
        /// <param name="session">Session information passed by WiX installer</param>
        /// <returns>Action result of success or failure, passed back to WiX installer</returns>
        [CustomAction]
        public static ActionResult ConfigureApplicationSettings(Session session)
        {
            if (session == (Session)null)
                throw new ArgumentNullException("session");

            #if DEBUG
                Debugger.Launch();
            #endif
            
            session.Log("Start Custom Action: Configure Application Settings");

            string[] properties = session.CustomActionData.ToString().Split(new string[] { "," }, StringSplitOptions.None);

            ApplicationConfigSettings.EnvironmentType environment = GetEvironmentType(properties[0], session.Log);

            string configPath = properties[1];
            if (string.IsNullOrEmpty(configPath))
                return ActionResult.Failure;

            settingXMLPath = properties[2];
            if (string.IsNullOrEmpty(settingXMLPath))
                return ActionResult.Failure;

            List<ApplicationConfigSettings.Setting> settings = GetApplicationSettings(session.Log, environment);
            if (settings.Count == 0)
            {
                session.Log("Error: unable to load application settings");
                return ActionResult.Failure;
            }

            UpdateConfigurationFile(session.Log, settings, configPath);

            session.Log("End Custom Action: Configure Application Settings");
            return ActionResult.Success;
        }

        /// <summary>
        /// Selects the correct environment for configuration from command line parameter ENV
        /// </summary>
        /// <param name="env">The string representing the environment. e.g. DEV, UAT, PROD etc</param>
        /// <param name="sessionLog">The session object from the installer for log file writing</param>
        /// <returns></returns>
        private static ApplicationConfigSettings.EnvironmentType GetEvironmentType(string env, Action<string> sessionLog)
        {
            string MessageOutput = String.Format("Configuring for environment: {0}", env);
            ApplicationConfigSettings.EnvironmentType configForEnvironment = new ApplicationConfigSettings.EnvironmentType();
            switch (env)
            {
                case "DEV":
                    sessionLog(MessageOutput);
                    configForEnvironment = ApplicationConfigSettings.EnvironmentType.DEV;
                    break;
                case "QA":
                    sessionLog(MessageOutput);
                    configForEnvironment = ApplicationConfigSettings.EnvironmentType.QA;
                    break;
                case "UAT":
                    sessionLog(MessageOutput);
                    configForEnvironment = ApplicationConfigSettings.EnvironmentType.UAT;
                    break;
                case "INT":
                    sessionLog(MessageOutput);
                    configForEnvironment = ApplicationConfigSettings.EnvironmentType.INT;
                    break;
                case "CAP":
                    sessionLog(MessageOutput);
                    configForEnvironment = ApplicationConfigSettings.EnvironmentType.CAP;
                    break;
                case "STG":
                    sessionLog(MessageOutput);
                    configForEnvironment = ApplicationConfigSettings.EnvironmentType.STG;
                    break;
                case "PROD":
                    sessionLog(MessageOutput);
                    configForEnvironment = ApplicationConfigSettings.EnvironmentType.PROD;
                    break;
                default:
                    sessionLog("Warning: Environment not specified, defaulting to DEV");
                    configForEnvironment = ApplicationConfigSettings.EnvironmentType.DEV;
                    break;
            }
            return configForEnvironment;
        }

        /// <summary>
        /// Iterates through the settings retrieved from the Installer Settings Service, and updates the selected .config file
        /// </summary>
        /// <param name="session">The session object from the installer for log file writing</param>
        /// <param name="settings">A list of settings retrieved from the Installer Settings Service</param>
        /// <param name="configUpdater">Instance of the configUpdater Class to perform the updates on the file</param>
        private static void UpdateConfigurationFile(Action<string> sessionLog, List<ApplicationConfigSettings.Setting> settings, string configPath)
        {
            foreach (ApplicationConfigSettings.Setting setting in settings)
            {
                configUpdater thisConfig = new configUpdater() { docName = Path.Combine(configPath, setting.configFile) };
                bool result = false;
                switch (setting.settingType)
                {
                    case ApplicationConfigSettings.SettingType.ConnectionString:
                        result = thisConfig.SetConnectionStringValue(setting.settingKey, setting.settingValue, sessionLog);
                        if (result)
                        {
                            sessionLog(String.Format("Connection String {0} updated", setting.settingName));
                        }
                        else
                        {
                            sessionLog(String.Format("Error: Connection String {0} update failed", setting.settingName));
                        }
                        break;
                    case ApplicationConfigSettings.SettingType.AppSetting:
                        result = thisConfig.SetAppSettingValue(setting.settingKey, setting.settingValue, sessionLog);
                        if (result)
                        {
                            sessionLog(String.Format("App Setting {0} updated", setting.settingName));
                        }
                        else
                        {
                            sessionLog(String.Format("Error: App Setting {0} update failed", setting.settingName));
                        }
                        break;
                    case ApplicationConfigSettings.SettingType.ClientEndpoint:
                        result = thisConfig.SetClientEndpointValue(setting.settingKey, setting.settingValue, sessionLog);
                        if (result)
                        {
                            sessionLog(String.Format("Client Endpoint {0} updated", setting.settingName));
                        }
                        else
                        {
                            sessionLog(String.Format("Error: Client Endpoint {0} update failed", setting.settingName));
                        }
                        break;
                    case ApplicationConfigSettings.SettingType.ServiceEndpoint:
                        result = thisConfig.SetServiceEndPointValue(setting.settingName, setting.settingKey, setting.settingValue, sessionLog);
                        if (result)
                        {
                            sessionLog(String.Format("Service Endpoint {0} updated", setting.settingName));
                        }
                        else
                        {
                            sessionLog(String.Format("Error: Service Endpoint {0} update failed", setting.settingName));
                        }
                        break;
                    case ApplicationConfigSettings.SettingType.ConfigurationSetting:
                        result = thisConfig.SetCustomConfigSectionAttribute(setting.settingName, setting.settingKey, setting.settingValue, sessionLog);
                        if (result)
                        {
                            sessionLog(String.Format("Configuration Section {0} updated", setting.settingName));
                        }
                        else
                        {
                            sessionLog(String.Format("Error: Configuration Section {0} update failed", setting.settingName));
                        }
                        break;
                    case ApplicationConfigSettings.SettingType.ConfigurationValue:
                        result = thisConfig.SetCustomConfigSectionValue(setting.settingName, setting.settingKey, setting.settingValue, sessionLog);
                        if (result)
                        {
                            sessionLog(String.Format("Configuration Value {0} updated", setting.settingName));
                        }
                        else
                        {
                            sessionLog(String.Format("Error: Configuration Value {0} update failed", setting.settingName));
                        }
                        break;
                    case ApplicationConfigSettings.SettingType.ServiceBaseAddress:
                        result = thisConfig.SetServiceBaseAddress(setting.settingName, setting.settingKey, setting.settingValue, sessionLog);
                        if (result)
                        {
                            sessionLog(String.Format("Service Base Address {0} updated", setting.settingName));
                        }
                        else
                        {
                            sessionLog(String.Format("Error: Service Base Address {0} update failed", setting.settingName));
                        }
                        break;
                    case ApplicationConfigSettings.SettingType.XMLNodeValue:
                        result = thisConfig.SetXMLNodeValue(setting.settingKey, setting.settingValue, sessionLog);
                        if (result)
                        {
                            sessionLog(String.Format("XML Node value {0} updated", setting.settingName));
                        }
                        else
                        {
                            sessionLog(String.Format("Error: XML Node value {0} update failed", setting.settingName));
                        }
                        break;
                    case ApplicationConfigSettings.SettingType.DefaultProxy:
                        result = thisConfig.SetDefaultProxy(setting.settingValue, sessionLog);
                        if (result)
                        {
                            sessionLog(String.Format("Default Proxy value {0} updated", setting.settingName));
                        }
                        else
                        {
                            sessionLog(String.Format("Error: Default Proxy value {0} update failed", setting.settingName));
                        }
                        break;
                    case ApplicationConfigSettings.SettingType.XMLNodeAttribute:
                        result = thisConfig.SetXMLNodeAttributeValue(setting.settingName, setting.settingKey, setting.settingValue, sessionLog);
                        if (result)
                        {
                            sessionLog(String.Format("XML Node value {0} updated", setting.settingName));
                        }
                        else
                        {
                            sessionLog(String.Format("Error: XML Node value {0} update failed", setting.settingName));
                        }
                        break;
                    default:
                        sessionLog("Error: An unknown setting was found, please check the configuration database");
                        break;
                }
            }
        }

        /// <summary>
        /// Creates a connection to the Installer Settings Web Service. This is only used is an XML= is not specified on the msiexec command line. 
        /// </summary>
        /// <returns>An instance of the Installer Settings Web Service</returns>
        private static ApplicationSettingsService.ApplicationSettingsClient GetSessingsServiceReference()
        {
            BasicHttpBinding binding = new BasicHttpBinding() { Name = "BasicHttpBinding_IApplicationSettings" };
            EndpointAddress address = new EndpointAddress("http://devapp.dfg.local/SettingsService/ApplicationSettings.svc");
            ApplicationSettingsService.ApplicationSettingsClient serviceClient = new ApplicationSettingsService.ApplicationSettingsClient(binding, address);
            return serviceClient;
        }

        /// <summary>
        /// Method to get the settings for the application being configured (this method was extracted to allow for unit testing without
        /// a session object 
        /// </summary>
        /// <param name="sessionLog">SessionLoging method</param>
        /// <param name="configFile">.config file location</param>
        /// <param name="environment">environment application is to be configured for</param>
        /// <param name="applicationID">Id of the application to be configured</param>
        /// <returns>Returns true for success and false for failure</returns>
        public static List<ApplicationConfigSettings.Setting> GetApplicationSettings(Action<String> sessionLog, ApplicationConfigSettings.EnvironmentType environment)
        {
            List<ApplicationConfigSettings.Setting> settings;
            using (ApplicationSettingsService.ApplicationSettingsClient client = GetSessingsServiceReference())
            {
                    XmlSerializer formatter = new XmlSerializer(typeof(ApplicationConfigSettings.SettingsList));
                    using (Stream stream = new FileStream(settingXMLPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        ApplicationConfigSettings.SettingsList settingsList = (ApplicationConfigSettings.SettingsList)formatter.Deserialize(stream);
                        settings = settingsList.settings.Where(s => s.environmentType == environment).ToList();
                    }  
            }
            return settings;
        }
    }
}
