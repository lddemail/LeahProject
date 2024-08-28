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

    m_ComboxBox1.onChanged.Set(ComboxBox1ChangeHandler);
    m_ComboxBox2.onChanged.Set(ComboxBox2ChangeHandler);
    m_InputLab1.onChanged.Set(OnInputLab1Change);
    m_InputLab2.onChanged.Set(OnInputLab2Change);
  }

  private void OnInputLab1Change(EventContext context)
  {
    string val = m_InputLab1.text;
    if(!string.IsNullOrEmpty(val))
    {
      AppData.currTc.SetFieldVal(fieldName1, val);
      RefreshUI();
    }
  }
  private void OnInputLab2Change(EventContext context)
  {
    string val = m_InputLab2.text;
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
      string val = AppData.allTabContractFiels[fieldName1][m_ComboxBox1.selectedIndex];
      AppData.currTc.SetFieldVal(fieldName1, val);
      RefreshUI();
    }
 
  }
  private void ComboxBox2ChangeHandler(EventContext context)
  {
    if (AppData.allTabContractFiels.ContainsKey(fieldName2))
    {
      string val = AppData.allTabContractFiels[fieldName2][m_ComboxBox2.selectedIndex];
      AppData.currTc.SetFieldVal(fieldName2, val);
      RefreshUI();
    }
  }

  public void SetData(string _fieldName1,string _fieldName2)
  {
    fieldName1 = _fieldName1;
    fieldName2 = _fieldName2;

    m_title1.text = fieldName1;
    m_title2.text = fieldName2;

    if (AppData.allTabContractFiels.ContainsKey(fieldName1))
    {
      m_ComboxBox1.visible = true;
      m_ComboxBox1.items = AppData.allTabContractFiels[fieldName1].ToArray();
      object val = AppData.currTc.GetFieldVal(fieldName1);
      m_ComboxBox1.selectedIndex = AppUtil.GetIndexByList(AppData.allTabContractFiels[fieldName1], val.ToString());
    }
    else
    {
      m_ComboxBox1.visible = false;
    }

    if (AppData.allTabContractFiels.ContainsKey(fieldName2))
    {
      m_ComboxBox2.visible = true;
      m_ComboxBox2.items = AppData.allTabContractFiels[fieldName2].ToArray();
      object val = AppData.currTc.GetFieldVal(fieldName2);
      m_ComboxBox2.selectedIndex = AppUtil.GetIndexByList(AppData.allTabContractFiels[fieldName2], val.ToString());
    }
    else
    {
      m_ComboxBox2.visible = false;
    }

    RefreshUI();
  }
  public void RefreshUI()
  {
    object val1 = AppData.currTc.GetFieldVal(fieldName1);
    m_InputLab1.text = val1 == null ? "" : val1.ToString();

    Debug.Log(m_InputLab1.text);

    object val2 = AppData.currTc.GetFieldVal(fieldName2);
    m_InputLab2.text = val2 == null ? "" : val2.ToString();
  }

}