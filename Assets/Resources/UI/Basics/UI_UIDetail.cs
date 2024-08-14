/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_UIDetail : GComponent
    {
        public GButton m_BtnClose;
        public GButton m_BtnSave;
        public GList m_DetailList;
        public const string URL = "ui://z3yueri4i6pi6i";

        public static UI_UIDetail CreateInstance()
        {
            return (UI_UIDetail)UIPackage.CreateObject("Basics", "UIDetail");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_BtnClose = (GButton)GetChildAt(1);
            m_BtnSave = (GButton)GetChildAt(2);
            m_DetailList = (GList)GetChildAt(3);
        }
    }
}