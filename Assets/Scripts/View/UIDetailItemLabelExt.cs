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
  private string template1;

  public override void ConstructFromXML(XML xml)
  {
    base.ConstructFromXML(xml);

    m_InputCombox1.m_ComboxBox1.onChanged.Set(ComboxBox1ChangeHandler);
    m_InputCombox1.m_InputLab.onChanged.Set(OnInputLabChange);
  }

  private void OnInputLabChange(EventContext context)
  {
    string val = m_InputCombox1.m_InputLab.text;
    if (!string.IsNullOrEmpty(val))
    {
      AppData.currTc.SetFieldVal(fieldName, val);
      RefreshUI();
    }
  }

  private void ComboxBox1ChangeHandler(EventContext context)
  {
    if (isHaveTeml1)
    {
      string val = AppData.allTemplates[template1][m_InputCombox1.m_ComboxBox1.selectedIndex];
      AppData.currTc.SetFieldVal(fieldName, val);
      RefreshUI();
    }
  }

  bool isHaveTeml1;
  public void SetData(string _fieldName, string _template1)
  {
    fieldName = _fieldName;
    template1 = _template1;

    isHaveTeml1 = !string.IsNullOrEmpty(template1);

    m_InputCombox1.m_Title.text = AppConfig.fieldsNameDic[fieldName];

    (m_InputCombox1 as UI_InputComboxLabelCompExt).Set_cPosIndex(isHaveTeml1 ? 0 : 1);
    if (isHaveTeml1)
    {
      m_InputCombox1.m_ComboxBox1.items = AppData.allTemplates[template1].ToArray();
      object val = AppData.currTc.GetFieldVal(fieldName);
      m_InputCombox1.m_ComboxBox1.selectedIndex = AppUtil.GetIndexByList(AppData.allTemplates[template1], val.ToString());
    }

    bool isEnabled = AppUtil.GetInputLabEnabled(fieldName);
    m_InputCombox1.m_InputLab.enabled = isEnabled;
    m_InputCombox1.m_ComboxBox1.visible = isEnabled;

    RefreshUI();
  }

  public void RefreshUI()
  {
    if(fieldName == AppConfig.t_totalDebt)
    {
      //欠款金额=(合同总额t_productsPrice-到账总额t_totalAccount)
      object productsPrice = AppData.currTc.GetFieldVal(AppConfig.t_productsPrice);
      object totalAccount = AppData.currTc.GetFieldVal(AppConfig.t_totalAccount);
      float val = (float)productsPrice - (float)totalAccount;
      m_InputCombox1.m_InputLab.text = $"合同金额:{productsPrice} - 到账总额:{totalAccount} = 欠款金额:{val}";
    }
    else
    {
      object val1 = AppData.currTc.GetFieldVal(fieldName);
      m_InputCombox1.m_InputLab.text = val1 == null ? "" : val1.ToString();
    }
  }
}