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

        public static class VisualHack
        {
            public static readonly MenuBool WallHackFull = new MenuBool("wallhack", "Enable WallHack", true);
            public static readonly MenuBool RWO = new MenuBool("rwo", "Enable RWO In Wall", true);
            public static readonly MenuBool RWUO = new MenuBool("rwuo", "Enable RWUO In Wall", true);
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
            };
            PrincipalMenu = new Menu("basecsgo", "Base CSGO - Assembly", true)
            {
                WallHackMenu,
            };
            PrincipalMenu.Attach();
        }

        private static void OnRenderer(int fps, EventArgs args)
        {
            if (!gameProcessExists) return; //process is dead, don't bother drawing
            if ((!isGameOnTop) && (!isOverlayOnTop)) return; //if game and overlay are not on top, don't draw

            if (VisualHack.WallHackFull.Enabled)
            {
                int Max_players = MaxPlayer;
                for (int i = 0; i < Max_players; i++)
                {
                    var EntityList = WeScriptWrapper.Memory.ReadInt32(processHandle, (IntPtr)(client_panorama.ToInt64() + dwEntityList.ToInt64()) + (i - 1) * 0x10);
                    var Team_Id = WeScriptWrapper.Memory.ReadInt32(processHandle, (IntPtr)EntityList + 0xF4);

                    if (Team_Id != Team)
                    {
                        var GlowObjectPtr = WeScriptWrapper.Memory.ReadInt32(processHandle, (IntPtr)(EntityList + 0xA438));
                        var GlowObject = WeScriptWrapper.Memory.ReadInt32(processHandle, (IntPtr)(client_panorama.ToInt64() + dwGlowObjectManager.ToInt64()));

                        var TimeDelay = GlowObjectPtr * 0x38 + 0x4;
                        var current = GlowObject + TimeDelay;
                        WeScriptWrapper.Memory.WriteFloat(processHandle, (IntPtr)current, 1); //Red

                        TimeDelay = GlowObjectPtr * 0x38 + 0x8;
                        current = GlowObject + TimeDelay;
                        WeScriptWrapper.Memory.WriteFloat(processHandle, (IntPtr)current, 0); //Green

                        TimeDelay = GlowObjectPtr * 0x38 + 0xc;
                        current = GlowObject + TimeDelay;
                        WeScriptWrapper.Memory.WriteFloat(processHandle, (IntPtr)current, 0); //Blue


                        TimeDelay = GlowObjectPtr * 0x38 + 0x10;
                        current = GlowObject + TimeDelay;
                        WeScriptWrapper.Memory.WriteFloat(processHandle, (IntPtr)current, 1.7f); //Alpha

                        TimeDelay = GlowObjectPtr * 0x38 + 0x24;
                        current = GlowObject + TimeDelay;
                        WeScriptWrapper.Memory.WriteBool(processHandle, (IntPtr)current, VisualHack.RWO.Enabled); //RWO

                        TimeDelay = GlowObjectPtr * 0x38 + 0x25;
                        current = GlowObject + TimeDelay;
                        WeScriptWrapper.Memory.WriteBool(processHandle, (IntPtr)current, VisualHack.RWUO.Enabled); //RWUO
                    }
                }
                                    Thread.Sleep(15);
            }
        }
    }
}
