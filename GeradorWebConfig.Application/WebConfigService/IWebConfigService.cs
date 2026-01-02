using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using GeradorWebConfig.Domain.Models;

namespace GeradorWebConfig.Application.WebConfigService
{
    public interface IWebConfigService
    {
        IReadOnlyList<string> ListEmpresas();
        void CreateEmpresa(string empresa);
        IReadOnlyList<TemplateInfo> ListTemplates(string empresa);
        IReadOnlyList<string> ListWebConfigFiles(string baseFolder);
        WebConfigSections ReadWebConfig(string webConfigPath);
        WebConfigSections ReadTemplate(string templatePath);
        WebConfigSections BuildSections(string connectionStringsXml, string appSettingsXml);
        void SaveWebConfig(string webConfigPath, WebConfigSections sections);
        void SaveTemplate(string empresa, string templateName, WebConfigSections sections);
    }
}
