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

    m_ComboxBox1.onChanged.Add(ComboxBox1ChangeHandler);
  }

  private void ComboxBox1ChangeHandler(EventContext context)
  {
    if (m_title.text == "t_province")
    {
      AppConfig.selectProvinceIndex = m_ComboxBox1.selectedIndex;
      m_InputLab.text = AppConfig.provinceList[m_ComboxBox1.selectedIndex];

      //需要抛出事件联动市的选择
    }
    else
    {
      m_InputLab.text = AppData.allTabContractFiels[m_title.text][m_ComboxBox1.selectedIndex];
    }
  }

  public void SetData(ObjectVal va, bool isEnabled=true)
  {
    data = va;
    m_title.text = va.name;
    m_InputLab.text = va.val == null ?  "": va.val.ToString();
    m_InputLab.enabled = isEnabled;

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
        m_ComboxBox1.items = AppData.allTabContractFiels[va.name].ToArray();
        m_ComboxBox1.selectedIndex = 0;
        break;
      default:
        m_ComboxBox1.visible = false;
        break;
    }
  }
}