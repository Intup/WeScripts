using System;
using static WeCSGlobal.Memory.Memory;

namespace WeCSGlobal.Memory
{
    class Signature
    {
        public static IntPtr client_panorama = IntPtr.Zero;
        public static IntPtr client_panorama_size = IntPtr.Zero;
        public static IntPtr engine_dll = IntPtr.Zero;
        public static IntPtr engine_dll_size = IntPtr.Zero;
        public static IntPtr dwViewMatrix_Offs = IntPtr.Zero;
        public static IntPtr dwEntityList_Offs = IntPtr.Zero;
        public static IntPtr dwLocalPlayer_Offs = IntPtr.Zero;
        public static IntPtr dwSetViewAng_Addr = IntPtr.Zero;
        public static IntPtr dwSetViewAng_Offs = IntPtr.Zero;
        public static IntPtr ClientState_Off = IntPtr.Zero;
        public static IntPtr dwClientState_State = IntPtr.Zero;
        public static IntPtr dwGlowObjectManager = IntPtr.Zero;
        public static IntPtr dwClientState_MaxPlayer = IntPtr.Zero;
        public static IntPtr clientstate_delta_ticks = IntPtr.Zero;
        public static IntPtr dwClientState_ViewAngles = IntPtr.Zero;
        public static IntPtr dwForceAttack = IntPtr.Zero;
        public static IntPtr dwForceAttack2 = IntPtr.Zero;
        public static IntPtr dwForceJump = IntPtr.Zero;

        internal static void ClientDLL()
        {
            if (dwViewMatrix_Offs == IntPtr.Zero) 
            {
                dwViewMatrix_Offs = WeScriptWrapper.Memory.FindSignature(csHandle, client_panorama, client_panorama_size, "0F 10 05 ? ? ? ? 8D 85 ? ? ? ? B9", 0x3);
            }
            if (dwEntityList_Offs == IntPtr.Zero)
            {
                dwEntityList_Offs = WeScriptWrapper.Memory.FindSignature(csHandle, client_panorama, client_panorama_size, "BB ? ? ? ? 83 FF 01 0F 8C ? ? ? ? 3B F8", 0x1);
            }
            if (dwLocalPlayer_Offs == IntPtr.Zero)
            {
                dwLocalPlayer_Offs = WeScriptWrapper.Memory.FindSignature(csHandle, client_panorama, client_panorama_size, "42 56 8D 34 85 ? ? ? ? 89", 0x5);
            }
            if (dwGlowObjectManager == IntPtr.Zero)
            {
                dwGlowObjectManager = WeScriptWrapper.Memory.FindSignature(csHandle, client_panorama, client_panorama_size, "A1 ? ? ? ? A8 01 75 4B", 0x1);
            }
            if (dwForceAttack == IntPtr.Zero)
            {
                dwForceAttack = WeScriptWrapper.Memory.FindSignature(csHandle, client_panorama, client_panorama_size, "89 0D ? ? ? ? 8B 0D ? ? ? ? 8B F2 8B C1 83 CE 04", 0x2);
            }
            if (dwForceJump == IntPtr.Zero)
            {
                dwForceJump = WeScriptWrapper.Memory.FindSignature(csHandle, client_panorama, client_panorama_size, "8B 0D ? ? ? ? 8B D6 8B C1 83 CA 02", 0x2);
            }
        }

        internal static void EngineDLL()
        {
            if (dwSetViewAng_Addr == IntPtr.Zero)
            {
                dwSetViewAng_Addr = WeScriptWrapper.Memory.FindSignature(csHandle, engine_dll, engine_dll_size, "F3 0F 11 80 ? ? ? ? D9 46 04 D9 05", -0x4);
            }
            if (dwSetViewAng_Offs == IntPtr.Zero)
            {
                dwSetViewAng_Offs = WeScriptWrapper.Memory.FindSignature(csHandle, engine_dll, engine_dll_size, "F3 0F 11 80 ? ? ? ? D9 46 04 D9 05", 0x4);
            }
            if (ClientState_Off == IntPtr.Zero)
            {
                ClientState_Off = WeScriptWrapper.Memory.FindSignature(csHandle, engine_dll, engine_dll_size, "A1 ? ? ? ? 33 D2 6A 00 6A 00 33 C9 89 B0", 0x1);
            }
            if (dwClientState_State == IntPtr.Zero)
            {
                dwClientState_State = WeScriptWrapper.Memory.FindSignature(csHandle, engine_dll, engine_dll_size, "83 B8 ? ? ? ? ? 0F 94 C0 C3", 0x2);
            }
            if (dwClientState_MaxPlayer == IntPtr.Zero)
            {
                dwClientState_MaxPlayer = WeScriptWrapper.Memory.FindSignature(csHandle, engine_dll, engine_dll_size, "A1 ? ? ? ? 8B 80 ? ? ? ? C3 CC CC CC CC 55 8B EC 8A 45 08", 0x7);
            }
            if (clientstate_delta_ticks == IntPtr.Zero)
            {
                clientstate_delta_ticks = WeScriptWrapper.Memory.FindSignature(csHandle, engine_dll, engine_dll_size, "C7 87 ? ? ? ? ? ? ? ? FF 15 ? ? ? ? 83 C4 08", 0x2);
            }
            if (dwClientState_ViewAngles == IntPtr.Zero)
            {
                dwClientState_ViewAngles = WeScriptWrapper.Memory.FindSignature(csHandle, engine_dll, engine_dll_size, "F3 0F 11 80 ? ? ? ? D9 46 04 D9 05", 0x4);
            }
        }
    }
}
