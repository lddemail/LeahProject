﻿using Basics;
using FairyGUI.Utils;
using FairyGUI;
using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;

public class UI_InputComboxLabelCompExt : UI_InputComboxLabelComp
{

  /// <summary>
  /// 当前模版 因为过滤会改
  /// </summary>
  public List<string> currTemplateList;

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
      currTemplateList = templateList1.FindAll(x => x.Contains(m_FilterLab.text));
    }
    else
    {
      currTemplateList = templateList1;
    }
    if (currTemplateList != null)
    {
      m_ComboxBox1.items = currTemplateList.ToArray();
    }
    _filterChangeCallBack?.Invoke();
  }
  private Action _filterChangeCallBack;
  public void SetFilterChangeCallBack(Action changeCallBack)
  {
    _filterChangeCallBack = changeCallBack;
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

  public string GetCurrVal()
  {
    if (currTemplateList != null)
    {
      string val = currTemplateList[m_ComboxBox1.selectedIndex];
      return val;
    }
    return "";
  }

  private List<string> templateList1;
  public void SetData(string title,List<string> itemList,string val)
  {
    m_Title.text = title;
    Set_cPosIndex(itemList != null ? 0 : 1);
    templateList1 = itemList;
    currTemplateList = itemList;
    if (templateList1 != null)
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