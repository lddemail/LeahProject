using Basics;
using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class UITips : UIBase
{
  public UITips(Transform tf)
  {
    ui = UIRoot.ins.uiMain.UIPanel.m_Tips;
  }

  public UI_Tips UIPanel
  {
    get { return ui as UI_Tips; }
  }

  public override void Init()
  {

  }

  public override void Show(object obj=null)
  {
    UIPanel.visible = true;
    UIPanel.m_title.text = obj.ToString();
    Timers.inst.Add(1f, 1, (object param) => {
      Hide();
    });
  }

  public override void Hide()
  {
    UIPanel.visible = false;
  }

}