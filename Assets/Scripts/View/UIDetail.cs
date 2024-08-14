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

    UIPanel.m_DetailList.itemRenderer = ItemRendererHandler;
  }

  private void ItemRendererHandler(int index, GObject item)
  {
    if(objectVals != null)
    {
      ObjectVal val = objectVals[index];
    }
  }

  private void BtnSaveHandler(EventContext context)
  {
    Hide();
  }

  private void BtnCloseHandler(EventContext context)
  {
    Hide();
  }

  List<ObjectVal> objectVals;
  TabContract tc;
  public override void Show(object obj = null)
  {
    if(obj != null)
    {
      tc = obj as TabContract;
    }
    UIPanel.visible = true;

    if(tc != null)
    {
      objectVals = tc.GetObjectVals();
    }

    int count = objectVals == null ? 0: objectVals.Count;
    UIPanel.m_DetailList.numItems = count;
  }

  public override void Hide()
  {
    UIPanel.visible = false;
  }

}