/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_DetailItemProduct : GComponent
    {
        public GTextInput m_InputLabName;
        public GTextInput m_InputLabPrice;
        public GTextField m_fTimeTxt;
        public GTextInput m_InputLabfTime;
        public GTextField m_tTimeTxt;
        public GTextInput m_InputLabtTime;
        public GTextField m_remarkTxt;
        public GTextInput m_InputLabRemark;
        public GButton m_BtnDel;
        public const string URL = "ui://z3yueri4ldks6k";

        public static UI_DetailItemProduct CreateInstance()
        {
            return (UI_DetailItemProduct)UIPackage.CreateObject("Basics", "DetailItemProduct");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_InputLabName = (GTextInput)GetChildAt(0);
            m_InputLabPrice = (GTextInput)GetChildAt(2);
            m_fTimeTxt = (GTextField)GetChildAt(3);
            m_InputLabfTime = (GTextInput)GetChildAt(4);
            m_tTimeTxt = (GTextField)GetChildAt(5);
            m_InputLabtTime = (GTextInput)GetChildAt(6);
            m_remarkTxt = (GTextField)GetChildAt(7);
            m_InputLabRemark = (GTextInput)GetChildAt(8);
            m_BtnDel = (GButton)GetChildAt(10);
        }
    }
}