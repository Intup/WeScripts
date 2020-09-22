using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Direct3D9;
using SharpDX.Mathematics;
using SharpDX.XInput;
using WeScriptWrapper;
using WeScript.SDK.UI;
using WeScript.SDK.UI.Components;
using WeScript.SDK.Utils;

namespace WeCSGlobal
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Main");
            WeScriptWrapper.Memory.OnTick += Memory.Memory.OnTick;
            //Render
            Renderer.OnRenderer += Hacks.OnRenderer;
            //Menu
            Menu.Menu.LoadMenu();
        }
    }
}
