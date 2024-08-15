using Basics;
using FairyGUI.Utils;
using FairyGUI;
using System.Collections;
using UnityEngine;

public class UIDetailItemLabelExt : UI_DetailItemLabel
{

  public override void ConstructFromXML(XML xml)
  {
    base.ConstructFromXML(xml);

  }

  public void SetData(ObjectVal va, bool isEnabled=true)
  {
    data = va;
    m_title.text = va.name;
    m_InputLab.text = va.val == null ?  "": va.val.ToString();
    m_InputLab.enabled = isEnabled;
  }
}