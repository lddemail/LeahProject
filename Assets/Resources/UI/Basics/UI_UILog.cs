/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_UILog : GComponent
    {
        public GButton m_BtnClose;
        public GButton m_BtnSave;
        public GList m_LogList;
        public const string URL = "ui://z3yueri4qjfs7b";

        public static UI_UILog CreateInstance()
        {
            return (UI_UILog)UIPackage.CreateObject("Basics", "UILog");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_BtnClose = (GButton)GetChildAt(1);
            m_BtnSave = (GButton)GetChildAt(2);
            m_LogList = (GList)GetChildAt(3);
        }
    }
}