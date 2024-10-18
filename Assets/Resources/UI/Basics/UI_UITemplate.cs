/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_UITemplate : GComponent
    {
        public GButton m_BtnClose;
        public GButton m_BtnSave;
        public GButton m_BtnPaymentTemp;
        public GButton m_BtnSignedTemp;
        public GButton m_BtnHotelRelevanceTemp;
        public GList m_PaymentTempList;
        public GList m_SignedTempList;
        public GList m_HotelRelevanceTempList;
        public const string URL = "ui://z3yueri4gich77";

        public static UI_UITemplate CreateInstance()
        {
            return (UI_UITemplate)UIPackage.CreateObject("Basics", "UITemplate");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_BtnClose = (GButton)GetChildAt(1);
            m_BtnSave = (GButton)GetChildAt(2);
            m_BtnPaymentTemp = (GButton)GetChildAt(3);
            m_BtnSignedTemp = (GButton)GetChildAt(4);
            m_BtnHotelRelevanceTemp = (GButton)GetChildAt(5);
            m_PaymentTempList = (GList)GetChildAt(6);
            m_SignedTempList = (GList)GetChildAt(7);
            m_HotelRelevanceTempList = (GList)GetChildAt(8);
        }
    }
}