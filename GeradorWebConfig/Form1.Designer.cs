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
            lstConfigs = new ListBox();
            btnSelecionaPasta = new Button();
            txtCaminho = new TextBox();
            label3 = new Label();
            panel1 = new Panel();
            label1 = new Label();
            panel2 = new Panel();
            openFileDialog1 = new OpenFileDialog();
            splitContainer1 = new SplitContainer();
            txtConnectionString = new TextBox();
            txtAppSettings = new TextBox();
            groupBox2.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // btnGerarWebConfig
            // 
            btnGerarWebConfig.BackColor = Color.SteelBlue;
            btnGerarWebConfig.Dock = DockStyle.Bottom;
            btnGerarWebConfig.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGerarWebConfig.ForeColor = SystemColors.ActiveCaptionText;
            btnGerarWebConfig.Location = new Point(0, 0);
            btnGerarWebConfig.Name = "btnGerarWebConfig";
            btnGerarWebConfig.Size = new Size(1304, 88);
            btnGerarWebConfig.TabIndex = 1;
            btnGerarWebConfig.Text = "Gerar arquivo Web.config";
            btnGerarWebConfig.UseVisualStyleBackColor = false;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(lstConfigs);
            groupBox2.Controls.Add(btnSelecionaPasta);
            groupBox2.Controls.Add(txtCaminho);
            groupBox2.Controls.Add(label3);
            groupBox2.ForeColor = Color.WhiteSmoke;
            groupBox2.Location = new Point(12, 111);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(1215, 176);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Caminho Físico";
            // 
            // lstConfigs
            // 
            lstConfigs.FormattingEnabled = true;
            lstConfigs.Location = new Point(723, 40);
            lstConfigs.Name = "lstConfigs";
            lstConfigs.Size = new Size(475, 104);
            lstConfigs.TabIndex = 8;
            // 
            // btnSelecionaPasta
            // 
            btnSelecionaPasta.ForeColor = Color.Black;
            btnSelecionaPasta.Location = new Point(362, 44);
            btnSelecionaPasta.Name = "btnSelecionaPasta";
            btnSelecionaPasta.Size = new Size(153, 29);
            btnSelecionaPasta.TabIndex = 7;
            btnSelecionaPasta.Text = "Selecionar pasta";
            btnSelecionaPasta.UseVisualStyleBackColor = true;
            btnSelecionaPasta.Click += btnSelecionaPasta_Click;
            // 
            // txtCaminho
            // 
            txtCaminho.Location = new Point(172, 44);
            txtCaminho.Name = "txtCaminho";
            txtCaminho.Size = new Size(161, 27);
            txtCaminho.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.WhiteSmoke;
            label3.Location = new Point(17, 47);
            label3.Name = "label3";
            label3.Size = new Size(149, 20);
            label3.TabIndex = 5;
            label3.Text = "Caminho do Arquivo:";
            // 
            // panel1
            // 
            panel1.BackColor = Color.SteelBlue;
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1328, 94);
            panel1.TabIndex = 3;
            // 
            // label1
            // 
            label1.BackColor = Color.MidnightBlue;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.WhiteSmoke;
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(1328, 94);
            label1.TabIndex = 4;
            label1.Text = "Gerador de Configuração Web.config";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel2.BackColor = Color.Gray;
            panel2.Controls.Add(btnGerarWebConfig);
            panel2.Location = new Point(12, 293);
            panel2.Name = "panel2";
            panel2.Size = new Size(1304, 88);
            panel2.TabIndex = 5;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.Location = new Point(0, 387);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(txtConnectionString);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(txtAppSettings);
            splitContainer1.Size = new Size(1328, 362);
            splitContainer1.SplitterDistance = 632;
            splitContainer1.TabIndex = 6;
            // 
            // txtConnectionString
            // 
            txtConnectionString.Dock = DockStyle.Fill;
            txtConnectionString.Location = new Point(0, 0);
            txtConnectionString.Multiline = true;
            txtConnectionString.Name = "txtConnectionString";
            txtConnectionString.Size = new Size(632, 362);
            txtConnectionString.TabIndex = 0;
            // 
            // txtAppSettings
            // 
            txtAppSettings.Dock = DockStyle.Fill;
            txtAppSettings.Location = new Point(0, 0);
            txtAppSettings.Multiline = true;
            txtAppSettings.Name = "txtAppSettings";
            txtAppSettings.Size = new Size(692, 362);
            txtAppSettings.TabIndex = 1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(1328, 749);
            Controls.Add(splitContainer1);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(groupBox2);
            Name = "Form1";
            Text = "Form1";
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Button btnGerarWebConfig;
        private GroupBox groupBox2;
        private Panel panel1;
        private Label label1;
        private Panel panel2;
        private Label label3;
        private OpenFileDialog openFileDialog1;
        private TextBox txtCaminho;
        private SplitContainer splitContainer1;
        private TextBox txtConnectionString;
        private TextBox txtAppSettings;
        private Button btnSelecionaPasta;
        private ListBox lstConfigs;
    }
}
