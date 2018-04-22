using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cosmos.Hardware;

namespace FlowDOS.Graphics.Windows.Controls
{
    class TextBox : Control
    {
        public override string Name { get; set; }
        public override Size Size { get; set; }
        public override Point Location { get; set; }
        public bool IsActive { get; set; }
        public string Text { get; set; }

        public override void Draw(ref VGAScreen scr)
        {
            if (!IsActive)
            {
                GraphFuncs.drawRect(ref scr, (uint)this.Location.x, (uint)this.Location.y, (uint)this.Size.Width, (uint)this.Size.Height, (uint)144);
                GraphFuncs.drawRect(ref scr, (uint)this.Location.x + 1, (uint)this.Location.y + 1, (uint)this.Size.Width - 2, (uint)this.Size.Height - 2, (uint)0);
            }
            else
            {

            }
        }

        public void OnClick()
        {
            IsActive = true;
        }
        public void OnReleaseClick()
        {
            throw new NotImplementedException();
        }

        public void KeyPress(ConsoleKey key)
        {
        }
    }
}
