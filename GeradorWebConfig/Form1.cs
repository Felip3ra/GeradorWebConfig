using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace GeradorWebConfig
{
    public partial class Form1 : Form
    {
        // Raiz para caminho relativo 
        private readonly string _appRoot = AppContext.BaseDirectory;

        // Pasta base selecionada (absoluta)
        private string? _baseFolderFullPath;

        // Full paths dos web.config na mesma ordem do ListBox
        private readonly List<string> _foundConfigsFullPath = new();

        // Arquivo atualmente selecionado
        private string? _selectedConfigFullPath;

        public Form1()
        {
            InitializeComponent();

            // Ajustes úteis
            txtConnectionString.ScrollBars = ScrollBars.Both;
            txtConnectionString.WordWrap = false;

            txtAppSettings.ScrollBars = ScrollBars.Both;
            txtAppSettings.WordWrap = false;

            // eventos
            lstConfigs.SelectedIndexChanged += lstConfigs_SelectedIndexChanged;
            btnGerarWebConfig.Click += btnGerarWebConfig_Click;

            // estado inicial
            btnGerarWebConfig.Enabled = false;
        }

        private void btnSelecionaPasta_Click(object sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog
            {
                Description = "Selecione a pasta base (onde existem subpastas com web.config)"
            };

            if (fbd.ShowDialog() != DialogResult.OK)
                return;

            _baseFolderFullPath = Path.GetFullPath(fbd.SelectedPath);

            // Exibe caminho relativo no txtCaminho
            txtCaminho.Text = Path.GetRelativePath(_appRoot, _baseFolderFullPath);

            BuscarWebConfigs();
        }

        private void BuscarWebConfigs()
        {
            lstConfigs.Items.Clear();
            _foundConfigsFullPath.Clear();
            _selectedConfigFullPath = null;

            txtConnectionString.Clear();
            txtAppSettings.Clear();
            btnGerarWebConfig.Enabled = false;

            if (string.IsNullOrWhiteSpace(_baseFolderFullPath) || !Directory.Exists(_baseFolderFullPath))
            {
                MessageBox.Show("Pasta base inválida.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                //verifica todos os diretorios com o web.config
                var files = Directory.EnumerateFiles(_baseFolderFullPath, "web.config", SearchOption.AllDirectories)
                                     .ToList();

                if (files.Count == 0)
                {
                    MessageBox.Show("Nenhum web.config encontrado nessa pasta.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (var full in files)
                {
                    _foundConfigsFullPath.Add(full);

                    // Mostrar relativo à pasta base
                    var rel = Path.GetRelativePath(_baseFolderFullPath, full);
                    lstConfigs.Items.Add(rel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao buscar web.config:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //ao selecionar o index
        private void lstConfigs_SelectedIndexChanged(object? sender, EventArgs e)
        {
            var idx = lstConfigs.SelectedIndex;
            if (idx < 0 || idx >= _foundConfigsFullPath.Count)
                return;

            _selectedConfigFullPath = _foundConfigsFullPath[idx];

            try
            {
                CarregarSecoesDoWebConfig(_selectedConfigFullPath);
                btnGerarWebConfig.Enabled = true;
            }
            catch (Exception ex)
            {
                btnGerarWebConfig.Enabled = false;
                MessageBox.Show($"Erro ao ler web.config:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarSecoesDoWebConfig(string filePath)
        {
            // Lê XML
            var doc = XDocument.Load(filePath, LoadOptions.PreserveWhitespace);

            // web.config pode ter namespace ou não; usar LocalName evita dor de cabeça
            var configuration = doc.Root;
            if (configuration == null || configuration.Name.LocalName != "configuration")
                throw new InvalidOperationException("Arquivo não parece ser um web.config válido (root != configuration).");

            var connectionStrings = configuration.Elements()
                .FirstOrDefault(e => e.Name.LocalName == "connectionStrings");

            var appSettings = configuration.Elements()
                .FirstOrDefault(e => e.Name.LocalName == "appSettings");

            txtConnectionString.Text = connectionStrings != null
                ? connectionStrings.ToString()
                : "<connectionStrings />";

            txtAppSettings.Text = appSettings != null
                ? appSettings.ToString()
                : "<appSettings />";
        }

        private void btnGerarWebConfig_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_selectedConfigFullPath) || !File.Exists(_selectedConfigFullPath))
            {
                MessageBox.Show("Selecione um web.config na lista.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // Backup (só cria se não existir)
                var bak = _selectedConfigFullPath + ".bak";
                if (!File.Exists(bak))
                    File.Copy(_selectedConfigFullPath, bak);

                // Carrega doc original
                var doc = XDocument.Load(_selectedConfigFullPath, LoadOptions.PreserveWhitespace);
                var configuration = doc.Root;

                if (configuration == null || configuration.Name.LocalName != "configuration")
                    throw new InvalidOperationException("Arquivo não parece ser um web.config válido.");

                // Parse do que o usuário editou nos textboxes
                var newConn = ParseSectionXml(txtConnectionString.Text, expectedRootLocalName: "connectionStrings");
                var newApp = ParseSectionXml(txtAppSettings.Text, expectedRootLocalName: "appSettings");

                // Remove seções antigas (por LocalName)
                configuration.Elements().Where(e => e.Name.LocalName == "connectionStrings").Remove();
                configuration.Elements().Where(e => e.Name.LocalName == "appSettings").Remove();

                // Insere novamente (em ordem “normal”: appSettings e connectionStrings geralmente ficam no topo)
                // Ajuste se você preferir outra ordem.
                configuration.AddFirst(newConn);
                configuration.AddFirst(newApp);

                // Salva
                doc.Save(_selectedConfigFullPath);

                MessageBox.Show("web.config atualizado com sucesso! (backup .bak criado se ainda não existia)",
                    "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static XElement ParseSectionXml(string xml, string expectedRootLocalName)
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
