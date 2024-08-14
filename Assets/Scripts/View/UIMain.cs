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
  public UIMain(Transform tf)
  {
    ui = tf.Find("MainPanel").GetComponent<UIPanel>().ui;
  }
       
  public UI_MainPanel UIPanel
  {
    get { return ui as UI_MainPanel; }
  }

  List<TabContract> _currTabContracts;

  public override void Init()
  {
    UIPanel.m_BtnInputExcel.onClick.Add(BtnAddHotelHandler);
    UIPanel.m_BtnAllOrAdvent.onClick.Add(BtnAllOrAdventHandler);
    UIPanel.m_BtnAdd.onClick.Add(BtnAddHandler);

    UIPanel.m_titleList.itemRenderer = TitleListRender;
    UIPanel.m_titleList.enabled = false;

    UIPanel.m_mainList.itemRenderer = MainListRender;
    UIPanel.m_mainList.SetVirtual();
  }

  private void BtnAddHandler(EventContext context)
  {
    Debug.Log("添加数据");
  }

  //显示全部/临期
  private void BtnAllOrAdventHandler(EventContext context)
  {
    bool isAdvent30 = UIPanel.m_BtnAllOrAdvent.title.Contains("临期(30)");
    bool isAdvent60 =  UIPanel.m_BtnAllOrAdvent.title.Contains("临期(60)");
    if (isAdvent30)
    {
      UIPanel.m_BtnAllOrAdvent.title = "临期(60)";
      _currTabContracts = AppData.allTabContract.FindAll(x => x.isAdvent(60) == true);
    }
    else if(isAdvent60)
    {
      UIPanel.m_BtnAllOrAdvent.title = "全部";
      _currTabContracts = AppData.allTabContract.FindAll(x => x.isAdvent() == true);
    }
    else
    {
      UIPanel.m_BtnAllOrAdvent.title = "临期(30)";
      _currTabContracts = AppData.allTabContract.FindAll(x => x.isAdvent(30) == true);
    }
    RefreshUI();
    int count = _currTabContracts == null ? 0 : _currTabContracts.Count;
    UIRoot.ins.uiTips.Show($"检索到{count}条数据");
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
    _item.onRollOut.Add(MainItemRollOut);
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

  private void MainItemRollOut(EventContext context)
  {
    UI_MainListItem obj = context.sender as UI_MainListItem;
    obj.m_BtnGroup.visible = false;
  }

  private void MainItemRightClick(EventContext context)
  {
    UI_MainListItem obj = context.sender as UI_MainListItem;
    TabContract tc = obj.data as TabContract;
    obj.m_BtnGroup.visible = true;
    obj.m_BtnGroup.x = obj.displayObject.GlobalToLocal(context.inputEvent.position).x;
    obj.m_BtnDetails.onClick.Add(() => {
      Debug.Log($"详情:{tc.t_id}");
    });
    obj.m_BtnDel.onClick.Add(() => {
      Debug.Log($"删除:{tc.t_id}");
      UIRoot.ins.uiConfirm.Show(tc, () => {
        _currTabContracts.Remove(tc);
        RefreshUI();
      });
    });
  }

  public override void Show(object obj=null)
  {
    //主界面展示项
    UIPanel.m_titleList.numItems = AppConfig.mainTitles.Count;

    //UIPanel.m_mainList.numItems = AppData.allTabContract.Count;
    UIPanel.m_BtnAllOrAdvent.FireClick(true, true);
  }

  private void RefreshUI()
  {
    int count = _currTabContracts == null ? 0 : _currTabContracts.Count;
    UIPanel.m_mainList.numItems = count;
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
          AppUtil.Insert2DB<TabContract>(contract, AppConfig.tabKey);
        }

        AppUtil.Quit();
      }
    }
  }
}