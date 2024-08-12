using FairyGUI;
using Main;
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
    UIPanel.m_BtnAddHotel.onClick.Add(BtnAddHotelHandler);
  }

  public override void Show()
  {

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
        list.Add(contract);
      }

      if(list.Count > 0)
      {
        Debug.Log($"导入数据:{list.Count}条");
        foreach (TabContract contract in list)
        {
          AppUtil.Insert2DB<TabContract>(contract);
        }
      }
    }
  }
}