/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_DetailItemThreeLabel : GComponent
    {
        public UI_InputComboxLabelComp m_InputCombox1;
        public UI_InputComboxLabelComp m_InputCombox2;
        public UI_InputComboxLabelComp m_InputCombox3;
        public GGraph m_line;
        public const string URL = "ui://z3yueri4h63l6y";

        public static UI_DetailItemThreeLabel CreateInstance()
        {
            return (UI_DetailItemThreeLabel)UIPackage.CreateObject("Basics", "DetailItemThreeLabel");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_InputCombox1 = (UI_InputComboxLabelComp)GetChildAt(0);
            m_InputCombox2 = (UI_InputComboxLabelComp)GetChildAt(1);
            m_InputCombox3 = (UI_InputComboxLabelComp)GetChildAt(2);
            m_line = (GGraph)GetChildAt(3);
        }
    }
}