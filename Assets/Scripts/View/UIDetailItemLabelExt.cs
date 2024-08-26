using Basics;
using FairyGUI.Utils;
using FairyGUI;
using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;

public class UIDetailItemLabelExt : UI_DetailItemLabel
{

  public override void ConstructFromXML(XML xml)
  {
    base.ConstructFromXML(xml);

    m_ComboxBox1.onChanged.Set(ComboxBox1ChangeHandler);
    m_InputLab.onChanged.Set(OnInputLabChange);
  }

  private void OnInputLabChange(EventContext context)
  {
    ObjectVal va = (ObjectVal)data;
    va.val = m_InputLab.text;
  }

  private void ComboxBox1ChangeHandler(EventContext context)
  {
    m_InputLab.text = AppData.allTabContractFiels[m_title.text][m_ComboxBox1.selectedIndex];
  }

  public void SetData(ObjectVal va)
  {
    data = va;
    RefreshUI();
  }
  public void RefreshUI()
  {
    ObjectVal va = (ObjectVal)data;
    va.val = AppData.currTc.GetObjectVal(va.name);

    m_title.text = va.name;
    m_InputLab.text = va.val == null ? "" : va.val.ToString();

    m_ComboxBox1.visible = true;
    switch (va.name)
    {
      case "t_hotelName":
      case "t_group":
      case "t_brand":
      case "t_originalFollowup":
      case "t_newSales":
      case "t_payment":
      case "t_a_contract":
        m_InputLab.enabled = true;
        m_ComboxBox1.visible = true;
        m_ComboxBox1.items = AppData.allTabContractFiels[va.name].ToArray();
        m_ComboxBox1.selectedIndex = 0;
        break;
      case "t_productsPrice":
      case "t_totalBarter":
      case "t_totalAccount":
      case "t_totalDebt":
        m_InputLab.enabled = false;
        m_ComboxBox1.visible = false;
        break;
      default:
        m_ComboxBox1.visible = false;
        break;
    }
  }
  public ObjectVal GetOV()
  {
    ObjectVal res = (ObjectVal)data;
    return res;
  }
}