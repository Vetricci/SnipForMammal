
namespace SnipForMammal
{
    partial class DebugConsole
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DebugConsole));
            this.TableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ConsoleTextBox = new System.Windows.Forms.RichTextBox();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItem_File = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_FileClose = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_FileOutput = new System.Windows.Forms.ToolStripMenuItem();
            this.TableLayoutPanel.SuspendLayout();
            this.MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // TableLayoutPanel
            // 
            this.TableLayoutPanel.ColumnCount = 4;
            this.TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TableLayoutPanel.Controls.Add(this.ConsoleTextBox, 0, 1);
            this.TableLayoutPanel.Controls.Add(this.MenuStrip, 0, 0);
            this.TableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.TableLayoutPanel.Name = "TableLayoutPanel";
            this.TableLayoutPanel.RowCount = 3;
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel.Size = new System.Drawing.Size(800, 450);
            this.TableLayoutPanel.TabIndex = 0;
            // 
            // ConsoleTextBox
            // 
            this.ConsoleTextBox.BackColor = System.Drawing.Color.DarkGray;
            this.ConsoleTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TableLayoutPanel.SetColumnSpan(this.ConsoleTextBox, 4);
            this.ConsoleTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConsoleTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConsoleTextBox.ForeColor = System.Drawing.Color.Black;
            this.ConsoleTextBox.Location = new System.Drawing.Point(3, 28);
            this.ConsoleTextBox.Name = "ConsoleTextBox";
            this.ConsoleTextBox.ReadOnly = true;
            this.TableLayoutPanel.SetRowSpan(this.ConsoleTextBox, 2);
            this.ConsoleTextBox.Size = new System.Drawing.Size(794, 419);
            this.ConsoleTextBox.TabIndex = 5;
            this.ConsoleTextBox.Text = "";
            // 
            // MenuStrip
            // 
            this.TableLayoutPanel.SetColumnSpan(this.MenuStrip, 4);
            this.MenuStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_File});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(800, 25);
            this.MenuStrip.TabIndex = 6;
            this.MenuStrip.Text = "menuStrip1";
            // 
            // ToolStripMenuItem_File
            // 
            this.ToolStripMenuItem_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_FileOutput,
            this.ToolStripMenuItem_FileClose});
            this.ToolStripMenuItem_File.Name = "ToolStripMenuItem_File";
            this.ToolStripMenuItem_File.Size = new System.Drawing.Size(37, 21);
            this.ToolStripMenuItem_File.Text = "File";
            // 
            // ToolStripMenuItem_FileClose
            // 
            this.ToolStripMenuItem_FileClose.Name = "ToolStripMenuItem_FileClose";
            this.ToolStripMenuItem_FileClose.Size = new System.Drawing.Size(180, 22);
            this.ToolStripMenuItem_FileClose.Text = "Close";
            this.ToolStripMenuItem_FileClose.Click += new System.EventHandler(this.ToolStripMenuItem_FileClose_Click);
            // 
            // ToolStripMenuItem_FileOutput
            // 
            this.ToolStripMenuItem_FileOutput.Name = "ToolStripMenuItem_FileOutput";
            this.ToolStripMenuItem_FileOutput.Size = new System.Drawing.Size(180, 22);
            this.ToolStripMenuItem_FileOutput.Text = "Output to File";
            this.ToolStripMenuItem_FileOutput.Click += new System.EventHandler(this.ToolStripMenuItem_FileOutput_Click);
            // 
            // DebugConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TableLayoutPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MenuStrip;
            this.Name = "DebugConsole";
            this.Text = "Snip For Mammal - Debug Console";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DebugConsole_FormClosing);
            this.TableLayoutPanel.ResumeLayout(false);
            this.TableLayoutPanel.PerformLayout();
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel TableLayoutPanel;
        private System.Windows.Forms.RichTextBox ConsoleTextBox;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_File;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_FileClose;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_FileOutput;
    }
}