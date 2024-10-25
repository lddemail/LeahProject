using Basics;
using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

/// <summary>
/// Log
/// </summary>
public class UILog : UIBase
{

  private List<string> _logList = new List<string>();
  public UILog(Transform tf)
  {
    ui = UIRoot.ins.uiMain.UIPanel.m_UILog;
  }

  public UI_UILog UIPanel
  {
    get { return ui as UI_UILog; }
  }

  public override void Init()
  {
    UIPanel.draggable = true;
    UIPanel.m_BtnClose.onClick.Add(BtnCloseHandler);
    UIPanel.m_BtnSave.onClick.Add(BtnSaveHandler);

    UIPanel.m_LogList.itemRenderer = ItemRendererLogList;
    UIPanel.m_LogList.SetVirtual();

    Application.logMessageReceived += HandleLog;

  }
  void HandleLog(string logString, string stackTrace, LogType type)
  {
    string log = "";
    if (!string.IsNullOrEmpty(logString)) log += logString;

    switch(type)
    {
      case LogType.Error:
      case LogType.Exception:
        if (!string.IsNullOrEmpty(stackTrace))
        {
          log += stackTrace;
          UIRoot.ins.uiTips.Show(log);
        }
        break;
    }
    AddLog(log);
  }

  private void ItemRendererLogList(int index, GObject item)
  {
    string val = _logList[index];
    var _item = item as UI_UILogListItemExt;
    _item.SetData(val);
  }

  public static void Log(string val)
  {
    Debug.Log(val);
    if (UIRoot.ins != null)
    {
      //UIRoot.ins.uiLog.AddLog(val);
    }
  }
  public void AddLog(string val)
  {
    string log = $"{DateTime.Now.ToString("HH:mm:ss")} {val}";
    _logList.Add(log);
    RefreshUI();
  }

  private void BtnCloseHandler(EventContext context)
  {
    Hide();
  }
  private void BtnSaveHandler(EventContext context)
  {
    try
    {
      string exportName = "LP_Log";
      string filePath = SFB.StandaloneFileBrowser.SaveFilePanel("保存日志", "", exportName, "txt");
      if (!string.IsNullOrEmpty(filePath))
      {
        File.WriteAllLines(filePath, _logList.ToArray());
      }
    }
    catch (Exception ex)
    {
      Debug.LogError(ex);
    }
  }

  public override void Show(object obj = null)
  {
    base.Show();
    RefreshUI();
  }

  public override void RefreshUI()
  {
    if (UIPanel.visible)
    {
      UIPanel.m_LogList.numItems = _logList.Count;
    }
  }

  public override void Hide()
  {
    base.Hide();
  }

}