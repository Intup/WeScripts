using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeScriptWrapper;
using WeScript.SDK.UI;
using WeScript.SDK.UI.Components;
using WeScript.SDK.Utils;

using static WeCSGlobal.Menu.Menu;

namespace WeCSGlobal.Config
{
    class Aimbot
    {
        public static class cAimbot
        {
            public static readonly MenuBool AimEnable = new MenuBool("enable", "Enable Aimbot", true);
            public static readonly MenuBool AimSilent = new MenuBool("enablesielnt", "Enable Silent Aimbot", true);
            public static readonly MenuKeyBind AimKey = new MenuKeyBind("aimkey", "Aimbot HotKey", VirtualKeyCode.LeftMouse, KeybindType.Hold, false);
            //public static readonly MenuList AimPriority = new MenuList("aimpriority", "Aimbot Priority", new List<string>() { "Most Damage", "Closest Enemy" }, 0);
            //Spoot
            //public static readonly MenuBool Head = new MenuBool("headspoot", "Hitscan: Head", true);
            //public static readonly MenuBool Body = new MenuBool("Bodyspoot", "Hitscan: Body", true);
            //public static readonly MenuBool Shoulders = new MenuBool("Shouldersspoot", "Hitscan: Shoulders", true);
            //public static readonly MenuBool Legs = new MenuBool("Legsspoot", "Hitscan: Legs", true);
            //public static readonly MenuBool Toes = new MenuBool("Toesspoot", "Hitscan: Toes", true);
            //Rage
            //public static readonly MenuBool scope = new MenuBool("scoope", "Enable Auto-Scope", true);
            public static readonly MenuList PriorityBone = new MenuList("prioritybone", "Prioritize bone", new List<string>() { "Head", "Body" }, 0);
            public static readonly MenuSlider AimFov = new MenuSlider("aimfov", "Aimbot FOV", 30, 1, 1000);
            //public static readonly MenuBool AimBody = new MenuBool("body", "Body aim with AWP", true);
        }
    }
}
