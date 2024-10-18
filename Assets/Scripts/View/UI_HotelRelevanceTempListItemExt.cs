using Basics;
using FairyGUI.Utils;
using FairyGUI;
using System.Collections;
using UnityEngine;
using System;

public class UI_HotelRelevanceTempListItemExt : UI_HotelRelevanceTempListItem
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
      data = val;
      //AppData.ChangeTempVal(AppConfig.HotelRelevanceTemplateName, dataStr, val);
      //RefreshUI();
    }
  }

  private void OnFocusInHandler(EventContext context)
  {
  }
  private void OnFocusOutHandler(EventContext context)
  {
  }
  public void SetData(HotelRelevanceTempData val)
  {
    data = val;
    RefreshUI();
  }
  public void RefreshUI()
  {
    m_InputLab.text = (data as HotelRelevanceTempData).ToTemplateShowStr();
  }
}