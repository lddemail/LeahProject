/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_ComboBoxPopupCopy : GComponent
    {
        public GList m_list;
        public const string URL = "ui://z3yueri4phex6o";

        public static UI_ComboBoxPopupCopy CreateInstance()
        {
            return (UI_ComboBoxPopupCopy)UIPackage.CreateObject("Basics", "ComboBoxPopupCopy");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_list = (GList)GetChildAt(1);
        }
    }
}