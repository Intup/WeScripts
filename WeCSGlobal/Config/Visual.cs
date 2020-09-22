using WeScriptWrapper;
using WeScript.SDK.UI.Components;

namespace WeCSGlobal.Config
{
    class Visual
    {
        public static class cVisual
        {
            public static readonly MenuBool DrawTheVisuals = new MenuBool("drawthevisuals", "Enable all of the Visuals", true);
            public static readonly MenuColor ESPColor = new MenuColor("ctcolor", "ESP Color", new SharpDX.Color(0, 0, 255));
            public static readonly MenuBool DrawBox = new MenuBool("drawbox", "Draw Box ESP", true);
            public static readonly MenuSlider DrawBoxThic = new MenuSlider("boxthickness", "Draw Box Thickness", 0, 0, 10);
            public static readonly MenuBool DrawBoxBorder = new MenuBool("drawboxborder", "Draw Border around Box and Text?", true);
            public static readonly MenuBool DrawBoxHP = new MenuBool("drawboxhp", "Draw Health", true);
            public static readonly MenuSliderBool DrawTextSize = new MenuSliderBool("drawtextsize", "Text Size", false, 14, 4, 72);
            public static readonly MenuBool DrawTextDist = new MenuBool("drawtextdist", "Draw Distance", true);
        }
    }
}
