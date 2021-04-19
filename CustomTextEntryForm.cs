using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnipForMammal
{
    public partial class CustomTextEntryForm : Form
    {
        public CustomTextEntryForm()
        {
            InitializeComponent();
        }

        private void button_SetCustomText_Click(object sender, EventArgs e)
        {
            Global.spotify.CustomTrack(textBox_EnterCustomText.Text);
            Global.IsTextOverriden = true;
            this.Hide();
        }

        private void CustomTextEntryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void CustomTextEntryForm_Shown(object sender, EventArgs e)
        {
            textBox_EnterCustomText.Focus();
        }
    }
}
