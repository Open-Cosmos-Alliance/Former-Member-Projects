using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cosmos.Hardware;

namespace FlowDOS.Graphics.Windows
{
    abstract class Control
    {
        abstract public string Name { get; set; }
        abstract public Size Size { get; set; }
        abstract public Point Location { get; set; }

        abstract public void Draw(ref VGAScreen scr);

        /*abstract public void OnClick();

        abstract public void OnReleaseClick();

        abstract public void KeyPress(ConsoleKey key);*/
    }
}
