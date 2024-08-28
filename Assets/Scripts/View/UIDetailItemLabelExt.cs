using Basics;
using FairyGUI.Utils;
using FairyGUI;
using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;

public class UIDetailItemLabelExt : UI_DetailItemLabel
{
  private string fieldName;
  public override void ConstructFromXML(XML xml)
  {
    base.ConstructFromXML(xml);

    m_ComboxBox1.onChanged.Set(ComboxBox1ChangeHandler);
    m_InputLab.onChanged.Set(OnInputLabChange);
  }

  private void OnInputLabChange(EventContext context)
  {
    string val = m_InputLab.text;
    if (!string.IsNullOrEmpty(val))
    {
      AppData.currTc.SetFieldVal(fieldName, val);
      RefreshUI();
    }
  }

  private void ComboxBox1ChangeHandler(EventContext context)
  {
    if (AppData.allTabContractFiels.ContainsKey(fieldName))
    {
      string val = AppData.allTabContractFiels[fieldName][m_ComboxBox1.selectedIndex];
      AppData.currTc.SetFieldVal(fieldName, val);
      RefreshUI();
    }
  }

  public void SetData(string _fieldName)
  {
    fieldName = _fieldName;
    m_title.text = fieldName;
    if (AppData.allTabContractFiels.ContainsKey(fieldName))
    {
      m_ComboxBox1.visible = true;
      m_ComboxBox1.items = AppData.allTabContractFiels[fieldName].ToArray();
      object val = AppData.currTc.GetFieldVal(fieldName);
      m_ComboxBox1.selectedIndex = AppUtil.GetIndexByList(AppData.allTabContractFiels[fieldName], val.ToString());
    }
    else
    {
      m_ComboxBox1.visible = false;
    }

    switch (fieldName)
    {
      case "t_productsPrice":
      case "t_totalBarter":
      case "t_totalAccount":
      case "t_totalDebt":
        m_InputLab.enabled = false;
        m_ComboxBox1.visible = false;
        break;
      default:
        m_InputLab.enabled = true;
        m_ComboxBox1.visible = true;
        break;
    }

    RefreshUI();
  }

  public void RefreshUI()
  {
    if(fieldName == "t_totalDebt")
    {
      //欠款金额=(合同总额t_productsPrice-到账总额t_totalAccount)
      object productsPrice = AppData.currTc.GetFieldVal("t_productsPrice");
      object totalAccount = AppData.currTc.GetFieldVal("t_totalAccount");
      float val = (float)productsPrice - (float)totalAccount;
      m_InputLab.text = $"合同金额:{productsPrice} - 到账总额:{totalAccount} = 欠款金额:{val}";
    }
    else
    {
      object val1 = AppData.currTc.GetFieldVal(fieldName);
      m_InputLab.text = val1 == null ? "" : val1.ToString();
    }
  }
}