/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_DetailItemTwoLabel : GComponent
    {
        public GTextField m_title1;
        public GComboBox m_ComboxBox1;
        public GTextInput m_InputLab1;
        public GTextField m_title2;
        public GComboBox m_ComboxBox2;
        public GTextInput m_InputLab2;
        public const string URL = "ui://z3yueri47t4l6r";

        public static UI_DetailItemTwoLabel CreateInstance()
        {
            return (UI_DetailItemTwoLabel)UIPackage.CreateObject("Basics", "DetailItemTwoLabel");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_title1 = (GTextField)GetChildAt(0);
            m_ComboxBox1 = (GComboBox)GetChildAt(1);
            m_InputLab1 = (GTextInput)GetChildAt(2);
            m_title2 = (GTextField)GetChildAt(3);
            m_ComboxBox2 = (GComboBox)GetChildAt(4);
            m_InputLab2 = (GTextInput)GetChildAt(5);
        }
    }
}