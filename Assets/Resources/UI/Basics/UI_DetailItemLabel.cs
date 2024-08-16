/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_DetailItemLabel : GComponent
    {
        public GTextField m_title;
        public GTextInput m_InputLab;
        public GComboBox m_ComboxBox1;
        public const string URL = "ui://z3yueri4ihug6j";

        public static UI_DetailItemLabel CreateInstance()
        {
            return (UI_DetailItemLabel)UIPackage.CreateObject("Basics", "DetailItemLabel");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_title = (GTextField)GetChildAt(0);
            m_InputLab = (GTextInput)GetChildAt(1);
            m_ComboxBox1 = (GComboBox)GetChildAt(2);
        }
    }
}