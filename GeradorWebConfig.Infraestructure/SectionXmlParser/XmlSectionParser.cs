using GeradorWebConfig.Application.SectionXmlParser;
using System;
using System.Xml.Linq;

namespace GeradorWebConfig.Infraestructure.SectionXmlParser
{
    public sealed class XmlSectionParser : ISectionXmlParser
    {
        public XElement ParseSectionXml(string xml, string expectedRootLocalName)
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
                    $"O texto do {expectedRootLocalName} nao e um XML valido.\nDetalhe: {ex.Message}");
            }

            if (!string.Equals(el.Name.LocalName, expectedRootLocalName, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException(
                    $"Esperado <{expectedRootLocalName}> mas veio <{el.Name.LocalName}>.");

            return el;
        }
    }
}
