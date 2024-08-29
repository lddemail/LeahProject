using Basics;
using FairyGUI.Utils;
using FairyGUI;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;

public class UIDetailItemCityExt : UI_DetailItemCity
{
  private int selectProvinceIndex;
  private string provinceName;
  private string cityName;
  public override void ConstructFromXML(XML xml)
  {
    base.ConstructFromXML(xml);

    m_ComboxBox1.onChanged.Add(ComboxBox1ChangeHandler);
    m_ComboxBox2.onChanged.Add(ComboxBox2ChangeHandler);
  }

  private void ComboxBox1ChangeHandler(EventContext context)
  {
    selectProvinceIndex = m_ComboxBox1.selectedIndex;
    string province = AppConfig.provinceList[selectProvinceIndex];
    if(!string.IsNullOrEmpty(province))
    {
      AppData.currTc.SetFieldVal(provinceName, province);
      string city = AppConfig.cityDic[province][0];
      SetCity(city);
      AppData.currTc.SetFieldVal(cityName, city);
    }
  }
  private void ComboxBox2ChangeHandler(EventContext context)
  {
    string province = AppConfig.provinceList[selectProvinceIndex];
    string city = AppConfig.cityDic[province][m_ComboxBox2.selectedIndex];
    if(!string.IsNullOrEmpty(city))
    {
      AppData.currTc.SetFieldVal(cityName, city);
    }
  }


  private void SetProvince(string val)
  {
    m_ComboxBox1.items = AppConfig.provinceList.ToArray();
    m_ComboxBox1.selectedIndex = AppUtil.GetIndexByList(AppConfig.provinceList, val);
    selectProvinceIndex = m_ComboxBox1.selectedIndex;
  }
  private void SetCity(string val)
  {
    string province = AppConfig.provinceList[selectProvinceIndex];
    List<string> _cityList = AppConfig.cityDic[province];
    m_ComboxBox2.items = _cityList.ToArray();
    m_ComboxBox2.selectedIndex = AppUtil.GetIndexByList(_cityList, val);
  }

  public void SetData(string _provinceName,string _cityName)
  {
    provinceName = _provinceName;
    cityName = _cityName;
    m_title.text = "省/市";


    RefreshUI();
  }
  public void RefreshUI()
  {
    object obj1 = AppData.currTc.GetFieldVal(provinceName);
    SetProvince(obj1 == null ? "": obj1.ToString());

    object obj2 = AppData.currTc.GetFieldVal(cityName);
    SetCity(obj2 == null ? "" : obj2.ToString());
  }

  public string GetProvince()
  {
    string province = AppConfig.provinceList[selectProvinceIndex];
    return province;
  }
  public string GetCity()
  {
    string province = AppConfig.provinceList[selectProvinceIndex];
    List<string> _cityList = AppConfig.cityDic[province];
    string city = _cityList[m_ComboxBox2.selectedIndex];
    return city;
  }
}