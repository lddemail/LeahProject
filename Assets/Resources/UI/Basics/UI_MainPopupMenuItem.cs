/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_MainPopupMenuItem : GButton
    {
        public Controller m_checked;
        public const string URL = "ui://z3yueri4rrp475";

        public static UI_MainPopupMenuItem CreateInstance()
        {
            return (UI_MainPopupMenuItem)UIPackage.CreateObject("Basics", "MainPopupMenuItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_checked = GetControllerAt(1);
        }
    }
}