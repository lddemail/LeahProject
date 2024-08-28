﻿using Basics;
using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Reflection;
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
    Timers.inst.Add(3, 1, (object param) => {
      Hide();
    });
  }

  public void Show(string text,float delayTime=1)
  {
    UIPanel.visible = true;
    UIPanel.m_title.text = text;
    Timers.inst.Add(delayTime, 1, (object param) => {
      Hide();
    });
  }

  public bool HasProperty(object obj,string propertyName)
  {
    FieldInfo field = obj.GetType().GetField(propertyName);
    return field != null;
  }

  public override void Hide()
  {
    UIPanel.visible = false;
  }

}