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
    m_InputBg.onFocusIn.Add(OnFocusInHandler);
    m_InputBg.onFocusOut.Add(OnFocusOutHandler);
    m_InputLab.onFocusIn.Add(OnFocusInHandler);
    m_InputLab.onFocusOut.Add(OnFocusOutHandler);
  }

  private void OnFocusInHandler(EventContext context)
  {
    m_InputBg.color = AppConfig.selectBgColor;
  }
  private void OnFocusOutHandler(EventContext context)
  {
    m_InputBg.color = Color.white;
  }

  public void Set_cPosIndex(int index)
  {
    m_cPos.SetSelectedIndex(index);
  }

}