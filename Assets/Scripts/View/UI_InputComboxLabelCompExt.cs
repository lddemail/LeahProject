using Basics;
using FairyGUI.Utils;
using FairyGUI;
using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;

public class UI_InputComboxLabelCompExt : UI_InputComboxLabelComp
{
  public override void ConstructFromXML(XML xml)
  {
    base.ConstructFromXML(xml);
    m_InputBg.onFocusIn.Add(OnFocusInHandler);
    m_InputBg.onFocusOut.Add(OnFocusOutHandler);
    m_InputLab.onFocusIn.Add(OnFocusInHandler);
    m_InputLab.onFocusOut.Add(OnFocusOutHandler);

    m_FilterLab.onChanged.Add(FilterLabChangeHandler);
  }

  private void FilterLabChangeHandler(EventContext context)
  {
    if(!string.IsNullOrEmpty(m_FilterLab.text))
    {
      List<string> list = templateList1.FindAll(x => x.Contains(m_FilterLab.text));
      if(list != null)
      {
        m_ComboxBox1.items = list.ToArray();
      }
    }
    else
    {
      m_ComboxBox1.items = templateList1.ToArray();
    }
  }

  private void OnFocusInHandler(EventContext context)
  {
    m_InputBg.color = AppConfig.selectBgColor;
  }
  private void OnFocusOutHandler(EventContext context)
  {
    m_InputBg.color = Color.white;
  }

  public void Set_cPosIndex(int index)
  {
    m_cPos.SetSelectedIndex(index);
  }

  private List<string> templateList1;
  public void SetData(string title,List<string> itemList,string val)
  {
    m_Title.text = title;
    Set_cPosIndex(itemList != null ? 0 : 1);
    templateList1 = itemList;
    if(templateList1 != null)
    {
      m_ComboxBox1.items = templateList1.ToArray();
      int index = 0;
      if (!string.IsNullOrEmpty(val))
      {
        index = AppUtil.GetIndexByList(templateList1, val);
      }
      m_ComboxBox1.selectedIndex = index;
      m_InputLab.text = templateList1[index];
      m_FilterLab.text = "";
    }
    RefreshUI();
  }
  public void RefreshUI()
  {

  }
}