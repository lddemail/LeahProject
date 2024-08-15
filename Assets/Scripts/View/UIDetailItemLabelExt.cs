using Basics;
using FairyGUI.Utils;
using FairyGUI;
using System.Collections;
using UnityEngine;
using System;

public class UIDetailItemLabelExt : UI_DetailItemLabel
{

  public override void ConstructFromXML(XML xml)
  {
    base.ConstructFromXML(xml);

    m_ComboxBox1.items = AppConfig.provinceAry;
    m_ComboxBox1.onChanged.Add(ComboxBox1ChangeHandler);
    m_ComboxBox1.selectedIndex = 0;
    ComboxBox1ChangeHandler(null);
  }

  private void ComboxBox1ChangeHandler(EventContext context)
  {
    string province = AppConfig.provinceAry[m_ComboxBox1.selectedIndex];
    m_ComboxBox2.items = AppConfig.cityDic[province].ToArray();
    m_ComboxBox2.selectedIndex = 0;
  }

  public void SetData(ObjectVal va, bool isEnabled=true)
  {
    data = va;
    m_title.text = va.name;
    m_InputLab.text = va.val == null ?  "": va.val.ToString();
    m_InputLab.enabled = isEnabled;
  }
}