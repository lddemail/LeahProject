/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Main
{
    public partial class UI_MainPanel : GComponent
    {
        public GGraph m_bg;
        public GGraph m_bgTitle;
        public GGraph m_bgList;
        public GList m_mainList;
        public GButton m_BtnAddHotel;
        public const string URL = "ui://ziofp0dshsyt0";

        public static UI_MainPanel CreateInstance()
        {
            return (UI_MainPanel)UIPackage.CreateObject("Main", "MainPanel");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bg = (GGraph)GetChildAt(0);
            m_bgTitle = (GGraph)GetChildAt(1);
            m_bgList = (GGraph)GetChildAt(3);
            m_mainList = (GList)GetChildAt(4);
            m_BtnAddHotel = (GButton)GetChildAt(5);
        }
    }
}