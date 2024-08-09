/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Main
{
    public partial class UI_MainListItem : GComponent
    {
        public GTextField m_NameTxt;
        public const string URL = "ui://ziofp0dseblsj";

        public static UI_MainListItem CreateInstance()
        {
            return (UI_MainListItem)UIPackage.CreateObject("Main", "MainListItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_NameTxt = (GTextField)GetChildAt(0);
        }
    }
}