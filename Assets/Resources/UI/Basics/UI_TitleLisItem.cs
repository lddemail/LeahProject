/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_TitleLisItem : GComponent
    {
        public GTextField m_title;
        public const string URL = "ui://z3yueri4p5s36b";

        public static UI_TitleLisItem CreateInstance()
        {
            return (UI_TitleLisItem)UIPackage.CreateObject("Basics", "TitleLisItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_title = (GTextField)GetChildAt(0);
        }
    }
}