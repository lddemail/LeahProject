/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_MainListItem : GButton
    {
        public GButton m_cb;
        public GTextField m_t1;
        public GTextField m_t2;
        public GTextField m_t3;
        public GTextField m_t4;
        public GTextField m_t5;
        public GTextField m_t6;
        public GTextField m_t7;
        public GTextField m_t8;
        public GButton m_BtnDetails;
        public GButton m_BtnDel;
        public GGroup m_BtnGroup;
        public const string URL = "ui://z3yueri4p5s36c";

        public static UI_MainListItem CreateInstance()
        {
            return (UI_MainListItem)UIPackage.CreateObject("Basics", "MainListItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_cb = (GButton)GetChildAt(3);
            m_t1 = (GTextField)GetChildAt(4);
            m_t2 = (GTextField)GetChildAt(5);
            m_t3 = (GTextField)GetChildAt(6);
            m_t4 = (GTextField)GetChildAt(7);
            m_t5 = (GTextField)GetChildAt(8);
            m_t6 = (GTextField)GetChildAt(9);
            m_t7 = (GTextField)GetChildAt(10);
            m_t8 = (GTextField)GetChildAt(11);
            m_BtnDetails = (GButton)GetChildAt(12);
            m_BtnDel = (GButton)GetChildAt(13);
            m_BtnGroup = (GGroup)GetChildAt(14);
        }
    }
}