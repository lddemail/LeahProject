/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_DetailItemTwoLabel : GComponent
    {
        public UI_InputComboxLabelComp m_InputCombox1;
        public UI_InputComboxLabelComp m_InputCombox2;
        public GGraph m_line;
        public const string URL = "ui://z3yueri47t4l6r";

        public static UI_DetailItemTwoLabel CreateInstance()
        {
            return (UI_DetailItemTwoLabel)UIPackage.CreateObject("Basics", "DetailItemTwoLabel");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_InputCombox1 = (UI_InputComboxLabelComp)GetChildAt(0);
            m_InputCombox2 = (UI_InputComboxLabelComp)GetChildAt(1);
            m_line = (GGraph)GetChildAt(2);
        }
    }
}