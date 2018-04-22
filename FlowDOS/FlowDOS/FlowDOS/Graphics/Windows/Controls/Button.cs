using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cosmos.Hardware;

namespace FlowDOS.Graphics.Windows.Controls
{
    class Button : Control
    {
        public override string Name { get; set; }
        public override Size Size { get; set; }
        public override Point Location { get; set; }
        public bool IsClicked { get; set; }
        public string Text { get; set; }
        public event ClickEventHandler Clicked;
        public event ReleaseClickEventHandler ReleaseClick;
        public event KeyPressEventHandler KeyPressed;

        public Button()
        {
            IsClicked = false;
        }

        public override void Draw(ref VGAScreen scr)
        {
            if (!IsClicked)
            {
                GraphFuncs.drawRect(ref scr, (uint)this.Location.x, (uint)this.Location.y, (uint)this.Size.Width, (uint)this.Size.Height, (uint)2);
                //scr.SetPixel320x200x8((uint)319, (uint)198, (uint)1);
            }
            else
            {
                GraphFuncs.drawRect(ref scr, (uint)this.Location.x, (uint)this.Location.y, (uint)this.Size.Width, (uint)this.Size.Height, (uint)7);
            }
        }

       

        public virtual void OnClick()
        {
            IsClicked = true;
            if (Clicked != null)
                Clicked();
        }

        protected  virtual void OnReleaseClick()
        {
            IsClicked = false;
            if (Clicked != null)
                Clicked();
        }



        protected  virtual void KeyPress(ConsoleKey key)
        {
            if (KeyPressed != null)
                KeyPressed(key);
        }

        public delegate void ClickEventHandler();
        public delegate void ReleaseClickEventHandler();
        public delegate void KeyPressEventHandler(ConsoleKey key);
    }
}
