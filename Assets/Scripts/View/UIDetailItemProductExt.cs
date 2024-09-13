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
    m_InputComboxName.m_ComboxBox1.onChanged.Set(ComboxBox1ChangeHandler);
  }
  private void ComboxBox1ChangeHandler(EventContext context)
  {
    if (templateList1 != null)
    {
      string val = templateList1[m_InputComboxName.m_ComboxBox1.selectedIndex];
      m_InputComboxName.m_InputLab.text = val;
      OnChangeCallBack(null);
    }
  }
  private void OnChangeCallBack(EventContext context)
  {
    if(string.IsNullOrEmpty(m_InputLabPrice.m_InputLab.text))
    {
      m_InputLabPrice.m_InputLab.text = "0";
    }  

    ProductData pd = (ProductData)data;
    pd.name = m_InputComboxName.m_InputLab.text;
    pd.price = float.Parse(m_InputLabPrice.m_InputLab.text);
    pd.fTime = AppUtil.StringToTime(m_InputLabfTime.text);
    pd.tTime = AppUtil.StringToTime(m_InputLabtTime.text);
    pd.remark = m_InputLabRemark.text;

    _changeCallBack?.Invoke();
  }

  public void SetData(ProductData pd)
  {
    data = pd;
    templateList1 = AppData.allTemplates[AppConfig.ProductTemplateName];
    (m_InputComboxName as UI_InputComboxLabelCompExt).SetData("产品名字:",templateList1, pd.name);
    m_InputComboxName.m_InputLab.enabled = false;

    m_InputLabPrice.m_Title.text = "价格:";
    m_InputLabfTime.m_Title.text = "开始日期:";
    m_InputLabtTime.m_Title.text = "结束日期:";

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