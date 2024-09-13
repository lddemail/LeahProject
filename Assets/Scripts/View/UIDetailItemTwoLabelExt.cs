using Basics;
using FairyGUI.Utils;
using FairyGUI;
using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;

public class UIDetailItemTwoLabelExt : UI_DetailItemTwoLabel
{

  private string fieldName1;
  private string fieldName2;

  private string template1;
  private string template2;
  //模版数据
  private List<string> templateList1;
  private List<string> templateList2;

  public override void ConstructFromXML(XML xml)
  {
    base.ConstructFromXML(xml);

    m_InputCombox1.m_ComboxBox1.onChanged.Set(ComboxBox1ChangeHandler);
    m_InputCombox2.m_ComboxBox1.onChanged.Set(ComboxBox2ChangeHandler);
    m_InputCombox1.m_InputLab.onChanged.Set(OnInputLab1Change);
    m_InputCombox2.m_InputLab.onChanged.Set(OnInputLab2Change);

  }

  private void OnInputLab1Change(EventContext context)
  {
    string val = m_InputCombox1.m_InputLab.text;
    if(!string.IsNullOrEmpty(val))
    {
      AppData.currTc.SetFieldVal(fieldName1, val);
      RefreshUI();
    }
  }
  private void OnInputLab2Change(EventContext context)
  {
    string val = m_InputCombox2.m_InputLab.text;
    if (!string.IsNullOrEmpty(val))
    {
      AppData.currTc.SetFieldVal(fieldName2, val);
      RefreshUI();
    }
  }
  private void ComboxBox1ChangeHandler(EventContext context)
  {
    if(templateList1 != null)
    {
      string val = templateList1[m_InputCombox1.m_ComboxBox1.selectedIndex];
      AppData.currTc.SetFieldVal(fieldName1, val);
      RefreshUI();
    }
  }
  private void ComboxBox2ChangeHandler(EventContext context)
  {
    if (templateList2 != null)
    {
      string val = templateList2[m_InputCombox2.m_ComboxBox1.selectedIndex];
      AppData.currTc.SetFieldVal(fieldName2, val);
      RefreshUI();
    }
  }

  public void SetData(string _fieldName1, string _template1, string _fieldName2, string _template2)
  {
    fieldName1 = _fieldName1;
    fieldName2 = _fieldName2;

    template1 = _template1;
    template2 = _template2;

    templateList1 = string.IsNullOrEmpty(template1) ? null : AppData.allTemplates[template1];
    templateList2 = string.IsNullOrEmpty(template2) ? null : AppData.allTemplates[template2];

    m_InputCombox1.m_InputLab.enabled = AppUtil.GetInputLabEnabled(fieldName1);
    m_InputCombox2.m_InputLab.enabled = AppUtil.GetInputLabEnabled(fieldName2);

    object val = AppData.currTc.GetFieldVal(fieldName1);
    (m_InputCombox1 as UI_InputComboxLabelCompExt).SetData(AppConfig.fieldsNameDic[fieldName1],templateList1, val.ToString());

    val = AppData.currTc.GetFieldVal(fieldName2);
    (m_InputCombox2 as UI_InputComboxLabelCompExt).SetData(AppConfig.fieldsNameDic[fieldName2],templateList2, val.ToString());

    RefreshUI();
  }
  public void RefreshUI()
  {
    object val1 = AppData.currTc.GetFieldVal(fieldName1);
    m_InputCombox1.m_InputLab.text = val1 == null ? "" : val1.ToString();

    object val2 = AppData.currTc.GetFieldVal(fieldName2);
    m_InputCombox2.m_InputLab.text = val2 == null ? "" : val2.ToString();
  }

}