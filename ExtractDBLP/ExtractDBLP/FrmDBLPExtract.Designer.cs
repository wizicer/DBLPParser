namespace ExtractDBLPForm
{
    partial class FrmDBLPExtract
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnStart = new System.Windows.Forms.Button();
            txtDBLPfile = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            lblStatus = new System.Windows.Forms.Label();
            barProgress = new System.Windows.Forms.ProgressBar();
            btnIndex = new System.Windows.Forms.Button();
            btnExportDb = new System.Windows.Forms.Button();
            btnExportPapers = new System.Windows.Forms.Button();
            cmbKeyPrefix = new System.Windows.Forms.ComboBox();
            numYear = new System.Windows.Forms.NumericUpDown();
            numVolume = new System.Windows.Forms.NumericUpDown();
            btnExportSite = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)numYear).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numVolume).BeginInit();
            SuspendLayout();
            // 
            // btnStart
            // 
            btnStart.Location = new System.Drawing.Point(18, 112);
            btnStart.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnStart.Name = "btnStart";
            btnStart.Size = new System.Drawing.Size(88, 27);
            btnStart.TabIndex = 0;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // txtDBLPfile
            // 
            txtDBLPfile.Location = new System.Drawing.Point(124, 7);
            txtDBLPfile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtDBLPfile.Name = "txtDBLPfile";
            txtDBLPfile.Size = new System.Drawing.Size(251, 23);
            txtDBLPfile.TabIndex = 4;
            txtDBLPfile.Text = "G:\\dblp\\2014\\dblp-2014-06-28\\dblp.xml";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(14, 10);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(83, 15);
            label1.TabIndex = 5;
            label1.Text = "DBLP XML File";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new System.Drawing.Point(120, 118);
            lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new System.Drawing.Size(0, 15);
            lblStatus.TabIndex = 7;
            // 
            // barProgress
            // 
            barProgress.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            barProgress.Location = new System.Drawing.Point(4, 145);
            barProgress.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            barProgress.Name = "barProgress";
            barProgress.Size = new System.Drawing.Size(966, 12);
            barProgress.TabIndex = 8;
            // 
            // btnIndex
            // 
            btnIndex.Location = new System.Drawing.Point(18, 228);
            btnIndex.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnIndex.Name = "btnIndex";
            btnIndex.Size = new System.Drawing.Size(88, 27);
            btnIndex.TabIndex = 0;
            btnIndex.Text = "Index";
            btnIndex.UseVisualStyleBackColor = true;
            btnIndex.Click += btnIndex_Click;
            // 
            // btnExportDb
            // 
            btnExportDb.Location = new System.Drawing.Point(120, 228);
            btnExportDb.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnExportDb.Name = "btnExportDb";
            btnExportDb.Size = new System.Drawing.Size(88, 27);
            btnExportDb.TabIndex = 0;
            btnExportDb.Text = "Export DB";
            btnExportDb.UseVisualStyleBackColor = true;
            btnExportDb.Click += btnExportDb_Click;
            // 
            // btnExportPapers
            // 
            btnExportPapers.Location = new System.Drawing.Point(18, 318);
            btnExportPapers.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnExportPapers.Name = "btnExportPapers";
            btnExportPapers.Size = new System.Drawing.Size(88, 27);
            btnExportPapers.TabIndex = 0;
            btnExportPapers.Text = "Export Papers";
            btnExportPapers.UseVisualStyleBackColor = true;
            btnExportPapers.Click += btnExportPapers_Click;
            // 
            // cmbKeyPrefix
            // 
            cmbKeyPrefix.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbKeyPrefix.FormattingEnabled = true;
            cmbKeyPrefix.Location = new System.Drawing.Point(120, 275);
            cmbKeyPrefix.Name = "cmbKeyPrefix";
            cmbKeyPrefix.Size = new System.Drawing.Size(175, 23);
            cmbKeyPrefix.TabIndex = 9;
            // 
            // numYear
            // 
            numYear.Location = new System.Drawing.Point(18, 275);
            numYear.Maximum = new decimal(new int[] { 2050, 0, 0, 0 });
            numYear.Minimum = new decimal(new int[] { 1900, 0, 0, 0 });
            numYear.Name = "numYear";
            numYear.Size = new System.Drawing.Size(89, 23);
            numYear.TabIndex = 10;
            numYear.Value = new decimal(new int[] { 2022, 0, 0, 0 });
            // 
            // numVolume
            // 
            numVolume.Location = new System.Drawing.Point(301, 276);
            numVolume.Name = "numVolume";
            numVolume.Size = new System.Drawing.Size(89, 23);
            numVolume.TabIndex = 10;
            // 
            // btnExportSite
            // 
            btnExportSite.Location = new System.Drawing.Point(226, 228);
            btnExportSite.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnExportSite.Name = "btnExportSite";
            btnExportSite.Size = new System.Drawing.Size(88, 27);
            btnExportSite.TabIndex = 0;
            btnExportSite.Text = "Export Site";
            btnExportSite.UseVisualStyleBackColor = true;
            btnExportSite.Click += btnExportSite_Click;
            // 
            // FrmDBLPExtract
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(973, 392);
            Controls.Add(numVolume);
            Controls.Add(numYear);
            Controls.Add(cmbKeyPrefix);
            Controls.Add(barProgress);
            Controls.Add(lblStatus);
            Controls.Add(label1);
            Controls.Add(txtDBLPfile);
            Controls.Add(btnExportPapers);
            Controls.Add(btnExportSite);
            Controls.Add(btnExportDb);
            Controls.Add(btnIndex);
            Controls.Add(btnStart);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "FrmDBLPExtract";
            Text = "DBLP Extraction";
            Load += FrmDBLPExtract_Load;
            ((System.ComponentModel.ISupportInitialize)numYear).EndInit();
            ((System.ComponentModel.ISupportInitialize)numVolume).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtDBLPfile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar barProgress;
        private System.Windows.Forms.Button btnIndex;
        private System.Windows.Forms.Button btnExportDb;
        private System.Windows.Forms.Button btnExportPapers;
        private System.Windows.Forms.ComboBox cmbKeyPrefix;
        private System.Windows.Forms.NumericUpDown numYear;
        private System.Windows.Forms.NumericUpDown numVolume;
        private System.Windows.Forms.Button btnExportSite;
    }
}

