using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorWebConfig.Domain.Models;

namespace GeradorWebConfig.Application.WebConfigRepository
{
    public interface IWebConfigRepository
    {
        WebConfigSections ReadSections(string webConfigPath);
        void SaveSections(string webConfigPath, WebConfigSections sections);
    }
}
