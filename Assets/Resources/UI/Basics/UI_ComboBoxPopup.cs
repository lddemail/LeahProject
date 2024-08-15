/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_ComboBoxPopup : GComponent
    {
        public GList m_list;
        public const string URL = "ui://z3yueri4p5s31r";

        public static UI_ComboBoxPopup CreateInstance()
        {
            return (UI_ComboBoxPopup)UIPackage.CreateObject("Basics", "ComboBoxPopup");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_list = (GList)GetChildAt(1);
        }
    }
}