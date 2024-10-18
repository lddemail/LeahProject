using Basics;
using FairyGUI.Utils;
using FairyGUI;
using System.Collections;
using UnityEngine;
using System;
using System.Xml.Serialization;

public class UI_PaymentTempListItemExt : UI_PaymentTempListItem
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
      //AppData.ChangeTempVal(AppConfig.PaymentTemplateName, dataStr, val);
      //SetData(val);
    }
  }

  private void OnFocusInHandler(EventContext context)
  {
  }
  private void OnFocusOutHandler(EventContext context)
  {
  }
  public void SetData(PaymentTempData val)
  {
    data = val;
    RefreshUI();
  }

  public void RefreshUI()
  {
    m_InputLab.text = (data as PaymentTempData).ToTemplateShowStr();
  }
}