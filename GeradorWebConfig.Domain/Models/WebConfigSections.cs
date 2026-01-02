using System;
using System.Xml.Linq;

namespace GeradorWebConfig.Domain.Models
{
    public sealed class WebConfigSections
    {
        public XElement ConnectionStrings { get; }
        public XElement AppSettings { get; }

        public WebConfigSections(XElement connectionStrings, XElement appSettings)
        {
            if (!string.Equals(connectionStrings.Name.LocalName, "connectionStrings", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Expected <connectionStrings>.", nameof(connectionStrings));

            if (!string.Equals(appSettings.Name.LocalName, "appSettings", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Expected <appSettings>.", nameof(appSettings));

            ConnectionStrings = connectionStrings;
            AppSettings = appSettings;
        }

        public string ConnectionStringsXml => ConnectionStrings.ToString();
        public string AppSettingsXml => AppSettings.ToString();
    }
}
