/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_MainPanel : GComponent
    {
        public GGraph m_bg;
        public GButton m_BtnInputExcel;
        public GButton m_BtnAllOrAdvent;
        public GButton m_BtnAdd;
        public GList m_mainList;
        public GList m_titleList;
        public UI_Tips m_Tips;
        public UI_UIConfirm m_UIConfirm;
        public UI_UIDetail m_UIDetail;
        public const string URL = "ui://z3yueri4p5s369";

        public static UI_MainPanel CreateInstance()
        {
            return (UI_MainPanel)UIPackage.CreateObject("Basics", "MainPanel");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bg = (GGraph)GetChildAt(0);
            m_BtnInputExcel = (GButton)GetChildAt(3);
            m_BtnAllOrAdvent = (GButton)GetChildAt(4);
            m_BtnAdd = (GButton)GetChildAt(5);
            m_mainList = (GList)GetChildAt(6);
            m_titleList = (GList)GetChildAt(7);
            m_Tips = (UI_Tips)GetChildAt(8);
            m_UIConfirm = (UI_UIConfirm)GetChildAt(9);
            m_UIDetail = (UI_UIDetail)GetChildAt(10);
        }
    }
}