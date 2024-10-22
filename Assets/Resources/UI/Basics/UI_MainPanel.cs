/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_MainPanel : GComponent
    {
        public GGraph m_bg;
        public GTextField m_VersionTxt;
        public GButton m_BtnMainPop;
        public GButton m_BtnAdd;
        public GButton m_BtnTemplate;
        public GTextField m_title_id;
        public GGraph m_title_hotelNameBg;
        public GTextInput m_title_hotelName;
        public GTextField m_title_interiorNo;
        public GComboBox m_title_product;
        public GTextField m_title_productsPrice;
        public GTextField m_title_totalDebt;
        public GList m_mainList;
        public UI_UIDetail m_UIDetail;
        public UI_UITemplate m_UITemplate;
        public UI_UILog m_UILog;
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
            m_VersionTxt = (GTextField)GetChildAt(3);
            m_BtnMainPop = (GButton)GetChildAt(4);
            m_BtnAdd = (GButton)GetChildAt(5);
            m_BtnTemplate = (GButton)GetChildAt(6);
            m_title_id = (GTextField)GetChildAt(7);
            m_title_hotelNameBg = (GGraph)GetChildAt(8);
            m_title_hotelName = (GTextInput)GetChildAt(9);
            m_title_interiorNo = (GTextField)GetChildAt(10);
            m_title_product = (GComboBox)GetChildAt(11);
            m_title_productsPrice = (GTextField)GetChildAt(12);
            m_title_totalDebt = (GTextField)GetChildAt(13);
            m_mainList = (GList)GetChildAt(14);
            m_UIDetail = (UI_UIDetail)GetChildAt(15);
            m_UITemplate = (UI_UITemplate)GetChildAt(16);
            m_UILog = (UI_UILog)GetChildAt(17);
            m_Tips = (UI_Tips)GetChildAt(18);
            m_UIConfirm = (UI_UIConfirm)GetChildAt(19);
        }
    }
}