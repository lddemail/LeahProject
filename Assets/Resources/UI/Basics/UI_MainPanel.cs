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
        public GGroup m_BtnGroup;
        public GTextField m_title_id;
        public GComboBox m_title_hotelName;
        public GComboBox m_title_group;
        public GTextField m_title_interiorNo;
        public GTextField m_title_products;
        public GTextField m_title_productsPrice;
        public GTextField m_title_totalAccount;
        public GTextField m_title_totalDebt;
        public GList m_mainList;
        public UI_UIDetail m_UIDetail;
        public UI_Tips m_Tips;
        public UI_UIConfirm m_UIConfirm;
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
            m_BtnGroup = (GGroup)GetChildAt(6);
            m_title_id = (GTextField)GetChildAt(7);
            m_title_hotelName = (GComboBox)GetChildAt(8);
            m_title_group = (GComboBox)GetChildAt(9);
            m_title_interiorNo = (GTextField)GetChildAt(10);
            m_title_products = (GTextField)GetChildAt(11);
            m_title_productsPrice = (GTextField)GetChildAt(12);
            m_title_totalAccount = (GTextField)GetChildAt(13);
            m_title_totalDebt = (GTextField)GetChildAt(14);
            m_mainList = (GList)GetChildAt(15);
            m_UIDetail = (UI_UIDetail)GetChildAt(16);
            m_Tips = (UI_Tips)GetChildAt(17);
            m_UIConfirm = (UI_UIConfirm)GetChildAt(18);
        }
    }
}