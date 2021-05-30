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
            this.chkinproceeding = new System.Windows.Forms.CheckBox();
            this.chkArticle = new System.Windows.Forms.CheckBox();
            this.chkProceeding = new System.Windows.Forms.CheckBox();
            this.chkAuthor = new System.Windows.Forms.CheckBox();
            this.chkPhdthesis = new System.Windows.Forms.CheckBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.barProgress = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(15, 97);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtDBLPfile
            // 
            this.txtDBLPfile.Location = new System.Drawing.Point(106, 6);
            this.txtDBLPfile.Name = "txtDBLPfile";
            this.txtDBLPfile.Size = new System.Drawing.Size(216, 20);
            this.txtDBLPfile.TabIndex = 4;
            this.txtDBLPfile.Text = "G:\\dblp\\2014\\dblp-2014-06-28\\dblp.xml";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "DBLP XML File";
            // 
            // chkinproceeding
            // 
            this.chkinproceeding.AutoSize = true;
            this.chkinproceeding.Checked = true;
            this.chkinproceeding.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkinproceeding.Location = new System.Drawing.Point(25, 179);
            this.chkinproceeding.Name = "chkinproceeding";
            this.chkinproceeding.Size = new System.Drawing.Size(93, 17);
            this.chkinproceeding.TabIndex = 6;
            this.chkinproceeding.Text = "Inproceedings";
            this.chkinproceeding.UseVisualStyleBackColor = true;
            // 
            // chkArticle
            // 
            this.chkArticle.AutoSize = true;
            this.chkArticle.Checked = true;
            this.chkArticle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkArticle.Location = new System.Drawing.Point(25, 156);
            this.chkArticle.Name = "chkArticle";
            this.chkArticle.Size = new System.Drawing.Size(60, 17);
            this.chkArticle.TabIndex = 6;
            this.chkArticle.Text = "Articles";
            this.chkArticle.UseVisualStyleBackColor = true;
            // 
            // chkProceeding
            // 
            this.chkProceeding.AutoSize = true;
            this.chkProceeding.Checked = true;
            this.chkProceeding.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkProceeding.Location = new System.Drawing.Point(25, 202);
            this.chkProceeding.Name = "chkProceeding";
            this.chkProceeding.Size = new System.Drawing.Size(85, 17);
            this.chkProceeding.TabIndex = 6;
            this.chkProceeding.Text = "Proceedings";
            this.chkProceeding.UseVisualStyleBackColor = true;
            // 
            // chkAuthor
            // 
            this.chkAuthor.AutoSize = true;
            this.chkAuthor.Checked = true;
            this.chkAuthor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAuthor.Location = new System.Drawing.Point(149, 179);
            this.chkAuthor.Name = "chkAuthor";
            this.chkAuthor.Size = new System.Drawing.Size(62, 17);
            this.chkAuthor.TabIndex = 6;
            this.chkAuthor.Text = "Authors";
            this.chkAuthor.UseVisualStyleBackColor = true;
            // 
            // chkPhdthesis
            // 
            this.chkPhdthesis.AutoSize = true;
            this.chkPhdthesis.Checked = true;
            this.chkPhdthesis.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPhdthesis.Location = new System.Drawing.Point(149, 156);
            this.chkPhdthesis.Name = "chkPhdthesis";
            this.chkPhdthesis.Size = new System.Drawing.Size(76, 17);
            this.chkPhdthesis.TabIndex = 6;
            this.chkPhdthesis.Text = "PhdThesis";
            this.chkPhdthesis.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(103, 102);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 7;
            // 
            // barProgress
            // 
            this.barProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.barProgress.Location = new System.Drawing.Point(3, 126);
            this.barProgress.Name = "barProgress";
            this.barProgress.Size = new System.Drawing.Size(828, 10);
            this.barProgress.TabIndex = 8;
            // 
            // FrmDBLPExtract
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 340);
            this.Controls.Add(this.barProgress);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.chkArticle);
            this.Controls.Add(this.chkPhdthesis);
            this.Controls.Add(this.chkAuthor);
            this.Controls.Add(this.chkProceeding);
            this.Controls.Add(this.chkinproceeding);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDBLPfile);
            this.Controls.Add(this.btnStart);
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
        private System.Windows.Forms.CheckBox chkinproceeding;
        private System.Windows.Forms.CheckBox chkArticle;
        private System.Windows.Forms.CheckBox chkProceeding;
        private System.Windows.Forms.CheckBox chkAuthor;
        private System.Windows.Forms.CheckBox chkPhdthesis;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar barProgress;
    }
}

