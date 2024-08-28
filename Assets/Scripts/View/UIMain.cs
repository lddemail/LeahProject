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

  List<TabContract> _currTabContracts = new List<TabContract>();
  List<string> hotelNameList;
  List<string> groupList;

  public override void Init()
  {
    UIPanel.m_BtnInputExcel.onClick.Add(BtnAddHotelHandler);
    UIPanel.m_BtnAllOrAdvent.onClick.Add(BtnAllOrAdventHandler);
    UIPanel.m_BtnAdd.onClick.Add(BtnAddHandler);

    UIPanel.m_mainList.itemRenderer = MainListRender;
    UIPanel.m_mainList.SetVirtual();

    UIPanel.m_title_hotelName.onChanged.Add(Title_hotelNameChange);
    UIPanel.m_title_group.onChanged.Add(Title_GroupChange);

    EvtMgr.Add(Evt.UpdateQuery, QueryByTerm);
  }

  private void Title_hotelNameChange(EventContext context)
  {
    hotelNameTerm = hotelNameList[UIPanel.m_title_hotelName.selectedIndex];
    QueryByTerm();
  }
  private void Title_GroupChange(EventContext context)
  {
    groupTerm = groupList[UIPanel.m_title_group.selectedIndex];
    QueryByTerm();
  }

  private void BtnAddHandler(EventContext context)
  {
    Debug.Log("添加数据");
    UIRoot.ins.uiDetail.Show();
  }

  int adventTerm = 30;
  string hotelNameTerm = "ALL";
  string groupTerm = "ALL";

  /// <summary>
  /// 根据条件检索
  /// </summary>
  private void QueryByTerm()
  {

    _currTabContracts.Clear();
    foreach(var tab in AppData.allTabContract)
    {
      bool isAdventTerm = tab.isAdventTerm(adventTerm);
      bool isHotelNameTerm = tab.isHotelNameTerm(hotelNameTerm);
      bool isGroupTerm = tab.isGroupTerm(groupTerm);
      //Debug.Log($"检索2:{tab.t_id}:{isAdventTerm} {isHotelNameTerm} {isGroupTerm}");
      if (isAdventTerm && isHotelNameTerm && isGroupTerm)
      {
        _currTabContracts.Add(tab);
      }
    }
    int count = _currTabContracts == null ? 0 : _currTabContracts.Count;
    UIRoot.ins.uiTips.Show($"检索到{count}条数据");
    Debug.Log($"检索条件 :{adventTerm} {hotelNameTerm} {groupTerm} 检索到{count}条数据");
    RefreshUI();
  }

  //显示全部/临期
  private void BtnAllOrAdventHandler(EventContext context)
  {
    bool isAdvent30 = UIPanel.m_BtnAllOrAdvent.title.Contains("临期(30)");
    bool isAdvent60 =  UIPanel.m_BtnAllOrAdvent.title.Contains("临期(60)");
    if (isAdvent30)
    {
      UIPanel.m_BtnAllOrAdvent.title = "临期(60)";
      adventTerm = 60;
    }
    else if(isAdvent60)
    {
      UIPanel.m_BtnAllOrAdvent.title = "全部";
      adventTerm = 0;
    }
    else
    {
      UIPanel.m_BtnAllOrAdvent.title = "临期(30)";
      adventTerm = 30;
    }
    QueryByTerm();
  }


  private void MainListRender(int index, GObject item)
  {
    TabContract tabC = _currTabContracts[index];
    var _item = item as UIMainListItemExt;
    _item.SetData(tabC);
    _item.onRightClick.Set(MainItemRightClick);
    _item.onRollOut.Set(MainItemRollOut);
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
    obj.m_BtnGroup.x = obj.displayObject.GlobalToLocal(context.inputEvent.position).x-160;
    obj.m_BtnDetails.onClick.Set(() => {
      Debug.Log($"详情:{tc.t_id}");
      UIRoot.ins.uiDetail.Show(tc);
    });
    obj.m_BtnDel.onClick.Set(() => {
      Debug.Log($"删除:{tc.t_id}");
      UIRoot.ins.uiConfirm.Show($"确定要删除:{tc.t_hotelName}吗?", () => {
        bool isOk = AppData.DelTabContract(tc.t_id);
        if(isOk)
        {
          _currTabContracts.Remove(tc);
          RefreshUI();
        }
      });
    });
  }

  public override void Show(object obj=null)
  {
    hotelNameList = AppData.allTabContractFiels["t_hotelName"];
    hotelNameList.Insert(0,"ALL");
    UIPanel.m_title_hotelName.items = hotelNameList.ToArray();
    UIPanel.m_title_hotelName.selectedIndex = 0;

    groupList = AppData.allTabContractFiels["t_group"];
    groupList.Insert(0, "ALL");
    UIPanel.m_title_group.items = groupList.ToArray();
    UIPanel.m_title_group.selectedIndex = 0;

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
    List<TabContract> list = new List<TabContract>();
    try
    {
      ExcelSheet es = ExcelHelper.ImportExcel();
      if (es != null)
      {
        Dictionary<int, List<ObjVal>> vals = es.GetObjVal();
        foreach (int index in vals.Keys)
        {
          TabContract contract = TabContract.Create(index, vals[index]);
          contract.Compute();
          list.Add(contract);
        }
      }
    }
    catch(Exception ex)
    {
      string error = $"读取Excel 失败:{ex.ToString()}";
      Debug.LogError(error);
      AppUtil.AddLog(error);
      UIRoot.ins.uiTips.Show(error, 99);
    }

    try
    {
      if (list.Count > 0)
      {
        Debug.Log($"导入数据:{list.Count}条");
        foreach (TabContract contract in list)
        {
          AppUtil.Insert2DB<TabContract>(contract, AppConfig.tabKey);
        }

        AppUtil.Quit();
      }
    }
    catch(Exception ex)
    {
      string error = $"导入失败:{ex.ToString()}";
      Debug.LogError(error);
      AppUtil.AddLog(error);
      UIRoot.ins.uiTips.Show(error,99);
    }

  }
}