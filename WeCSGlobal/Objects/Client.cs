using System;
using SharpDX;
using WeScript.SDK.Utils;
using static WeCSGlobal.Memory.Memory;
using static WeCSGlobal.Memory.Signature;
using static WeCSGlobal.Objects.Engine;

namespace WeCSGlobal.Objects
{
    static class Client
    {
        public static bool ForceAttack(bool shoot) => WeScriptWrapper.Memory.WriteInt32(csHandle, (IntPtr)(dwForceAttack.ToInt64()), shoot ? 5 : 4);

        public static void ForceRightAttack(bool trigger) => WeScriptWrapper.Memory.WriteInt32(csHandle, (IntPtr)(dwForceAttack.ToInt64()), trigger ? 5 : 4);

        public static void ForceJump(bool jump) => WeScriptWrapper.Memory.WriteInt32(csHandle, (IntPtr)(client_panorama.ToInt64() + 0x51F8E14), jump ? 5 : 4);
        public static void ForceForward(bool Forward) => WeScriptWrapper.Memory.WriteInt32(csHandle, (IntPtr)(client_panorama.ToInt64() + 0x3180754), Forward ? 5 : 4);

    }
}
