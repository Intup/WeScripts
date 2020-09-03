using System;
using static BaseCSGO.Memory.Memory;

namespace BaseCSGO.Player
{
    class CSPlayer
    {
        //Addrs and Ptr
        public static IntPtr dwClientState = (IntPtr)0x589DD4;
        public static IntPtr dwEntityList = (IntPtr)0x4D5442C;
        public static IntPtr LocalPlayer = (IntPtr)0xD3FC5C;
        public static IntPtr dwGlowObjectManager = (IntPtr)0x529C208;
        public static int Ptr
        {
            get
            {
                return WeScriptWrapper.Memory.ReadInt32(processHandle, (IntPtr)(engine_dll.ToInt64() + dwClientState.ToInt64()));
            }
        }

        public static int MaxPlayer
        {
            get
            {
                return WeScriptWrapper.Memory.ReadInt32(processHandle, (IntPtr)(Ptr + 0x388));
            }
        }

        public static int LocalPlayerPtr
        {
            get
            {
                return WeScriptWrapper.Memory.ReadInt32(processHandle, (IntPtr)(client_panorama.ToInt64() + LocalPlayer.ToInt64()));
            }
        }

        public static int Team
        {
            get
            {
                return WeScriptWrapper.Memory.ReadInt32(processHandle, (IntPtr)LocalPlayerPtr + 0xF4);
            }
        }
    }
}
