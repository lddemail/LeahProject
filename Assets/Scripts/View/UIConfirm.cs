using Basics;
using FairyGUI;
using System;
using UnityEngine;
using static UnityEngine.Application;

public class UIConfirm : UIBase
{
  public UIConfirm(Transform tf)
  {
    ui = UIRoot.ins.uiMain.UIPanel.m_UIConfirm;
  }

  public UI_UIConfirm UIPanel
  {
    get { return ui as UI_UIConfirm; }
  }

  private Action OkCallBack;
  private Action CloseCallBack;

  public override void Init()
  {
    UIPanel.m_BtnOk.onClick.Add(BtnOkHandler);
    UIPanel.m_BtnClose.onClick.Add(BtnCloseHandler);
  }

  private void BtnCloseHandler(EventContext context)
  {
    CloseCallBack?.Invoke();
    Hide();
  }

  private void BtnOkHandler(EventContext context)
  {

    OkCallBack?.Invoke();
    Hide();
  }

  public override void Show(object obj = null)
  {
    UIPanel.visible = true;
  }

  public void Show(object obj, Action OkCallBack, Action CloseCallBack=null)
  {
    Show(obj);
    this.OkCallBack = OkCallBack;
    this.CloseCallBack = CloseCallBack;
  }

  public override void Hide()
  {
    OkCallBack = null;
    CloseCallBack = null;
    UIPanel.visible = false;
  }

}