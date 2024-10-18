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

  }

  private void OnFocusInHandler(EventContext context)
  {
  }
  private void OnFocusOutHandler(EventContext context)
  {
  }
  public void SetData(HotelRelevanceData val)
  {
    data = val.t_hotelName;
    m_InputLab.text = val.ToTemplateStr();
  }
}