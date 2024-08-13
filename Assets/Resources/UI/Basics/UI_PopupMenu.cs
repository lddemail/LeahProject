/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_PopupMenu : GComponent
    {
        public GList m_list;
        public const string URL = "ui://z3yueri4p5s35a";

        public static UI_PopupMenu CreateInstance()
        {
            return (UI_PopupMenu)UIPackage.CreateObject("Basics", "PopupMenu");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_list = (GList)GetChildAt(1);
        }
    }
}