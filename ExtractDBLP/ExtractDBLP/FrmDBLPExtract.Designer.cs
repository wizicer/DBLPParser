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
            this.btnStart = new System.Windows.Forms.Button();
            this.txtDBLPfile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.barProgress = new System.Windows.Forms.ProgressBar();
            this.btnIndex = new System.Windows.Forms.Button();
            this.btnExportDb = new System.Windows.Forms.Button();
            this.btnExportPapers = new System.Windows.Forms.Button();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.txtKeyPrefix = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(18, 112);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(88, 27);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtDBLPfile
            // 
            this.txtDBLPfile.Location = new System.Drawing.Point(124, 7);
            this.txtDBLPfile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtDBLPfile.Name = "txtDBLPfile";
            this.txtDBLPfile.Size = new System.Drawing.Size(251, 23);
            this.txtDBLPfile.TabIndex = 4;
            this.txtDBLPfile.Text = "G:\\dblp\\2014\\dblp-2014-06-28\\dblp.xml";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "DBLP XML File";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(120, 118);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 15);
            this.lblStatus.TabIndex = 7;
            // 
            // barProgress
            // 
            this.barProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.barProgress.Location = new System.Drawing.Point(4, 145);
            this.barProgress.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.barProgress.Name = "barProgress";
            this.barProgress.Size = new System.Drawing.Size(966, 12);
            this.barProgress.TabIndex = 8;
            // 
            // btnIndex
            // 
            this.btnIndex.Location = new System.Drawing.Point(18, 228);
            this.btnIndex.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnIndex.Name = "btnIndex";
            this.btnIndex.Size = new System.Drawing.Size(88, 27);
            this.btnIndex.TabIndex = 0;
            this.btnIndex.Text = "Index";
            this.btnIndex.UseVisualStyleBackColor = true;
            this.btnIndex.Click += new System.EventHandler(this.btnIndex_Click);
            // 
            // btnExportDb
            // 
            this.btnExportDb.Location = new System.Drawing.Point(120, 228);
            this.btnExportDb.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnExportDb.Name = "btnExportDb";
            this.btnExportDb.Size = new System.Drawing.Size(88, 27);
            this.btnExportDb.TabIndex = 0;
            this.btnExportDb.Text = "Export DB";
            this.btnExportDb.UseVisualStyleBackColor = true;
            this.btnExportDb.Click += new System.EventHandler(this.btnExportDb_Click);
            // 
            // btnExportPapers
            // 
            this.btnExportPapers.Location = new System.Drawing.Point(18, 318);
            this.btnExportPapers.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnExportPapers.Name = "btnExportPapers";
            this.btnExportPapers.Size = new System.Drawing.Size(88, 27);
            this.btnExportPapers.TabIndex = 0;
            this.btnExportPapers.Text = "Export Papers";
            this.btnExportPapers.UseVisualStyleBackColor = true;
            this.btnExportPapers.Click += new System.EventHandler(this.btnExportPapers_Click);
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(18, 275);
            this.txtYear.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(88, 23);
            this.txtYear.TabIndex = 4;
            this.txtYear.Text = "2022";
            // 
            // txtKeyPrefix
            // 
            this.txtKeyPrefix.Location = new System.Drawing.Point(120, 275);
            this.txtKeyPrefix.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtKeyPrefix.Name = "txtKeyPrefix";
            this.txtKeyPrefix.Size = new System.Drawing.Size(175, 23);
            this.txtKeyPrefix.TabIndex = 4;
            this.txtKeyPrefix.Text = "conf/sigmod";
            // 
            // FrmDBLPExtract
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(973, 392);
            this.Controls.Add(this.barProgress);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtKeyPrefix);
            this.Controls.Add(this.txtYear);
            this.Controls.Add(this.txtDBLPfile);
            this.Controls.Add(this.btnExportPapers);
            this.Controls.Add(this.btnExportDb);
            this.Controls.Add(this.btnIndex);
            this.Controls.Add(this.btnStart);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FrmDBLPExtract";
            this.Text = "DBLP Extraction";
            this.Load += new System.EventHandler(this.FrmDBLPExtract_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.TextBox txtKeyPrefix;
    }
}

