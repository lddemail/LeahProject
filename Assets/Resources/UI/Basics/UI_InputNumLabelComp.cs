/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_InputNumLabelComp : GComponent
    {
        public Controller m_c1;
        public GTextField m_Title;
        public GTextInput m_InputLab;
        public const string URL = "ui://z3yueri4h63l6v";

        public static UI_InputNumLabelComp CreateInstance()
        {
            return (UI_InputNumLabelComp)UIPackage.CreateObject("Basics", "InputNumLabelComp");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_c1 = GetControllerAt(0);
            m_Title = (GTextField)GetChildAt(1);
            m_InputLab = (GTextInput)GetChildAt(2);
        }
    }
}