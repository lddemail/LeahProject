using Basics;
using FairyGUI.Utils;
using FairyGUI;
using System.Collections;
using UnityEngine;
using System;

public class UIDetailItemLineExt : UI_DetailItemLine
{
  public int childIndex;
  public override void ConstructFromXML(XML xml)
  {
    base.ConstructFromXML(xml);
  }

  public void SetData(string name)
  {
    m_title.text = name;
  }
}