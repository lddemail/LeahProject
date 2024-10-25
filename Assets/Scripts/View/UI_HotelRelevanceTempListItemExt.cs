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

    m_hotelNameInputLab.onChanged.Add(OnInputLabChange);
    m_groupInputLab.onChanged.Add(OnInputLabChange);
    m_brandInputLab.onChanged.Add(OnInputLabChange);
    m_contractInputLab.onChanged.Add(OnInputLabChange);
    m_provinceInputLab.onChanged.Add(OnInputLabChange);
    m_cityInputLab.onChanged.Add(OnInputLabChange);
  }

  private void OnInputLabChange(EventContext context)
  {
    GTextField Gtext = (GTextField)context.sender;
    string val = Gtext.text;
    if (string.IsNullOrEmpty(val)) return;

    HotelRelevanceTempData hrtd = data as HotelRelevanceTempData;
    hrtd.SetFieldVal(Gtext.data.ToString(), val);
    RefreshUI();

    _changeCallBack?.Invoke();
  }

  private void OnFocusInHandler(EventContext context)
  {
  }
  private void OnFocusOutHandler(EventContext context)
  {
  }
  private Action _changeCallBack;
  public void SetChangeCallBack(Action changeCallBack)
  {
    _changeCallBack = changeCallBack;
  }
  public void SetData(HotelRelevanceTempData val)
  {
    data = val;
    RefreshUI();
  }
  public void RefreshUI()
  {
    SetObj(m_t_id, AppConfig.t_id);
    SetObj(m_hotelNameInputLab, AppConfig.t_hotelName);
    SetObj(m_groupInputLab, AppConfig.t_group);
    SetObj(m_brandInputLab, AppConfig.t_brand);
    SetObj(m_contractInputLab, AppConfig.t_a_contract);
    SetObj(m_provinceInputLab, AppConfig.t_province);
    SetObj(m_cityInputLab, AppConfig.t_city);
  }
  private void SetObj(GTextField Gtext,string fieldName)
  {
    HotelRelevanceTempData hrtd = data as HotelRelevanceTempData;
    object obj = hrtd.GetFieldVal(fieldName);
    string val =  obj == null ? "" : obj.ToString();
    Gtext.text = val;
    Gtext.data = fieldName;
  }
}