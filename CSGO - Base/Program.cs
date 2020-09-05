using System;
using WeScriptWrapper;
using WeScript.SDK.UI;
using WeScript.SDK.UI.Components;
using static BaseCSGO.Memory.Memory;
using static BaseCSGO.Player.CSPlayer;
using System.Threading;

namespace BaseCSGO
{
    class Program
    {
        public static Menu PrincipalMenu { get; private set; }
        public static Menu WallHackMenu { get; private set; }
        public static Menu AimMenu { get; private set; }
        public static IntPtr playerBase = (IntPtr)0xD3FC5C;
        public static IntPtr entityBase = (IntPtr)0x4D5442C;
        public static IntPtr crosshairOffset = (IntPtr)0xB3E4;

        public static IntPtr teamOffset = (IntPtr)0xF4;
        public static IntPtr healthOffset = (IntPtr)0x100;

        public static class VisualHack
        {
            public static readonly MenuBool WallHackFull = new MenuBool("wallhack", "Enable WallHack", true);
            public static readonly MenuBool RWO = new MenuBool("rwo", "Enable RWO In Wall", true);
            public static readonly MenuBool RWUO = new MenuBool("rwuo", "Enable RWUO In Wall", true);

            //Red
            public static readonly MenuSlider Reed = new MenuSlider("red", "Red",  1, 0, 255);
            //Green
            public static readonly MenuSlider Green = new MenuSlider("green", "Green",  0, 0, 255);
            //Bluee
            public static readonly MenuSlider Bluee = new MenuSlider("bluee", "Blue",  0, 0, 255);
            //Aplha
            public static readonly MenuSlider Aplha = new MenuSlider("aplha", "Aplha", 5, 0, 255);
            //
        }

        public static class AimHack
        {
            public static readonly MenuBool Trigger = new MenuBool("triggerbot", "Enable Trigger", true);
        }
        static void Main(string[] args)
        {
            LoadMenu(); //Menu

            //Memory.OnTick(int counter, EventArgs args) - with a delay of 33ms between each tick, used for heavy calculation codes which are not frame dependant
            WeScriptWrapper.Memory.OnTick += OnTick;
            Renderer.OnRenderer += OnRenderer;
        }

        private static void LoadMenu()
        {
            WallHackMenu = new Menu("visualsmenu", "WallHack Menu")
            {
                VisualHack.WallHackFull,
                VisualHack.RWO,
                VisualHack.RWUO,
                VisualHack.Reed,
                VisualHack.Green,
                VisualHack.Bluee,
                VisualHack.Aplha,
            };

            AimMenu = new Menu("aimshack", "AimTrigger")
            {
               AimHack.Trigger,
            };


            PrincipalMenu = new Menu("basecsgo", "[WeScripts] Base CSGO - Assembly", true)
            {
                WallHackMenu,
                AimMenu,
            };
            PrincipalMenu.Attach();
        }

        private static void OnRenderer(int fps, EventArgs args)
        {
            if (!gameProcessExists) return; //process is dead, don't bother drawing
            if ((!isGameOnTop) && (!isOverlayOnTop)) return; //if game and overlay are not on top, don't draw

            if (VisualHack.WallHackFull.Enabled) //Menu
            {
                int Max_players = MaxPlayer;
                for (int i = 0; i < Max_players; i++)
                {
                    var EntityList = WeScriptWrapper.Memory.ReadInt32(processHandle, (IntPtr)(client_panorama.ToInt64() + dwEntityList.ToInt64()) + (i - 1) * 0x10);
                    var Team_Id = WeScriptWrapper.Memory.ReadInt32(processHandle, (IntPtr)EntityList + 0xF4);

                    if (Team_Id != Team)
                    {
                        var entity_glow = WeScriptWrapper.Memory.ReadInt32(processHandle, (IntPtr)(EntityList + 0xA438));
                        var GlowObject = WeScriptWrapper.Memory.ReadInt32(processHandle, (IntPtr)(client_panorama.ToInt64() + dwGlowObjectManager.ToInt64()));

                        WeScriptWrapper.Memory.WriteFloat(processHandle, (IntPtr)GlowObject + entity_glow * 0x38 + 0x4, VisualHack.Reed.Value);
                        WeScriptWrapper.Memory.WriteFloat(processHandle, (IntPtr)GlowObject + entity_glow * 0x38 + 0x8, VisualHack.Green.Value);
                        WeScriptWrapper.Memory.WriteFloat(processHandle, (IntPtr)GlowObject + entity_glow * 0x38 + 0xC, VisualHack.Bluee.Value);
                        WeScriptWrapper.Memory.WriteFloat(processHandle, (IntPtr)GlowObject + entity_glow * 0x38 + 0x10, VisualHack.Aplha.Value);
                        WeScriptWrapper.Memory.WriteInt32(processHandle, (IntPtr)GlowObject + entity_glow * 0x38 + 0x24, 1);
                    }
                }
            }

            if (AimHack.Trigger.Enabled)
            {
                var LocalPlayer = WeScriptWrapper.Memory.ReadDWORD(processHandle, (IntPtr)(client_panorama.ToInt64() + playerBase.ToInt64()));
                var LocalTeam = WeScriptWrapper.Memory.ReadInt32(processHandle, (IntPtr)(LocalPlayer + teamOffset.ToInt64()));
                int CrossHairID = WeScriptWrapper.Memory.ReadInt32(processHandle, (IntPtr)(LocalPlayer + crosshairOffset.ToInt64()));

                var EnemyInCH = WeScriptWrapper.Memory.ReadDWORD(processHandle, (IntPtr)(client_panorama.ToInt64() + entityBase.ToInt64() + (CrossHairID - 1) * 0x10));
                int EnemyHealth = WeScriptWrapper.Memory.ReadInt32(processHandle, (IntPtr)(EnemyInCH + healthOffset.ToInt64()));
                int EnemyTeam = WeScriptWrapper.Memory.ReadInt32(processHandle, (IntPtr)EnemyInCH + 0xF4);

                if (LocalTeam != EnemyTeam && EnemyHealth > 0)
                {
                    //You can add a Sleep() here. Add a little delay yourself by Sleep()
                    Input.mouse_eventWS(MouseEventFlags.LEFTDOWN, (int)0, (int)0, MouseEventDataXButtons.NONE, IntPtr.Zero);
                    // You can use Sleep() here too, that line is made for autos like ak47. Not useful with pistol
                    Input.mouse_eventWS(MouseEventFlags.LEFTUP, (int)0, (int)0, MouseEventDataXButtons.NONE, IntPtr.Zero);
                    // Now you can make a cooldown beetween shots using Sleep() again.
                }
            }
        }
    }
}
