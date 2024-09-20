/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_MainListItem : GButton
    {
        public GButton m_selectBtn;
        public GTextField m_t_id;
        public GGraph m_t_hotelName_bg;
        public GTextField m_t_hotelName;
        public GTextField m_t_group;
        public GTextField m_t_interiorNo;
        public GGraph m_t_products_bg;
        public GTextField m_t_products;
        public GTextField m_t_productsPrice;
        public GTextField m_t_totalAccount;
        public GTextField m_t_totalDebt;
        public const string URL = "ui://z3yueri4p5s36c";

        public static UI_MainListItem CreateInstance()
        {
            return (UI_MainListItem)UIPackage.CreateObject("Basics", "MainListItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_selectBtn = (GButton)GetChildAt(3);
            m_t_id = (GTextField)GetChildAt(4);
            m_t_hotelName_bg = (GGraph)GetChildAt(5);
            m_t_hotelName = (GTextField)GetChildAt(6);
            m_t_group = (GTextField)GetChildAt(7);
            m_t_interiorNo = (GTextField)GetChildAt(8);
            m_t_products_bg = (GGraph)GetChildAt(9);
            m_t_products = (GTextField)GetChildAt(10);
            m_t_productsPrice = (GTextField)GetChildAt(11);
            m_t_totalAccount = (GTextField)GetChildAt(12);
            m_t_totalDebt = (GTextField)GetChildAt(13);
        }
    }
}