using Basics;
using FairyGUI.Utils;
using FairyGUI;
using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;

public class UIDetailItemOneLabelExt : UI_DetailItemLabel
{
  private string fieldName1;
  private string template1;
  //模版数据
  private List<string> templateList1;

  public override void ConstructFromXML(XML xml)
  {
    base.ConstructFromXML(xml);

    m_InputCombox1.m_ComboxBox1.onChanged.Set(ComboxBox1ChangeHandler);
    //m_InputCombox1.m_InputLab.onChanged.Set(OnInputLabChange);
  }

  private void OnInputLabChange(EventContext context)
  {
    //string val = m_InputCombox1.m_InputLab.text;
    //if (!string.IsNullOrEmpty(val))
    //{
    //  AppData.currTc.SetFieldVal(fieldName1, val);
    //  RefreshUI();
    //}
  }

  private void ComboxBox1ChangeHandler(EventContext context)
  {
    string val = (m_InputCombox1 as UI_InputComboxLabelCompExt).GetCurrVal();
    if (!string.IsNullOrEmpty(val))
    {
      AppData.currTc.SetFieldVal(fieldName1, val);
      RefreshUI();
    }
  }

  public void SetData(string _fieldName, string _template1)
  {
    fieldName1 = _fieldName;
    template1 = _template1;

    templateList1 = string.IsNullOrEmpty(template1) ? null : AppData.GetTempList(template1);

    m_InputCombox1.m_InputLab.enabled = AppUtil.GetInputLabEnabled(fieldName1);
    m_InputCombox1.m_ComboxBox1.visible = AppUtil.GetInputLabEnabled(fieldName1);

    InitInputCombox(m_InputCombox1, fieldName1, templateList1);

    RefreshUI();
  }
  private Action _changeCallBack;
  public void SetChangeCallBack(Action changeCallBack)
  {
    _changeCallBack = changeCallBack;
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
    string text = AppUtil.GetFormatVal(AppData.currTc, fieldName1);
    m_InputCombox1.tooltips = text;
    m_InputCombox1.m_InputLab.text = text;
  }
}