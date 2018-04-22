using System;
using Sys = Cosmos.System;

using WitchcraftOS.Witchcraftfx.Core;
using WitchcraftOS.Witchcraftfx.Textscreen;

namespace WitchcraftOS
{
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            MainLoop.Start();
        }
        protected override void Run()
        {
            MainLoop.Main();
        }
    }
}
