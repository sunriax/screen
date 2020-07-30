using System;

namespace ScreenRotator
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.notifyIconDisplay = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.menuItemStart = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemStartQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHelpVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBoxRoom = new System.Windows.Forms.ComboBox();
            this.comboBoxDisplay = new System.Windows.Forms.ComboBox();
            this.comboBoxRotation = new System.Windows.Forms.ComboBox();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelVersionNr = new System.Windows.Forms.Label();
            this.timerQuery = new System.Windows.Forms.Timer(this.components);
            this.labelConnection = new System.Windows.Forms.Label();
            this.menuStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIconDisplay
            // 
            this.notifyIconDisplay.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIconDisplay.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconDisplay.Icon")));
            this.notifyIconDisplay.Text = "ScreenRotator";
            this.notifyIconDisplay.Visible = true;
            this.notifyIconDisplay.DoubleClick += new System.EventHandler(this.notifyIconDisplay_DoubleClick);
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemStart,
            this.menuItemHelp});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(384, 24);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "Main Menu";
            // 
            // menuItemStart
            // 
            this.menuItemStart.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemStartQuit});
            this.menuItemStart.Name = "menuItemStart";
            this.menuItemStart.Size = new System.Drawing.Size(43, 20);
            this.menuItemStart.Text = "Start";
            // 
            // menuItemStartQuit
            // 
            this.menuItemStartQuit.Name = "menuItemStartQuit";
            this.menuItemStartQuit.Size = new System.Drawing.Size(120, 22);
            this.menuItemStartQuit.Text = "Beenden";
            this.menuItemStartQuit.Click += new System.EventHandler(this.menuItemStartQuit_Click);
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.Name = "menuItemHelp";
            this.menuItemHelp.Size = new System.Drawing.Size(12, 20);
            // 
            // menuItemHelpVersion
            // 
            this.menuItemHelpVersion.Name = "menuItemHelpVersion";
            this.menuItemHelpVersion.Size = new System.Drawing.Size(32, 19);
            // 
            // comboBoxRoom
            // 
            this.comboBoxRoom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxRoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRoom.FormattingEnabled = true;
            this.comboBoxRoom.Location = new System.Drawing.Point(10, 30);
            this.comboBoxRoom.Name = "comboBoxRoom";
            this.comboBoxRoom.Size = new System.Drawing.Size(363, 23);
            this.comboBoxRoom.TabIndex = 1;
            // 
            // comboBoxDisplay
            // 
            this.comboBoxDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDisplay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDisplay.FormattingEnabled = true;
            this.comboBoxDisplay.Location = new System.Drawing.Point(10, 60);
            this.comboBoxDisplay.Name = "comboBoxDisplay";
            this.comboBoxDisplay.Size = new System.Drawing.Size(241, 23);
            this.comboBoxDisplay.TabIndex = 1;
            // 
            // comboBoxRotation
            // 
            this.comboBoxRotation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxRotation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRotation.FormattingEnabled = true;
            this.comboBoxRotation.Location = new System.Drawing.Point(260, 60);
            this.comboBoxRotation.Name = "comboBoxRotation";
            this.comboBoxRotation.Size = new System.Drawing.Size(113, 23);
            this.comboBoxRotation.TabIndex = 1;
            // 
            // labelVersion
            // 
            this.labelVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(260, 89);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(48, 15);
            this.labelVersion.TabIndex = 2;
            this.labelVersion.Text = "Version:";
            // 
            // labelVersionNr
            // 
            this.labelVersionNr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelVersionNr.AutoSize = true;
            this.labelVersionNr.Location = new System.Drawing.Point(314, 89);
            this.labelVersionNr.Name = "labelVersionNr";
            this.labelVersionNr.Size = new System.Drawing.Size(12, 15);
            this.labelVersionNr.TabIndex = 2;
            this.labelVersionNr.Text = "?";
            // 
            // timerQuery
            // 
            this.timerQuery.Interval = 1000;
            this.timerQuery.Tick += new System.EventHandler(this.timerQuery_Tick);
            // 
            // labelConnection
            // 
            this.labelConnection.AutoSize = true;
            this.labelConnection.Location = new System.Drawing.Point(10, 89);
            this.labelConnection.Name = "labelConnection";
            this.labelConnection.Size = new System.Drawing.Size(0, 15);
            this.labelConnection.TabIndex = 3;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 111);
            this.Controls.Add(this.labelConnection);
            this.Controls.Add(this.labelVersionNr);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.comboBoxRotation);
            this.Controls.Add(this.comboBoxDisplay);
            this.Controls.Add(this.comboBoxRoom);
            this.Controls.Add(this.menuStripMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(350, 150);
            this.Name = "FormMain";
            this.Text = "ScreenRotator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.NotifyIcon notifyIconDisplay;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem menuItemStart;
        private System.Windows.Forms.ToolStripMenuItem menuItemStartQuit;
        private System.Windows.Forms.ToolStripMenuItem menuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem menuItemHelpVersion;
        private System.Windows.Forms.ComboBox comboBoxRoom;
        private System.Windows.Forms.ComboBox comboBoxDisplay;
        private System.Windows.Forms.ComboBox comboBoxRotation;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelVersionNr;
        private System.Windows.Forms.Timer timerQuery;
        private System.Windows.Forms.Label labelConnection;
    }
}

