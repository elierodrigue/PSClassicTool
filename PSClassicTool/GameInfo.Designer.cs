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
            this.txtBaseName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPlayers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGameId)).BeginInit();
            this.grpDisc.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtGameName
            // 
            this.txtGameName.Location = new System.Drawing.Point(6, 16);
            this.txtGameName.Name = "txtGameName";
            this.txtGameName.Size = new System.Drawing.Size(262, 20);
            this.txtGameName.TabIndex = 0;
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
            this.nudPlayers.Size = new System.Drawing.Size(262, 20);
            this.nudPlayers.TabIndex = 8;
            this.nudPlayers.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
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
            this.btnDelete.Location = new System.Drawing.Point(6, 373);
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
            this.comboBox1.Location = new System.Drawing.Point(6, 215);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(262, 21);
            this.comboBox1.TabIndex = 12;
            // 
            // grpDisc
            // 
            this.grpDisc.Controls.Add(this.txtBaseName);
            this.grpDisc.Location = new System.Drawing.Point(6, 242);
            this.grpDisc.Name = "grpDisc";
            this.grpDisc.Size = new System.Drawing.Size(262, 125);
            this.grpDisc.TabIndex = 13;
            this.grpDisc.TabStop = false;
            this.grpDisc.Text = "Disc";
            // 
            // txtBaseName
            // 
            this.txtBaseName.Location = new System.Drawing.Point(6, 19);
            this.txtBaseName.Name = "txtBaseName";
            this.txtBaseName.Size = new System.Drawing.Size(250, 20);
            this.txtBaseName.TabIndex = 0;
            // 
            // GameInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
            this.Size = new System.Drawing.Size(277, 424);
            ((System.ComponentModel.ISupportInitialize)(this.nudYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPlayers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGameId)).EndInit();
            this.grpDisc.ResumeLayout(false);
            this.grpDisc.PerformLayout();
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
    }
}
