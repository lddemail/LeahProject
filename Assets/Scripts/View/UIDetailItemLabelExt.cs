using Basics;
using FairyGUI.Utils;
using FairyGUI;
using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;

public class UIDetailItemLabelExt : UI_DetailItemLabel
{
  private string fieldName1;
  private string template1;
  //模版数据
  private List<string> templateList1;

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
      AppData.currTc.SetFieldVal(fieldName1, val);
      RefreshUI();
    }
  }

  private void ComboxBox1ChangeHandler(EventContext context)
  {
    if (templateList1 != null)
    {
      string val = templateList1[m_InputCombox1.m_ComboxBox1.selectedIndex];
      AppData.currTc.SetFieldVal(fieldName1, val);
      RefreshUI();
    }
  }

  public void SetData(string _fieldName, string _template1)
  {
    fieldName1 = _fieldName;
    template1 = _template1;

    templateList1 = string.IsNullOrEmpty(template1) ? null : AppData.allTemplates[template1];

    object val = AppData.currTc.GetFieldVal(fieldName1);
    (m_InputCombox1 as UI_InputComboxLabelCompExt).SetData(AppConfig.fieldsNameDic[fieldName1],templateList1, val.ToString());

    m_InputCombox1.m_InputLab.enabled = AppUtil.GetInputLabEnabled(fieldName1); 
    m_InputCombox1.m_ComboxBox1.visible = m_InputCombox1.m_InputLab.enabled;

    RefreshUI();
  }

  public void RefreshUI()
  {
    if(fieldName1 == AppConfig.t_totalDebt)
    {
      //欠款金额=(合同总额t_productsPrice-到账总额t_totalAccount)
      object productsPrice = AppData.currTc.GetFieldVal(AppConfig.t_productsPrice);
      object totalAccount = AppData.currTc.GetFieldVal(AppConfig.t_totalAccount);
      float val = (float)productsPrice - (float)totalAccount;
      m_InputCombox1.m_InputLab.text = $"合同金额:{productsPrice} - 到账总额:{totalAccount} = 欠款金额:{val}";
    }
    else
    {
      object val1 = AppData.currTc.GetFieldVal(fieldName1);
      m_InputCombox1.m_InputLab.text = val1 == null ? "" : val1.ToString();
    }
  }
}