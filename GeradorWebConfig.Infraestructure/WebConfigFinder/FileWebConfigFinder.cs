using GeradorWebConfig.Application.WebConfigFinder;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GeradorWebConfig.Infraestructure.WebConfigFinder
{
    public sealed class FileWebConfigFinder : IWebConfigFinder
    {
        public IReadOnlyList<string> FindWebConfigFiles(string baseFolder)
        {
            if (string.IsNullOrWhiteSpace(baseFolder) || !Directory.Exists(baseFolder))
                return new List<string>();

            return Directory.EnumerateFiles(baseFolder, "web.config", SearchOption.AllDirectories)
                .ToList();
        }
    }
}
