/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_UILogListItem : GComponent
    {
        public GGraph m_InputBg;
        public GTextField m_Lab;
        public const string URL = "ui://z3yueri4qjfs7c";

        public static UI_UILogListItem CreateInstance()
        {
            return (UI_UILogListItem)UIPackage.CreateObject("Basics", "UILogListItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_InputBg = (GGraph)GetChildAt(0);
            m_Lab = (GTextField)GetChildAt(1);
        }
    }
}