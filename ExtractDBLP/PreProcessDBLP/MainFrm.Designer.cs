namespace PreProcessDBLP
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
            this.chkSaveConference = new System.Windows.Forms.CheckBox();
            this.chkSaveArticles = new System.Windows.Forms.CheckBox();
            this.chkSaveInproceedings = new System.Windows.Forms.CheckBox();
            this.chkSaveAuthors = new System.Windows.Forms.CheckBox();
            this.chkLoadBeforeYear = new System.Windows.Forms.CheckBox();
            this.chkSetStartValue = new System.Windows.Forms.CheckBox();
            this.txtConferenceRatio = new System.Windows.Forms.TextBox();
            this.txtStartRatio = new System.Windows.Forms.TextBox();
            this.txtMinCountInproceedings = new System.Windows.Forms.TextBox();
            this.txtTotalValue = new System.Windows.Forms.TextBox();
            this.txtMinValue = new System.Windows.Forms.TextBox();
            this.txtBeforeYear = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtStartValue = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtMsgLog = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnMSCalculator = new System.Windows.Forms.Button();
            this.btnCalculator = new System.Windows.Forms.Button();
            this.btnTestDBLP = new System.Windows.Forms.Button();
            this.btnLoadData = new System.Windows.Forms.Button();
            this.txtResultPrefix = new System.Windows.Forms.TextBox();
            this.txtConferencePrefix = new System.Windows.Forms.TextBox();
            this.txtPhdThesisPrefix = new System.Windows.Forms.TextBox();
            this.txtProceedingsPrefix = new System.Windows.Forms.TextBox();
            this.txtArticlesPrefix = new System.Windows.Forms.TextBox();
            this.txtInproceedingsPrefix = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtAuthorPrefix = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvAuthors = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgvInproceedings = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dgvConferences = new System.Windows.Forms.DataGridView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.btnTestIEEE = new System.Windows.Forms.Button();
            this.txtarticleDetails = new System.Windows.Forms.TextBox();
            this.txtabstractCitations = new System.Windows.Forms.TextBox();
            this.txtabstractReferences = new System.Windows.Forms.TextBox();
            this.txtabstractAuthors = new System.Windows.Forms.TextBox();
            this.txtTimeout = new System.Windows.Forms.TextBox();
            this.txtStopNumber = new System.Windows.Forms.TextBox();
            this.txtRestartTimes = new System.Windows.Forms.TextBox();
            this.txtStartNumber = new System.Windows.Forms.TextBox();
            this.txtLogCitations = new System.Windows.Forms.TextBox();
            this.txtURLAddress = new System.Windows.Forms.TextBox();
            this.wb = new System.Windows.Forms.WebBrowser();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.txtCiteProceedings = new System.Windows.Forms.TextBox();
            this.txtCiteArticles = new System.Windows.Forms.TextBox();
            this.txtCiteInproceedings = new System.Windows.Forms.TextBox();
            this.txtCiteAuthor = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.txtFileVersion = new System.Windows.Forms.TextBox();
            this.txtCitationsFile = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.btnLoadCitation = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuthors)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInproceedings)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConferences)).BeginInit();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(519, 378);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chkSaveConference);
            this.tabPage1.Controls.Add(this.chkSaveArticles);
            this.tabPage1.Controls.Add(this.chkSaveInproceedings);
            this.tabPage1.Controls.Add(this.chkSaveAuthors);
            this.tabPage1.Controls.Add(this.chkLoadBeforeYear);
            this.tabPage1.Controls.Add(this.chkSetStartValue);
            this.tabPage1.Controls.Add(this.txtConferenceRatio);
            this.tabPage1.Controls.Add(this.txtStartRatio);
            this.tabPage1.Controls.Add(this.txtMinCountInproceedings);
            this.tabPage1.Controls.Add(this.txtTotalValue);
            this.tabPage1.Controls.Add(this.txtMinValue);
            this.tabPage1.Controls.Add(this.txtBeforeYear);
            this.tabPage1.Controls.Add(this.label17);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.txtStartValue);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.txtMsgLog);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.btnMSCalculator);
            this.tabPage1.Controls.Add(this.btnCalculator);
            this.tabPage1.Controls.Add(this.btnTestDBLP);
            this.tabPage1.Controls.Add(this.btnLoadData);
            this.tabPage1.Controls.Add(this.txtResultPrefix);
            this.tabPage1.Controls.Add(this.txtConferencePrefix);
            this.tabPage1.Controls.Add(this.txtPhdThesisPrefix);
            this.tabPage1.Controls.Add(this.txtProceedingsPrefix);
            this.tabPage1.Controls.Add(this.txtArticlesPrefix);
            this.tabPage1.Controls.Add(this.txtInproceedingsPrefix);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.txtAuthorPrefix);
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
            this.tabPage1.Size = new System.Drawing.Size(511, 352);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Config";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chkSaveConference
            // 
            this.chkSaveConference.AutoSize = true;
            this.chkSaveConference.Location = new System.Drawing.Point(254, 156);
            this.chkSaveConference.Name = "chkSaveConference";
            this.chkSaveConference.Size = new System.Drawing.Size(109, 17);
            this.chkSaveConference.TabIndex = 21;
            this.chkSaveConference.Text = "Save Conference";
            this.chkSaveConference.UseVisualStyleBackColor = true;
            // 
            // chkSaveArticles
            // 
            this.chkSaveArticles.AutoSize = true;
            this.chkSaveArticles.Location = new System.Drawing.Point(255, 85);
            this.chkSaveArticles.Name = "chkSaveArticles";
            this.chkSaveArticles.Size = new System.Drawing.Size(88, 17);
            this.chkSaveArticles.TabIndex = 21;
            this.chkSaveArticles.Text = "Save Articles";
            this.chkSaveArticles.UseVisualStyleBackColor = true;
            // 
            // chkSaveInproceedings
            // 
            this.chkSaveInproceedings.AutoSize = true;
            this.chkSaveInproceedings.Location = new System.Drawing.Point(254, 106);
            this.chkSaveInproceedings.Name = "chkSaveInproceedings";
            this.chkSaveInproceedings.Size = new System.Drawing.Size(121, 17);
            this.chkSaveInproceedings.TabIndex = 21;
            this.chkSaveInproceedings.Text = "Save Inproceedings";
            this.chkSaveInproceedings.UseVisualStyleBackColor = true;
            // 
            // chkSaveAuthors
            // 
            this.chkSaveAuthors.AutoSize = true;
            this.chkSaveAuthors.Location = new System.Drawing.Point(255, 12);
            this.chkSaveAuthors.Name = "chkSaveAuthors";
            this.chkSaveAuthors.Size = new System.Drawing.Size(85, 17);
            this.chkSaveAuthors.TabIndex = 21;
            this.chkSaveAuthors.Text = "Save Author";
            this.chkSaveAuthors.UseVisualStyleBackColor = true;
            // 
            // chkLoadBeforeYear
            // 
            this.chkLoadBeforeYear.AutoSize = true;
            this.chkLoadBeforeYear.Checked = true;
            this.chkLoadBeforeYear.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLoadBeforeYear.Location = new System.Drawing.Point(255, 62);
            this.chkLoadBeforeYear.Name = "chkLoadBeforeYear";
            this.chkLoadBeforeYear.Size = new System.Drawing.Size(107, 17);
            this.chkLoadBeforeYear.TabIndex = 21;
            this.chkLoadBeforeYear.Text = "Load Before year";
            this.chkLoadBeforeYear.UseVisualStyleBackColor = true;
            // 
            // chkSetStartValue
            // 
            this.chkSetStartValue.AutoSize = true;
            this.chkSetStartValue.Location = new System.Drawing.Point(255, 38);
            this.chkSetStartValue.Name = "chkSetStartValue";
            this.chkSetStartValue.Size = new System.Drawing.Size(97, 17);
            this.chkSetStartValue.TabIndex = 21;
            this.chkSetStartValue.Text = "Set Start Value";
            this.chkSetStartValue.UseVisualStyleBackColor = true;
            // 
            // txtConferenceRatio
            // 
            this.txtConferenceRatio.Location = new System.Drawing.Point(427, 232);
            this.txtConferenceRatio.Name = "txtConferenceRatio";
            this.txtConferenceRatio.Size = new System.Drawing.Size(56, 20);
            this.txtConferenceRatio.TabIndex = 20;
            this.txtConferenceRatio.Text = "0.4";
            // 
            // txtStartRatio
            // 
            this.txtStartRatio.Location = new System.Drawing.Point(427, 209);
            this.txtStartRatio.Name = "txtStartRatio";
            this.txtStartRatio.Size = new System.Drawing.Size(56, 20);
            this.txtStartRatio.TabIndex = 20;
            this.txtStartRatio.Text = "0.4";
            // 
            // txtMinCountInproceedings
            // 
            this.txtMinCountInproceedings.Location = new System.Drawing.Point(245, 236);
            this.txtMinCountInproceedings.Name = "txtMinCountInproceedings";
            this.txtMinCountInproceedings.Size = new System.Drawing.Size(86, 20);
            this.txtMinCountInproceedings.TabIndex = 20;
            this.txtMinCountInproceedings.Text = "300";
            // 
            // txtTotalValue
            // 
            this.txtTotalValue.Location = new System.Drawing.Point(245, 210);
            this.txtTotalValue.Name = "txtTotalValue";
            this.txtTotalValue.Size = new System.Drawing.Size(86, 20);
            this.txtTotalValue.TabIndex = 20;
            this.txtTotalValue.Text = "1046030";
            // 
            // txtMinValue
            // 
            this.txtMinValue.Location = new System.Drawing.Point(427, 189);
            this.txtMinValue.Name = "txtMinValue";
            this.txtMinValue.Size = new System.Drawing.Size(56, 20);
            this.txtMinValue.TabIndex = 20;
            this.txtMinValue.Text = "0.01";
            // 
            // txtBeforeYear
            // 
            this.txtBeforeYear.Location = new System.Drawing.Point(417, 59);
            this.txtBeforeYear.Name = "txtBeforeYear";
            this.txtBeforeYear.Size = new System.Drawing.Size(66, 20);
            this.txtBeforeYear.TabIndex = 20;
            this.txtBeforeYear.Text = "2014";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(337, 239);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(90, 13);
            this.label17.TabIndex = 19;
            this.label17.Text = "Conference Ratio";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(337, 216);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(66, 13);
            this.label13.TabIndex = 19;
            this.label13.Text = "Author Ratio";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(115, 239);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(125, 13);
            this.label15.TabIndex = 19;
            this.label15.Text = "Min Count Inproceedings";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(154, 213);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(86, 13);
            this.label12.TabIndex = 19;
            this.label12.Text = "Total Start Value";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(339, 194);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(46, 13);
            this.label11.TabIndex = 19;
            this.label11.Text = "MinStop";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(372, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Year";
            // 
            // txtStartValue
            // 
            this.txtStartValue.Location = new System.Drawing.Point(417, 35);
            this.txtStartValue.Name = "txtStartValue";
            this.txtStartValue.Size = new System.Drawing.Size(66, 20);
            this.txtStartValue.TabIndex = 20;
            this.txtStartValue.Text = "1000";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(352, 38);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Start Value";
            // 
            // txtMsgLog
            // 
            this.txtMsgLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMsgLog.Location = new System.Drawing.Point(3, 266);
            this.txtMsgLog.Multiline = true;
            this.txtMsgLog.Name = "txtMsgLog";
            this.txtMsgLog.ReadOnly = true;
            this.txtMsgLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMsgLog.Size = new System.Drawing.Size(505, 83);
            this.txtMsgLog.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 239);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Message Logs";
            // 
            // btnMSCalculator
            // 
            this.btnMSCalculator.Location = new System.Drawing.Point(369, 136);
            this.btnMSCalculator.Name = "btnMSCalculator";
            this.btnMSCalculator.Size = new System.Drawing.Size(106, 23);
            this.btnMSCalculator.TabIndex = 16;
            this.btnMSCalculator.Text = "New Calculator";
            this.btnMSCalculator.UseVisualStyleBackColor = true;
            this.btnMSCalculator.Click += new System.EventHandler(this.btnMSCalculator_Click);
            // 
            // btnCalculator
            // 
            this.btnCalculator.Location = new System.Drawing.Point(400, 160);
            this.btnCalculator.Name = "btnCalculator";
            this.btnCalculator.Size = new System.Drawing.Size(75, 23);
            this.btnCalculator.TabIndex = 16;
            this.btnCalculator.Text = "Calculator";
            this.btnCalculator.UseVisualStyleBackColor = true;
            this.btnCalculator.Click += new System.EventHandler(this.btnCalculator_Click);
            // 
            // btnTestDBLP
            // 
            this.btnTestDBLP.Location = new System.Drawing.Point(400, 107);
            this.btnTestDBLP.Name = "btnTestDBLP";
            this.btnTestDBLP.Size = new System.Drawing.Size(75, 23);
            this.btnTestDBLP.TabIndex = 16;
            this.btnTestDBLP.Text = "Test DBLP site";
            this.btnTestDBLP.UseVisualStyleBackColor = true;
            this.btnTestDBLP.Click += new System.EventHandler(this.btnTestDBLP_Click);
            // 
            // btnLoadData
            // 
            this.btnLoadData.Location = new System.Drawing.Point(400, 6);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(75, 23);
            this.btnLoadData.TabIndex = 16;
            this.btnLoadData.Text = "Load data";
            this.btnLoadData.UseVisualStyleBackColor = true;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // txtResultPrefix
            // 
            this.txtResultPrefix.Location = new System.Drawing.Point(110, 183);
            this.txtResultPrefix.Name = "txtResultPrefix";
            this.txtResultPrefix.Size = new System.Drawing.Size(138, 20);
            this.txtResultPrefix.TabIndex = 9;
            this.txtResultPrefix.Text = "result-";
            // 
            // txtConferencePrefix
            // 
            this.txtConferencePrefix.Location = new System.Drawing.Point(110, 157);
            this.txtConferencePrefix.Name = "txtConferencePrefix";
            this.txtConferencePrefix.Size = new System.Drawing.Size(138, 20);
            this.txtConferencePrefix.TabIndex = 9;
            this.txtConferencePrefix.Text = "dblp-conferences-";
            // 
            // txtPhdThesisPrefix
            // 
            this.txtPhdThesisPrefix.Location = new System.Drawing.Point(110, 130);
            this.txtPhdThesisPrefix.Name = "txtPhdThesisPrefix";
            this.txtPhdThesisPrefix.Size = new System.Drawing.Size(138, 20);
            this.txtPhdThesisPrefix.TabIndex = 10;
            this.txtPhdThesisPrefix.Text = "dblp.xml-phdthesis-";
            // 
            // txtProceedingsPrefix
            // 
            this.txtProceedingsPrefix.Location = new System.Drawing.Point(110, 104);
            this.txtProceedingsPrefix.Name = "txtProceedingsPrefix";
            this.txtProceedingsPrefix.Size = new System.Drawing.Size(138, 20);
            this.txtProceedingsPrefix.TabIndex = 11;
            this.txtProceedingsPrefix.Text = "dblp.xml-proceedings-";
            // 
            // txtArticlesPrefix
            // 
            this.txtArticlesPrefix.Location = new System.Drawing.Point(110, 82);
            this.txtArticlesPrefix.Name = "txtArticlesPrefix";
            this.txtArticlesPrefix.Size = new System.Drawing.Size(138, 20);
            this.txtArticlesPrefix.TabIndex = 12;
            this.txtArticlesPrefix.Text = "dblp.xml-articles-";
            // 
            // txtInproceedingsPrefix
            // 
            this.txtInproceedingsPrefix.Location = new System.Drawing.Point(110, 59);
            this.txtInproceedingsPrefix.Name = "txtInproceedingsPrefix";
            this.txtInproceedingsPrefix.Size = new System.Drawing.Size(138, 20);
            this.txtInproceedingsPrefix.TabIndex = 13;
            this.txtInproceedingsPrefix.Text = "dblp.xml-inproceedings-";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 186);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(66, 13);
            this.label14.TabIndex = 2;
            this.label14.Text = "Result Prefix";
            // 
            // txtAuthorPrefix
            // 
            this.txtAuthorPrefix.Location = new System.Drawing.Point(110, 33);
            this.txtAuthorPrefix.Name = "txtAuthorPrefix";
            this.txtAuthorPrefix.Size = new System.Drawing.Size(138, 20);
            this.txtAuthorPrefix.TabIndex = 14;
            this.txtAuthorPrefix.Text = "dblp.xml-www-";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 160);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Conference Prefix";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 133);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "PhDThesis Prefix";
            // 
            // txtFolder
            // 
            this.txtFolder.Location = new System.Drawing.Point(110, 8);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(138, 20);
            this.txtFolder.TabIndex = 15;
            this.txtFolder.Text = "D:\\dblp";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Proceedings Prefix";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Articles Prefix";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Inproceedings Prefix";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Authors Prefix";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Folder";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvAuthors);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(511, 352);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Authors";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvAuthors
            // 
            this.dgvAuthors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAuthors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAuthors.Location = new System.Drawing.Point(3, 3);
            this.dgvAuthors.Name = "dgvAuthors";
            this.dgvAuthors.Size = new System.Drawing.Size(505, 346);
            this.dgvAuthors.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgvInproceedings);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(511, 352);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Inproceedings";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgvInproceedings
            // 
            this.dgvInproceedings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInproceedings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInproceedings.Location = new System.Drawing.Point(3, 3);
            this.dgvInproceedings.Name = "dgvInproceedings";
            this.dgvInproceedings.Size = new System.Drawing.Size(505, 346);
            this.dgvInproceedings.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dgvConferences);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(511, 352);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Conferences";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dgvConferences
            // 
            this.dgvConferences.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConferences.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvConferences.Location = new System.Drawing.Point(3, 3);
            this.dgvConferences.Name = "dgvConferences";
            this.dgvConferences.Size = new System.Drawing.Size(505, 346);
            this.dgvConferences.TabIndex = 0;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.label26);
            this.tabPage5.Controls.Add(this.label25);
            this.tabPage5.Controls.Add(this.label24);
            this.tabPage5.Controls.Add(this.label23);
            this.tabPage5.Controls.Add(this.btnTestIEEE);
            this.tabPage5.Controls.Add(this.txtarticleDetails);
            this.tabPage5.Controls.Add(this.txtabstractCitations);
            this.tabPage5.Controls.Add(this.txtabstractReferences);
            this.tabPage5.Controls.Add(this.txtabstractAuthors);
            this.tabPage5.Controls.Add(this.txtTimeout);
            this.tabPage5.Controls.Add(this.txtStopNumber);
            this.tabPage5.Controls.Add(this.txtRestartTimes);
            this.tabPage5.Controls.Add(this.txtStartNumber);
            this.tabPage5.Controls.Add(this.txtLogCitations);
            this.tabPage5.Controls.Add(this.txtURLAddress);
            this.tabPage5.Controls.Add(this.wb);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(511, 352);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Web Browser";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(104, 151);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(34, 13);
            this.label26.TabIndex = 3;
            this.label26.Text = "To ID";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(8, 151);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(44, 13);
            this.label25.TabIndex = 3;
            this.label25.Text = "From ID";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(205, 151);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(63, 13);
            this.label24.TabIndex = 3;
            this.label24.Text = "Restart time";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(316, 151);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(67, 13);
            this.label23.TabIndex = 3;
            this.label23.Text = "Timeout (ms)";
            // 
            // btnTestIEEE
            // 
            this.btnTestIEEE.Location = new System.Drawing.Point(405, 41);
            this.btnTestIEEE.Name = "btnTestIEEE";
            this.btnTestIEEE.Size = new System.Drawing.Size(75, 23);
            this.btnTestIEEE.TabIndex = 2;
            this.btnTestIEEE.Text = "test IEEE";
            this.btnTestIEEE.UseVisualStyleBackColor = true;
            this.btnTestIEEE.Click += new System.EventHandler(this.btnTestIEEE_Click);
            // 
            // txtarticleDetails
            // 
            this.txtarticleDetails.Location = new System.Drawing.Point(8, 122);
            this.txtarticleDetails.Name = "txtarticleDetails";
            this.txtarticleDetails.Size = new System.Drawing.Size(375, 20);
            this.txtarticleDetails.TabIndex = 1;
            this.txtarticleDetails.Text = "http://ieeexplore.ieee.org/xpl/articleDetails.jsp?arnumber=";
            // 
            // txtabstractCitations
            // 
            this.txtabstractCitations.Location = new System.Drawing.Point(6, 96);
            this.txtabstractCitations.Name = "txtabstractCitations";
            this.txtabstractCitations.Size = new System.Drawing.Size(375, 20);
            this.txtabstractCitations.TabIndex = 1;
            this.txtabstractCitations.Text = "http://ieeexplore.ieee.org/xpl/abstractCitations.jsp?arnumber=";
            // 
            // txtabstractReferences
            // 
            this.txtabstractReferences.Location = new System.Drawing.Point(8, 70);
            this.txtabstractReferences.Name = "txtabstractReferences";
            this.txtabstractReferences.Size = new System.Drawing.Size(373, 20);
            this.txtabstractReferences.TabIndex = 1;
            this.txtabstractReferences.Text = "http://ieeexplore.ieee.org/xpl/abstractReferences.jsp?arnumber=";
            // 
            // txtabstractAuthors
            // 
            this.txtabstractAuthors.Location = new System.Drawing.Point(6, 44);
            this.txtabstractAuthors.Name = "txtabstractAuthors";
            this.txtabstractAuthors.Size = new System.Drawing.Size(375, 20);
            this.txtabstractAuthors.TabIndex = 1;
            this.txtabstractAuthors.Text = "http://ieeexplore.ieee.org/xpl/abstractAuthors.jsp?arnumber=";
            // 
            // txtTimeout
            // 
            this.txtTimeout.Location = new System.Drawing.Point(389, 148);
            this.txtTimeout.Name = "txtTimeout";
            this.txtTimeout.Size = new System.Drawing.Size(51, 20);
            this.txtTimeout.TabIndex = 1;
            this.txtTimeout.Text = "500000";
            // 
            // txtStopNumber
            // 
            this.txtStopNumber.Location = new System.Drawing.Point(148, 148);
            this.txtStopNumber.Name = "txtStopNumber";
            this.txtStopNumber.Size = new System.Drawing.Size(51, 20);
            this.txtStopNumber.TabIndex = 1;
            this.txtStopNumber.Text = "100";
            // 
            // txtRestartTimes
            // 
            this.txtRestartTimes.Location = new System.Drawing.Point(268, 148);
            this.txtRestartTimes.Name = "txtRestartTimes";
            this.txtRestartTimes.Size = new System.Drawing.Size(42, 20);
            this.txtRestartTimes.TabIndex = 1;
            this.txtRestartTimes.Text = "5";
            // 
            // txtStartNumber
            // 
            this.txtStartNumber.Location = new System.Drawing.Point(56, 148);
            this.txtStartNumber.Name = "txtStartNumber";
            this.txtStartNumber.Size = new System.Drawing.Size(42, 20);
            this.txtStartNumber.TabIndex = 1;
            this.txtStartNumber.Text = "0";
            // 
            // txtLogCitations
            // 
            this.txtLogCitations.Location = new System.Drawing.Point(8, 194);
            this.txtLogCitations.Multiline = true;
            this.txtLogCitations.Name = "txtLogCitations";
            this.txtLogCitations.Size = new System.Drawing.Size(434, 119);
            this.txtLogCitations.TabIndex = 1;
            // 
            // txtURLAddress
            // 
            this.txtURLAddress.Location = new System.Drawing.Point(8, 18);
            this.txtURLAddress.Name = "txtURLAddress";
            this.txtURLAddress.Size = new System.Drawing.Size(206, 20);
            this.txtURLAddress.TabIndex = 1;
            // 
            // wb
            // 
            this.wb.Location = new System.Drawing.Point(3, 220);
            this.wb.MinimumSize = new System.Drawing.Size(20, 20);
            this.wb.Name = "wb";
            this.wb.Size = new System.Drawing.Size(500, 129);
            this.wb.TabIndex = 0;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.txtCiteProceedings);
            this.tabPage6.Controls.Add(this.txtCiteArticles);
            this.tabPage6.Controls.Add(this.txtCiteInproceedings);
            this.tabPage6.Controls.Add(this.txtCiteAuthor);
            this.tabPage6.Controls.Add(this.label18);
            this.tabPage6.Controls.Add(this.label19);
            this.tabPage6.Controls.Add(this.label20);
            this.tabPage6.Controls.Add(this.label21);
            this.tabPage6.Controls.Add(this.txtFileVersion);
            this.tabPage6.Controls.Add(this.txtCitationsFile);
            this.tabPage6.Controls.Add(this.label22);
            this.tabPage6.Controls.Add(this.label16);
            this.tabPage6.Controls.Add(this.btnLoadCitation);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(511, 352);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Citations";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // txtCiteProceedings
            // 
            this.txtCiteProceedings.Location = new System.Drawing.Point(110, 107);
            this.txtCiteProceedings.Name = "txtCiteProceedings";
            this.txtCiteProceedings.Size = new System.Drawing.Size(138, 20);
            this.txtCiteProceedings.TabIndex = 21;
            this.txtCiteProceedings.Text = "dblp.cite-proceedings-";
            // 
            // txtCiteArticles
            // 
            this.txtCiteArticles.Location = new System.Drawing.Point(110, 85);
            this.txtCiteArticles.Name = "txtCiteArticles";
            this.txtCiteArticles.Size = new System.Drawing.Size(138, 20);
            this.txtCiteArticles.TabIndex = 22;
            this.txtCiteArticles.Text = "dblp.cite-articles-";
            // 
            // txtCiteInproceedings
            // 
            this.txtCiteInproceedings.Location = new System.Drawing.Point(110, 62);
            this.txtCiteInproceedings.Name = "txtCiteInproceedings";
            this.txtCiteInproceedings.Size = new System.Drawing.Size(138, 20);
            this.txtCiteInproceedings.TabIndex = 23;
            this.txtCiteInproceedings.Text = "dblp.cite-inproceedings-";
            // 
            // txtCiteAuthor
            // 
            this.txtCiteAuthor.Location = new System.Drawing.Point(110, 36);
            this.txtCiteAuthor.Name = "txtCiteAuthor";
            this.txtCiteAuthor.Size = new System.Drawing.Size(138, 20);
            this.txtCiteAuthor.TabIndex = 24;
            this.txtCiteAuthor.Text = "dblp.cite-www-";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 110);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(95, 13);
            this.label18.TabIndex = 17;
            this.label18.Text = "Proceedings Prefix";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 88);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(70, 13);
            this.label19.TabIndex = 18;
            this.label19.Text = "Articles Prefix";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 65);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(103, 13);
            this.label20.TabIndex = 19;
            this.label20.Text = "Inproceedings Prefix";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 39);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(72, 13);
            this.label21.TabIndex = 20;
            this.label21.Text = "Authors Prefix";
            // 
            // txtFileVersion
            // 
            this.txtFileVersion.Location = new System.Drawing.Point(298, 12);
            this.txtFileVersion.Name = "txtFileVersion";
            this.txtFileVersion.Size = new System.Drawing.Size(55, 20);
            this.txtFileVersion.TabIndex = 16;
            this.txtFileVersion.Text = "5";
            // 
            // txtCitationsFile
            // 
            this.txtCitationsFile.Location = new System.Drawing.Point(109, 12);
            this.txtCitationsFile.Name = "txtCitationsFile";
            this.txtCitationsFile.Size = new System.Drawing.Size(138, 20);
            this.txtCitationsFile.TabIndex = 16;
            this.txtCitationsFile.Text = "DBLP-citation-Feb21-v5.txt";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(253, 15);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(42, 13);
            this.label22.TabIndex = 15;
            this.label22.Text = "Version";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(5, 15);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(66, 13);
            this.label16.TabIndex = 15;
            this.label16.Text = "Citations File";
            // 
            // btnLoadCitation
            // 
            this.btnLoadCitation.Location = new System.Drawing.Point(362, 10);
            this.btnLoadCitation.Name = "btnLoadCitation";
            this.btnLoadCitation.Size = new System.Drawing.Size(118, 23);
            this.btnLoadCitation.TabIndex = 0;
            this.btnLoadCitation.Text = "Load Citations";
            this.btnLoadCitation.UseVisualStyleBackColor = true;
            this.btnLoadCitation.Click += new System.EventHandler(this.btnLoadCitation_Click);
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 378);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainFrm";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuthors)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInproceedings)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvConferences)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtConferencePrefix;
        private System.Windows.Forms.TextBox txtPhdThesisPrefix;
        private System.Windows.Forms.TextBox txtProceedingsPrefix;
        private System.Windows.Forms.TextBox txtArticlesPrefix;
        private System.Windows.Forms.TextBox txtInproceedingsPrefix;
        private System.Windows.Forms.TextBox txtAuthorPrefix;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.TextBox txtMsgLog;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkSetStartValue;
        private System.Windows.Forms.TextBox txtStartValue;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView dgvAuthors;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dgvInproceedings;
        private System.Windows.Forms.CheckBox chkSaveAuthors;
        private System.Windows.Forms.CheckBox chkLoadBeforeYear;
        private System.Windows.Forms.TextBox txtBeforeYear;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkSaveConference;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView dgvConferences;
        private System.Windows.Forms.CheckBox chkSaveInproceedings;
        private System.Windows.Forms.Button btnTestDBLP;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TextBox txtURLAddress;
        private System.Windows.Forms.WebBrowser wb;
        private System.Windows.Forms.Button btnCalculator;
        private System.Windows.Forms.TextBox txtMinValue;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtTotalValue;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtStartRatio;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtResultPrefix;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnMSCalculator;
        private System.Windows.Forms.TextBox txtMinCountInproceedings;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TextBox txtCitationsFile;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnLoadCitation;
        private System.Windows.Forms.TextBox txtConferenceRatio;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox chkSaveArticles;
        private System.Windows.Forms.TextBox txtCiteProceedings;
        private System.Windows.Forms.TextBox txtCiteArticles;
        private System.Windows.Forms.TextBox txtCiteInproceedings;
        private System.Windows.Forms.TextBox txtCiteAuthor;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtFileVersion;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button btnTestIEEE;
        private System.Windows.Forms.TextBox txtabstractCitations;
        private System.Windows.Forms.TextBox txtabstractReferences;
        private System.Windows.Forms.TextBox txtabstractAuthors;
        private System.Windows.Forms.TextBox txtarticleDetails;
        private System.Windows.Forms.TextBox txtStopNumber;
        private System.Windows.Forms.TextBox txtStartNumber;
        private System.Windows.Forms.TextBox txtLogCitations;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtTimeout;
        private System.Windows.Forms.TextBox txtRestartTimes;
    }
}

