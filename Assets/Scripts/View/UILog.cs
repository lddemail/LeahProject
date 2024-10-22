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

  }

  private void ItemRendererLogList(int index, GObject item)
  {
    string val = _logList[index];
    var _item = item as UI_UILogListItemExt;
    _item.SetData(val);
  }

  public static void AddLog(string val)
  {
    Debug.Log(val);
    UIRoot.ins.uiLog.Add(val);
  }
  public void Add(string val)
  {
    string log = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} [{val}]";
    _logList.Add(log);
    UIPanel.m_LogList.numItems = _logList.Count;
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

    UIPanel.visible = true;
    InitUI();
  }

  private void InitUI()
  {

  }


  public override void RefreshUI()
  {
  }

  public override void Hide()
  {
    UIPanel.visible = false;

  }

}