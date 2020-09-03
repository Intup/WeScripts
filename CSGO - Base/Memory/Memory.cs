using SharpDX;
using System;
//This code comes from the example given by the admin Feretorix
//Github: https://github.com/WeScript/WeScript.Assemblies
//Forum: https://wescript.app/topic/8-wescriptapp-csgoexample-assembly/
namespace BaseCSGO.Memory
{
    class Memory
    {
        public static float M_PI_F = (180.0f / Convert.ToSingle(System.Math.PI));
        public static IntPtr processHandle = IntPtr.Zero; //processHandle variable used by OpenProcess (once)
        public static bool gameProcessExists = false; //avoid drawing if the game process is dead, or not existent
        public static bool isWow64Process = false; //we all know the game is 32bit, but anyway...
        public static bool isGameOnTop = false; //we should avoid drawing while the game is not set on top
        public static bool isOverlayOnTop = false; //we might allow drawing visuals, while the user is working with the "menu"
        public static uint PROCESS_ALL_ACCESS = 0x1FFFFF; //hardcoded access right to OpenProcess
        public static IntPtr client_panorama = IntPtr.Zero;
        public static IntPtr client_panorama_size = IntPtr.Zero;
        public static IntPtr engine_dll = IntPtr.Zero;
        public static IntPtr engine_dll_size = IntPtr.Zero;
        public static Vector2 wndMargins = new Vector2(0, 0); //if the game window is smaller than your desktop resolution, you should avoid drawing outside of it
        public static Vector2 wndSize = new Vector2(0, 0); //get the size of the game window ... to know where to draw
        public static void OnTick(int counter, EventArgs args)
        {
            if (processHandle == IntPtr.Zero) //if we still don't have a handle to the process
            {
                var wndHnd = WeScriptWrapper.Memory.FindWindowName("Counter-Strike: Global Offensive"); //try finding the window of the process (check if it's spawned and loaded)
                if (wndHnd != IntPtr.Zero) //if it exists
                {
                    var calcPid = WeScriptWrapper.Memory.GetPIDFromHWND(wndHnd); //get the PID of that same process
                    if (calcPid > 0) //if we got the PID
                    {
                        processHandle = WeScriptWrapper.Memory.OpenProcess(PROCESS_ALL_ACCESS, calcPid); //get full access to the process so we can use it later
                        if (processHandle != IntPtr.Zero)
                        {
                            //if we got access to the game, check if it's x64 bit, this is needed when reading pointers, since their size is 4 for x86 and 8 for x64
                            isWow64Process = WeScriptWrapper.Memory.IsProcess64Bit(processHandle);
                        }
                    }
                }
            }
            else //else we have a handle, lets check if we should close it, or use it
            {
                var wndHnd = WeScriptWrapper.Memory.FindWindowName("Counter-Strike: Global Offensive");
                if (wndHnd != IntPtr.Zero) //window still exists, so handle should be valid? let's keep using it
                {
                    //the lines of code below execute every 33ms outside of the renderer thread, heavy code can be put here if it's not render dependant
                    gameProcessExists = true;
                    wndMargins = WeScriptWrapper.Renderer.GetWindowMargins(wndHnd);
                    wndSize = WeScriptWrapper.Renderer.GetWindowSize(wndHnd);
                    isGameOnTop = WeScriptWrapper.Renderer.IsGameOnTop(wndHnd);
                    isOverlayOnTop = WeScriptWrapper.Overlay.IsOnTop();

                    if (client_panorama == IntPtr.Zero) //if the dll is still null
                    {
                        client_panorama = WeScriptWrapper.Memory.GetModule(processHandle, "client.dll", isWow64Process); //attempt to find the module (if it's loaded)
                    }
                    else
                    {
                        if (client_panorama_size == IntPtr.Zero) //dll got loaded, check if size is zero
                        {
                            client_panorama_size = WeScriptWrapper.Memory.GetModuleSize(processHandle, "client.dll", isWow64Process); //get module size
                        }
                    }
                    if (engine_dll == IntPtr.Zero)
                    {
                        engine_dll = WeScriptWrapper.Memory.GetModule(processHandle, "engine.dll", isWow64Process);
                    }
                    else
                    {
                        if (engine_dll_size == IntPtr.Zero)
                        {
                            engine_dll_size = WeScriptWrapper.Memory.GetModuleSize(processHandle, "engine.dll", isWow64Process);
                        }
                    }
                }
                else //else most likely the process is dead, clean up
                {
                    WeScriptWrapper.Memory.CloseHandle(processHandle); //close the handle to avoid leaks
                    processHandle = IntPtr.Zero; //set it like this just in case for C# logic
                    gameProcessExists = false;

                    //clear your offsets, modules
                    client_panorama = IntPtr.Zero;
                    engine_dll = IntPtr.Zero;
                    client_panorama_size = IntPtr.Zero;
                    engine_dll_size = IntPtr.Zero;

                }
            }
        }
    }
}
