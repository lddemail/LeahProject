/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_InputStrLabelComp : GComponent
    {
        public GTextField m_Title;
        public GTextInput m_InputLab;
        public const string URL = "ui://z3yueri4h63l6w";

        public static UI_InputStrLabelComp CreateInstance()
        {
            return (UI_InputStrLabelComp)UIPackage.CreateObject("Basics", "InputStrLabelComp");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_Title = (GTextField)GetChildAt(1);
            m_InputLab = (GTextInput)GetChildAt(2);
        }
    }
}