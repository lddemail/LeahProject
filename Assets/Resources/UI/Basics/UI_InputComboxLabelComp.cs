/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_InputComboxLabelComp : GComponent
    {
        public Controller m_cPos;
        public GTextField m_Title;
        public GComboBox m_ComboxBox1;
        public GGraph m_InputBg;
        public GTextInput m_InputLab;
        public GGraph m_FilterBg;
        public GTextInput m_FilterLab;
        public const string URL = "ui://z3yueri4h63l70";

        public static UI_InputComboxLabelComp CreateInstance()
        {
            return (UI_InputComboxLabelComp)UIPackage.CreateObject("Basics", "InputComboxLabelComp");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_cPos = GetControllerAt(0);
            m_Title = (GTextField)GetChildAt(0);
            m_ComboxBox1 = (GComboBox)GetChildAt(1);
            m_InputBg = (GGraph)GetChildAt(2);
            m_InputLab = (GTextInput)GetChildAt(3);
            m_FilterBg = (GGraph)GetChildAt(4);
            m_FilterLab = (GTextInput)GetChildAt(5);
        }
    }
}