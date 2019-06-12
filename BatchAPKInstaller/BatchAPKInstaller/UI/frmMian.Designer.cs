namespace BatchAPKInstaller.UI
{
    partial class frmMian
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMian));
            this.lsvSoftware = new System.Windows.Forms.ListView();
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.lblPhoneName = new System.Windows.Forms.Label();
            this.cmdStart = new System.Windows.Forms.Button();
            this.pbMain = new System.Windows.Forms.ProgressBar();
            this.lstResult = new System.Windows.Forms.ListBox();
            this.cmdOption = new System.Windows.Forms.Button();
            this.fbdMain = new System.Windows.Forms.FolderBrowserDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPath = new System.Windows.Forms.Label();
            this.tmCheck = new System.Windows.Forms.Timer(this.components);
            this.cmdReset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lsvSoftware
            // 
            this.lsvSoftware.CheckBoxes = true;
            this.lsvSoftware.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.columnHeader1});
            this.lsvSoftware.FullRowSelect = true;
            this.lsvSoftware.GridLines = true;
            this.lsvSoftware.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lsvSoftware.Location = new System.Drawing.Point(12, 41);
            this.lsvSoftware.MultiSelect = false;
            this.lsvSoftware.Name = "lsvSoftware";
            this.lsvSoftware.Size = new System.Drawing.Size(760, 346);
            this.lsvSoftware.TabIndex = 0;
            this.lsvSoftware.UseCompatibleStateImageBehavior = false;
            this.lsvSoftware.View = System.Windows.Forms.View.Details;
            // 
            // chName
            // 
            this.chName.Text = "Software Name";
            this.chName.Width = 600;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Status";
            this.columnHeader1.Width = 130;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Connected Phone:";
            // 
            // lblPhoneName
            // 
            this.lblPhoneName.AutoSize = true;
            this.lblPhoneName.ForeColor = System.Drawing.Color.Maroon;
            this.lblPhoneName.Location = new System.Drawing.Point(119, 17);
            this.lblPhoneName.Name = "lblPhoneName";
            this.lblPhoneName.Size = new System.Drawing.Size(71, 12);
            this.lblPhoneName.TabIndex = 3;
            this.lblPhoneName.Text = "No Connect!";
            // 
            // cmdStart
            // 
            this.cmdStart.Location = new System.Drawing.Point(672, 527);
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(100, 23);
            this.cmdStart.TabIndex = 7;
            this.cmdStart.Text = "&Start";
            this.cmdStart.UseVisualStyleBackColor = true;
            this.cmdStart.Click += new System.EventHandler(this.cmdStart_Click);
            // 
            // pbMain
            // 
            this.pbMain.Location = new System.Drawing.Point(12, 527);
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(640, 23);
            this.pbMain.TabIndex = 8;
            // 
            // lstResult
            // 
            this.lstResult.FormattingEnabled = true;
            this.lstResult.ItemHeight = 12;
            this.lstResult.Location = new System.Drawing.Point(12, 398);
            this.lstResult.Name = "lstResult";
            this.lstResult.Size = new System.Drawing.Size(760, 88);
            this.lstResult.TabIndex = 9;
            // 
            // cmdOption
            // 
            this.cmdOption.Location = new System.Drawing.Point(672, 498);
            this.cmdOption.Name = "cmdOption";
            this.cmdOption.Size = new System.Drawing.Size(100, 23);
            this.cmdOption.TabIndex = 10;
            this.cmdOption.Text = "&Choose";
            this.cmdOption.UseVisualStyleBackColor = true;
            this.cmdOption.Click += new System.EventHandler(this.cmdOption_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 503);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "APK Files Path:";
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblPath.Location = new System.Drawing.Point(107, 503);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(0, 12);
            this.lblPath.TabIndex = 12;
            // 
            // tmCheck
            // 
            this.tmCheck.Interval = 3000;
            this.tmCheck.Tick += new System.EventHandler(this.tmCheck_Tick);
            // 
            // cmdReset
            // 
            this.cmdReset.Enabled = false;
            this.cmdReset.Location = new System.Drawing.Point(697, 12);
            this.cmdReset.Name = "cmdReset";
            this.cmdReset.Size = new System.Drawing.Size(75, 23);
            this.cmdReset.TabIndex = 13;
            this.cmdReset.Text = "&Reset";
            this.cmdReset.UseVisualStyleBackColor = true;
            this.cmdReset.Click += new System.EventHandler(this.cmdReset_Click);
            // 
            // frmMian
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.cmdReset);
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmdOption);
            this.Controls.Add(this.lstResult);
            this.Controls.Add(this.pbMain);
            this.Controls.Add(this.cmdStart);
            this.Controls.Add(this.lblPhoneName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lsvSoftware);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMian";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Batch APK Installer";
            this.Load += new System.EventHandler(this.frmMian_Load);
            this.Shown += new System.EventHandler(this.frmMian_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lsvSoftware;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPhoneName;
        private System.Windows.Forms.Button cmdStart;
        private System.Windows.Forms.ProgressBar pbMain;
        private System.Windows.Forms.ListBox lstResult;
        private System.Windows.Forms.Button cmdOption;
        private System.Windows.Forms.FolderBrowserDialog fbdMain;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.Timer tmCheck;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button cmdReset;
    }
}