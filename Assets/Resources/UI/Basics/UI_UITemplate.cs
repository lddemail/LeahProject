/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_UITemplate : GComponent
    {
        public GTextField m_Temp;
        public const string URL = "ui://z3yueri4gich77";

        public static UI_UITemplate CreateInstance()
        {
            return (UI_UITemplate)UIPackage.CreateObject("Basics", "UITemplate");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_Temp = (GTextField)GetChildAt(0);
        }
    }
}