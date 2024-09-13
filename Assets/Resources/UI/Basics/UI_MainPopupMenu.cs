/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_MainPopupMenu : GComponent
    {
        public GList m_list;
        public const string URL = "ui://z3yueri4rrp474";

        public static UI_MainPopupMenu CreateInstance()
        {
            return (UI_MainPopupMenu)UIPackage.CreateObject("Basics", "MainPopupMenu");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_list = (GList)GetChildAt(1);
        }
    }
}