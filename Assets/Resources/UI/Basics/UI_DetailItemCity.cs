/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_DetailItemCity : GComponent
    {
        public GTextField m_title;
        public GComboBox m_ComboxBox1;
        public GComboBox m_ComboxBox2;
        public UI_InputComboxLabelComp m_InputCombox1;
        public GGraph m_line;
        public const string URL = "ui://z3yueri4cfou6q";

        public static UI_DetailItemCity CreateInstance()
        {
            return (UI_DetailItemCity)UIPackage.CreateObject("Basics", "DetailItemCity");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_title = (GTextField)GetChildAt(0);
            m_ComboxBox1 = (GComboBox)GetChildAt(1);
            m_ComboxBox2 = (GComboBox)GetChildAt(2);
            m_InputCombox1 = (UI_InputComboxLabelComp)GetChildAt(3);
            m_line = (GGraph)GetChildAt(4);
        }
    }
}