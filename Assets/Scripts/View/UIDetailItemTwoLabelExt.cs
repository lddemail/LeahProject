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
    if (AppData.allTabContractFiels.ContainsKey(fieldName1))
    {
      string val = AppData.allTabContractFiels[fieldName1][m_InputCombox1.m_ComboxBox1.selectedIndex];
      AppData.currTc.SetFieldVal(fieldName1, val);
      RefreshUI();
    }
 
  }
  private void ComboxBox2ChangeHandler(EventContext context)
  {
    if (AppData.allTabContractFiels.ContainsKey(fieldName2))
    {
      string val = AppData.allTabContractFiels[fieldName2][m_InputCombox2.m_ComboxBox1.selectedIndex];
      AppData.currTc.SetFieldVal(fieldName2, val);
      RefreshUI();
    }
  }

  public void SetData(string _fieldName1,string _fieldName2)
  {
    fieldName1 = _fieldName1;
    fieldName2 = _fieldName2;

    m_InputCombox1.m_InputLab.enabled = AppUtil.GetInputLabEnabled(fieldName1);
    m_InputCombox2.m_InputLab.enabled = AppUtil.GetInputLabEnabled(fieldName2);

    m_InputCombox1.m_Title.text = AppConfig.fieldsNameDic[fieldName1];
    m_InputCombox2.m_Title.text = AppConfig.fieldsNameDic[fieldName2];

    if (AppData.allTabContractFiels.ContainsKey(fieldName1))
    {
      (m_InputCombox1 as UI_InputComboxLabelCompExt).Set_cPosIndex(0);
      m_InputCombox1.m_ComboxBox1.items = AppData.allTabContractFiels[fieldName1].ToArray();
      object val = AppData.currTc.GetFieldVal(fieldName1);
      m_InputCombox1.m_ComboxBox1.selectedIndex = AppUtil.GetIndexByList(AppData.allTabContractFiels[fieldName1], val.ToString());
    }
    else
    {
      (m_InputCombox1 as UI_InputComboxLabelCompExt).Set_cPosIndex(1);
    }

    if (AppData.allTabContractFiels.ContainsKey(fieldName2))
    {
      (m_InputCombox2 as UI_InputComboxLabelCompExt).Set_cPosIndex(0);
      m_InputCombox2.m_ComboxBox1.items = AppData.allTabContractFiels[fieldName2].ToArray();
      object val = AppData.currTc.GetFieldVal(fieldName2);
      m_InputCombox2.m_ComboxBox1.selectedIndex = AppUtil.GetIndexByList(AppData.allTabContractFiels[fieldName2], val.ToString());
    }
    else
    {
      (m_InputCombox2 as UI_InputComboxLabelCompExt).Set_cPosIndex(1);
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