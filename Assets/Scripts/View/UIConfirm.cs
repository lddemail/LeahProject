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
    string log = CloseCallBack == null ? "null" : CloseCallBack.ToString();
    UILog.Log($"取消:{log}");
    CloseCallBack?.Invoke();
    Hide();
  }

  private void BtnOkHandler(EventContext context)
  {
    string log = OkCallBack == null ? "null" : OkCallBack.ToString();
    UILog.Log($"确认:{log}");
    OkCallBack?.Invoke();
    Hide();
  }

  public override void Show(object obj = null)
  {
    UIPanel.m_Message.text = obj.ToString();
    UIPanel.visible = true;
    UILog.Log(UIPanel.m_Message.text);
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