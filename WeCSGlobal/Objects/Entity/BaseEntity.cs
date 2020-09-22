using System;
using SharpDX;
using WeScript.SDK.Utils;
using static WeCSGlobal.Memory.Memory;
using static WeCSGlobal.Memory.Signature;

namespace WeCSGlobal.Objects.Entity
{
    class EntityList
    {
        private BaseEntity[] entities = new BaseEntity[4096];

        public EntityList()
        {
            for (int i = 0; i < entities.Length; i++)
            {
                entities[i] = new BaseEntity(i);
            }
        }

        public BaseEntity this[int index]
        {
            get
            {
                try
                {
                    return entities[index];
                }
                catch
                {
                    return null;
                }
            }
        }
    }

    class BaseEntity
    {
        public int index;

        public BaseEntity(int index)
        {
            this.index = index;
        }

        public int Base
        {
            get
            {
                return WeScriptWrapper.Memory.ReadInt32(csHandle, (IntPtr)(client_panorama.ToInt64() + 0x4D4F1FC) + index * 0x10);
            }
        }

        public GlowObject GlowObject
        {
            get
            {
                return SDKUtil.ReadStructure<GlowObject>(csHandle, (IntPtr)GlowObject.Ptr + GlowIndex * 0x38);
            }
        }
        protected int GlowIndex
        {
            get
            {
                return WeScriptWrapper.Memory.ReadInt32(csHandle, (IntPtr)Base + 0xA438);
            }
        }


        public Vector3 VectorOrigin
        {
            get
            {
                return WeScriptWrapper.Memory.ReadVector3(csHandle, (IntPtr)Base + 0x138);
            }
        }

        public Vector2 VectorOrigin2D
        {
            get
            {
                return WeScriptWrapper.Memory.ReadVector2(csHandle, (IntPtr)Base + 0x138);
            }
        }
    }
}
