using WeScriptWrapper;
using WeScript.SDK.UI.Components;


namespace WeCSGlobal.Config
{
    class TriggerBot
    {
        public static class cTrigger
        {
            public static readonly MenuBool TriggerEnable = new MenuBool("enable", "Enable TriggerBot", false);
            public static readonly MenuKeyBind TriggerKey = new MenuKeyBind("triggerkey", "TriggerBot HotKey", VirtualKeyCode.Shift, KeybindType.Hold, false);
        }
    }
}
