/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_DetailItemLine : GComponent
    {
        public GTextField m_title;
        public const string URL = "ui://z3yueri47t4l6t";

        public static UI_DetailItemLine CreateInstance()
        {
            return (UI_DetailItemLine)UIPackage.CreateObject("Basics", "DetailItemLine");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_title = (GTextField)GetChildAt(0);
        }
    }
}