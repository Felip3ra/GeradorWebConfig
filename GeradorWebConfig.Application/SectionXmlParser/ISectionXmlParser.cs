using System.Xml.Linq;

namespace GeradorWebConfig.Application.SectionXmlParser
{
    public interface ISectionXmlParser
    {
        XElement ParseSectionXml(string xml, string expectedRootLocalName);
    }
}
