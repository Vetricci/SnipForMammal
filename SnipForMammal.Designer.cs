
namespace SnipForMammal
{
    partial class SnipForMammal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SnipForMammal));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_Version = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_TrackHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_CustomText = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_Settings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_ForceUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_RestartSpotify = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Snip For Mammal";
            this.notifyIcon.Visible = true;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Version,
            this.toolStripSeparator1,
            this.toolStripMenuItem_TrackHistory,
            this.toolStripMenuItem_CustomText,
            this.toolStripSeparator2,
            this.toolStripMenuItem_Settings,
            this.toolStripMenuItem_ForceUpdate,
            this.toolStripMenuItem_RestartSpotify,
            this.toolStripSeparator3,
            this.toolStripMenuItem_Exit});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(170, 176);
            // 
            // toolStripMenuItem_Version
            // 
            this.toolStripMenuItem_Version.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItem_Version.Enabled = false;
            this.toolStripMenuItem_Version.Name = "toolStripMenuItem_Version";
            this.toolStripMenuItem_Version.Size = new System.Drawing.Size(169, 22);
            this.toolStripMenuItem_Version.Text = "Snip for Mammal ";
            this.toolStripMenuItem_Version.ToolTipText = "Program version";
            this.toolStripMenuItem_Version.Click += new System.EventHandler(this.toolStripMenuItem_Version_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(166, 6);
            // 
            // toolStripMenuItem_TrackHistory
            // 
            this.toolStripMenuItem_TrackHistory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItem_TrackHistory.Name = "toolStripMenuItem_TrackHistory";
            this.toolStripMenuItem_TrackHistory.Size = new System.Drawing.Size(169, 22);
            this.toolStripMenuItem_TrackHistory.Text = "Track History";
            // 
            // toolStripMenuItem_CustomText
            // 
            this.toolStripMenuItem_CustomText.Name = "toolStripMenuItem_CustomText";
            this.toolStripMenuItem_CustomText.Size = new System.Drawing.Size(169, 22);
            this.toolStripMenuItem_CustomText.Text = "Custom Text";
            this.toolStripMenuItem_CustomText.Click += new System.EventHandler(this.toolStripMenuItem_CustomText_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(166, 6);
            // 
            // toolStripMenuItem_Settings
            // 
            this.toolStripMenuItem_Settings.Name = "toolStripMenuItem_Settings";
            this.toolStripMenuItem_Settings.Size = new System.Drawing.Size(169, 22);
            this.toolStripMenuItem_Settings.Text = "Settings";
            this.toolStripMenuItem_Settings.Click += new System.EventHandler(this.toolStripMenuItemSettings_Click);
            // 
            // toolStripMenuItem_ForceUpdate
            // 
            this.toolStripMenuItem_ForceUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItem_ForceUpdate.Name = "toolStripMenuItem_ForceUpdate";
            this.toolStripMenuItem_ForceUpdate.Size = new System.Drawing.Size(169, 22);
            this.toolStripMenuItem_ForceUpdate.Text = "Force Update";
            this.toolStripMenuItem_ForceUpdate.Click += new System.EventHandler(this.toolStripMenuItem_ForceUpdate_Click);
            // 
            // toolStripMenuItem_RestartSpotify
            // 
            this.toolStripMenuItem_RestartSpotify.Name = "toolStripMenuItem_RestartSpotify";
            this.toolStripMenuItem_RestartSpotify.Size = new System.Drawing.Size(169, 22);
            this.toolStripMenuItem_RestartSpotify.Text = "Restart Spotify";
            this.toolStripMenuItem_RestartSpotify.Click += new System.EventHandler(this.toolStripMenuItem_RestartSpotify_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(166, 6);
            // 
            // toolStripMenuItem_Exit
            // 
            this.toolStripMenuItem_Exit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItem_Exit.Name = "toolStripMenuItem_Exit";
            this.toolStripMenuItem_Exit.Size = new System.Drawing.Size(169, 22);
            this.toolStripMenuItem_Exit.Text = "Exit";
            this.toolStripMenuItem_Exit.Click += new System.EventHandler(this.toolStripMenuItem_Exit_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(89, 43);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // SnipForMammal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 78);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SnipForMammal";
            this.Text = "Snip For Mammal";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SnipForMammal_FormClosing);
            this.Load += new System.EventHandler(this.SnipForMammal_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Version;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_TrackHistory;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ForceUpdate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Exit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Settings;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_CustomText;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_RestartSpotify;
        private System.Windows.Forms.Button button1;
    }
}

