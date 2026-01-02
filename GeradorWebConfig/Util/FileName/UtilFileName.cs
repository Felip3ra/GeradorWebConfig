using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GeradorWebConfig.Util.FileName
{
    public class UtilFileName
    {
        public static string FormatFileName(
    string? input,
    char replacement = '_',
    int maxLength = 80)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "sem_nome";

            var invalid = Path.GetInvalidFileNameChars();

            var sb = new StringBuilder(input.Length);

            foreach (var ch in input.Trim())
            {
                if (invalid.Contains(ch) || char.IsControl(ch))
                {
                    sb.Append(replacement);
                }
                else
                {
                    sb.Append(ch);
                }
            }

            // Remove múltiplos _ seguidos
            var result = Regex.Replace(sb.ToString(), @"_+", "_");

            // Remove _ no começo e no fim
            result = result.Trim(replacement, ' ');

            // Limita tamanho
            if (result.Length > maxLength)
                result = result[..maxLength];

            return result.Length == 0 ? "sem_nome" : result;
        }
    }
}
