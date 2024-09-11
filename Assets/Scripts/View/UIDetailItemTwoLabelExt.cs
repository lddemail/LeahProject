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
    if(isHaveTeml1)
    {
      string val = AppData.allTemplates[template1][m_InputCombox1.m_ComboxBox1.selectedIndex];
      AppData.currTc.SetFieldVal(fieldName1, val);
      RefreshUI();
    }
 
  }
  private void ComboxBox2ChangeHandler(EventContext context)
  {
    if (isHaveTeml2)
    {
      string val = AppData.allTemplates[template2][m_InputCombox2.m_ComboxBox1.selectedIndex];
      AppData.currTc.SetFieldVal(fieldName2, val);
      RefreshUI();
    }
  }

  bool isHaveTeml1;
  bool isHaveTeml2;
  public void SetData(string _fieldName1, string _template1, string _fieldName2, string _template2)
  {
    fieldName1 = _fieldName1;
    fieldName2 = _fieldName2;

    template1 = _template1;
    template2 = _template2;

    isHaveTeml1 = !string.IsNullOrEmpty(template1);
    isHaveTeml2 = !string.IsNullOrEmpty(template2);

    m_InputCombox1.m_InputLab.enabled = AppUtil.GetInputLabEnabled(fieldName1);
    m_InputCombox2.m_InputLab.enabled = AppUtil.GetInputLabEnabled(fieldName2);

    m_InputCombox1.m_Title.text = AppConfig.fieldsNameDic[fieldName1];
    m_InputCombox2.m_Title.text = AppConfig.fieldsNameDic[fieldName2];


    (m_InputCombox1 as UI_InputComboxLabelCompExt).Set_cPosIndex(isHaveTeml1 ? 0 : 1);
    if (isHaveTeml1)
    {
      m_InputCombox1.m_ComboxBox1.items = AppData.allTemplates[template1].ToArray();
      object val = AppData.currTc.GetFieldVal(fieldName1);
      m_InputCombox1.m_ComboxBox1.selectedIndex = AppUtil.GetIndexByList(AppData.allTemplates[template1], val.ToString());
    }

    (m_InputCombox2 as UI_InputComboxLabelCompExt).Set_cPosIndex(isHaveTeml2 ? 0 : 1);
    if (isHaveTeml2)
    {
      m_InputCombox2.m_ComboxBox1.items = AppData.allTemplates[template2].ToArray();
      object val = AppData.currTc.GetFieldVal(fieldName2);
      m_InputCombox2.m_ComboxBox1.selectedIndex = AppUtil.GetIndexByList(AppData.allTemplates[template2], val.ToString());
    }

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