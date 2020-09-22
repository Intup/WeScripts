using System;
using SharpDX;
using WeScript.SDK.Utils;
using static WeCSGlobal.Memory.Memory;
using static WeCSGlobal.Memory.Signature;
using static WeCSGlobal.Objects.Engine;

namespace WeCSGlobal.Objects.Entity
{
    class Player : BaseEntity
    {
        public Player(int index) : base(index)
        {
        }

        public Player(BaseEntity baseEnt) : base(baseEnt.index)
        {
        }

        public int Team
        {
            get
            {
                return WeScriptWrapper.Memory.ReadInt32(csHandle, (IntPtr)Base + 0xF4);
            }
        }

        public int Health
        {
            get
            {
                return WeScriptWrapper.Memory.ReadInt32(csHandle, (IntPtr)Base + 0x100);
            }
        }

        public bool Dormant
        {
            get
            {
                return WeScriptWrapper.Memory.ReadInt32(csHandle, (IntPtr)Base + 0xED) == 1;
            }
        }

        public bool Spotted
        {
            get
            {
                return WeScriptWrapper.Memory.ReadInt32(csHandle, (IntPtr)Base + 0x93D) == 1;
            }

            set
            {
                WeScriptWrapper.Memory.WriteInt32(csHandle, (IntPtr)Base + 0x93D, value ? 1 : 0);
            }
        }

        public Vector3 GetBonePosition(int boneIndex)
        {
            var matrix = WeScriptWrapper.Memory.ReadPointer(csHandle, (IntPtr)(Base + 0x26A8), isWow64Process); //m_dwBoneMatrix

            Vector3 bonePos = new Vector3();

            bonePos.Z = WeScriptWrapper.Memory.ReadFloat(csHandle, matrix + (0x30 * boneIndex) + 0x0C);
            bonePos.Y = WeScriptWrapper.Memory.ReadFloat(csHandle, matrix + (0x30 * boneIndex) + 0x1C);
            bonePos.Z = WeScriptWrapper.Memory.ReadFloat(csHandle, matrix + (0x30 * boneIndex) + 0x2C);

            return bonePos;
        }

        public RenderColor RenderColor
        {
            set
            {
                SDKUtil.ReadStructureEx<RenderColor>(csHandle, (IntPtr)Base + 0x70, isWow64Process);
            }
        }
    }
}
