using Basics;
using FairyGUI.Utils;
using FairyGUI;
using System.Collections;
using UnityEngine;
using System;

public class UI_InputDescLabelCompExt : UI_InputDescLabelComp
{
  public override void ConstructFromXML(XML xml)
  {
    base.ConstructFromXML(xml);

    m_InputLab.onFocusIn.Add(OnFocusInHandler);
    m_InputLab.onFocusOut.Add(OnFocusOutHandler);
  }

  private void OnFocusInHandler(EventContext context)
  {
    m_c1.SetSelectedIndex(1);
  }
  private void OnFocusOutHandler(EventContext context)
  {
    m_c1.SetSelectedIndex(0);
  }
}