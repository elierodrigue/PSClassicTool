namespace PSClassicTool
{
    partial class GameInfo
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtGameName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPublisher = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nudYear = new System.Windows.Forms.NumericUpDown();
            this.nudPlayers = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nudGameId = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.grpDisc = new System.Windows.Forms.GroupBox();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.btnReplace = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtBaseName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkConfig = new System.Windows.Forms.CheckBox();
            this.btnDefaultConfig = new System.Windows.Forms.Button();
            this.chkDoubleResolution = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPlayers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGameId)).BeginInit();
            this.grpDisc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtGameName
            // 
            this.txtGameName.Location = new System.Drawing.Point(6, 16);
            this.txtGameName.Name = "txtGameName";
            this.txtGameName.Size = new System.Drawing.Size(262, 20);
            this.txtGameName.TabIndex = 0;
            this.txtGameName.TextChanged += new System.EventHandler(this.txtGameName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Game Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Publisher";
            // 
            // txtPublisher
            // 
            this.txtPublisher.Location = new System.Drawing.Point(6, 55);
            this.txtPublisher.Name = "txtPublisher";
            this.txtPublisher.Size = new System.Drawing.Size(262, 20);
            this.txtPublisher.TabIndex = 2;
            this.txtPublisher.TextChanged += new System.EventHandler(this.txtPublisher_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Release Year";
            // 
            // nudYear
            // 
            this.nudYear.Location = new System.Drawing.Point(6, 99);
            this.nudYear.Maximum = new decimal(new int[] {
            2040,
            0,
            0,
            0});
            this.nudYear.Minimum = new decimal(new int[] {
            1970,
            0,
            0,
            0});
            this.nudYear.Name = "nudYear";
            this.nudYear.Size = new System.Drawing.Size(262, 20);
            this.nudYear.TabIndex = 6;
            this.nudYear.Value = new decimal(new int[] {
            1970,
            0,
            0,
            0});
            this.nudYear.ValueChanged += new System.EventHandler(this.nudYear_ValueChanged);
            // 
            // nudPlayers
            // 
            this.nudPlayers.Location = new System.Drawing.Point(6, 142);
            this.nudPlayers.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudPlayers.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPlayers.Name = "nudPlayers";
            this.nudPlayers.Size = new System.Drawing.Size(63, 20);
            this.nudPlayers.TabIndex = 8;
            this.nudPlayers.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudPlayers.ValueChanged += new System.EventHandler(this.nudPlayers_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Players";
            // 
            // nudGameId
            // 
            this.nudGameId.Enabled = false;
            this.nudGameId.Location = new System.Drawing.Point(6, 189);
            this.nudGameId.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nudGameId.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudGameId.Name = "nudGameId";
            this.nudGameId.ReadOnly = true;
            this.nudGameId.Size = new System.Drawing.Size(262, 20);
            this.nudGameId.TabIndex = 10;
            this.nudGameId.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 172);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "GameID";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(3, 548);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(262, 42);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click_1);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(3, 215);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(262, 21);
            this.comboBox1.TabIndex = 12;
            // 
            // grpDisc
            // 
            this.grpDisc.Controls.Add(this.txtUrl);
            this.grpDisc.Controls.Add(this.btnReplace);
            this.grpDisc.Controls.Add(this.pictureBox1);
            this.grpDisc.Controls.Add(this.txtBaseName);
            this.grpDisc.Location = new System.Drawing.Point(3, 242);
            this.grpDisc.Name = "grpDisc";
            this.grpDisc.Size = new System.Drawing.Size(262, 300);
            this.grpDisc.TabIndex = 13;
            this.grpDisc.TabStop = false;
            this.grpDisc.Text = "Disc";
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(6, 265);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(250, 20);
            this.txtUrl.TabIndex = 3;
            // 
            // btnReplace
            // 
            this.btnReplace.Location = new System.Drawing.Point(93, 236);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(75, 23);
            this.btnReplace.TabIndex = 2;
            this.btnReplace.Text = "Replace";
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(6, 45);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(250, 185);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // txtBaseName
            // 
            this.txtBaseName.Location = new System.Drawing.Point(6, 19);
            this.txtBaseName.Name = "txtBaseName";
            this.txtBaseName.Size = new System.Drawing.Size(250, 20);
            this.txtBaseName.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(75, 125);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Got Config File";
            // 
            // chkConfig
            // 
            this.chkConfig.AutoSize = true;
            this.chkConfig.Enabled = false;
            this.chkConfig.Location = new System.Drawing.Point(78, 146);
            this.chkConfig.Name = "chkConfig";
            this.chkConfig.Size = new System.Drawing.Size(15, 14);
            this.chkConfig.TabIndex = 15;
            this.chkConfig.UseVisualStyleBackColor = true;
            // 
            // btnDefaultConfig
            // 
            this.btnDefaultConfig.Location = new System.Drawing.Point(99, 141);
            this.btnDefaultConfig.Name = "btnDefaultConfig";
            this.btnDefaultConfig.Size = new System.Drawing.Size(75, 23);
            this.btnDefaultConfig.TabIndex = 16;
            this.btnDefaultConfig.Text = "Default";
            this.btnDefaultConfig.UseVisualStyleBackColor = true;
            this.btnDefaultConfig.Click += new System.EventHandler(this.btnDefaultConfig_Click);
            // 
            // chkDoubleResolution
            // 
            this.chkDoubleResolution.AutoSize = true;
            this.chkDoubleResolution.Location = new System.Drawing.Point(78, 166);
            this.chkDoubleResolution.Name = "chkDoubleResolution";
            this.chkDoubleResolution.Size = new System.Drawing.Size(108, 17);
            this.chkDoubleResolution.TabIndex = 18;
            this.chkDoubleResolution.Text = "Double resolution";
            this.chkDoubleResolution.UseVisualStyleBackColor = true;
            this.chkDoubleResolution.CheckedChanged += new System.EventHandler(this.chkDoubleResolution_CheckedChanged);
            // 
            // GameInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkDoubleResolution);
            this.Controls.Add(this.btnDefaultConfig);
            this.Controls.Add(this.chkConfig);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.grpDisc);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.nudGameId);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nudPlayers);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nudYear);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPublisher);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtGameName);
            this.Name = "GameInfo";
            this.Size = new System.Drawing.Size(277, 650);
            ((System.ComponentModel.ISupportInitialize)(this.nudYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPlayers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGameId)).EndInit();
            this.grpDisc.ResumeLayout(false);
            this.grpDisc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtGameName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPublisher;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudYear;
        private System.Windows.Forms.NumericUpDown nudPlayers;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudGameId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.GroupBox grpDisc;
        private System.Windows.Forms.TextBox txtBaseName;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkConfig;
        private System.Windows.Forms.Button btnDefaultConfig;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.CheckBox chkDoubleResolution;
    }
}
