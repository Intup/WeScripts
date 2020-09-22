using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeCSGlobal.Memory
{
    class Address
    {
        public static IntPtr m_bIsDefusing = (IntPtr)0x3930;
        public static IntPtr m_iHealth = (IntPtr)0x100;
        public static IntPtr m_iTeamNum = (IntPtr)0xF4;
        public static IntPtr m_iGlowIndex = (IntPtr)0xA438;
        public static IntPtr GlowObjectManager = (IntPtr)0x5296FB0;
        public static IntPtr model_ambient_min = (IntPtr)0x58CE4C;
    }
}
