/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_Tips : GComponent
    {
        public GTextField m_title;
        public const string URL = "ui://z3yueri4i6pi6d";

        public static UI_Tips CreateInstance()
        {
            return (UI_Tips)UIPackage.CreateObject("Basics", "Tips");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_title = (GTextField)GetChildAt(1);
        }
    }
}