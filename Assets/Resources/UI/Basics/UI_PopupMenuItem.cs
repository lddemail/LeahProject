/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_PopupMenuItem : GButton
    {
        public Controller m_checked;
        public const string URL = "ui://z3yueri4p5s35b";

        public static UI_PopupMenuItem CreateInstance()
        {
            return (UI_PopupMenuItem)UIPackage.CreateObject("Basics", "PopupMenuItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_checked = GetControllerAt(1);
        }
    }
}