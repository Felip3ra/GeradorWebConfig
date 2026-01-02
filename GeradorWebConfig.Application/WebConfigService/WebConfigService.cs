using GeradorWebConfig.Application.SectionXmlParser;
using GeradorWebConfig.Application.TemplateRepository;
using GeradorWebConfig.Application.WebConfigFinder;
using GeradorWebConfig.Application.WebConfigRepository;
using GeradorWebConfig.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace GeradorWebConfig.Application.WebConfigService
{
    public sealed class WebConfigService : IWebConfigService
    {
        private readonly IWebConfigRepository _webConfigRepository;
        private readonly ITemplateRepository _templateRepository;
        private readonly IWebConfigFinder _webConfigFinder;
        private readonly ISectionXmlParser _sectionXmlParser;

        public WebConfigService(
            IWebConfigRepository webConfigRepository,
            ITemplateRepository templateRepository,
            IWebConfigFinder webConfigFinder,
            ISectionXmlParser sectionXmlParser)
        {
            _webConfigRepository = webConfigRepository ?? throw new ArgumentNullException(nameof(webConfigRepository));
            _templateRepository = templateRepository ?? throw new ArgumentNullException(nameof(templateRepository));
            _webConfigFinder = webConfigFinder ?? throw new ArgumentNullException(nameof(webConfigFinder));
            _sectionXmlParser = sectionXmlParser ?? throw new ArgumentNullException(nameof(sectionXmlParser));
        }

        public IReadOnlyList<string> ListEmpresas() => _templateRepository.ListEmpresas();

        public void CreateEmpresa(string empresa)
        {
            var sanitized = SanitizeName(empresa);
            if (string.IsNullOrWhiteSpace(sanitized))
                throw new ArgumentException("Empresa invalida.", nameof(empresa));

            _templateRepository.CreateEmpresa(sanitized);
        }

        public IReadOnlyList<TemplateInfo> ListTemplates(string empresa) =>
            _templateRepository.ListTemplates(empresa);

        public IReadOnlyList<string> ListWebConfigFiles(string baseFolder) =>
            _webConfigFinder.FindWebConfigFiles(baseFolder);

        public WebConfigSections ReadWebConfig(string webConfigPath) =>
            _webConfigRepository.ReadSections(webConfigPath);

        public WebConfigSections ReadTemplate(string templatePath) =>
            _templateRepository.ReadTemplate(templatePath);

        public WebConfigSections BuildSections(string connectionStringsXml, string appSettingsXml)
        {
            var conn = _sectionXmlParser.ParseSectionXml(connectionStringsXml, "connectionStrings");
            var app = _sectionXmlParser.ParseSectionXml(appSettingsXml, "appSettings");
            return new WebConfigSections(conn, app);
        }

        public void SaveWebConfig(string webConfigPath, WebConfigSections sections) =>
            _webConfigRepository.SaveSections(webConfigPath, sections);

        public void SaveTemplate(string empresa, string templateName, WebConfigSections sections) =>
            _templateRepository.SaveTemplate(empresa, templateName, sections);

        private static string SanitizeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;

            foreach (var c in Path.GetInvalidFileNameChars())
                name = name.Replace(c, '_');

            return name.Trim();
        }
    }
}
