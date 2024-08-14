/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_UIConfirm : GComponent
    {
        public GButton m_BtnOk;
        public UI_Button5Copy m_BtnClose;
        public const string URL = "ui://z3yueri4i6pi6f";

        public static UI_UIConfirm CreateInstance()
        {
            return (UI_UIConfirm)UIPackage.CreateObject("Basics", "UIConfirm");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_BtnOk = (GButton)GetChildAt(1);
            m_BtnClose = (UI_Button5Copy)GetChildAt(2);
        }
    }
}