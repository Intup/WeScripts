using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeScriptWrapper;
using WeScript.SDK.UI;
using WeScript.SDK.UI.Components;
using WeScript.SDK.Utils;

using static WeCSGlobal.Config.Aimbot;
using static WeCSGlobal.Config.TriggerBot;
using static WeCSGlobal.Config.Visual;
using static WeCSGlobal.Config.Misc;

namespace WeCSGlobal.Menu
{
    class Menu
    {
        //MainMenu
        public static WeScript.SDK.UI.Menu MainMenu { get; private set; }
        //Aimbot
        public static WeScript.SDK.UI.Menu MenuAimbot { get; private set; }
        public static WeScript.SDK.UI.Menu AimSpot { get; private set; }
        public static WeScript.SDK.UI.Menu AimRage { get; private set; }
        //Trigger
        public static WeScript.SDK.UI.Menu MenuTrigger { get; private set; }
        //Visual 
        public static WeScript.SDK.UI.Menu MenuVisual { get; private set; }
        //Misc
        public static WeScript.SDK.UI.Menu MenuMisc { get; private set; }
        internal static void LoadMenu()
        {
            /*AimSpot = new WeScript.SDK.UI.Menu("AimSpothack", "Spoot")
            {
                //cAimbot.Head,
                //cAimbot.Body,
                //cAimbot.Shoulders,
                //cAimbot.Legs,
                //cAimbot.Toes,
            };*/

            AimRage = new WeScript.SDK.UI.Menu("AimRagehack", "Rage")
            {
                //cAimbot.scope,
                cAimbot.PriorityBone,
                cAimbot.AimFov,
                //cAimbot.AimBody,
            };

            MenuAimbot = new WeScript.SDK.UI.Menu("aimbothack", "Aimbot")
            {
                cAimbot.AimEnable,
                cAimbot.AimSilent,
                cAimbot.AimKey,
                //cAimbot.AimPriority,
                AimSpot,
                AimRage,
            };

            MenuTrigger = new WeScript.SDK.UI.Menu("triggerhack", "TriggerBot")
            {
                cTrigger.TriggerEnable,
                cTrigger.TriggerKey,
            };

            MenuVisual = new WeScript.SDK.UI.Menu("visualhack", "Visual")
            {

                cVisual.DrawTheVisuals,
                cVisual.ESPColor,
                cVisual.DrawBox,
                cVisual.DrawBoxThic.SetToolTip("Setting thickness to 0 will let the assembly auto-adjust itself depending on model distance"),
                cVisual.DrawBoxBorder.SetToolTip("Drawing borders may take extra performance (FPS) on low-end computers"),
                cVisual.DrawBoxHP,
                cVisual.DrawTextSize,
                cVisual.DrawTextDist,
            };

            MenuMisc = new WeScript.SDK.UI.Menu("mischack", "Misc")
            {
                cMisc.aintiflash,
                cMisc.BunnyKey,
            };

            MainMenu = new WeScript.SDK.UI.Menu("csgoexample", "[WeScript] Counter Strike Global", true)
            {
                MenuAimbot,
                MenuTrigger,
                MenuVisual,
                MenuMisc,

            };
            MainMenu.Attach();
        }
    }
}
