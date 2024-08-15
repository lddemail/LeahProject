/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_DetailItemAccount : GComponent
    {
        public GTextField m_barterTxt;
        public GTextInput m_InputLabBarter;
        public GTextField m_timeTxt;
        public GTextInput m_InputLabTime;
        public GTextField m_remarkTxt;
        public GTextInput m_InputLabRemark;
        public GButton m_BtnDel;
        public const string URL = "ui://z3yueri4ldks6m";

        public static UI_DetailItemAccount CreateInstance()
        {
            return (UI_DetailItemAccount)UIPackage.CreateObject("Basics", "DetailItemAccount");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_barterTxt = (GTextField)GetChildAt(1);
            m_InputLabBarter = (GTextInput)GetChildAt(2);
            m_timeTxt = (GTextField)GetChildAt(3);
            m_InputLabTime = (GTextInput)GetChildAt(4);
            m_remarkTxt = (GTextField)GetChildAt(5);
            m_InputLabRemark = (GTextInput)GetChildAt(6);
            m_BtnDel = (GButton)GetChildAt(7);
        }
    }
}