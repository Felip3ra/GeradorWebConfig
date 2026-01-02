using GeradorWebConfig.Application;
using GeradorWebConfig.Application.WebConfigService;
using GeradorWebConfig.Infraestructure;
using GeradorWebConfig.Infraestructure.SectionXmlParser;
using GeradorWebConfig.Infraestructure.TemplateRepository;
using GeradorWebConfig.Infraestructure.WebConfigFinder;
using GeradorWebConfig.Infraestructure.WebConfigRepository;

namespace GeradorWebConfig
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            var templatesDir = Path.Combine(AppContext.BaseDirectory, "Templates");
            var sectionParser = new XmlSectionParser();
            var templateRepository = new FileTemplateRepository(templatesDir);
            var webConfigRepository = new FileWebConfigRepository();
            var webConfigFinder = new FileWebConfigFinder();

            IWebConfigService service = new WebConfigService(
                webConfigRepository,
                templateRepository,
                webConfigFinder,
                sectionParser);

            System.Windows.Forms.Application.Run(new Form1(service, AppContext.BaseDirectory));
        }
    }
}
