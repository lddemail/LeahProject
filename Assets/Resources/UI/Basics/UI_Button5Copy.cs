/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_Button5Copy : GButton
    {
        public GImage m_bg;
        public const string URL = "ui://z3yueri4i6pi6g";

        public static UI_Button5Copy CreateInstance()
        {
            return (UI_Button5Copy)UIPackage.CreateObject("Basics", "Button5Copy");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bg = (GImage)GetChildAt(0);
        }
    }
}