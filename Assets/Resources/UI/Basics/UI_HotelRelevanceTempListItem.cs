/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_HotelRelevanceTempListItem : GButton
    {
        public GTextField m_t_id;
        public GTextInput m_hotelNameInputLab;
        public GTextInput m_groupInputLab;
        public GTextInput m_brandInputLab;
        public GTextInput m_contractInputLab;
        public GTextInput m_provinceInputLab;
        public GTextInput m_cityInputLab;
        public const string URL = "ui://z3yueri4oh1s7d";

        public static UI_HotelRelevanceTempListItem CreateInstance()
        {
            return (UI_HotelRelevanceTempListItem)UIPackage.CreateObject("Basics", "HotelRelevanceTempListItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_t_id = (GTextField)GetChildAt(3);
            m_hotelNameInputLab = (GTextInput)GetChildAt(4);
            m_groupInputLab = (GTextInput)GetChildAt(5);
            m_brandInputLab = (GTextInput)GetChildAt(6);
            m_contractInputLab = (GTextInput)GetChildAt(7);
            m_provinceInputLab = (GTextInput)GetChildAt(8);
            m_cityInputLab = (GTextInput)GetChildAt(9);
        }
    }
}