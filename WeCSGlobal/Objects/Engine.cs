using System;
using static WeCSGlobal.Memory.Memory;
using static WeCSGlobal.Memory.Signature;

namespace WeCSGlobal.Objects
{
    class Engine
    {

        public static int ClientStatePtr
        {
            get
            {
                return WeScriptWrapper.Memory.ReadInt32(csHandle, (IntPtr)(engine_dll.ToInt64() + 0x589DD4));
            }
        }

        public static int MaxPlayer
        {
            get
            {
                return WeScriptWrapper.Memory.ReadInt32(csHandle, (IntPtr)(ClientStatePtr + 0x388));
            }
        }

        public static bool InGame
        {
            get
            {
                return WeScriptWrapper.Memory.ReadInt32(csHandle, (IntPtr)(ClientStatePtr + 0x108)) == 6;
            }
        }

        public static int ForceReload
        {
            get
            {
                return WeScriptWrapper.Memory.ReadInt32(csHandle, (IntPtr)(ClientStatePtr + 0x174));
            }
            set
            {
                WeScriptWrapper.Memory.WriteInt32(csHandle, (IntPtr)(ClientStatePtr + 0x174), value);
            }
        }

        public static float ModelAmbientIntensity
        {
            get
            {
                return WeScriptWrapper.Memory.ReadInt32(csHandle, (IntPtr)(engine_dll.ToInt64() + 0x58CE4C));
            }
            set
            {
                int ptr = WeScriptWrapper.Memory.ReadInt32(csHandle, (IntPtr)engine_dll.ToInt64() + 0x58CE4C - 0x2C);
                int convertedBrightness = BitConverter.ToInt32(BitConverter.GetBytes(value), 0);
                int xored = convertedBrightness ^ ptr;
                WeScriptWrapper.Memory.WriteInt32(csHandle, (IntPtr)(engine_dll.ToInt64() + 0x58CE4C), xored);
            }
        }

    }
}
