using Basics;
using FairyGUI.Utils;
using FairyGUI;
using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;

public class UIDetailItemThreeLabelExt : UI_DetailItemThreeLabel
{
  private string fieldName1;
  private string fieldName2;
  private string fieldName3;
  public override void ConstructFromXML(XML xml)
  {
    base.ConstructFromXML(xml);

    m_InputCombox1.m_ComboxBox1.onChanged.Set(ComboxBox1ChangeHandler);
    m_InputCombox2.m_ComboxBox1.onChanged.Set(ComboxBox2ChangeHandler);
    m_InputCombox3.m_ComboxBox1.onChanged.Set(ComboxBox3ChangeHandler);
    m_InputCombox1.m_InputLab.onChanged.Set(OnInputLab1Change);
    m_InputCombox2.m_InputLab.onChanged.Set(OnInputLab2Change);
    m_InputCombox3.m_InputLab.onChanged.Set(OnInputLab3Change);

  }

  private void OnInputLab1Change(EventContext context)
  {
    string val = m_InputCombox1.m_InputLab.text;
    if (!string.IsNullOrEmpty(val))
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
  private void OnInputLab3Change(EventContext context)
  {
    string val = m_InputCombox3.m_InputLab.text;
    if (!string.IsNullOrEmpty(val))
    {
      AppData.currTc.SetFieldVal(fieldName3, val);
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
  private void ComboxBox3ChangeHandler(EventContext context)
  {
    if (AppData.allTabContractFiels.ContainsKey(fieldName3))
    {
      string val = AppData.allTabContractFiels[fieldName3][m_InputCombox3.m_ComboxBox1.selectedIndex];
      AppData.currTc.SetFieldVal(fieldName3, val);
      RefreshUI();
    }
  }

  public void SetData(string _fieldName1, string _fieldName2, string _fieldName3)
  {
    fieldName1 = _fieldName1;
    fieldName2 = _fieldName2;
    fieldName3 = _fieldName3;

    m_InputCombox1.m_InputLab.enabled = AppUtil.GetInputLabEnabled(fieldName1);
    m_InputCombox2.m_InputLab.enabled = AppUtil.GetInputLabEnabled(fieldName2);
    m_InputCombox3.m_InputLab.enabled = AppUtil.GetInputLabEnabled(fieldName3);

    m_InputCombox1.m_Title.text = AppConfig.fieldsNameDic[fieldName1];
    m_InputCombox2.m_Title.text = AppConfig.fieldsNameDic[fieldName2];
    m_InputCombox3.m_Title.text = AppConfig.fieldsNameDic[fieldName3];


    if (AppData.allTabContractFiels.ContainsKey(fieldName1))
    {
      m_InputCombox1.m_c1.SetSelectedIndex(0);
      m_InputCombox1.m_ComboxBox1.items = AppData.allTabContractFiels[fieldName1].ToArray();
      object val = AppData.currTc.GetFieldVal(fieldName1);
      m_InputCombox1.m_ComboxBox1.selectedIndex = AppUtil.GetIndexByList(AppData.allTabContractFiels[fieldName1], val.ToString());
    }
    else
    {
      m_InputCombox1.m_c1.SetSelectedIndex(1);
    }

    if (AppData.allTabContractFiels.ContainsKey(fieldName2))
    {
      m_InputCombox2.m_c1.SetSelectedIndex(0);
      m_InputCombox2.m_ComboxBox1.items = AppData.allTabContractFiels[fieldName2].ToArray();
      object val = AppData.currTc.GetFieldVal(fieldName2);
      m_InputCombox2.m_ComboxBox1.selectedIndex = AppUtil.GetIndexByList(AppData.allTabContractFiels[fieldName2], val.ToString());
    }
    else
    {
      m_InputCombox2.m_c1.SetSelectedIndex(1);
    }
 

    if (AppData.allTabContractFiels.ContainsKey(fieldName3))
    {
      m_InputCombox3.m_c1.SetSelectedIndex(0);
      m_InputCombox3.m_ComboxBox1.items = AppData.allTabContractFiels[fieldName3].ToArray();
      object val = AppData.currTc.GetFieldVal(fieldName3);
      m_InputCombox3.m_ComboxBox1.selectedIndex = AppUtil.GetIndexByList(AppData.allTabContractFiels[fieldName3], val.ToString());
    }
    else
    {
      m_InputCombox3.m_c1.SetSelectedIndex(1);
    }

  

    RefreshUI();
  }
  public void RefreshUI()
  {
    object val1 = AppData.currTc.GetFieldVal(fieldName1);
    m_InputCombox1.m_InputLab.text = val1 == null ? "" : val1.ToString();

    object val2 = AppData.currTc.GetFieldVal(fieldName2);
    m_InputCombox2.m_InputLab.text = val2 == null ? "" : val2.ToString();

    if (fieldName3 == AppConfig.t_totalDebt)
    {
      m_InputCombox3.m_InputLab.enabled = false;
      //欠款金额=(合同总额t_productsPrice-到账总额t_totalAccount)
      object productsPrice = AppData.currTc.GetFieldVal(AppConfig.t_productsPrice);
      object totalAccount = AppData.currTc.GetFieldVal(AppConfig.t_totalAccount);
      float val = (float)productsPrice - (float)totalAccount;
      m_InputCombox3.m_InputLab.text = $"{val} = {productsPrice} - {totalAccount}";
    }
    else
    {
      m_InputCombox3.m_InputLab.enabled = false;
      object val3 = AppData.currTc.GetFieldVal(fieldName3);
      m_InputCombox3.m_InputLab.text = val3 == null ? "" : val3.ToString();
    }
  }
}