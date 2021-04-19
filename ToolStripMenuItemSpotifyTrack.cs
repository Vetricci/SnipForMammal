using System.ComponentModel;
using System.Windows.Forms;

namespace SnipForMammal
{
    public class ToolStripMenuItemSpotifyTrack : System.Windows.Forms.ToolStripMenuItem
    {
        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks the item.")]
        public event MouseEventHandler MouseClick
        {
            add
            {
                onMouseClick += value;
            }
            remove
            {
                onMouseClick -= value;
            }
        }

        private MouseEventHandler onMouseClick;

        protected virtual void OnMouseClick(MouseEventArgs e)
        {
            onMouseClick?.Invoke(this, e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            onMouseClick?.Invoke(this, e);
        }

    }
}
