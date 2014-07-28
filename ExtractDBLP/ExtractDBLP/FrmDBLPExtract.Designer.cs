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
      this.btnConnect = new System.Windows.Forms.Button();
      this.btnLoadData = new System.Windows.Forms.Button();
      this.btnRun = new System.Windows.Forms.Button();
      this.btnDelete = new System.Windows.Forms.Button();
      this.txtOutput = new System.Windows.Forms.TextBox();
      this.txtDBLPfile = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.chkinproceeding = new System.Windows.Forms.CheckBox();
      this.chkArticle = new System.Windows.Forms.CheckBox();
      this.chkProceeding = new System.Windows.Forms.CheckBox();
      this.chkAuthor = new System.Windows.Forms.CheckBox();
      this.btnUpdateAuthors = new System.Windows.Forms.Button();
      this.txtStartValue = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.txtMinstop = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.dgvAuthors = new System.Windows.Forms.DataGridView();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage4 = new System.Windows.Forms.TabPage();
      this.txtMsg = new System.Windows.Forms.TextBox();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.dgvInproceedings = new System.Windows.Forms.DataGridView();
      this.tabPage3 = new System.Windows.Forms.TabPage();
      this.dgvConferences = new System.Windows.Forms.DataGridView();
      this.chkPhdthesis = new System.Windows.Forms.CheckBox();
      this.btnNew = new System.Windows.Forms.Button();
      this.txtAuthorIDStart = new System.Windows.Forms.TextBox();
      this.txtAuthorIDEnd = new System.Windows.Forms.TextBox();
      this.chkUpdateAll = new System.Windows.Forms.CheckBox();
      ((System.ComponentModel.ISupportInitialize)(this.dgvAuthors)).BeginInit();
      this.tabControl1.SuspendLayout();
      this.tabPage4.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvInproceedings)).BeginInit();
      this.tabPage3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvConferences)).BeginInit();
      this.SuspendLayout();
      // 
      // btnStart
      // 
      this.btnStart.Location = new System.Drawing.Point(48, 62);
      this.btnStart.Name = "btnStart";
      this.btnStart.Size = new System.Drawing.Size(75, 23);
      this.btnStart.TabIndex = 0;
      this.btnStart.Text = "Start";
      this.btnStart.UseVisualStyleBackColor = true;
      this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
      // 
      // btnConnect
      // 
      this.btnConnect.Location = new System.Drawing.Point(48, 33);
      this.btnConnect.Name = "btnConnect";
      this.btnConnect.Size = new System.Drawing.Size(75, 23);
      this.btnConnect.TabIndex = 0;
      this.btnConnect.Text = "Connect DB";
      this.btnConnect.UseVisualStyleBackColor = true;
      this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
      // 
      // btnLoadData
      // 
      this.btnLoadData.Location = new System.Drawing.Point(189, 33);
      this.btnLoadData.Name = "btnLoadData";
      this.btnLoadData.Size = new System.Drawing.Size(75, 23);
      this.btnLoadData.TabIndex = 1;
      this.btnLoadData.Text = "Load Data";
      this.btnLoadData.UseVisualStyleBackColor = true;
      this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
      // 
      // btnRun
      // 
      this.btnRun.Location = new System.Drawing.Point(189, 62);
      this.btnRun.Name = "btnRun";
      this.btnRun.Size = new System.Drawing.Size(75, 23);
      this.btnRun.TabIndex = 1;
      this.btnRun.Text = "Run";
      this.btnRun.UseVisualStyleBackColor = true;
      this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
      // 
      // btnDelete
      // 
      this.btnDelete.Location = new System.Drawing.Point(189, 118);
      this.btnDelete.Name = "btnDelete";
      this.btnDelete.Size = new System.Drawing.Size(75, 23);
      this.btnDelete.TabIndex = 1;
      this.btnDelete.Text = "Delete";
      this.btnDelete.UseVisualStyleBackColor = true;
      // 
      // txtOutput
      // 
      this.txtOutput.Location = new System.Drawing.Point(48, 93);
      this.txtOutput.Name = "txtOutput";
      this.txtOutput.Size = new System.Drawing.Size(200, 20);
      this.txtOutput.TabIndex = 3;
      this.txtOutput.Text = "G:\\dblp\\2014\\dblp-2014-06-28";
      // 
      // txtDBLPfile
      // 
      this.txtDBLPfile.Location = new System.Drawing.Point(48, 147);
      this.txtDBLPfile.Name = "txtDBLPfile";
      this.txtDBLPfile.Size = new System.Drawing.Size(216, 20);
      this.txtDBLPfile.TabIndex = 4;
      this.txtDBLPfile.Text = "G:\\dblp\\2014\\dblp-2014-06-28\\dblp.xml";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(48, 128);
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
      this.chkinproceeding.Location = new System.Drawing.Point(48, 211);
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
      this.chkArticle.Location = new System.Drawing.Point(48, 188);
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
      this.chkProceeding.Location = new System.Drawing.Point(48, 234);
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
      this.chkAuthor.Location = new System.Drawing.Point(172, 211);
      this.chkAuthor.Name = "chkAuthor";
      this.chkAuthor.Size = new System.Drawing.Size(62, 17);
      this.chkAuthor.TabIndex = 6;
      this.chkAuthor.Text = "Authors";
      this.chkAuthor.UseVisualStyleBackColor = true;
      // 
      // btnUpdateAuthors
      // 
      this.btnUpdateAuthors.Location = new System.Drawing.Point(48, 294);
      this.btnUpdateAuthors.Name = "btnUpdateAuthors";
      this.btnUpdateAuthors.Size = new System.Drawing.Size(186, 23);
      this.btnUpdateAuthors.TabIndex = 1;
      this.btnUpdateAuthors.Text = "Update Authors Infor";
      this.btnUpdateAuthors.UseVisualStyleBackColor = true;
      // 
      // txtStartValue
      // 
      this.txtStartValue.Location = new System.Drawing.Point(368, 62);
      this.txtStartValue.Name = "txtStartValue";
      this.txtStartValue.Size = new System.Drawing.Size(100, 20);
      this.txtStartValue.TabIndex = 7;
      this.txtStartValue.Text = "1000";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(306, 65);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(58, 13);
      this.label2.TabIndex = 8;
      this.label2.Text = "Start value";
      // 
      // txtMinstop
      // 
      this.txtMinstop.Location = new System.Drawing.Point(542, 63);
      this.txtMinstop.Name = "txtMinstop";
      this.txtMinstop.Size = new System.Drawing.Size(100, 20);
      this.txtMinstop.TabIndex = 7;
      this.txtMinstop.Text = "0.0001";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(482, 67);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(34, 13);
      this.label3.TabIndex = 8;
      this.label3.Text = "Steps";
      // 
      // dgvAuthors
      // 
      this.dgvAuthors.AllowUserToAddRows = false;
      this.dgvAuthors.AllowUserToDeleteRows = false;
      this.dgvAuthors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvAuthors.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgvAuthors.Location = new System.Drawing.Point(3, 3);
      this.dgvAuthors.Name = "dgvAuthors";
      this.dgvAuthors.ReadOnly = true;
      this.dgvAuthors.Size = new System.Drawing.Size(525, 203);
      this.dgvAuthors.TabIndex = 9;
      // 
      // tabControl1
      // 
      this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tabControl1.Controls.Add(this.tabPage4);
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Controls.Add(this.tabPage3);
      this.tabControl1.Location = new System.Drawing.Point(283, 93);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(539, 235);
      this.tabControl1.TabIndex = 10;
      // 
      // tabPage4
      // 
      this.tabPage4.Controls.Add(this.txtMsg);
      this.tabPage4.Location = new System.Drawing.Point(4, 22);
      this.tabPage4.Name = "tabPage4";
      this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage4.Size = new System.Drawing.Size(531, 209);
      this.tabPage4.TabIndex = 3;
      this.tabPage4.Text = "Status";
      this.tabPage4.UseVisualStyleBackColor = true;
      // 
      // txtMsg
      // 
      this.txtMsg.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtMsg.Location = new System.Drawing.Point(3, 3);
      this.txtMsg.Multiline = true;
      this.txtMsg.Name = "txtMsg";
      this.txtMsg.Size = new System.Drawing.Size(525, 203);
      this.txtMsg.TabIndex = 0;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.dgvAuthors);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(531, 209);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Authors";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.dgvInproceedings);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(531, 209);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Inproceedings";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // dgvInproceedings
      // 
      this.dgvInproceedings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvInproceedings.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgvInproceedings.Location = new System.Drawing.Point(3, 3);
      this.dgvInproceedings.Name = "dgvInproceedings";
      this.dgvInproceedings.Size = new System.Drawing.Size(525, 203);
      this.dgvInproceedings.TabIndex = 0;
      // 
      // tabPage3
      // 
      this.tabPage3.Controls.Add(this.dgvConferences);
      this.tabPage3.Location = new System.Drawing.Point(4, 22);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage3.Size = new System.Drawing.Size(531, 209);
      this.tabPage3.TabIndex = 2;
      this.tabPage3.Text = "Conference";
      this.tabPage3.UseVisualStyleBackColor = true;
      // 
      // dgvConferences
      // 
      this.dgvConferences.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvConferences.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgvConferences.Location = new System.Drawing.Point(3, 3);
      this.dgvConferences.Name = "dgvConferences";
      this.dgvConferences.Size = new System.Drawing.Size(525, 203);
      this.dgvConferences.TabIndex = 0;
      // 
      // chkPhdthesis
      // 
      this.chkPhdthesis.AutoSize = true;
      this.chkPhdthesis.Checked = true;
      this.chkPhdthesis.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkPhdthesis.Location = new System.Drawing.Point(172, 188);
      this.chkPhdthesis.Name = "chkPhdthesis";
      this.chkPhdthesis.Size = new System.Drawing.Size(76, 17);
      this.chkPhdthesis.TabIndex = 6;
      this.chkPhdthesis.Text = "PhdThesis";
      this.chkPhdthesis.UseVisualStyleBackColor = true;
      // 
      // btnNew
      // 
      this.btnNew.Location = new System.Drawing.Point(309, 12);
      this.btnNew.Name = "btnNew";
      this.btnNew.Size = new System.Drawing.Size(92, 23);
      this.btnNew.TabIndex = 1;
      this.btnNew.Text = "Update Author";
      this.btnNew.UseVisualStyleBackColor = true;
      this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
      // 
      // txtAuthorIDStart
      // 
      this.txtAuthorIDStart.Location = new System.Drawing.Point(407, 15);
      this.txtAuthorIDStart.Name = "txtAuthorIDStart";
      this.txtAuthorIDStart.Size = new System.Drawing.Size(100, 20);
      this.txtAuthorIDStart.TabIndex = 7;
      this.txtAuthorIDStart.Text = "500000";
      // 
      // txtAuthorIDEnd
      // 
      this.txtAuthorIDEnd.Location = new System.Drawing.Point(542, 15);
      this.txtAuthorIDEnd.Name = "txtAuthorIDEnd";
      this.txtAuthorIDEnd.Size = new System.Drawing.Size(100, 20);
      this.txtAuthorIDEnd.TabIndex = 7;
      this.txtAuthorIDEnd.Text = "2000000";
      // 
      // chkUpdateAll
      // 
      this.chkUpdateAll.AutoSize = true;
      this.chkUpdateAll.Checked = true;
      this.chkUpdateAll.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkUpdateAll.Location = new System.Drawing.Point(664, 17);
      this.chkUpdateAll.Name = "chkUpdateAll";
      this.chkUpdateAll.Size = new System.Drawing.Size(75, 17);
      this.chkUpdateAll.TabIndex = 11;
      this.chkUpdateAll.Text = "Update All";
      this.chkUpdateAll.UseVisualStyleBackColor = true;
      // 
      // FrmDBLPExtract
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(834, 340);
      this.Controls.Add(this.chkUpdateAll);
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.txtMinstop);
      this.Controls.Add(this.txtAuthorIDEnd);
      this.Controls.Add(this.txtAuthorIDStart);
      this.Controls.Add(this.txtStartValue);
      this.Controls.Add(this.chkArticle);
      this.Controls.Add(this.chkPhdthesis);
      this.Controls.Add(this.chkAuthor);
      this.Controls.Add(this.chkProceeding);
      this.Controls.Add(this.chkinproceeding);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtDBLPfile);
      this.Controls.Add(this.txtOutput);
      this.Controls.Add(this.btnDelete);
      this.Controls.Add(this.btnUpdateAuthors);
      this.Controls.Add(this.btnRun);
      this.Controls.Add(this.btnNew);
      this.Controls.Add(this.btnLoadData);
      this.Controls.Add(this.btnConnect);
      this.Controls.Add(this.btnStart);
      this.Name = "FrmDBLPExtract";
      this.Text = "DBLP Extraction";
      this.Load += new System.EventHandler(this.FrmDBLPExtract_Load);
      ((System.ComponentModel.ISupportInitialize)(this.dgvAuthors)).EndInit();
      this.tabControl1.ResumeLayout(false);
      this.tabPage4.ResumeLayout(false);
      this.tabPage4.PerformLayout();
      this.tabPage1.ResumeLayout(false);
      this.tabPage2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgvInproceedings)).EndInit();
      this.tabPage3.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgvConferences)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnConnect;

        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.TextBox txtDBLPfile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkinproceeding;
        private System.Windows.Forms.CheckBox chkArticle;
        private System.Windows.Forms.CheckBox chkProceeding;
        private System.Windows.Forms.CheckBox chkAuthor;
        private System.Windows.Forms.Button btnUpdateAuthors;
        private System.Windows.Forms.TextBox txtStartValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMinstop;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvAuthors;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvInproceedings;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dgvConferences;
        private System.Windows.Forms.CheckBox chkPhdthesis;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.TextBox txtAuthorIDStart;
        private System.Windows.Forms.TextBox txtAuthorIDEnd;
        private System.Windows.Forms.CheckBox chkUpdateAll;
    }
}

