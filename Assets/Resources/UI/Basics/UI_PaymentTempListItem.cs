/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_PaymentTempListItem : GComponent
    {
        public GGraph m_InputBg;
        public GTextInput m_InputLab;
        public const string URL = "ui://z3yueri4ofvt78";

        public static UI_PaymentTempListItem CreateInstance()
        {
            return (UI_PaymentTempListItem)UIPackage.CreateObject("Basics", "PaymentTempListItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_InputBg = (GGraph)GetChildAt(0);
            m_InputLab = (GTextInput)GetChildAt(1);
        }
    }
}