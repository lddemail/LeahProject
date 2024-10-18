using Basics;
using FairyGUI.Utils;
using FairyGUI;
using System.Collections;
using UnityEngine;
using System;

public class UI_SignedTempListItemExt : UI_SignedTempListItem
{
  public override void ConstructFromXML(XML xml)
  {
    base.ConstructFromXML(xml);

    m_InputLab.onChanged.Set(OnInputLab1Change);
  }

  private void OnInputLab1Change(EventContext context)
  {
    string val = m_InputLab.text;
    if (string.IsNullOrEmpty(val)) return;

    string dataStr = data.ToString();
    if (dataStr != val)
    {
      //AppData.ChangeTempVal(AppConfig.SignedTemplateName, dataStr, val);
      //SetData(val);
    }
  }

  private void OnFocusInHandler(EventContext context)
  {
  }
  private void OnFocusOutHandler(EventContext context)
  {
  }
  public void SetData(SignedTempData val)
  {
    data = val;
    RefreshUI();
  }

  public void RefreshUI()
  {
    m_InputLab.text = (data as SignedTempData).ToTemplateShowStr();
  }
}