using GeradorWebConfig.Application.WebConfigService;
using GeradorWebConfig.Domain.Models;
using GeradorWebConfig.Util.FileName;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GeradorWebConfig
{
    public partial class Form1 : Form
    {
        private readonly IWebConfigService _service;

        // Raiz da aplicação
        private readonly string _appRoot;

        // Pasta base selecionada (absoluta)
        private string? _baseFolderFullPath;

        // Full paths dos web.config
        private readonly List<string> _foundConfigsFullPath = new();

        // Web.config selecionado
        private string? _selectedConfigFullPath;

        // Templates da empresa selecionada
        private readonly List<TemplateInfo> _templates = new();

        private bool _applyingTemplate;
        private readonly int _split2MinPanel1;
        private readonly int _split2MinPanel2;

        public Form1(IWebConfigService service, string appRoot)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _appRoot = appRoot ?? AppContext.BaseDirectory;

            InitializeComponent();

            _split2MinPanel1 = splitContainer2.Panel1MinSize;
            _split2MinPanel2 = splitContainer2.Panel2MinSize;

            txtConnectionString.ScrollBars = RichTextBoxScrollBars.Both;
            txtConnectionString.WordWrap = false;

            txtAppSettings.ScrollBars = RichTextBoxScrollBars.Both;
            txtAppSettings.WordWrap = false;

            lstConfigs.SelectedIndexChanged += lstConfigs_SelectedIndexChanged;
            lstTemplates.SelectedIndexChanged += lstTemplates_SelectedIndexChanged;
            cmbEmpresas.SelectedIndexChanged += cmbEmpresas_SelectedIndexChanged;

            LoadEmpresas();

            btnGerarWebConfig.Enabled = false;

            AdjustSplitContainer2();
            splitContainer2.Resize += (_, __) => AdjustSplitContainer2();
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

            if (string.IsNullOrWhiteSpace(_baseFolderFullPath))
                return;

            foreach (var file in _service.ListWebConfigFiles(_baseFolderFullPath))
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
            var sections = _service.ReadWebConfig(path);

            txtConnectionString.Text = sections.ConnectionStringsXml;
            txtAppSettings.Text = sections.AppSettingsXml;
        }
        #endregion

        #region SALVAR WEBCONFIG + TEMPLATE
        private void btnGerarWebConfig_Click(object? sender, EventArgs e)
        {
            if (_selectedConfigFullPath == null)
                return;

            var sections = _service.BuildSections(txtConnectionString.Text, txtAppSettings.Text);
            _service.SaveWebConfig(_selectedConfigFullPath, sections);

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

            _service.SaveTemplate(empresa, nomeTemplate, sections);

            LoadTemplatesByEmpresa(empresa);

            MessageBox.Show("Template salvo com sucesso!");
        }
        #endregion

        #region TEMPLATE
        private void ApplyTemplateXml(string path)
        {
            var sections = _service.ReadTemplate(path);
            txtConnectionString.Text = sections.ConnectionStringsXml;
            txtAppSettings.Text = sections.AppSettingsXml;
        }
        #endregion

        #region EMPRESAS / TEMPLATES
        private void LoadEmpresas()
        {
            cmbEmpresas.Items.Clear();
            var empresas = _service.ListEmpresas();
            cmbEmpresas.Items.AddRange(empresas.ToArray());

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
            _templates.Clear();

            foreach (var template in _service.ListTemplates(empresa))
            {
                _templates.Add(template);
                lstTemplates.Items.Add(template.Name);
            }
        }

        private void lstTemplates_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_applyingTemplate || lstTemplates.SelectedIndex < 0)
                return;

            try
            {
                _applyingTemplate = true;
                ApplyTemplateXml(_templates[lstTemplates.SelectedIndex].FullPath);
            }
            finally
            {
                _applyingTemplate = false;
            }
        }
        #endregion

        private void btnNovaEmpresa_Click(object sender, EventArgs e)
        {
            var empresa = Microsoft.VisualBasic.Interaction.InputBox(
                "Nome da nova empresa:",
                "Nova empresa",
                "");

            if (string.IsNullOrWhiteSpace(empresa))
                return;

            empresa = UtilFileName.FormatFileName(empresa);
            try
            {
                _service.CreateEmpresa(empresa);
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Essa empresa já existe ou é inválida.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Atualiza combo e seleciona automaticamente
            LoadEmpresas();
            cmbEmpresas.SelectedItem = empresa;

            MessageBox.Show(
                "Empresa criada com sucesso!",
                "OK",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void AdjustSplitContainer2()
        {
            var minTotal = _split2MinPanel1 + _split2MinPanel2 + splitContainer2.SplitterWidth;
            if (splitContainer2.Width < minTotal)
            {
                splitContainer2.Panel1MinSize = 0;
                splitContainer2.Panel2MinSize = 0;
            }
            else
            {
                splitContainer2.Panel1MinSize = _split2MinPanel1;
                splitContainer2.Panel2MinSize = _split2MinPanel2;
            }

            var min = splitContainer2.Panel1MinSize;
            var max = splitContainer2.Width - splitContainer2.Panel2MinSize - splitContainer2.SplitterWidth;
            if (max < min)
                return;

            var target = splitContainer2.Width / 2;
            if (target < min)
                target = min;
            if (target > max)
                target = max;

            splitContainer2.SplitterDistance = target;
        }

    }
}
