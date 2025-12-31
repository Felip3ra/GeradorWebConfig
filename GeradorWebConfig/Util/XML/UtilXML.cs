using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GeradorWebConfig.Util.XML
{
    public class UtilXML
    {
        public static XElement ParseSectionXml(string xml, string expectedRootLocalName)
        {
            if (string.IsNullOrWhiteSpace(xml))
                return new XElement(expectedRootLocalName);

            XElement el;
            try
            {
                el = XElement.Parse(xml, LoadOptions.PreserveWhitespace);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"O texto do {expectedRootLocalName} não é um XML válido.\nDetalhe: {ex.Message}");
            }

            if (!string.Equals(el.Name.LocalName, expectedRootLocalName, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException(
                    $"Esperado <{expectedRootLocalName}> mas veio <{el.Name.LocalName}>.");

            return el;
        }
    }
}
