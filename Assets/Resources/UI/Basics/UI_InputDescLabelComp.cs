/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_InputDescLabelComp : GComponent
    {
        public GTextField m_Title;
        public GTextInput m_InputLab;
        public const string URL = "ui://z3yueri4h63l6x";

        public static UI_InputDescLabelComp CreateInstance()
        {
            return (UI_InputDescLabelComp)UIPackage.CreateObject("Basics", "InputDescLabelComp");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_Title = (GTextField)GetChildAt(1);
            m_InputLab = (GTextInput)GetChildAt(2);
        }
    }
}