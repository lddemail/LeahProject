using Basics;
using FairyGUI.Utils;
using FairyGUI;
using System.Collections;
using UnityEngine;
using System;

public class UI_UILogListItemExt : UI_UILogListItem
{
  public override void ConstructFromXML(XML xml)
  {
    base.ConstructFromXML(xml);

  }

  public void SetData(string val)
  {
    m_Lab.text = val;
    RefreshUI();
  }
  public void RefreshUI()
  {

  }
}