using System.Collections.Generic;

namespace GeradorWebConfig.Application.WebConfigFinder
{
    public interface IWebConfigFinder
    {
        IReadOnlyList<string> FindWebConfigFiles(string baseFolder);
    }
}
