using Basics;
using FairyGUI.Utils;
using FairyGUI;
using System.Collections;
using UnityEngine;
using System;

public class UI_InputComboxLabelCompExt : UI_InputComboxLabelComp
{
  public override void ConstructFromXML(XML xml)
  {
    base.ConstructFromXML(xml);

  }

  public void Set_cPosIndex(int index)
  {
    m_cPos.SetSelectedIndex(index);
  }

}