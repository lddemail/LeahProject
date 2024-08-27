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

    ProductData pd = (ProductData)data;
    pd.name = m_InputLabName.text;
    pd.price = float.Parse(m_InputLabPrice.text);
    pd.fTime = AppUtil.StringToTime(m_InputLabfTime.text);
    pd.tTime = AppUtil.StringToTime(m_InputLabtTime.text);
    pd.remark = m_InputLabRemark.text;

    _changeCallBack?.Invoke();
  }

  public void SetData(ProductData pd)
  {
    data = pd;
    RefreshUI();
  }

  public void RefreshUI()
  {
    ProductData pd = (ProductData)data;
    m_InputLabName.text = pd.name;
    m_InputLabPrice.text = pd.price.ToString();
    m_InputLabfTime.text = AppUtil.TimeToString(pd.fTime);
    m_InputLabtTime.text = AppUtil.TimeToString(pd.tTime);
    m_InputLabRemark.text = pd.remark;
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