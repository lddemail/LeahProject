using Basics;
using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using static ExcelSheet;

public class UIMain:UIBase
{

  public UI_MainPanel UIPanel
  {
    get { return ui as UI_MainPanel; }
  }

  public override void Init()
  {
    UIPanel.m_BtnInputExcel.onClick.Add(BtnAddHotelHandler);

    UIPanel.m_titleList.itemRenderer = TitleListRender;
    UIPanel.m_titleList.enabled = false;

    UIPanel.m_mainList.itemRenderer = MainListRender;
    UIPanel.m_mainList.SetVirtual();
  }

  private void TitleListRender(int index, GObject item)
  {
    string title =  AppConfig.mainTitles[index];
    var _item = item as UI_TitleLisItem;
    _item.m_title.text = title;
  }

  private void MainListRender(int index, GObject item)
  {
    TabContract tabC = AppData.allTabContract[index];
    var _item = item as UI_MainListItem;
    _item.onRightClick.Add(MainItemRightClick);
    _item.data = tabC;
    for (int i=0;i< AppConfig.mainTitles.Count; i++)
    {
      string title = AppConfig.mainTitles[i];
      object val = tabC.GetPropertyValue($"t_{title}");
      string valStr = val.ToString();
      switch (i)
      {
        case 0:
          _item.m_t1.text = valStr;
          break;
        case 1:
          _item.m_t2.text = valStr;
          break;
        case 2:
          _item.m_t3.text = valStr;
          break;
        case 3:
          _item.m_t4.text = valStr;
          break;
        case 4:
          _item.m_t5.text = valStr;
          break;
        case 5:
          _item.m_t6.text = valStr;
          break;
        case 6:
          _item.m_t7.text = valStr;
          break;
        case 7:
          _item.m_t8.text = valStr;
          break;
      }
    }
  }

  private void MainItemRightClick(EventContext context)
  {
    GObject obj = context.sender as GObject;
    Debug.Log(obj.data);
  }

  public override void Show()
  {
    //主界面展示项
    UIPanel.m_titleList.numItems = AppConfig.mainTitles.Count;

    UIPanel.m_mainList.numItems = AppData.allTabContract.Count;
  }

  public override void Hide()
  {

  }

  void BtnAddHotelHandler()
  {
    ExcelSheet es = ExcelHelper.ImportExcel();
    if(es != null)
    {
      List<TabContract> list = new List<TabContract>();
      Dictionary<int, List<ObjVal>> vals = es.GetObjVal();
      foreach(int index in vals.Keys)
      {
        TabContract contract = TabContract.Create(index, vals[index]);
        contract.Compute();
        list.Add(contract);
      }

      if(list.Count > 0)
      {
        Debug.Log($"导入数据:{list.Count}条");
        foreach (TabContract contract in list)
        {
          AppUtil.Insert2DB<TabContract>(contract, "t_id");

          AppUtil.Quit();
        }
      }
    }
  }
}