using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorWebConfig.Domain.Models;

namespace GeradorWebConfig.Application.TemplateRepository
{
    public interface ITemplateRepository
    {
        IReadOnlyList<string> ListEmpresas();
        void CreateEmpresa(string empresa);
        IReadOnlyList<TemplateInfo> ListTemplates(string empresa);
        void SaveTemplate(string empresa, string templateName, WebConfigSections sections);
        WebConfigSections ReadTemplate(string templatePath);
    }
}
