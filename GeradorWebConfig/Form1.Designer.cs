namespace GeradorWebConfig
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnGerarWebConfig = new Button();
            groupBox2 = new GroupBox();
            grpLists = new GroupBox();
            splitContainer2 = new SplitContainer();
            lstConfigs = new ListBox();
            lstTemplates = new ListBox();
            tblHeader = new TableLayoutPanel();
            btnNovaEmpresa = new Button();
            label3 = new Label();
            txtCaminho = new TextBox();
            btnSelecionaPasta = new Button();
            label2 = new Label();
            cmbEmpresas = new ComboBox();
            panel2 = new Panel();
            openFileDialog1 = new OpenFileDialog();
            splitContainer1 = new SplitContainer();
            grpConn = new GroupBox();
            txtConnectionString = new RichTextBox();
            grpApp = new GroupBox();
            txtAppSettings = new RichTextBox();
            groupBox2.SuspendLayout();
            grpLists.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            tblHeader.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            grpConn.SuspendLayout();
            grpApp.SuspendLayout();
            SuspendLayout();
            // 
            // btnGerarWebConfig
            // 
            btnGerarWebConfig.BackColor = Color.SteelBlue;
            btnGerarWebConfig.Dock = DockStyle.Fill;
            btnGerarWebConfig.FlatAppearance.BorderSize = 0;
            btnGerarWebConfig.FlatStyle = FlatStyle.Flat;
            btnGerarWebConfig.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnGerarWebConfig.ForeColor = Color.White;
            btnGerarWebConfig.Location = new Point(10, 10);
            btnGerarWebConfig.Name = "btnGerarWebConfig";
            btnGerarWebConfig.Size = new Size(1543, 50);
            btnGerarWebConfig.TabIndex = 0;
            btnGerarWebConfig.Text = "SALVAR WEB.CONFIG";
            btnGerarWebConfig.UseVisualStyleBackColor = false;
            btnGerarWebConfig.Click += btnGerarWebConfig_Click;
            // 
            // groupBox2
            // 
            groupBox2.BackColor = Color.FromArgb(55, 55, 55);
            groupBox2.Controls.Add(grpLists);
            groupBox2.Controls.Add(tblHeader);
            groupBox2.Dock = DockStyle.Top;
            groupBox2.ForeColor = Color.WhiteSmoke;
            groupBox2.Location = new Point(0, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(10);
            groupBox2.Size = new Size(1563, 260);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Configuração";
            // 
            // grpLists
            // 
            grpLists.BackColor = Color.FromArgb(55, 55, 55);
            grpLists.Controls.Add(splitContainer2);
            grpLists.Dock = DockStyle.Fill;
            grpLists.ForeColor = Color.WhiteSmoke;
            grpLists.Location = new Point(10, 130);
            grpLists.Name = "grpLists";
            grpLists.Padding = new Padding(8);
            grpLists.Size = new Size(1543, 120);
            grpLists.TabIndex = 0;
            grpLists.TabStop = false;
            grpLists.Text = "Seleção";
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(8, 28);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(lstConfigs);
            splitContainer2.Panel1MinSize = 200;
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(lstTemplates);
            splitContainer2.Panel2MinSize = 200;
            splitContainer2.Size = new Size(1527, 84);
            splitContainer2.SplitterDistance = 834;
            splitContainer2.TabIndex = 0;
            // 
            // lstConfigs
            // 
            lstConfigs.Dock = DockStyle.Fill;
            lstConfigs.Font = new Font("Segoe UI", 11F);
            lstConfigs.IntegralHeight = false;
            lstConfigs.Location = new Point(0, 0);
            lstConfigs.Name = "lstConfigs";
            lstConfigs.Size = new Size(834, 84);
            lstConfigs.TabIndex = 0;
            // 
            // lstTemplates
            // 
            lstTemplates.Dock = DockStyle.Fill;
            lstTemplates.Font = new Font("Segoe UI", 11F);
            lstTemplates.IntegralHeight = false;
            lstTemplates.Location = new Point(0, 0);
            lstTemplates.Name = "lstTemplates";
            lstTemplates.Size = new Size(689, 84);
            lstTemplates.TabIndex = 0;
            lstTemplates.SelectedIndexChanged += lstTemplates_SelectedIndexChanged;
            // 
            // tblHeader
            // 
            tblHeader.ColumnCount = 4;
            tblHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            tblHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 214F));
            tblHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 96F));
            tblHeader.Controls.Add(btnNovaEmpresa, 2, 1);
            tblHeader.Controls.Add(label3, 0, 0);
            tblHeader.Controls.Add(txtCaminho, 1, 0);
            tblHeader.Controls.Add(btnSelecionaPasta, 2, 0);
            tblHeader.Controls.Add(label2, 0, 1);
            tblHeader.Controls.Add(cmbEmpresas, 1, 1);
            tblHeader.Dock = DockStyle.Top;
            tblHeader.Location = new Point(10, 30);
            tblHeader.Name = "tblHeader";
            tblHeader.Padding = new Padding(6);
            tblHeader.RowCount = 2;
            tblHeader.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tblHeader.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tblHeader.Size = new Size(1543, 100);
            tblHeader.TabIndex = 1;
            // 
            // btnNovaEmpresa
            // 
            btnNovaEmpresa.Dock = DockStyle.Fill;
            btnNovaEmpresa.Font = new Font("Segoe UI", 11F);
            btnNovaEmpresa.ForeColor = Color.Black;
            btnNovaEmpresa.Location = new Point(1230, 54);
            btnNovaEmpresa.Name = "btnNovaEmpresa";
            btnNovaEmpresa.Size = new Size(208, 39);
            btnNovaEmpresa.TabIndex = 7;
            btnNovaEmpresa.Text = "Nova empresa";
            btnNovaEmpresa.UseVisualStyleBackColor = true;
            btnNovaEmpresa.Click += btnNovaEmpresa_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            label3.ForeColor = Color.WhiteSmoke;
            label3.Location = new Point(9, 6);
            label3.Name = "label3";
            label3.Size = new Size(134, 45);
            label3.TabIndex = 0;
            label3.Text = "Caminho:";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtCaminho
            // 
            txtCaminho.Dock = DockStyle.Fill;
            txtCaminho.Font = new Font("Segoe UI", 11F);
            txtCaminho.Location = new Point(149, 12);
            txtCaminho.Margin = new Padding(3, 6, 3, 6);
            txtCaminho.Name = "txtCaminho";
            txtCaminho.Size = new Size(1075, 32);
            txtCaminho.TabIndex = 1;
            // 
            // btnSelecionaPasta
            // 
            btnSelecionaPasta.Dock = DockStyle.Fill;
            btnSelecionaPasta.Font = new Font("Segoe UI", 11F);
            btnSelecionaPasta.ForeColor = Color.Black;
            btnSelecionaPasta.Location = new Point(1230, 9);
            btnSelecionaPasta.Name = "btnSelecionaPasta";
            btnSelecionaPasta.Size = new Size(208, 39);
            btnSelecionaPasta.TabIndex = 2;
            btnSelecionaPasta.Text = "Selecionar";
            btnSelecionaPasta.UseVisualStyleBackColor = true;
            btnSelecionaPasta.Click += btnSelecionaPasta_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            label2.ForeColor = Color.WhiteSmoke;
            label2.Location = new Point(9, 51);
            label2.Name = "label2";
            label2.Size = new Size(134, 45);
            label2.TabIndex = 4;
            label2.Text = "Empresa:";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbEmpresas
            // 
            cmbEmpresas.Dock = DockStyle.Fill;
            cmbEmpresas.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEmpresas.Font = new Font("Segoe UI", 11F);
            cmbEmpresas.Location = new Point(149, 57);
            cmbEmpresas.Margin = new Padding(3, 6, 3, 6);
            cmbEmpresas.Name = "cmbEmpresas";
            cmbEmpresas.Size = new Size(1075, 33);
            cmbEmpresas.TabIndex = 5;
            cmbEmpresas.SelectedIndexChanged += cmbEmpresas_SelectedIndexChanged;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(45, 45, 45);
            panel2.Controls.Add(btnGerarWebConfig);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 753);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(10);
            panel2.Size = new Size(1563, 70);
            panel2.TabIndex = 1;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 260);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(grpConn);
            splitContainer1.Panel1MinSize = 300;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(grpApp);
            splitContainer1.Panel2MinSize = 300;
            splitContainer1.Size = new Size(1563, 493);
            splitContainer1.SplitterDistance = 1259;
            splitContainer1.TabIndex = 0;
            // 
            // grpConn
            // 
            grpConn.BackColor = Color.FromArgb(55, 55, 55);
            grpConn.Controls.Add(txtConnectionString);
            grpConn.Dock = DockStyle.Fill;
            grpConn.ForeColor = Color.WhiteSmoke;
            grpConn.Location = new Point(0, 0);
            grpConn.Name = "grpConn";
            grpConn.Padding = new Padding(10);
            grpConn.Size = new Size(1259, 493);
            grpConn.TabIndex = 0;
            grpConn.TabStop = false;
            grpConn.Text = "connectionStrings";
            // 
            // txtConnectionString
            // 
            txtConnectionString.BackColor = Color.FromArgb(245, 245, 245);
            txtConnectionString.BorderStyle = BorderStyle.None;
            txtConnectionString.Dock = DockStyle.Fill;
            txtConnectionString.Font = new Font("Consolas", 11F);
            txtConnectionString.Location = new Point(10, 30);
            txtConnectionString.Name = "txtConnectionString";
            txtConnectionString.Size = new Size(1239, 453);
            txtConnectionString.TabIndex = 0;
            txtConnectionString.Text = "";
            txtConnectionString.WordWrap = false;
            // 
            // grpApp
            // 
            grpApp.BackColor = Color.FromArgb(55, 55, 55);
            grpApp.Controls.Add(txtAppSettings);
            grpApp.Dock = DockStyle.Fill;
            grpApp.ForeColor = Color.WhiteSmoke;
            grpApp.Location = new Point(0, 0);
            grpApp.Name = "grpApp";
            grpApp.Padding = new Padding(10);
            grpApp.Size = new Size(300, 493);
            grpApp.TabIndex = 0;
            grpApp.TabStop = false;
            grpApp.Text = "appSettings";
            // 
            // txtAppSettings
            // 
            txtAppSettings.BackColor = Color.FromArgb(245, 245, 245);
            txtAppSettings.BorderStyle = BorderStyle.None;
            txtAppSettings.Dock = DockStyle.Fill;
            txtAppSettings.Font = new Font("Consolas", 11F);
            txtAppSettings.Location = new Point(10, 30);
            txtAppSettings.Name = "txtAppSettings";
            txtAppSettings.Size = new Size(280, 453);
            txtAppSettings.TabIndex = 0;
            txtAppSettings.Text = "";
            txtAppSettings.WordWrap = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(1563, 823);
            Controls.Add(splitContainer1);
            Controls.Add(panel2);
            Controls.Add(groupBox2);
            Name = "Form1";
            Text = "Gerador de Configuração Web.config";
            groupBox2.ResumeLayout(false);
            grpLists.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            tblHeader.ResumeLayout(false);
            tblHeader.PerformLayout();
            panel2.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            grpConn.ResumeLayout(false);
            grpApp.ResumeLayout(false);
            ResumeLayout(false);
        }



        #endregion
        private Button btnGerarWebConfig;
        private GroupBox groupBox2;
        private Panel panel2;
        private Label label3;
        private OpenFileDialog openFileDialog1;
        private TextBox txtCaminho;
        private SplitContainer splitContainer1;
        private Button btnSelecionaPasta;
        private ListBox lstConfigs;
        private ListBox lstTemplates;
        private RichTextBox txtAppSettings;
        private RichTextBox txtConnectionString;
        private SplitContainer splitContainer2;
        private Label label2;
        private ComboBox cmbEmpresas;
        private Button btnNovaEmpresa;
        private GroupBox grpConn;
        private GroupBox grpApp;
        private GroupBox grpConfigs;
        private GroupBox grpTemplates;
        private TableLayoutPanel tblHeader;
        private GroupBox grpLists;
    }
}
