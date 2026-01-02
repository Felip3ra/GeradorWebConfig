using GeradorWebConfig.Application.TemplateRepository;
using GeradorWebConfig.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace GeradorWebConfig.Infraestructure.TemplateRepository
{
    public sealed class FileTemplateRepository : ITemplateRepository
    {
        private readonly string _templatesRoot;

        public FileTemplateRepository(string templatesRoot)
        {
            if (string.IsNullOrWhiteSpace(templatesRoot))
                throw new ArgumentException("Templates root invalido.", nameof(templatesRoot));

            _templatesRoot = templatesRoot;
            Directory.CreateDirectory(_templatesRoot);
        }

        public IReadOnlyList<string> ListEmpresas()
        {
            Directory.CreateDirectory(_templatesRoot);
            return Directory.GetDirectories(_templatesRoot)
                .Select(Path.GetFileName)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .OrderBy(x => x)
                .ToList()!;
        }

        public void CreateEmpresa(string empresa)
        {
            var empresaDir = Path.Combine(_templatesRoot, empresa);
            if (Directory.Exists(empresaDir))
                throw new InvalidOperationException("Empresa ja existe.");

            Directory.CreateDirectory(empresaDir);
        }

        public IReadOnlyList<TemplateInfo> ListTemplates(string empresa)
        {
            var dir = Path.Combine(_templatesRoot, empresa);
            if (!Directory.Exists(dir))
                return new List<TemplateInfo>();

            return Directory.EnumerateFiles(dir, "*.xml")
                .Select(path => new TemplateInfo(Path.GetFileNameWithoutExtension(path)!, path))
                .OrderBy(t => t.Name)
                .ToList();
        }

        public void SaveTemplate(string empresa, string templateName, WebConfigSections sections)
        {
            var empresaDir = Path.Combine(_templatesRoot, empresa);
            Directory.CreateDirectory(empresaDir);

            var sanitizedName = SanitizeFileName(templateName);
            if (string.IsNullOrWhiteSpace(sanitizedName))
                throw new ArgumentException("Nome de template invalido.", nameof(templateName));

            var doc = new XDocument(
                new XElement("WebConfigTemplate",
                    new XAttribute("empresa", empresa),
                    new XAttribute("name", templateName),
                    new XAttribute("updatedAt", DateTime.Now.ToString("s")),
                    new XElement(sections.ConnectionStrings),
                    new XElement(sections.AppSettings)
                )
            );

            var path = Path.Combine(empresaDir, $"{sanitizedName}.xml");
            doc.Save(path);
        }

        public WebConfigSections ReadTemplate(string templatePath)
        {
            var doc = XDocument.Load(templatePath, LoadOptions.PreserveWhitespace);
            var root = doc.Root ?? throw new InvalidOperationException("Template invalido");

            var conn = root.Elements().FirstOrDefault(e => e.Name.LocalName == "connectionStrings")
                ?? new XElement("connectionStrings");

            var app = root.Elements().FirstOrDefault(e => e.Name.LocalName == "appSettings")
                ?? new XElement("appSettings");

            return new WebConfigSections(conn, app);
        }

        private static string SanitizeFileName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;

            foreach (var c in Path.GetInvalidFileNameChars())
                name = name.Replace(c, '_');

            return name.Trim();
        }
    }
}
