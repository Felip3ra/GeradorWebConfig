using GeradorWebConfig.Util.XML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace GeradorWebConfig
{
    public partial class Form1 : Form
    {
        // Raiz da aplicação
        private readonly string _appRoot = AppContext.BaseDirectory;

        // Pasta base selecionada (absoluta)
        private string? _baseFolderFullPath;

        // Full paths dos web.config
        private readonly List<string> _foundConfigsFullPath = new();

        // Web.config selecionado
        private string? _selectedConfigFullPath;

        // Pasta Templates
        private readonly string _templatesDir =
            Path.Combine(AppContext.BaseDirectory, "Templates");

        // Templates da empresa selecionada
        private readonly List<string> _templateFilesFullPath = new();

        private bool _applyingTemplate;

        public Form1()
        {
            InitializeComponent();

            txtConnectionString.ScrollBars = RichTextBoxScrollBars.Both;
            txtConnectionString.WordWrap = false;

            txtAppSettings.ScrollBars = RichTextBoxScrollBars.Both;
            txtAppSettings.WordWrap = false;

            lstConfigs.SelectedIndexChanged += lstConfigs_SelectedIndexChanged;
            lstTemplates.SelectedIndexChanged += lstTemplates_SelectedIndexChanged;
            cmbEmpresas.SelectedIndexChanged += cmbEmpresas_SelectedIndexChanged;

            LoadEmpresas();

            btnGerarWebConfig.Enabled = false;

            splitContainer2.SplitterDistance = splitContainer2.Width / 2;
            splitContainer2.Resize += (_, __) =>
                splitContainer2.SplitterDistance = splitContainer2.Width / 2;
        }

        #region PASTA BASE
        private void btnSelecionaPasta_Click(object sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog
            {
                Description = "Selecione a pasta base (onde existem subpastas com web.config)"
            };

            if (fbd.ShowDialog() != DialogResult.OK)
                return;

            _baseFolderFullPath = Path.GetFullPath(fbd.SelectedPath);
            txtCaminho.Text = Path.GetRelativePath(_appRoot, _baseFolderFullPath);

            BuscarWebConfigs();
        }
        #endregion

        #region BUSCAR WEBCONFIG
        private void BuscarWebConfigs()
        {
            lstConfigs.Items.Clear();
            _foundConfigsFullPath.Clear();
            txtConnectionString.Clear();
            txtAppSettings.Clear();

            if (!Directory.Exists(_baseFolderFullPath))
                return;

            foreach (var file in Directory.EnumerateFiles(
                _baseFolderFullPath, "web.config", SearchOption.AllDirectories))
            {
                _foundConfigsFullPath.Add(file);
                lstConfigs.Items.Add(Path.GetRelativePath(_baseFolderFullPath, file));
            }
        }
        #endregion

        #region SELEÇÃO WEBCONFIG
        private void lstConfigs_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (lstConfigs.SelectedIndex < 0)
                return;

            _selectedConfigFullPath = _foundConfigsFullPath[lstConfigs.SelectedIndex];
            CarregarSecoesDoWebConfig(_selectedConfigFullPath);

            btnGerarWebConfig.Enabled = true;
        }

        private void CarregarSecoesDoWebConfig(string path)
        {
            var doc = XDocument.Load(path, LoadOptions.PreserveWhitespace);
            var root = doc.Root;

            txtConnectionString.Text =
                root?.Elements().FirstOrDefault(e => e.Name.LocalName == "connectionStrings")?.ToString()
                ?? "<connectionStrings />";

            txtAppSettings.Text =
                root?.Elements().FirstOrDefault(e => e.Name.LocalName == "appSettings")?.ToString()
                ?? "<appSettings />";
        }
        #endregion

        #region SALVAR WEBCONFIG + TEMPLATE
        private void btnGerarWebConfig_Click(object? sender, EventArgs e)
        {
            if (_selectedConfigFullPath == null)
                return;

            var doc = XDocument.Load(_selectedConfigFullPath, LoadOptions.PreserveWhitespace);
            var root = doc.Root ?? throw new InvalidOperationException("web.config inválido");

            var newConn = UtilXML.ParseSectionXml(txtConnectionString.Text, "connectionStrings");
            var newApp = UtilXML.ParseSectionXml(txtAppSettings.Text, "appSettings");

            root.Elements()
                .Where(e => e.Name.LocalName is "connectionStrings" or "appSettings")
                .Remove();

            var configSections = root.Elements()
                .FirstOrDefault(e => e.Name.LocalName == "configSections");

            if (configSections != null)
            {
                configSections.AddAfterSelf(newApp);
                configSections.AddAfterSelf(newConn);
            }
            else
            {
                root.AddFirst(newApp);
                root.AddFirst(newConn);
            }

            doc.Save(_selectedConfigFullPath);

            var resp = MessageBox.Show(
                "Deseja salvar essas configurações como TEMPLATE?",
                "Template",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resp != DialogResult.Yes)
                return;

            if (cmbEmpresas.SelectedItem is not string empresa)
            {
                MessageBox.Show("Selecione uma empresa.", "Atenção");
                return;
            }

            var nomeTemplate = Microsoft.VisualBasic.Interaction.InputBox(
                "Nome do template:",
                "Salvar Template",
                "Padrao");

            if (string.IsNullOrWhiteSpace(nomeTemplate))
                return;

            var empresaDir = Path.Combine(_templatesDir, empresa);
            Directory.CreateDirectory(empresaDir);

            SaveTemplateXmlByFolder(empresaDir, nomeTemplate);

            LoadTemplatesByEmpresa(empresa);

            MessageBox.Show("Template salvo com sucesso!");
        }
        #endregion

        #region TEMPLATE
        private void SaveTemplateXmlByFolder(string empresaDir, string templateName)
        {
            var conn = UtilXML.ParseSectionXml(txtConnectionString.Text, "connectionStrings");
            var app = UtilXML.ParseSectionXml(txtAppSettings.Text, "appSettings");

            var doc = new XDocument(
                new XElement("WebConfigTemplate",
                    new XAttribute("empresa", Path.GetFileName(empresaDir)),
                    new XAttribute("name", templateName),
                    new XAttribute("updatedAt", DateTime.Now.ToString("s")),
                    conn,
                    app
                )
            );

            var path = Path.Combine(empresaDir, $"{Sanitize(templateName)}.xml");
            doc.Save(path);
        }

        private void ApplyTemplateXml(string path)
        {
            var doc = XDocument.Load(path, LoadOptions.PreserveWhitespace);
            var root = doc.Root;

            txtConnectionString.Text =
                root?.Elements().FirstOrDefault(e => e.Name.LocalName == "connectionStrings")?.ToString()
                ?? "<connectionStrings />";

            txtAppSettings.Text =
                root?.Elements().FirstOrDefault(e => e.Name.LocalName == "appSettings")?.ToString()
                ?? "<appSettings />";
        }
        #endregion

        #region EMPRESAS / TEMPLATES
        private void LoadEmpresas()
        {
            Directory.CreateDirectory(_templatesDir);

            cmbEmpresas.Items.Clear();
            cmbEmpresas.Items.AddRange(
                Directory.GetDirectories(_templatesDir)
                .Select(Path.GetFileName)
                .OrderBy(x => x)
                .ToArray()
            );

            if (cmbEmpresas.Items.Count > 0)
                cmbEmpresas.SelectedIndex = 0;
        }

        private void cmbEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEmpresas.SelectedItem is string empresa)
                LoadTemplatesByEmpresa(empresa);
        }

        private void LoadTemplatesByEmpresa(string empresa)
        {
            lstTemplates.Items.Clear();
            _templateFilesFullPath.Clear();

            var dir = Path.Combine(_templatesDir, empresa);
            if (!Directory.Exists(dir))
                return;

            foreach (var file in Directory.EnumerateFiles(dir, "*.xml"))
            {
                _templateFilesFullPath.Add(file);
                lstTemplates.Items.Add(Path.GetFileNameWithoutExtension(file));
            }
        }

        private void lstTemplates_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_applyingTemplate || lstTemplates.SelectedIndex < 0)
                return;

            try
            {
                _applyingTemplate = true;
                ApplyTemplateXml(_templateFilesFullPath[lstTemplates.SelectedIndex]);
            }
            finally
            {
                _applyingTemplate = false;
            }
        }
        #endregion

        private static string Sanitize(string name)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
                name = name.Replace(c, '_');
            return name;
        }

        private void btnNovaEmpresa_Click(object sender, EventArgs e)
        {
            var empresa = Microsoft.VisualBasic.Interaction.InputBox(
                "Nome da nova empresa:",
                "Nova empresa",
                "");

            if (string.IsNullOrWhiteSpace(empresa))
                return;

            empresa = Sanitize(empresa);

            var empresaDir = Path.Combine(_templatesDir, empresa);

            if (Directory.Exists(empresaDir))
            {
                MessageBox.Show(
                    "Essa empresa já existe.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            Directory.CreateDirectory(empresaDir);

            // Atualiza combo e seleciona automaticamente
            LoadEmpresas();
            cmbEmpresas.SelectedItem = empresa;

            MessageBox.Show(
                "Empresa criada com sucesso!",
                "OK",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

    }
}
