﻿using Basics;
using FairyGUI.Utils;
using FairyGUI;
using System.Collections;
using UnityEngine;
using System;

public class UIDetailItemLineExt : UI_DetailItemLine
{
  private string fieldName;
  public override void ConstructFromXML(XML xml)
  {
    base.ConstructFromXML(xml);
  }

  public void SetData(string _fieldName)
  {
    fieldName = _fieldName;
    RefreshUI();
  }

  public void RefreshUI()
  {
    string text = AppUtil.GetFormatVal(AppData.currTc, fieldName);
    string configFieldName = AppConfig.fieldsNameDic[fieldName];
    switch (fieldName)
    {
      case AppConfig.t_productsPrice: //产品总额
        m_title.text = $"{configFieldName}:{text}";
        break;
      case AppConfig.t_totalBarter: //酒店消费
        m_title.text = $"{configFieldName}:{text}";
        break;
      case AppConfig.t_totalAccount: //到账
        m_title.text = $"{configFieldName}:{text}";
        break;
    }
  }
}