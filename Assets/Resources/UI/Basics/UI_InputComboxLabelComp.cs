/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_InputComboxLabelComp : GComponent
    {
        public Controller m_c1;
        public GTextField m_Title;
        public GComboBox m_ComboxBox1;
        public GTextInput m_InputLab;
        public const string URL = "ui://z3yueri4h63l70";

        public static UI_InputComboxLabelComp CreateInstance()
        {
            return (UI_InputComboxLabelComp)UIPackage.CreateObject("Basics", "InputComboxLabelComp");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_c1 = GetControllerAt(0);
            m_Title = (GTextField)GetChildAt(1);
            m_ComboxBox1 = (GComboBox)GetChildAt(2);
            m_InputLab = (GTextInput)GetChildAt(3);
        }
    }
}