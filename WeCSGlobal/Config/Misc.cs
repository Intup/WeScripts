using WeScriptWrapper;
using WeScript.SDK.UI.Components;

namespace WeCSGlobal.Config
{
    class Misc
    {
        public static class cMisc
        {
            public static readonly MenuBool aintiflash = new MenuBool("flash", "Enable Anti-Flash", false);
            //Bunny
            public static readonly MenuKeyBind BunnyKey = new MenuKeyBind("bunny", "Bunny Hotkey", VirtualKeyCode.Alt, KeybindType.Toggle, false);
        }
    }
}
