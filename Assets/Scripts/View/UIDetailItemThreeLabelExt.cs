using Basics;
using FairyGUI.Utils;
using FairyGUI;
using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;

public class UIDetailItemThreeLabelExt : UI_DetailItemThreeLabel
{
  //字段名
  private string fieldName1;
  private string fieldName2;
  private string fieldName3;
  //模版名
  private string template1;
  private string template2;
  private string template3;
  //模版数据
  private List<string> templateList1;
  private List<string> templateList2;
  private List<string> templateList3;

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
    if (templateList1 != null)
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
  private void ComboxBox3ChangeHandler(EventContext context)
  {
    if (templateList3 != null)
    {
      string val = templateList3[m_InputCombox3.m_ComboxBox1.selectedIndex];
      AppData.currTc.SetFieldVal(fieldName3, val);
      RefreshUI();
    }
  }

  public void SetData(string _fieldName1,string _template1, string _fieldName2, string _template2, string _fieldName3, string _template3)
  {
    fieldName1 = _fieldName1;
    fieldName2 = _fieldName2;
    fieldName3 = _fieldName3;

    template1 = _template1;
    template2 = _template2;
    template3 = _template3;

    templateList1 = string.IsNullOrEmpty(template1) ? null : AppData.allTemplates[template1];
    templateList2 = string.IsNullOrEmpty(template2) ? null : AppData.allTemplates[template2];
    templateList3 = string.IsNullOrEmpty(template3) ? null : AppData.allTemplates[template3];

    m_InputCombox1.m_InputLab.enabled = AppUtil.GetInputLabEnabled(fieldName1);
    m_InputCombox2.m_InputLab.enabled = AppUtil.GetInputLabEnabled(fieldName2);
    m_InputCombox3.m_InputLab.enabled = AppUtil.GetInputLabEnabled(fieldName3);

    InitInputCombox(m_InputCombox1, fieldName1, templateList1);
    InitInputCombox(m_InputCombox2, fieldName2, templateList2);
    InitInputCombox(m_InputCombox3, fieldName3, templateList3);

    RefreshUI();
  }
  private void InitInputCombox(GComponent item,string fieldName,List<string> templateList)
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
    object val1 = AppData.currTc.GetFieldVal(fieldName1);
    m_InputCombox1.m_InputLab.text = val1 == null ? "" : val1.ToString();

    object val2 = AppData.currTc.GetFieldVal(fieldName2);
    m_InputCombox2.m_InputLab.text = val2 == null ? "" : val2.ToString();

    if (fieldName3 == AppConfig.t_totalDebt)
    {
      //欠款金额=(合同总额t_productsPrice-到账总额t_totalAccount)
      object productsPrice = AppData.currTc.GetFieldVal(AppConfig.t_productsPrice);
      object totalAccount = AppData.currTc.GetFieldVal(AppConfig.t_totalAccount);
      float val = (float)productsPrice - (float)totalAccount;
      m_InputCombox3.m_InputLab.text = $"{val} = {productsPrice} - {totalAccount}";
    }
    else
    {
      object val3 = AppData.currTc.GetFieldVal(fieldName3);
      m_InputCombox3.m_InputLab.text = val3 == null ? "" : val3.ToString();
    }
  }
}