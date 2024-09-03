/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_DetailItemLabel : GComponent
    {
        public UI_InputComboxLabelComp m_InputCombox1;
        public GGraph m_line;
        public const string URL = "ui://z3yueri4ihug6j";

        public static UI_DetailItemLabel CreateInstance()
        {
            return (UI_DetailItemLabel)UIPackage.CreateObject("Basics", "DetailItemLabel");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_InputCombox1 = (UI_InputComboxLabelComp)GetChildAt(0);
            m_line = (GGraph)GetChildAt(1);
        }
    }
}