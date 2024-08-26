using Basics;
using FairyGUI.Utils;
using FairyGUI;
using System.Collections;
using UnityEngine;
using System;

public class UIDetailItemProductExt : UI_DetailItemProduct
{

  public override void ConstructFromXML(XML xml)
  {
    base.ConstructFromXML(xml);

    m_InputLabPrice.onChanged.Set(OnChangeCallBack);
  }

  private void OnChangeCallBack(EventContext context)
  {
    if(string.IsNullOrEmpty(m_InputLabPrice.text))
    {
      m_InputLabPrice.text = "0";
    }  

    ProductData pdd = (ProductData)data;
    pdd.name = m_InputLabName.text;
    pdd.price = float.Parse(m_InputLabPrice.text);
    pdd.fTime = AppUtil.StringToTime(m_InputLabfTime.text);
    pdd.tTime = AppUtil.StringToTime(m_InputLabtTime.text);
    pdd.remark = m_InputLabRemark.text;

    _changeCallBack?.Invoke();
  }

  public void SetData(ProductData pdd)
  {
    data = pdd;
    RefreshUI();
  }

  public void RefreshUI()
  {
    ProductData pdd = (ProductData)data;
    m_InputLabName.text = pdd.name;
    m_InputLabPrice.text = pdd.price.ToString();
    m_InputLabfTime.text = AppUtil.TimeToString(pdd.fTime);
    m_InputLabtTime.text = AppUtil.TimeToString(pdd.tTime);
    m_InputLabRemark.text = pdd.remark;
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