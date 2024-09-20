/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_TipsLab : GLabel
    {
        public GGraph m_bg;
        public const string URL = "ui://z3yueri4jvya76";

        public static UI_TipsLab CreateInstance()
        {
            return (UI_TipsLab)UIPackage.CreateObject("Basics", "TipsLab");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bg = (GGraph)GetChildAt(0);
        }
    }
}