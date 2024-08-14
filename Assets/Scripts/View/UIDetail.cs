using Basics;
using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class UIDetail : UIBase
{
  public UIDetail(Transform tf)
  {
    ui = UIRoot.ins.uiMain.UIPanel.m_UIDetail;
  }

  public UI_UIDetail UIPanel
  {
    get { return ui as UI_UIDetail; }
  }

  public override void Init()
  {
    UIPanel.m_BtnClose.onClick.Add(BtnCloseHandler);
    UIPanel.m_BtnSave.onClick.Add(BtnSaveHandler);
  }

  private void BtnSaveHandler(EventContext context)
  {
    Hide();
  }

  private void BtnCloseHandler(EventContext context)
  {
    Hide();
  }

  TabContract tc;
  public override void Show(object obj = null)
  {
    if(obj != null)
    {
      tc = obj as TabContract;
    }
    UIPanel.visible = true;

  }

  public override void Hide()
  {
    UIPanel.visible = false;
  }

}