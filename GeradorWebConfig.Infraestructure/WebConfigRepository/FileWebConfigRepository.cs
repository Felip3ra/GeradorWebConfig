using GeradorWebConfig.Application.WebConfigRepository;
using GeradorWebConfig.Domain.Models;
using System;
using System.Linq;
using System.Xml.Linq;

namespace GeradorWebConfig.Infraestructure.WebConfigRepository
{
    public sealed class FileWebConfigRepository : IWebConfigRepository
    {
        public WebConfigSections ReadSections(string webConfigPath)
        {
            var doc = XDocument.Load(webConfigPath, LoadOptions.PreserveWhitespace);
            var root = doc.Root ?? throw new InvalidOperationException("web.config invalido");

            var conn = root.Elements().FirstOrDefault(e => e.Name.LocalName == "connectionStrings")
                ?? new XElement("connectionStrings");

            var app = root.Elements().FirstOrDefault(e => e.Name.LocalName == "appSettings")
                ?? new XElement("appSettings");

            return new WebConfigSections(conn, app);
        }

        public void SaveSections(string webConfigPath, WebConfigSections sections)
        {
            var doc = XDocument.Load(webConfigPath, LoadOptions.PreserveWhitespace);
            var root = doc.Root ?? throw new InvalidOperationException("web.config invalido");

            var newConn = new XElement(sections.ConnectionStrings);
            var newApp = new XElement(sections.AppSettings);

            root.Elements()
                .Where(e => e.Name.LocalName is "connectionStrings" or "appSettings")
                .Remove();

            var configSections = root.Elements()
                .FirstOrDefault(e => e.Name.LocalName == "configSections");

            if (configSections != null)
            {
                configSections.AddAfterSelf(newApp);
                configSections.AddAfterSelf(newConn);
            }
            else
            {
                root.AddFirst(newApp);
                root.AddFirst(newConn);
            }

            doc.Save(webConfigPath);
        }
    }
}
