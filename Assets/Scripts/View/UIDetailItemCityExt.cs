using Basics;
using FairyGUI.Utils;
using FairyGUI;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;

public class UIDetailItemCityExt : UI_DetailItemCity
{
  private string fieldName1;
  private string template1;
  //模版数据
  private List<string> templateList1;

  private int selectProvinceIndex;
  private string provinceName;
  private string cityName;
  public override void ConstructFromXML(XML xml)
  {
    base.ConstructFromXML(xml);

    m_ComboxBox1.onChanged.Add(ComboxBox1ChangeHandler);
    m_ComboxBox2.onChanged.Add(ComboxBox2ChangeHandler);

    m_InputCombox1.m_ComboxBox1.onChanged.Add(InputComboxBox1ChangeHandler);
    m_InputCombox1.m_ComboxBox1.width = m_InputCombox1.m_ComboxBox1.width + 250;
    m_InputCombox1.m_InputBg.width = m_InputCombox1.m_InputBg.width + 400;
  }
  private void InputComboxBox1ChangeHandler(EventContext context)
  {
    if (templateList1 != null)
    {
      string val = templateList1[m_InputCombox1.m_ComboxBox1.selectedIndex];
      AppData.currTc.SetFieldVal(fieldName1, val);
      RefreshUI();
    }
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

  public void SetData(string _provinceName,string _cityName, string _fieldName1, string _template1)
  {
    provinceName = _provinceName;
    cityName = _cityName;
    m_title.text = $"{AppConfig.fieldsNameDic[provinceName]}/{AppConfig.fieldsNameDic[cityName]}";

    fieldName1 = _fieldName1;

    template1 = _template1;

    templateList1 = string.IsNullOrEmpty(template1) ? null : AppData.allTemplates[template1];

    m_InputCombox1.m_InputLab.enabled = AppUtil.GetInputLabEnabled(fieldName1);

    InitInputCombox(m_InputCombox1, fieldName1, templateList1);

    RefreshUI();
  }
  private void InitInputCombox(GComponent item, string fieldName, List<string> templateList)
  {
    UI_InputComboxLabelCompExt itemExt = item as UI_InputComboxLabelCompExt;
    object val = AppData.currTc.GetFieldVal(fieldName);
    if (templateList != null && templateList.Count > 0 && string.IsNullOrEmpty(val.ToString()))
    {
      val = templateList[0];
      AppData.currTc.SetFieldVal(fieldName, val);
    }
    itemExt.SetData(AppConfig.fieldsNameDic[fieldName], templateList, val.ToString());
  }
  public void RefreshUI()
  {
    object obj1 = AppData.currTc.GetFieldVal(provinceName);
    SetProvince(obj1 == null ? "": obj1.ToString());

    object obj2 = AppData.currTc.GetFieldVal(cityName);
    SetCity(obj2 == null ? "" : obj2.ToString());

    object val1 = AppData.currTc.GetFieldVal(fieldName1);
    m_InputCombox1.m_InputLab.text = val1 == null ? "" : val1.ToString();
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