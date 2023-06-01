using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

using OpenTK;
using OpenTK.Windowing.Desktop;

using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

using Fractals;
using Fractals.Base;
using Fractals.Visualization;
using OpenTK.Windowing.Common;

namespace Fractals.Drivers
{
    public class OpenTk_TestDriver : Driver
    {
        public override async ValueTask DriveAsync(string[] args)
        {
            var gwSettings = new GameWindowSettings();
            var nwSettings = new NativeWindowSettings();

            using var window = new TestWindow(gwSettings, nwSettings);

            window.Run();
        }
    }


    public class TestWindow : GameWindow
    {
        public TestWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        { 


            base.OnRenderFrame(args);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {


            base.OnUpdateFrame(args);
        }
    }
}
