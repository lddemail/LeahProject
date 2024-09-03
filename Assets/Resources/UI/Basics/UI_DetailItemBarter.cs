/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_DetailItemBarter : GComponent
    {
        public UI_InputNumLabelComp m_InputLabBarter;
        public UI_InputTimeLabelComp m_InputLabTime;
        public UI_InputDescLabelComp m_InputLabRemark;
        public GButton m_BtnDel;
        public const string URL = "ui://z3yueri4ldks6l";

        public static UI_DetailItemBarter CreateInstance()
        {
            return (UI_DetailItemBarter)UIPackage.CreateObject("Basics", "DetailItemBarter");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_InputLabBarter = (UI_InputNumLabelComp)GetChildAt(0);
            m_InputLabTime = (UI_InputTimeLabelComp)GetChildAt(1);
            m_InputLabRemark = (UI_InputDescLabelComp)GetChildAt(2);
            m_BtnDel = (GButton)GetChildAt(3);
        }
    }
}