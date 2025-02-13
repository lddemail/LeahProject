using Basics;
using FairyGUI.Utils;
using FairyGUI;
using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;

public class UIDetailItemProductExt : UI_DetailItemProduct
{
  //模版数据
  private List<string> templateList1;

  public override void ConstructFromXML(XML xml)
  {
    base.ConstructFromXML(xml);

    m_InputLabPrice.m_InputLab.onChanged.Set(OnChangeCallBack);
    m_InputLabfTime.m_InputLab.onChanged.Set(OnChangeCallBack);
    m_InputLabtTime.m_InputLab.onChanged.Set(OnChangeCallBack);
    m_InputLabRemark.m_InputLab.onChanged.Set(OnChangeCallBack);
    m_InputComboxName.m_InputLab.onChanged.Set(OnChangeCallBack);

    //m_InputComboxName.m_ComboxBox1.onChanged.Set(ComboxBox1ChangeHandler);
    //m_InputComboxName.m_ComboxBox1.width = m_InputComboxName.m_ComboxBox1.width + 360;
    //m_InputComboxName.m_InputBg.width = m_InputComboxName.m_InputBg.width + 160;
  }
  private void ComboxBox1ChangeHandler(EventContext context)
  {
    if (templateList1 != null)
    {
      //string val = templateList1[m_InputComboxName.m_ComboxBox1.selectedIndex];
      //m_InputComboxName.m_InputLab.text = val;
      OnChangeCallBack(null);
    }
  }
  private void OnChangeCallBack(EventContext context)
  {
    if(string.IsNullOrEmpty(m_InputLabPrice.m_InputLab.text))
    {
      m_InputLabPrice.m_InputLab.text = "0";
    }
    int index = m_InputLabPrice.m_InputLab.text.LastIndexOf(".");
    if (index == (m_InputLabPrice.m_InputLab.text.Length - 1))
    {
      return;
    }

    ProductData pd = (ProductData)data;
    //pd.name = templateList1[m_InputComboxName.m_ComboxBox1.selectedIndex];
    pd.name = m_InputComboxName.m_InputLab.text;
    decimal.TryParse(m_InputLabPrice.m_InputLab.text,out pd.price);
    pd.fTime = AppUtil.StringToTime(m_InputLabfTime.m_InputLab.text);
    pd.tTime = AppUtil.StringToTime(m_InputLabtTime.m_InputLab.text);
    pd.remark = m_InputLabRemark.m_InputLab.text;
    Debug.Log("OnChangeCallBack:"+pd.ToStr());
    _changeCallBack?.Invoke();
  }

  public void SetData(ProductData pd)
  {
    data = pd;
    //templateList1 = AppData.allTemplates[AppConfig.ProductTemplateName];
    if(string.IsNullOrEmpty(pd.name) && templateList1 != null && templateList1.Count > 0)
    {
      pd.name = templateList1[0];
    }
    (m_InputComboxName as UI_InputComboxLabelCompExt).SetData("产品名字:",templateList1, pd.name);
    //m_InputComboxName.m_InputLab.enabled = false;

    m_InputLabPrice.m_Title.text = "价格:";

    if (pd.fTime < 1) pd.fTime = AppUtil.GetNowUnixTime();

    if (pd.tTime < 1) pd.tTime = pd.fTime;

    RefreshUI();
  }

  public void RefreshUI()
  {
    ProductData pd = (ProductData)data;
    m_InputComboxName.m_InputLab.text = pd.name;
    m_InputLabPrice.m_InputLab.text = pd.price.ToString();
    m_InputLabfTime.m_InputLab.text = AppUtil.TimeToString(pd.fTime);
    m_InputLabtTime.m_InputLab.text = $"{AppUtil.TimeToString(pd.tTime)}";
    m_AdventLab.text = $"[color=#FF0000]{pd.GetAdventStr()}[/color]";
    m_InputLabRemark.m_InputLab.text = pd.remark;

    //float _w = Mathf.Max(AppUtil.GetTextWidthByStr(pd.name), m_InputComboxName.m_ComboxBox1.width);
    //m_InputComboxName.m_InputBg.width = _w;
    m_InputComboxName.tooltips = pd.name;
    m_InputLabRemark.tooltips = pd.remark;
    Debug.Log("RefreshUI:"+pd.ToStr());
  }

  private Action _changeCallBack;
  public void SetChangeCallBack(Action changeCallBack)
  {
    _changeCallBack = changeCallBack;
  }

  public ProductData GetProductData()
  {
    ProductData res = (ProductData)data;
    return res;
  }
}