namespace ProcessData
{
    partial class MainFrm
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
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.chkSetStartValue = new System.Windows.Forms.CheckBox();
      this.chkLoadFromSave = new System.Windows.Forms.CheckBox();
      this.chkSaveAuthor = new System.Windows.Forms.CheckBox();
      this.chkSaveInproceedings = new System.Windows.Forms.CheckBox();
      this.chkSaveConference = new System.Windows.Forms.CheckBox();
      this.btnCompactStart = new System.Windows.Forms.Button();
      this.btnStartByYear = new System.Windows.Forms.Button();
      this.btnStart = new System.Windows.Forms.Button();
      this.btnBuildID = new System.Windows.Forms.Button();
      this.btnSaveData = new System.Windows.Forms.Button();
      this.btnLoadData = new System.Windows.Forms.Button();
      this.txtMsgLog = new System.Windows.Forms.TextBox();
      this.txtYearFrom = new System.Windows.Forms.TextBox();
      this.txtYearTo = new System.Windows.Forms.TextBox();
      this.txtResultPrefix = new System.Windows.Forms.TextBox();
      this.txtStartValue = new System.Windows.Forms.TextBox();
      this.txtMinDelta = new System.Windows.Forms.TextBox();
      this.txtConferencePrefix = new System.Windows.Forms.TextBox();
      this.txtPhdThesisPrefix = new System.Windows.Forms.TextBox();
      this.txtProceedingsPrefix = new System.Windows.Forms.TextBox();
      this.label13 = new System.Windows.Forms.Label();
      this.txtArticlesPrefix = new System.Windows.Forms.TextBox();
      this.label12 = new System.Windows.Forms.Label();
      this.label11 = new System.Windows.Forms.Label();
      this.txtInproceedingsPrefix = new System.Windows.Forms.TextBox();
      this.label10 = new System.Windows.Forms.Label();
      this.txtAuthorPrefix = new System.Windows.Forms.TextBox();
      this.label9 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.txtFolder = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.tabControl2 = new System.Windows.Forms.TabControl();
      this.tabPage3 = new System.Windows.Forms.TabPage();
      this.dgvAuthors = new System.Windows.Forms.DataGridView();
      this.tabPage4 = new System.Windows.Forms.TabPage();
      this.dgvInproceedings = new System.Windows.Forms.DataGridView();
      this.tabPage5 = new System.Windows.Forms.TabPage();
      this.dgvConferences = new System.Windows.Forms.DataGridView();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.tabControl2.SuspendLayout();
      this.tabPage3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvAuthors)).BeginInit();
      this.tabPage4.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvInproceedings)).BeginInit();
      this.tabPage5.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvConferences)).BeginInit();
      this.SuspendLayout();
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(0, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(601, 367);
      this.tabControl1.TabIndex = 0;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.chkSetStartValue);
      this.tabPage1.Controls.Add(this.chkLoadFromSave);
      this.tabPage1.Controls.Add(this.chkSaveAuthor);
      this.tabPage1.Controls.Add(this.chkSaveInproceedings);
      this.tabPage1.Controls.Add(this.chkSaveConference);
      this.tabPage1.Controls.Add(this.btnCompactStart);
      this.tabPage1.Controls.Add(this.btnStartByYear);
      this.tabPage1.Controls.Add(this.btnStart);
      this.tabPage1.Controls.Add(this.btnBuildID);
      this.tabPage1.Controls.Add(this.btnSaveData);
      this.tabPage1.Controls.Add(this.btnLoadData);
      this.tabPage1.Controls.Add(this.txtMsgLog);
      this.tabPage1.Controls.Add(this.txtYearFrom);
      this.tabPage1.Controls.Add(this.txtYearTo);
      this.tabPage1.Controls.Add(this.txtResultPrefix);
      this.tabPage1.Controls.Add(this.txtStartValue);
      this.tabPage1.Controls.Add(this.txtMinDelta);
      this.tabPage1.Controls.Add(this.txtConferencePrefix);
      this.tabPage1.Controls.Add(this.txtPhdThesisPrefix);
      this.tabPage1.Controls.Add(this.txtProceedingsPrefix);
      this.tabPage1.Controls.Add(this.label13);
      this.tabPage1.Controls.Add(this.txtArticlesPrefix);
      this.tabPage1.Controls.Add(this.label12);
      this.tabPage1.Controls.Add(this.label11);
      this.tabPage1.Controls.Add(this.txtInproceedingsPrefix);
      this.tabPage1.Controls.Add(this.label10);
      this.tabPage1.Controls.Add(this.txtAuthorPrefix);
      this.tabPage1.Controls.Add(this.label9);
      this.tabPage1.Controls.Add(this.label7);
      this.tabPage1.Controls.Add(this.label8);
      this.tabPage1.Controls.Add(this.label6);
      this.tabPage1.Controls.Add(this.txtFolder);
      this.tabPage1.Controls.Add(this.label5);
      this.tabPage1.Controls.Add(this.label4);
      this.tabPage1.Controls.Add(this.label3);
      this.tabPage1.Controls.Add(this.label2);
      this.tabPage1.Controls.Add(this.label1);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(593, 341);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Configuration";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // chkSetStartValue
      // 
      this.chkSetStartValue.AutoSize = true;
      this.chkSetStartValue.Location = new System.Drawing.Point(312, 92);
      this.chkSetStartValue.Name = "chkSetStartValue";
      this.chkSetStartValue.Size = new System.Drawing.Size(97, 17);
      this.chkSetStartValue.TabIndex = 3;
      this.chkSetStartValue.Text = "Set Start Value";
      this.chkSetStartValue.UseVisualStyleBackColor = true;
      // 
      // chkLoadFromSave
      // 
      this.chkLoadFromSave.AutoSize = true;
      this.chkLoadFromSave.Location = new System.Drawing.Point(312, 44);
      this.chkLoadFromSave.Name = "chkLoadFromSave";
      this.chkLoadFromSave.Size = new System.Drawing.Size(110, 17);
      this.chkLoadFromSave.TabIndex = 3;
      this.chkLoadFromSave.Text = "Load From Saved";
      this.chkLoadFromSave.UseVisualStyleBackColor = true;
      // 
      // chkSaveAuthor
      // 
      this.chkSaveAuthor.AutoSize = true;
      this.chkSaveAuthor.Location = new System.Drawing.Point(437, 44);
      this.chkSaveAuthor.Name = "chkSaveAuthor";
      this.chkSaveAuthor.Size = new System.Drawing.Size(111, 17);
      this.chkSaveAuthor.TabIndex = 3;
      this.chkSaveAuthor.Text = "Save Author Data";
      this.chkSaveAuthor.UseVisualStyleBackColor = true;
      // 
      // chkSaveInproceedings
      // 
      this.chkSaveInproceedings.AutoSize = true;
      this.chkSaveInproceedings.Location = new System.Drawing.Point(313, 67);
      this.chkSaveInproceedings.Name = "chkSaveInproceedings";
      this.chkSaveInproceedings.Size = new System.Drawing.Size(121, 17);
      this.chkSaveInproceedings.TabIndex = 3;
      this.chkSaveInproceedings.Text = "Save Inproceedings";
      this.chkSaveInproceedings.UseVisualStyleBackColor = true;
      // 
      // chkSaveConference
      // 
      this.chkSaveConference.AutoSize = true;
      this.chkSaveConference.Location = new System.Drawing.Point(436, 67);
      this.chkSaveConference.Name = "chkSaveConference";
      this.chkSaveConference.Size = new System.Drawing.Size(135, 17);
      this.chkSaveConference.TabIndex = 3;
      this.chkSaveConference.Text = "Save Conference Data";
      this.chkSaveConference.UseVisualStyleBackColor = true;
      // 
      // btnCompactStart
      // 
      this.btnCompactStart.Location = new System.Drawing.Point(455, 220);
      this.btnCompactStart.Name = "btnCompactStart";
      this.btnCompactStart.Size = new System.Drawing.Size(116, 23);
      this.btnCompactStart.TabIndex = 2;
      this.btnCompactStart.Text = "Compact By Year";
      this.btnCompactStart.UseVisualStyleBackColor = true;
      this.btnCompactStart.Click += new System.EventHandler(this.btnCompactStart_Click);
      // 
      // btnStartByYear
      // 
      this.btnStartByYear.Location = new System.Drawing.Point(455, 196);
      this.btnStartByYear.Name = "btnStartByYear";
      this.btnStartByYear.Size = new System.Drawing.Size(116, 23);
      this.btnStartByYear.TabIndex = 2;
      this.btnStartByYear.Text = "Start By Year";
      this.btnStartByYear.UseVisualStyleBackColor = true;
      this.btnStartByYear.Click += new System.EventHandler(this.btnStartByYear_Click);
      // 
      // btnStart
      // 
      this.btnStart.Location = new System.Drawing.Point(455, 172);
      this.btnStart.Name = "btnStart";
      this.btnStart.Size = new System.Drawing.Size(75, 23);
      this.btnStart.TabIndex = 2;
      this.btnStart.Text = "Start";
      this.btnStart.UseVisualStyleBackColor = true;
      this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
      // 
      // btnBuildID
      // 
      this.btnBuildID.Location = new System.Drawing.Point(496, 9);
      this.btnBuildID.Name = "btnBuildID";
      this.btnBuildID.Size = new System.Drawing.Size(75, 23);
      this.btnBuildID.TabIndex = 2;
      this.btnBuildID.Text = "Build Full ID";
      this.btnBuildID.UseVisualStyleBackColor = true;
      this.btnBuildID.Click += new System.EventHandler(this.btnBuildID_Click);
      // 
      // btnSaveData
      // 
      this.btnSaveData.Location = new System.Drawing.Point(496, 115);
      this.btnSaveData.Name = "btnSaveData";
      this.btnSaveData.Size = new System.Drawing.Size(75, 23);
      this.btnSaveData.TabIndex = 2;
      this.btnSaveData.Text = "Save Data";
      this.btnSaveData.UseVisualStyleBackColor = true;
      this.btnSaveData.Click += new System.EventHandler(this.btnSaveData_Click);
      // 
      // btnLoadData
      // 
      this.btnLoadData.Location = new System.Drawing.Point(405, 9);
      this.btnLoadData.Name = "btnLoadData";
      this.btnLoadData.Size = new System.Drawing.Size(75, 23);
      this.btnLoadData.TabIndex = 2;
      this.btnLoadData.Text = "Load data";
      this.btnLoadData.UseVisualStyleBackColor = true;
      this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
      // 
      // txtMsgLog
      // 
      this.txtMsgLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtMsgLog.Location = new System.Drawing.Point(8, 253);
      this.txtMsgLog.Multiline = true;
      this.txtMsgLog.Name = "txtMsgLog";
      this.txtMsgLog.ReadOnly = true;
      this.txtMsgLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtMsgLog.Size = new System.Drawing.Size(577, 80);
      this.txtMsgLog.TabIndex = 1;
      // 
      // txtYearFrom
      // 
      this.txtYearFrom.Location = new System.Drawing.Point(232, 199);
      this.txtYearFrom.Name = "txtYearFrom";
      this.txtYearFrom.Size = new System.Drawing.Size(55, 20);
      this.txtYearFrom.TabIndex = 1;
      this.txtYearFrom.Text = "1920";
      // 
      // txtYearTo
      // 
      this.txtYearTo.Location = new System.Drawing.Point(232, 223);
      this.txtYearTo.Name = "txtYearTo";
      this.txtYearTo.Size = new System.Drawing.Size(55, 20);
      this.txtYearTo.TabIndex = 1;
      this.txtYearTo.Text = "2020";
      // 
      // txtResultPrefix
      // 
      this.txtResultPrefix.Location = new System.Drawing.Point(368, 174);
      this.txtResultPrefix.Name = "txtResultPrefix";
      this.txtResultPrefix.Size = new System.Drawing.Size(55, 20);
      this.txtResultPrefix.TabIndex = 1;
      this.txtResultPrefix.Text = "result";
      // 
      // txtStartValue
      // 
      this.txtStartValue.Location = new System.Drawing.Point(496, 90);
      this.txtStartValue.Name = "txtStartValue";
      this.txtStartValue.Size = new System.Drawing.Size(66, 20);
      this.txtStartValue.TabIndex = 1;
      this.txtStartValue.Text = "1000";
      // 
      // txtMinDelta
      // 
      this.txtMinDelta.Location = new System.Drawing.Point(368, 224);
      this.txtMinDelta.Name = "txtMinDelta";
      this.txtMinDelta.Size = new System.Drawing.Size(66, 20);
      this.txtMinDelta.TabIndex = 1;
      this.txtMinDelta.Text = "0.00001";
      // 
      // txtConferencePrefix
      // 
      this.txtConferencePrefix.Location = new System.Drawing.Point(122, 165);
      this.txtConferencePrefix.Name = "txtConferencePrefix";
      this.txtConferencePrefix.Size = new System.Drawing.Size(138, 20);
      this.txtConferencePrefix.TabIndex = 1;
      this.txtConferencePrefix.Text = "dblp-conferences-";
      // 
      // txtPhdThesisPrefix
      // 
      this.txtPhdThesisPrefix.Location = new System.Drawing.Point(122, 138);
      this.txtPhdThesisPrefix.Name = "txtPhdThesisPrefix";
      this.txtPhdThesisPrefix.Size = new System.Drawing.Size(138, 20);
      this.txtPhdThesisPrefix.TabIndex = 1;
      this.txtPhdThesisPrefix.Text = "dblp.xml-phdthesis-";
      // 
      // txtProceedingsPrefix
      // 
      this.txtProceedingsPrefix.Location = new System.Drawing.Point(122, 112);
      this.txtProceedingsPrefix.Name = "txtProceedingsPrefix";
      this.txtProceedingsPrefix.Size = new System.Drawing.Size(138, 20);
      this.txtProceedingsPrefix.TabIndex = 1;
      this.txtProceedingsPrefix.Text = "dblp.xml-proceedings-";
      // 
      // label13
      // 
      this.label13.AutoSize = true;
      this.label13.Location = new System.Drawing.Point(165, 202);
      this.label13.Name = "label13";
      this.label13.Size = new System.Drawing.Size(53, 13);
      this.label13.TabIndex = 0;
      this.label13.Text = "From year";
      // 
      // txtArticlesPrefix
      // 
      this.txtArticlesPrefix.Location = new System.Drawing.Point(122, 90);
      this.txtArticlesPrefix.Name = "txtArticlesPrefix";
      this.txtArticlesPrefix.Size = new System.Drawing.Size(138, 20);
      this.txtArticlesPrefix.TabIndex = 1;
      this.txtArticlesPrefix.Text = "dblp.xml-articles-";
      // 
      // label12
      // 
      this.label12.AutoSize = true;
      this.label12.Location = new System.Drawing.Point(165, 226);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(45, 13);
      this.label12.TabIndex = 0;
      this.label12.Text = "To Year";
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.Location = new System.Drawing.Point(293, 177);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(66, 13);
      this.label11.TabIndex = 0;
      this.label11.Text = "Result Prefix";
      // 
      // txtInproceedingsPrefix
      // 
      this.txtInproceedingsPrefix.Location = new System.Drawing.Point(122, 67);
      this.txtInproceedingsPrefix.Name = "txtInproceedingsPrefix";
      this.txtInproceedingsPrefix.Size = new System.Drawing.Size(138, 20);
      this.txtInproceedingsPrefix.TabIndex = 1;
      this.txtInproceedingsPrefix.Text = "dblp.xml-inproceedings-";
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(431, 93);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(59, 13);
      this.label10.TabIndex = 0;
      this.label10.Text = "Start Value";
      // 
      // txtAuthorPrefix
      // 
      this.txtAuthorPrefix.Location = new System.Drawing.Point(122, 41);
      this.txtAuthorPrefix.Name = "txtAuthorPrefix";
      this.txtAuthorPrefix.Size = new System.Drawing.Size(138, 20);
      this.txtAuthorPrefix.TabIndex = 1;
      this.txtAuthorPrefix.Text = "dblp.xml-www-";
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(293, 227);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(69, 13);
      this.label9.TabIndex = 0;
      this.label9.Text = "Delta to Stop";
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(12, 237);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(76, 13);
      this.label7.TabIndex = 0;
      this.label7.Text = "Message Logs";
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(18, 168);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(91, 13);
      this.label8.TabIndex = 0;
      this.label8.Text = "Conference Prefix";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(18, 141);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(88, 13);
      this.label6.TabIndex = 0;
      this.label6.Text = "PhDThesis Prefix";
      // 
      // txtFolder
      // 
      this.txtFolder.Location = new System.Drawing.Point(122, 16);
      this.txtFolder.Name = "txtFolder";
      this.txtFolder.Size = new System.Drawing.Size(138, 20);
      this.txtFolder.TabIndex = 1;
      this.txtFolder.Text = "G:\\dblp\\2014\\dblp-2014-06-28";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(18, 115);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(95, 13);
      this.label5.TabIndex = 0;
      this.label5.Text = "Proceedings Prefix";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(18, 93);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(70, 13);
      this.label4.TabIndex = 0;
      this.label4.Text = "Articles Prefix";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(18, 70);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(103, 13);
      this.label3.TabIndex = 0;
      this.label3.Text = "Inproceedings Prefix";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(18, 44);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(72, 13);
      this.label2.TabIndex = 0;
      this.label2.Text = "Authors Prefix";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(18, 19);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(36, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Folder";
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.tabControl2);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(593, 341);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Data";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // tabControl2
      // 
      this.tabControl2.Controls.Add(this.tabPage3);
      this.tabControl2.Controls.Add(this.tabPage4);
      this.tabControl2.Controls.Add(this.tabPage5);
      this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl2.Location = new System.Drawing.Point(3, 3);
      this.tabControl2.Name = "tabControl2";
      this.tabControl2.SelectedIndex = 0;
      this.tabControl2.Size = new System.Drawing.Size(587, 335);
      this.tabControl2.TabIndex = 0;
      // 
      // tabPage3
      // 
      this.tabPage3.Controls.Add(this.dgvAuthors);
      this.tabPage3.Location = new System.Drawing.Point(4, 22);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage3.Size = new System.Drawing.Size(579, 309);
      this.tabPage3.TabIndex = 0;
      this.tabPage3.Text = "Authors";
      this.tabPage3.UseVisualStyleBackColor = true;
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
      this.dgvAuthors.Size = new System.Drawing.Size(573, 303);
      this.dgvAuthors.TabIndex = 0;
      // 
      // tabPage4
      // 
      this.tabPage4.Controls.Add(this.dgvInproceedings);
      this.tabPage4.Location = new System.Drawing.Point(4, 22);
      this.tabPage4.Name = "tabPage4";
      this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage4.Size = new System.Drawing.Size(579, 309);
      this.tabPage4.TabIndex = 1;
      this.tabPage4.Text = "Inproceedings";
      this.tabPage4.UseVisualStyleBackColor = true;
      // 
      // dgvInproceedings
      // 
      this.dgvInproceedings.AllowUserToAddRows = false;
      this.dgvInproceedings.AllowUserToDeleteRows = false;
      this.dgvInproceedings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvInproceedings.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgvInproceedings.Location = new System.Drawing.Point(3, 3);
      this.dgvInproceedings.Name = "dgvInproceedings";
      this.dgvInproceedings.ReadOnly = true;
      this.dgvInproceedings.Size = new System.Drawing.Size(573, 303);
      this.dgvInproceedings.TabIndex = 0;
      // 
      // tabPage5
      // 
      this.tabPage5.Controls.Add(this.dgvConferences);
      this.tabPage5.Location = new System.Drawing.Point(4, 22);
      this.tabPage5.Name = "tabPage5";
      this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage5.Size = new System.Drawing.Size(579, 309);
      this.tabPage5.TabIndex = 2;
      this.tabPage5.Text = "Conferences";
      this.tabPage5.UseVisualStyleBackColor = true;
      // 
      // dgvConferences
      // 
      this.dgvConferences.AllowUserToAddRows = false;
      this.dgvConferences.AllowUserToDeleteRows = false;
      this.dgvConferences.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvConferences.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgvConferences.Location = new System.Drawing.Point(3, 3);
      this.dgvConferences.Name = "dgvConferences";
      this.dgvConferences.ReadOnly = true;
      this.dgvConferences.Size = new System.Drawing.Size(573, 303);
      this.dgvConferences.TabIndex = 0;
      // 
      // MainFrm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(601, 367);
      this.Controls.Add(this.tabControl1);
      this.Name = "MainFrm";
      this.Text = "Process Data";
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      this.tabPage2.ResumeLayout(false);
      this.tabControl2.ResumeLayout(false);
      this.tabPage3.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgvAuthors)).EndInit();
      this.tabPage4.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgvInproceedings)).EndInit();
      this.tabPage5.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgvConferences)).EndInit();
      this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.TextBox txtPhdThesisPrefix;
        private System.Windows.Forms.TextBox txtProceedingsPrefix;
        private System.Windows.Forms.TextBox txtArticlesPrefix;
        private System.Windows.Forms.TextBox txtInproceedingsPrefix;
        private System.Windows.Forms.TextBox txtAuthorPrefix;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dgvAuthors;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView dgvInproceedings;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.DataGridView dgvConferences;
        private System.Windows.Forms.TextBox txtMsgLog;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtConferencePrefix;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkSaveConference;
        private System.Windows.Forms.Button btnBuildID;
        private System.Windows.Forms.CheckBox chkSaveAuthor;
        private System.Windows.Forms.CheckBox chkLoadFromSave;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtMinDelta;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnSaveData;
        private System.Windows.Forms.TextBox txtStartValue;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtResultPrefix;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chkSaveInproceedings;
        private System.Windows.Forms.Button btnStartByYear;
        private System.Windows.Forms.TextBox txtYearTo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtYearFrom;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox chkSetStartValue;
        private System.Windows.Forms.Button btnCompactStart;
    }
}

