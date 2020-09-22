using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeScriptWrapper;
using WeScript.SDK.UI;
using WeScript.SDK.UI.Components;
using WeScript.SDK.Utils;
using static WeCSGlobal.Memory.Signature;
using System.Runtime.InteropServices;

namespace WeCSGlobal.Memory
{
    class Memory
    {
        public static IntPtr csHandle = IntPtr.Zero;
        public static bool ProcessExists = false;
        public static bool isWow64Process = false;
        public static bool isGameOnTop = false;
        public static bool isOverlayOnTop = false;
        public static uint PROCESS_ALL_ACCESS = 0x1FFFFF;
        public static int m_iNumberOfBytesWritten = 0;
        public static SharpDX.Vector2 wndMargins = new SharpDX.Vector2(0, 0); 
        public static SharpDX.Vector2 wndSize = new SharpDX.Vector2(0, 0);
        public static SharpDX.Vector2 GameCenterPos = new SharpDX.Vector2(0, 0); 
        internal static void OnTick(int counter, EventArgs args)
        {
            Console.WriteLine("Main Ontick");
            if (csHandle == IntPtr.Zero)
            {
                var WindowsHandle = WeScriptWrapper.Memory.FindWindowName("Counter-Strike: Global Offensive");
                if (WindowsHandle != IntPtr.Zero)
                {
                    var Pid = WeScriptWrapper.Memory.GetPIDFromHWND(WindowsHandle);
                    if (Pid > 0)
                    {
                        csHandle = WeScriptWrapper.Memory.OpenProcess(PROCESS_ALL_ACCESS, Pid);
                        if (csHandle != IntPtr.Zero)
                        {
                            isWow64Process = WeScriptWrapper.Memory.IsProcess64Bit(csHandle);
                        }
                    }
                }
            }
            else
            {
                var wndHnd = WeScriptWrapper.Memory.FindWindowName("Counter-Strike: Global Offensive");
                if (wndHnd != IntPtr.Zero)
                {

                    ProcessExists = true;
                    wndMargins = Renderer.GetWindowMargins(wndHnd);
                    wndSize = Renderer.GetWindowSize(wndHnd);
                    isGameOnTop = Renderer.IsGameOnTop(wndHnd);
                    isOverlayOnTop = Overlay.IsOnTop();
                    GameCenterPos = new SharpDX.Vector2(wndSize.X / 2 + wndMargins.X, wndSize.Y / 2 + wndMargins.Y);

                    if (client_panorama == IntPtr.Zero)
                    {
                        client_panorama = WeScriptWrapper.Memory.GetModule(csHandle, "client.dll", isWow64Process);
                    }
                    else
                    {
                        if (client_panorama_size == IntPtr.Zero)
                        {
                            client_panorama_size = WeScriptWrapper.Memory.GetModuleSize(csHandle, "client.dll", isWow64Process);
                        }
                        else
                        {
                            ClientDLL();
                        }
                    }
                    if (engine_dll == IntPtr.Zero)
                    {
                        engine_dll = WeScriptWrapper.Memory.GetModule(csHandle, "engine.dll", isWow64Process);
                    }
                    else
                    {
                        if (engine_dll_size == IntPtr.Zero)
                        {
                            engine_dll_size = WeScriptWrapper.Memory.GetModuleSize(csHandle, "engine.dll", isWow64Process);
                        }
                        else
                        {
                            EngineDLL();
                        }
                    }
                }
                else
                {
                    WeScriptWrapper.Memory.CloseHandle(csHandle);
                    csHandle = IntPtr.Zero;
                    ProcessExists = false;

                    client_panorama = IntPtr.Zero;
                    engine_dll = IntPtr.Zero;
                    client_panorama_size = IntPtr.Zero;
                    engine_dll_size = IntPtr.Zero;
                    dwViewMatrix_Offs = IntPtr.Zero;
                    dwEntityList_Offs = IntPtr.Zero;
                    dwLocalPlayer_Offs = IntPtr.Zero;
                    dwSetViewAng_Addr = IntPtr.Zero;
                    dwSetViewAng_Offs = IntPtr.Zero;
                    dwClientState_State = IntPtr.Zero;
                    ClientState_Off = IntPtr.Zero;
                    dwClientState_MaxPlayer = IntPtr.Zero;
                    dwGlowObjectManager = IntPtr.Zero;
                    clientstate_delta_ticks = IntPtr.Zero;
                    dwClientState_ViewAngles = IntPtr.Zero;
                    dwForceAttack = IntPtr.Zero;
                    dwForceAttack2 = IntPtr.Zero;
                    dwForceJump = IntPtr.Zero;
                }
            }
        }

    }
}
