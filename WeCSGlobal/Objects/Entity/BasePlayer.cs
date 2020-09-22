using SharpDX;
using System;
using System.Security.Cryptography.X509Certificates;
using WeScript.SDK.Utils;
using static WeCSGlobal.Memory.Memory;
using static WeCSGlobal.Memory.Signature;
using static WeCSGlobal.Objects.Engine;

namespace WeCSGlobal.Objects.Entity
{
    static class BasePlayer
    {
        public static IntPtr LocalPlayerPtr
        {
            get
            {
                return WeScriptWrapper.Memory.ReadPointer(csHandle, (IntPtr)(dwLocalPlayer_Offs.ToInt64() + 4), isWow64Process);
            }
        }

        /*public static int GetViewModelIndex(int index)
        {
            return WeScriptWrapper.Memory.ReadInt32(csHandle, (IntPtr)LocalPlayerPtr + m_hViewModel + index * 0x4) & 0xFFF;
        }*/

        public static int Team
        {
            get
            {
                return WeScriptWrapper.Memory.ReadInt32(csHandle, (IntPtr)LocalPlayerPtr + 0xF4);
            }
        }

        public static int Flags
        {
            get
            {
                return WeScriptWrapper.Memory.ReadInt32(csHandle, (IntPtr)LocalPlayerPtr + 0x104);
            }
        }

        public static int CrosshairID
        {
            get
            {
                return WeScriptWrapper.Memory.ReadInt32(csHandle, (IntPtr)LocalPlayerPtr + 0xB3E4);
            }
        }

        public static Vector2 CrosshairAim
        {
            get
            {
                return WeScriptWrapper.Memory.ReadVector2(csHandle, (IntPtr)LocalPlayerPtr + 0xB3E4);
            }
        }

        public static bool isPlayerMoving()
        {
            Vector3 playerVel = WeScriptWrapper.Memory.ReadVector3(csHandle, (IntPtr) LocalPlayerPtr + 0x114);
                int vel = (int)(playerVel.X + playerVel.Y + playerVel.Z);
                if (vel != 0)
                    return true;
                else
                    return false;
        }

        public static Vector3 VectorOrigin
        {
            get
            {
                return WeScriptWrapper.Memory.ReadVector3(csHandle, (IntPtr)LocalPlayerPtr + 0x138);
            }
        }

        public static Vector2 VectorOrigin2D
        {
            get
            {
                return WeScriptWrapper.Memory.ReadVector2(csHandle, (IntPtr)LocalPlayerPtr + 0x138);
            }
        }


        public static Vector3 VectorEyeLevel
        {
            get
            {
                return VectorOrigin + WeScriptWrapper.Memory.ReadVector3(csHandle, (IntPtr)LocalPlayerPtr + 0x108);
            }
        }

        public static Vector3 ViewPunchAngle
        {
            get
            {
                return WeScriptWrapper.Memory.ReadVector3(csHandle, (IntPtr)LocalPlayerPtr + 0x302C);
            }
        }

        /*public static int ActiveWeaponIndex
        {
            get
            {
                return Memory.Read<int>(LocalPlayerPtr + m_hActiveWeapon) & 0xFFF;
            }
        }*/

        public static float FlashAlpha
        {
            set
            {
                WeScriptWrapper.Memory.WriteFloat(csHandle, (IntPtr)LocalPlayerPtr + 0xA41C, value);
            }
        }
    }
}
