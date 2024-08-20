using Basics;
using FairyGUI.Utils;
using FairyGUI;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;

public class UIDetailItemCityExt : UI_DetailItemCity
{

  public override void ConstructFromXML(XML xml)
  {
    base.ConstructFromXML(xml);

    m_ComboxBox1.onChanged.Add(ComboxBox1ChangeHandler);
    m_ComboxBox2.onChanged.Add(ComboxBox2ChangeHandler);
  }

  private void ComboxBox1ChangeHandler(EventContext context)
  {
    AppConfig.selectProvinceIndex = m_ComboxBox1.selectedIndex;
    SetCity("");
  }
  private void ComboxBox2ChangeHandler(EventContext context)
  {
 
  }


  private void SetProvince(string val)
  {
    m_ComboxBox1.items = AppConfig.provinceList.ToArray();
    int selectIndex = AppConfig.provinceList.FindIndex(x => x == val);
    if (selectIndex < 0) selectIndex = 0;
    m_ComboxBox1.selectedIndex = selectIndex;
    AppConfig.selectProvinceIndex = m_ComboxBox1.selectedIndex;
  }
  private void SetCity(string val)
  {
    string province = AppConfig.provinceList[AppConfig.selectProvinceIndex];
    List<string> _cityList = AppConfig.cityDic[province];
    m_ComboxBox2.items = _cityList.ToArray();
    int selectIndex = _cityList.FindIndex(x => x == val);
    if (selectIndex < 0) selectIndex = 0;
    m_ComboxBox2.selectedIndex = selectIndex;
  }

  public void SetData(ObjectVal va)
  {
    data = va;
    m_title.text = "省/市";

    string val = va.val == null ? "" : va.val.ToString();

    switch (va.name)
    {
      case "t_province":
        SetProvince(val);
        break;
      case "t_city":
        SetCity(val);
        break;
      default:
        break;
    }
  }
}