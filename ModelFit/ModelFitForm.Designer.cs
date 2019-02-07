/////////////////////////////////////////////////////////////////////
//
// Implements the ModelFitForm class
//
// Author: Jason Jenkins
//
// This class implements the form based GUI used by the application.
//
/////////////////////////////////////////////////////////////////////

namespace ModelFit
{
    partial class ModelFitForm
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
            this.components = new System.ComponentModel.Container();
            this.DataFileTBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.FileBrowseBtn = new System.Windows.Forms.Button();
            this.FitModelBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.XlistBox = new System.Windows.Forms.ListBox();
            this.YlistBox = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.HeaderCkBox = new System.Windows.Forms.CheckBox();
            this.InfoLabel = new System.Windows.Forms.Label();
            this.ExitBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.OutAmpNumUpDwn = new System.Windows.Forms.NumericUpDown();
            this.OutSlopeNumUpDwn = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.OutFuncListBox = new System.Windows.Forms.ListBox();
            this.LearnConstNumUpDwn = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.MomentNumUpDwn = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.MinNetErrNumUpDwn = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.NumIterNumUpDwn = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.InitRangeNumUpDwn = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.UnitsNumUpDwn = new System.Windows.Forms.NumericUpDown();
            this.ScaleNumUpDwn = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.HidAmpNumUpDwn = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.HidSlopeNumUpDwn = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.HidFuncListBox = new System.Windows.Forms.ListBox();
            this.AppStatusBar = new System.Windows.Forms.StatusStrip();
            this.PanelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.PanelIterations = new System.Windows.Forms.ToolStripStatusLabel();
            this.PanelNetError = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ModelFitToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SaveToBtn = new System.Windows.Forms.Button();
            this.SaveNetworkBtn = new System.Windows.Forms.Button();
            this.ExcelRadioBtn = new System.Windows.Forms.RadioButton();
            this.CSVRadioBtn = new System.Windows.Forms.RadioButton();
            this.ExcelOutputCkBox = new System.Windows.Forms.CheckBox();
            this.tableDataGridView = new System.Windows.Forms.DataGridView();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OutAmpNumUpDwn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OutSlopeNumUpDwn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LearnConstNumUpDwn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MomentNumUpDwn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinNetErrNumUpDwn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumIterNumUpDwn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InitRangeNumUpDwn)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UnitsNumUpDwn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ScaleNumUpDwn)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HidAmpNumUpDwn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HidSlopeNumUpDwn)).BeginInit();
            this.AppStatusBar.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tableDataGridView)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataFileTBox
            // 
            this.DataFileTBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataFileTBox.Location = new System.Drawing.Point(20, 31);
            this.DataFileTBox.Name = "DataFileTBox";
            this.DataFileTBox.ReadOnly = true;
            this.DataFileTBox.Size = new System.Drawing.Size(429, 20);
            this.DataFileTBox.TabIndex = 49;
            this.DataFileTBox.TabStop = false;
            this.ModelFitToolTip.SetToolTip(this.DataFileTBox, "displays the full name and path of the data file");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Data File";
            // 
            // FileBrowseBtn
            // 
            this.FileBrowseBtn.Location = new System.Drawing.Point(465, 31);
            this.FileBrowseBtn.Name = "FileBrowseBtn";
            this.FileBrowseBtn.Size = new System.Drawing.Size(85, 20);
            this.FileBrowseBtn.TabIndex = 2;
            this.FileBrowseBtn.Text = "Browse";
            this.ModelFitToolTip.SetToolTip(this.FileBrowseBtn, "click to browse for a CSV data file");
            this.FileBrowseBtn.UseVisualStyleBackColor = true;
            this.FileBrowseBtn.Click += new System.EventHandler(this.FileBrowseBtn_Click);
            // 
            // FitModelBtn
            // 
            this.FitModelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.FitModelBtn.Enabled = false;
            this.FitModelBtn.Location = new System.Drawing.Point(885, 441);
            this.FitModelBtn.Name = "FitModelBtn";
            this.FitModelBtn.Size = new System.Drawing.Size(85, 20);
            this.FitModelBtn.TabIndex = 47;
            this.FitModelBtn.Text = "Fit Model";
            this.ModelFitToolTip.SetToolTip(this.FitModelBtn, "click to start the training process and fit a model to the data");
            this.FitModelBtn.UseVisualStyleBackColor = true;
            this.FitModelBtn.Click += new System.EventHandler(this.FitModelBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Data Preview";
            this.ModelFitToolTip.SetToolTip(this.label2, "displays the first 100 lines of the data file - the last two rows contain the min" +
        " and max values");
            // 
            // XlistBox
            // 
            this.XlistBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.XlistBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.XlistBox.FormattingEnabled = true;
            this.XlistBox.Location = new System.Drawing.Point(20, 223);
            this.XlistBox.Name = "XlistBox";
            this.XlistBox.Size = new System.Drawing.Size(162, 69);
            this.XlistBox.TabIndex = 7;
            this.ModelFitToolTip.SetToolTip(this.XlistBox, "select the data column containing the predictor variable (X)");
            this.XlistBox.SelectedIndexChanged += new System.EventHandler(this.XlistBox_SelectedIndexChanged);
            // 
            // YlistBox
            // 
            this.YlistBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.YlistBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.YlistBox.FormattingEnabled = true;
            this.YlistBox.Location = new System.Drawing.Point(210, 223);
            this.YlistBox.Name = "YlistBox";
            this.YlistBox.Size = new System.Drawing.Size(162, 69);
            this.YlistBox.TabIndex = 9;
            this.ModelFitToolTip.SetToolTip(this.YlistBox, "select the data column containing the response variable (Y)");
            this.YlistBox.SelectedIndexChanged += new System.EventHandler(this.XlistBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(20, 204);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Predictor Variable (X)";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(210, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Response Variable (Y)";
            // 
            // HeaderCkBox
            // 
            this.HeaderCkBox.AutoSize = true;
            this.HeaderCkBox.Checked = true;
            this.HeaderCkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.HeaderCkBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HeaderCkBox.Location = new System.Drawing.Point(570, 34);
            this.HeaderCkBox.Name = "HeaderCkBox";
            this.HeaderCkBox.Size = new System.Drawing.Size(137, 17);
            this.HeaderCkBox.TabIndex = 3;
            this.HeaderCkBox.Text = "File has header row";
            this.ModelFitToolTip.SetToolTip(this.HeaderCkBox, "check if the data file has a header row");
            this.HeaderCkBox.UseVisualStyleBackColor = true;
            this.HeaderCkBox.CheckedChanged += new System.EventHandler(this.HeaderCkBox_CheckedChanged);
            // 
            // InfoLabel
            // 
            this.InfoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.InfoLabel.AutoSize = true;
            this.InfoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.818182F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InfoLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.InfoLabel.Location = new System.Drawing.Point(8, 10);
            this.InfoLabel.Name = "InfoLabel";
            this.InfoLabel.Size = new System.Drawing.Size(201, 20);
            this.InfoLabel.TabIndex = 46;
            this.InfoLabel.Text = "Please select a data file";
            // 
            // ExitBtn
            // 
            this.ExitBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ExitBtn.Location = new System.Drawing.Point(885, 31);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(85, 20);
            this.ExitBtn.TabIndex = 48;
            this.ExitBtn.Text = "Exit";
            this.ModelFitToolTip.SetToolTip(this.ExitBtn, "click to exit the application");
            this.ExitBtn.UseVisualStyleBackColor = true;
            this.ExitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.OutAmpNumUpDwn);
            this.groupBox1.Controls.Add(this.OutSlopeNumUpDwn);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.OutFuncListBox);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(20, 312);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(162, 150);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Output Layer";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(30, 126);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(51, 13);
            this.label13.TabIndex = 30;
            this.label13.Text = "Amplify:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(38, 104);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 13);
            this.label12.TabIndex = 28;
            this.label12.Text = "Slope:";
            // 
            // OutAmpNumUpDwn
            // 
            this.OutAmpNumUpDwn.DecimalPlaces = 2;
            this.OutAmpNumUpDwn.Location = new System.Drawing.Point(84, 124);
            this.OutAmpNumUpDwn.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.OutAmpNumUpDwn.Name = "OutAmpNumUpDwn";
            this.OutAmpNumUpDwn.Size = new System.Drawing.Size(65, 20);
            this.OutAmpNumUpDwn.TabIndex = 31;
            this.OutAmpNumUpDwn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ModelFitToolTip.SetToolTip(this.OutAmpNumUpDwn, "used to boost or reduce the output layer units signal value");
            this.OutAmpNumUpDwn.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.OutAmpNumUpDwn.ValueChanged += new System.EventHandler(this.XlistBox_SelectedIndexChanged);
            // 
            // OutSlopeNumUpDwn
            // 
            this.OutSlopeNumUpDwn.DecimalPlaces = 2;
            this.OutSlopeNumUpDwn.Location = new System.Drawing.Point(84, 99);
            this.OutSlopeNumUpDwn.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.OutSlopeNumUpDwn.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            393216});
            this.OutSlopeNumUpDwn.Name = "OutSlopeNumUpDwn";
            this.OutSlopeNumUpDwn.Size = new System.Drawing.Size(65, 20);
            this.OutSlopeNumUpDwn.TabIndex = 29;
            this.OutSlopeNumUpDwn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ModelFitToolTip.SetToolTip(this.OutSlopeNumUpDwn, "used to adjust the sensitivity of the output layer units activation function");
            this.OutSlopeNumUpDwn.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.OutSlopeNumUpDwn.ValueChanged += new System.EventHandler(this.XlistBox_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(32, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(117, 13);
            this.label10.TabIndex = 26;
            this.label10.Text = "Activation Function";
            // 
            // OutFuncListBox
            // 
            this.OutFuncListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutFuncListBox.FormattingEnabled = true;
            this.OutFuncListBox.Location = new System.Drawing.Point(31, 37);
            this.OutFuncListBox.Name = "OutFuncListBox";
            this.OutFuncListBox.Size = new System.Drawing.Size(119, 56);
            this.OutFuncListBox.TabIndex = 27;
            this.ModelFitToolTip.SetToolTip(this.OutFuncListBox, "select the output layer units activation function");
            this.OutFuncListBox.SelectedIndexChanged += new System.EventHandler(this.XlistBox_SelectedIndexChanged);
            // 
            // LearnConstNumUpDwn
            // 
            this.LearnConstNumUpDwn.DecimalPlaces = 3;
            this.LearnConstNumUpDwn.Increment = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            this.LearnConstNumUpDwn.Location = new System.Drawing.Point(139, 18);
            this.LearnConstNumUpDwn.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.LearnConstNumUpDwn.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.LearnConstNumUpDwn.Name = "LearnConstNumUpDwn";
            this.LearnConstNumUpDwn.Size = new System.Drawing.Size(65, 20);
            this.LearnConstNumUpDwn.TabIndex = 12;
            this.LearnConstNumUpDwn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ModelFitToolTip.SetToolTip(this.LearnConstNumUpDwn, "governs the \'size\' of the steps taken down the error surface");
            this.LearnConstNumUpDwn.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.LearnConstNumUpDwn.ValueChanged += new System.EventHandler(this.XlistBox_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(24, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Learning Constant:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(66, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Momentum:";
            // 
            // MomentNumUpDwn
            // 
            this.MomentNumUpDwn.DecimalPlaces = 3;
            this.MomentNumUpDwn.Increment = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            this.MomentNumUpDwn.Location = new System.Drawing.Point(139, 48);
            this.MomentNumUpDwn.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MomentNumUpDwn.Name = "MomentNumUpDwn";
            this.MomentNumUpDwn.Size = new System.Drawing.Size(65, 20);
            this.MomentNumUpDwn.TabIndex = 14;
            this.MomentNumUpDwn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ModelFitToolTip.SetToolTip(this.MomentNumUpDwn, "used to weight the search of the error surface to continue along the same \'direct" +
        "ion\' as the previous step");
            this.MomentNumUpDwn.ValueChanged += new System.EventHandler(this.XlistBox_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(20, 81);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(117, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Min. Network Error:";
            // 
            // MinNetErrNumUpDwn
            // 
            this.MinNetErrNumUpDwn.DecimalPlaces = 3;
            this.MinNetErrNumUpDwn.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.MinNetErrNumUpDwn.Location = new System.Drawing.Point(139, 78);
            this.MinNetErrNumUpDwn.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.MinNetErrNumUpDwn.Name = "MinNetErrNumUpDwn";
            this.MinNetErrNumUpDwn.Size = new System.Drawing.Size(65, 20);
            this.MinNetErrNumUpDwn.TabIndex = 16;
            this.MinNetErrNumUpDwn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ModelFitToolTip.SetToolTip(this.MinNetErrNumUpDwn, "training will stop when the total network error is less than this value");
            this.MinNetErrNumUpDwn.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.MinNetErrNumUpDwn.ValueChanged += new System.EventHandler(this.XlistBox_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(12, 111);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(126, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Number of Iterations:";
            // 
            // NumIterNumUpDwn
            // 
            this.NumIterNumUpDwn.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.NumIterNumUpDwn.Location = new System.Drawing.Point(139, 108);
            this.NumIterNumUpDwn.Maximum = new decimal(new int[] {
            500000,
            0,
            0,
            0});
            this.NumIterNumUpDwn.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.NumIterNumUpDwn.Name = "NumIterNumUpDwn";
            this.NumIterNumUpDwn.Size = new System.Drawing.Size(65, 20);
            this.NumIterNumUpDwn.TabIndex = 18;
            this.NumIterNumUpDwn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ModelFitToolTip.SetToolTip(this.NumIterNumUpDwn, "sets the maximum number of iterations for the training process");
            this.NumIterNumUpDwn.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.NumIterNumUpDwn.ValueChanged += new System.EventHandler(this.XlistBox_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(54, 171);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Initial Range:";
            // 
            // InitRangeNumUpDwn
            // 
            this.InitRangeNumUpDwn.Location = new System.Drawing.Point(139, 168);
            this.InitRangeNumUpDwn.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.InitRangeNumUpDwn.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.InitRangeNumUpDwn.Name = "InitRangeNumUpDwn";
            this.InitRangeNumUpDwn.Size = new System.Drawing.Size(65, 20);
            this.InitRangeNumUpDwn.TabIndex = 22;
            this.InitRangeNumUpDwn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ModelFitToolTip.SetToolTip(this.InitRangeNumUpDwn, "sets the range of the random values initially connecting the layers of the networ" +
        "k, 2 represents -1 to +1");
            this.InitRangeNumUpDwn.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.InitRangeNumUpDwn.ValueChanged += new System.EventHandler(this.XlistBox_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.UnitsNumUpDwn);
            this.groupBox2.Controls.Add(this.ScaleNumUpDwn);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.InitRangeNumUpDwn);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.NumIterNumUpDwn);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.MinNetErrNumUpDwn);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.MomentNumUpDwn);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.LearnConstNumUpDwn);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(395, 204);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(220, 228);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Main Settings";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(14, 201);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(123, 13);
            this.label17.TabIndex = 23;
            this.label17.Text = "No. of Hidden Units:";
            // 
            // UnitsNumUpDwn
            // 
            this.UnitsNumUpDwn.Location = new System.Drawing.Point(139, 198);
            this.UnitsNumUpDwn.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UnitsNumUpDwn.Name = "UnitsNumUpDwn";
            this.UnitsNumUpDwn.Size = new System.Drawing.Size(65, 20);
            this.UnitsNumUpDwn.TabIndex = 24;
            this.UnitsNumUpDwn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ModelFitToolTip.SetToolTip(this.UnitsNumUpDwn, "sets the number of units in the hidden layer");
            this.UnitsNumUpDwn.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.UnitsNumUpDwn.ValueChanged += new System.EventHandler(this.XlistBox_SelectedIndexChanged);
            // 
            // ScaleNumUpDwn
            // 
            this.ScaleNumUpDwn.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.ScaleNumUpDwn.Location = new System.Drawing.Point(139, 138);
            this.ScaleNumUpDwn.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.ScaleNumUpDwn.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ScaleNumUpDwn.Name = "ScaleNumUpDwn";
            this.ScaleNumUpDwn.Size = new System.Drawing.Size(65, 20);
            this.ScaleNumUpDwn.TabIndex = 20;
            this.ScaleNumUpDwn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ModelFitToolTip.SetToolTip(this.ScaleNumUpDwn, "used to divide the data values to reduce their magnitude - this may improve the f" +
        "it");
            this.ScaleNumUpDwn.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.ScaleNumUpDwn.ValueChanged += new System.EventHandler(this.XlistBox_SelectedIndexChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(53, 141);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(83, 13);
            this.label16.TabIndex = 19;
            this.label16.Text = "Scale Factor:";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.HidAmpNumUpDwn);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.HidSlopeNumUpDwn);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.HidFuncListBox);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(210, 312);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(162, 150);
            this.groupBox3.TabIndex = 32;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Hidden Layer";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(30, 126);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(51, 13);
            this.label14.TabIndex = 37;
            this.label14.Text = "Amplify:";
            // 
            // HidAmpNumUpDwn
            // 
            this.HidAmpNumUpDwn.DecimalPlaces = 2;
            this.HidAmpNumUpDwn.Location = new System.Drawing.Point(84, 124);
            this.HidAmpNumUpDwn.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.HidAmpNumUpDwn.Name = "HidAmpNumUpDwn";
            this.HidAmpNumUpDwn.Size = new System.Drawing.Size(65, 20);
            this.HidAmpNumUpDwn.TabIndex = 38;
            this.HidAmpNumUpDwn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ModelFitToolTip.SetToolTip(this.HidAmpNumUpDwn, "used to boost or reduce the hidden layer units signal value");
            this.HidAmpNumUpDwn.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.HidAmpNumUpDwn.ValueChanged += new System.EventHandler(this.XlistBox_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(38, 104);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(43, 13);
            this.label15.TabIndex = 35;
            this.label15.Text = "Slope:";
            // 
            // HidSlopeNumUpDwn
            // 
            this.HidSlopeNumUpDwn.DecimalPlaces = 2;
            this.HidSlopeNumUpDwn.Location = new System.Drawing.Point(84, 99);
            this.HidSlopeNumUpDwn.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.HidSlopeNumUpDwn.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            393216});
            this.HidSlopeNumUpDwn.Name = "HidSlopeNumUpDwn";
            this.HidSlopeNumUpDwn.Size = new System.Drawing.Size(65, 20);
            this.HidSlopeNumUpDwn.TabIndex = 36;
            this.HidSlopeNumUpDwn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ModelFitToolTip.SetToolTip(this.HidSlopeNumUpDwn, "used to adjust the sensitivity of the hidden layer units activation function");
            this.HidSlopeNumUpDwn.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.HidSlopeNumUpDwn.ValueChanged += new System.EventHandler(this.XlistBox_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(30, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(117, 13);
            this.label11.TabIndex = 33;
            this.label11.Text = "Activation Function";
            // 
            // HidFuncListBox
            // 
            this.HidFuncListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HidFuncListBox.FormattingEnabled = true;
            this.HidFuncListBox.Location = new System.Drawing.Point(29, 37);
            this.HidFuncListBox.Name = "HidFuncListBox";
            this.HidFuncListBox.Size = new System.Drawing.Size(119, 56);
            this.HidFuncListBox.TabIndex = 34;
            this.ModelFitToolTip.SetToolTip(this.HidFuncListBox, "select the hidden layer units activation function");
            this.HidFuncListBox.SelectedIndexChanged += new System.EventHandler(this.XlistBox_SelectedIndexChanged);
            // 
            // AppStatusBar
            // 
            this.AppStatusBar.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.AppStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PanelStatus,
            this.PanelIterations,
            this.PanelNetError});
            this.AppStatusBar.Location = new System.Drawing.Point(0, 468);
            this.AppStatusBar.Name = "AppStatusBar";
            this.AppStatusBar.Size = new System.Drawing.Size(992, 28);
            this.AppStatusBar.TabIndex = 26;
            this.AppStatusBar.Text = "statusStrip1";
            // 
            // PanelStatus
            // 
            this.PanelStatus.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.PanelStatus.Name = "PanelStatus";
            this.PanelStatus.Size = new System.Drawing.Size(80, 23);
            this.PanelStatus.Text = "Status: Idle";
            // 
            // PanelIterations
            // 
            this.PanelIterations.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.PanelIterations.Name = "PanelIterations";
            this.PanelIterations.Size = new System.Drawing.Size(4, 23);
            // 
            // PanelNetError
            // 
            this.PanelNetError.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.PanelNetError.Name = "PanelNetError";
            this.PanelNetError.Size = new System.Drawing.Size(4, 23);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.InfoLabel);
            this.groupBox4.Location = new System.Drawing.Point(589, 431);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(286, 35);
            this.groupBox4.TabIndex = 45;
            this.groupBox4.TabStop = false;
            // 
            // SaveToBtn
            // 
            this.SaveToBtn.Enabled = false;
            this.SaveToBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveToBtn.Location = new System.Drawing.Point(83, 82);
            this.SaveToBtn.Name = "SaveToBtn";
            this.SaveToBtn.Size = new System.Drawing.Size(85, 20);
            this.SaveToBtn.TabIndex = 42;
            this.SaveToBtn.Text = "Save";
            this.ModelFitToolTip.SetToolTip(this.SaveToBtn, "click to save the latest model output in the selected format");
            this.SaveToBtn.UseVisualStyleBackColor = true;
            this.SaveToBtn.Click += new System.EventHandler(this.SaveToBtn_Click);
            // 
            // SaveNetworkBtn
            // 
            this.SaveNetworkBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveNetworkBtn.Enabled = false;
            this.SaveNetworkBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveNetworkBtn.Location = new System.Drawing.Point(83, 39);
            this.SaveNetworkBtn.Name = "SaveNetworkBtn";
            this.SaveNetworkBtn.Size = new System.Drawing.Size(85, 20);
            this.SaveNetworkBtn.TabIndex = 44;
            this.SaveNetworkBtn.Text = "Save Network";
            this.ModelFitToolTip.SetToolTip(this.SaveNetworkBtn, "click to serialise the current trained neural network to a file");
            this.SaveNetworkBtn.UseVisualStyleBackColor = true;
            this.SaveNetworkBtn.Click += new System.EventHandler(this.SaveNetworkBtn_Click);
            // 
            // ExcelRadioBtn
            // 
            this.ExcelRadioBtn.AutoSize = true;
            this.ExcelRadioBtn.Location = new System.Drawing.Point(10, 51);
            this.ExcelRadioBtn.Name = "ExcelRadioBtn";
            this.ExcelRadioBtn.Size = new System.Drawing.Size(98, 17);
            this.ExcelRadioBtn.TabIndex = 41;
            this.ExcelRadioBtn.Text = "Save as XLS";
            this.ModelFitToolTip.SetToolTip(this.ExcelRadioBtn, "if set the model output is produced in XLS (Excel) format");
            this.ExcelRadioBtn.UseVisualStyleBackColor = true;
            // 
            // CSVRadioBtn
            // 
            this.CSVRadioBtn.AutoSize = true;
            this.CSVRadioBtn.Checked = true;
            this.CSVRadioBtn.Location = new System.Drawing.Point(10, 21);
            this.CSVRadioBtn.Name = "CSVRadioBtn";
            this.CSVRadioBtn.Size = new System.Drawing.Size(99, 17);
            this.CSVRadioBtn.TabIndex = 40;
            this.CSVRadioBtn.TabStop = true;
            this.CSVRadioBtn.Text = "Save as CSV";
            this.ModelFitToolTip.SetToolTip(this.CSVRadioBtn, "if set the model output is produced in CSV format");
            this.CSVRadioBtn.UseVisualStyleBackColor = true;
            // 
            // ExcelOutputCkBox
            // 
            this.ExcelOutputCkBox.AutoSize = true;
            this.ExcelOutputCkBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExcelOutputCkBox.Location = new System.Drawing.Point(12, 16);
            this.ExcelOutputCkBox.Name = "ExcelOutputCkBox";
            this.ExcelOutputCkBox.Size = new System.Drawing.Size(146, 17);
            this.ExcelOutputCkBox.TabIndex = 43;
            this.ExcelOutputCkBox.Text = "Show output in Excel";
            this.ModelFitToolTip.SetToolTip(this.ExcelOutputCkBox, "check to show the fitted model output in Excel at the end of the training process" +
        "");
            this.ExcelOutputCkBox.UseVisualStyleBackColor = true;
            // 
            // tableDataGridView
            // 
            this.tableDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableDataGridView.Location = new System.Drawing.Point(23, 84);
            this.tableDataGridView.Name = "tableDataGridView";
            this.tableDataGridView.ReadOnly = true;
            this.tableDataGridView.Size = new System.Drawing.Size(947, 102);
            this.tableDataGridView.TabIndex = 50;
            this.tableDataGridView.TabStop = false;
            this.ModelFitToolTip.SetToolTip(this.tableDataGridView, "displays the first 100 lines of the data file - the last two rows contain the min" +
        " and max values");
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.ExcelRadioBtn);
            this.groupBox5.Controls.Add(this.CSVRadioBtn);
            this.groupBox5.Controls.Add(this.SaveToBtn);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(796, 204);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(174, 113);
            this.groupBox5.TabIndex = 39;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Output";
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.SaveNetworkBtn);
            this.groupBox6.Controls.Add(this.ExcelOutputCkBox);
            this.groupBox6.Location = new System.Drawing.Point(796, 323);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(174, 67);
            this.groupBox6.TabIndex = 44;
            this.groupBox6.TabStop = false;
            // 
            // ModelFitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 496);
            this.Controls.Add(this.tableDataGridView);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.AppStatusBar);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ExitBtn);
            this.Controls.Add(this.HeaderCkBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.YlistBox);
            this.Controls.Add(this.XlistBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.FitModelBtn);
            this.Controls.Add(this.FileBrowseBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DataFileTBox);
            this.MinimumSize = new System.Drawing.Size(850, 490);
            this.Name = "ModelFitForm";
            this.Text = "ModelFit";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OutAmpNumUpDwn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OutSlopeNumUpDwn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LearnConstNumUpDwn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MomentNumUpDwn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinNetErrNumUpDwn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumIterNumUpDwn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InitRangeNumUpDwn)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UnitsNumUpDwn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ScaleNumUpDwn)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HidAmpNumUpDwn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HidSlopeNumUpDwn)).EndInit();
            this.AppStatusBar.ResumeLayout(false);
            this.AppStatusBar.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tableDataGridView)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox DataFileTBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button FileBrowseBtn;
        private System.Windows.Forms.Button FitModelBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox XlistBox;
        private System.Windows.Forms.ListBox YlistBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox HeaderCkBox;
        private System.Windows.Forms.Label InfoLabel;
        private System.Windows.Forms.Button ExitBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown LearnConstNumUpDwn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown MomentNumUpDwn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown MinNetErrNumUpDwn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown NumIterNumUpDwn;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown InitRangeNumUpDwn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox OutFuncListBox;
        private System.Windows.Forms.ListBox HidFuncListBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown OutAmpNumUpDwn;
        private System.Windows.Forms.NumericUpDown OutSlopeNumUpDwn;
        private System.Windows.Forms.NumericUpDown HidAmpNumUpDwn;
        private System.Windows.Forms.NumericUpDown HidSlopeNumUpDwn;
        private System.Windows.Forms.StatusStrip AppStatusBar;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown ScaleNumUpDwn;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ToolStripStatusLabel PanelStatus;
        private System.Windows.Forms.ToolStripStatusLabel PanelIterations;
        private System.Windows.Forms.ToolStripStatusLabel PanelNetError;
        private System.Windows.Forms.NumericUpDown UnitsNumUpDwn;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ToolTip ModelFitToolTip;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton ExcelRadioBtn;
        private System.Windows.Forms.RadioButton CSVRadioBtn;
        private System.Windows.Forms.Button SaveToBtn;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button SaveNetworkBtn;
        private System.Windows.Forms.CheckBox ExcelOutputCkBox;
        private System.Windows.Forms.DataGridView tableDataGridView;
    }
}

