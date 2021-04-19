
namespace SnipForMammal
{
    partial class CustomTextEntryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomTextEntryForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox_EnterCustomText = new System.Windows.Forms.TextBox();
            this.button_SetCustomText = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.Controls.Add(this.textBox_EnterCustomText, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_SetCustomText, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(514, 65);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // textBox_EnterCustomText
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.textBox_EnterCustomText, 3);
            this.textBox_EnterCustomText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_EnterCustomText.Location = new System.Drawing.Point(3, 3);
            this.textBox_EnterCustomText.Name = "textBox_EnterCustomText";
            this.textBox_EnterCustomText.Size = new System.Drawing.Size(508, 20);
            this.textBox_EnterCustomText.TabIndex = 0;
            // 
            // button_SetCustomText
            // 
            this.button_SetCustomText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_SetCustomText.Location = new System.Drawing.Point(174, 33);
            this.button_SetCustomText.Name = "button_SetCustomText";
            this.button_SetCustomText.Size = new System.Drawing.Size(165, 29);
            this.button_SetCustomText.TabIndex = 1;
            this.button_SetCustomText.Text = "Set Custom Text";
            this.button_SetCustomText.UseVisualStyleBackColor = true;
            this.button_SetCustomText.Click += new System.EventHandler(this.button_SetCustomText_Click);
            // 
            // CustomTextEntryForm
            // 
            this.AcceptButton = this.button_SetCustomText;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 65);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomTextEntryForm";
            this.Text = "Snip For Mammal - Enter Custom Text";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CustomTextEntryForm_FormClosing);
            this.Shown += new System.EventHandler(this.CustomTextEntryForm_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox textBox_EnterCustomText;
        private System.Windows.Forms.Button button_SetCustomText;
    }
}