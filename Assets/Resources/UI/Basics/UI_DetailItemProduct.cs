/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_DetailItemProduct : GComponent
    {
        public UI_InputStrLabelComp m_InputLabName;
        public UI_InputNumLabelComp m_InputLabPrice;
        public UI_InputTimeLabelComp m_InputLabfTime;
        public UI_InputTimeLabelComp m_InputLabtTime;
        public GTextField m_AdventLab;
        public UI_InputDescLabelComp m_InputLabRemark;
        public GButton m_BtnDel;
        public const string URL = "ui://z3yueri4ldks6k";

        public static UI_DetailItemProduct CreateInstance()
        {
            return (UI_DetailItemProduct)UIPackage.CreateObject("Basics", "DetailItemProduct");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_InputLabName = (UI_InputStrLabelComp)GetChildAt(0);
            m_InputLabPrice = (UI_InputNumLabelComp)GetChildAt(1);
            m_InputLabfTime = (UI_InputTimeLabelComp)GetChildAt(2);
            m_InputLabtTime = (UI_InputTimeLabelComp)GetChildAt(3);
            m_AdventLab = (GTextField)GetChildAt(4);
            m_InputLabRemark = (UI_InputDescLabelComp)GetChildAt(5);
            m_BtnDel = (GButton)GetChildAt(6);
        }
    }
}