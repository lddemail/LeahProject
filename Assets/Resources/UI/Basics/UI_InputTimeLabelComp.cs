/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_InputTimeLabelComp : GComponent
    {
        public GTextField m_Title;
        public GTextInput m_InputLab;
        public const string URL = "ui://z3yueri4h63l6u";

        public static UI_InputTimeLabelComp CreateInstance()
        {
            return (UI_InputTimeLabelComp)UIPackage.CreateObject("Basics", "InputTimeLabelComp");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_Title = (GTextField)GetChildAt(1);
            m_InputLab = (GTextInput)GetChildAt(2);
        }
    }
}