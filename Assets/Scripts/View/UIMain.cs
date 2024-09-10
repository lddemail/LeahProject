using Basics;
using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Reflection;
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
  List<string> productList;

  public override void Init()
  {
    UIPanel.m_BtnInputExcel.onClick.Add(BtnAddHotelHandler);
    UIPanel.m_BtnSavexcel.onClick.Add(BtnSaveExcelHandler);
    UIPanel.m_BtnAdd.onClick.Add(BtnAddHandler);


    UIPanel.m_BtnAdvent.items = AppConfig.adventBtns.ToArray();
    UIPanel.m_BtnAdvent.onChanged.Add(BtnAdventChange);
    UIPanel.m_BtnAdvent.selectedIndex = 0;

    UIPanel.m_mainList.itemRenderer = MainListRender;
    UIPanel.m_mainList.SetVirtual();

    UIPanel.m_title_hotelName.onChanged.Add(Title_hotelNameChange);
    UIPanel.m_title_group.onChanged.Add(Title_GroupChange);
    UIPanel.m_title_product.onChanged.Add(Title_ProductChange);

    EvtMgr.Add(Evt.UpdateQuery, QueryByTerm);
    EvtMgr.Add(Evt.UpdateMainItem, UpdateMainItem);
  }

  private void BtnAdventChange(EventContext context)
  {
    AppConfig.adventSelectDic.TryGetValue(UIPanel.m_BtnAdvent.selectedIndex,out adventTerm);
    QueryByTerm();
  }

  private void UpdateMainItem(EventContext context)
  {
    TabContract tc = context.data as TabContract;
    if(tc != null)
    {
      GObject[] gobs = UIPanel.m_mainList.GetChildren();
      foreach (GObject gob in gobs)
      {
        if(gob.data == context.data)
        {
          MethodInfo RefreshUI = gob.GetType().GetMethod(AppConfig.RefreshUI);
          RefreshUI?.Invoke(gob, null);
          break;
        }
      }
    }
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
  private void Title_ProductChange(EventContext context)
  {
    productTerm = productList[UIPanel.m_title_product.selectedIndex];
    QueryByTerm();
  }

  private void BtnAddHandler(EventContext context)
  {
    Debug.Log("添加数据");
    UIRoot.ins.uiDetail.Show();
  }

  int adventTerm = 0;
  string hotelNameTerm = AppConfig.ALL;
  string groupTerm = AppConfig.ALL;
  string productTerm = AppConfig.ALL;


  /// <summary>
  /// 设置查询条件查询
  /// </summary>
  /// <param name="_adventTerm"></param>
  /// <param name="_hotelNameTerm"></param>
  /// <param name="_groupTerm"></param>
  /// <param name="_productTerm"></param>
  public void SetQuery(int _adventTerm, string _hotelNameTerm, string _groupTerm, string _productTerm)
  {
    adventTerm = _adventTerm;
    foreach(int key in AppConfig.adventSelectDic.Keys)
    {
      if (AppConfig.adventSelectDic[key] == adventTerm)
      {
        UIPanel.m_BtnAdvent.selectedIndex = key;
        break;
      }
    }

    hotelNameTerm = _hotelNameTerm;
    UIPanel.m_title_hotelName.selectedIndex = hotelNameList.FindIndex(x => x == hotelNameTerm);

    groupTerm = _groupTerm;
    UIPanel.m_title_group.selectedIndex = groupList.FindIndex(x => x == groupTerm);

    productTerm = _productTerm;
    UIPanel.m_title_product.selectedIndex = productList.FindIndex(x => x == productTerm);
    QueryByTerm();
  }

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
      bool isProductTerm = tab.isProductTerm(groupTerm);
      if (isAdventTerm && isHotelNameTerm && isGroupTerm && isProductTerm)
      {
        _currTabContracts.Add(tab);
      }
    }
    int count = _currTabContracts == null ? 0 : _currTabContracts.Count;
    UIRoot.ins.uiTips.Show($"检索到{count}条数据");
    Debug.Log($"检索条件 :{adventTerm},{hotelNameTerm},{groupTerm},{productTerm} 检索到{count}条数据");
    RefreshUI();
  }

  private void MainListRender(int index, GObject item)
  {
    TabContract tabC = _currTabContracts[index];
    var _item = item as UIMainListItemExt;
    _item.SetData(tabC);
    _item.onRightClick.Set(MainItemRightClick);
    _item.onClick.Set(MainItemDoubleClick);
    _item.onRollOut.Set(MainItemRollOut);
  }

  private void MainItemRollOut(EventContext context)
  {
    UI_MainListItem obj = context.sender as UI_MainListItem;
    obj.m_BtnGroup.visible = false;
  }

  private float _lastClickTime = 0f;
  private void MainItemDoubleClick(EventContext context)
  {
    float currentTime = Time.time;
    if (currentTime - _lastClickTime < 0.25f)
    {
      UI_MainListItem obj = context.sender as UI_MainListItem;
      TabContract tc = obj.data as TabContract;
      Debug.Log($"双击详情:{tc.t_id}");
      UIRoot.ins.uiDetail.Show(tc);
    }
    _lastClickTime = currentTime;
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
    if(AppData.allTabContractFiels.Count == 0)
    {
      UIRoot.ins.uiTips.Show($"没有合同数据");
      return;
    }
    hotelNameList = AppData.allTabContractFiels[AppConfig.t_hotelName];
    hotelNameList.Insert(0, AppConfig.ALL);
    UIPanel.m_title_hotelName.items = hotelNameList.ToArray();
    UIPanel.m_title_hotelName.selectedIndex = 0;

    groupList = AppData.allTabContractFiels[AppConfig.t_group];
    groupList.Insert(0, AppConfig.ALL);
    UIPanel.m_title_group.items = groupList.ToArray();
    UIPanel.m_title_group.selectedIndex = 0;

    productList = AppData.allTabContractFiels[AppConfig.t_products];
    productList.Insert(0, AppConfig.ALL);
    UIPanel.m_title_product.items = productList.ToArray();
    UIPanel.m_title_product.selectedIndex = 0;

    QueryByTerm();

    //弹窗提示临期
    int day = 90;
    int count = AppData.GetTabAdventCount(day);
    if(count > 0)
    {
      UIRoot.ins.uiConfirm.Show($"有{count}份合同将在{day}天内到期或已过期.点击确定查看这些合同", () => {
        SetQuery(day,AppConfig.ALL,AppConfig.ALL,AppConfig.ALL);
      });
    }
  }

  private void RefreshUI()
  {
    int count = _currTabContracts == null ? 0 : _currTabContracts.Count;
    UIPanel.m_mainList.numItems = count;
  }

  public override void Hide()
  {

  }

  void BtnSaveExcelHandler()
  {
    List<TabContract> list = new List<TabContract>();
    UIMainListItemExt item;
    GObject[] gobs = UIPanel.m_mainList.GetChildren();
    foreach (GObject gob in gobs)
    {
      item = gob as UIMainListItemExt;
      if(item != null)
      {
        if (item.isSelect) list.Add(item.data as TabContract);
      }
    }
    try
    {
      if(ExcelHelper.SaveToExcel(list))
      {
        UIRoot.ins.uiTips.Show($"导出:{list.Count}条合同完成");
      }
    }
    catch(Exception ex)
    {
      UIRoot.ins.uiTips.Show($"导出失败:{ex.ToString()}");
    }
  }

  void BtnAddHotelHandler()
  {
    string log = "";
    TabContract tabc = null;
    List <TabContract> list = new List<TabContract>();
    try
    {
      ExcelSheet es = ExcelHelper.ImportExcel();
      if (es != null)
      {
        log = $"开始读取Excel";
        Debug.Log(log);
        Dictionary<int, List<ObjVal>> vals = es.GetObjVal();
        foreach (int index in vals.Keys)
        {
          tabc = TabContract.Create(index, vals[index]);
          tabc.Compute();
          list.Add(tabc);
        }
      }
    }
    catch(Exception ex)
    {
      log = $"读取Excel 失败:{ex.ToString()}";
      if (tabc != null)
      {
        log = $"读取Excel {tabc.t_hotelName} {tabc.t_interiorNo} 失败:{ex.ToString()}";
      }
      Debug.LogError(log);
      AppUtil.AddLog(log);
      UIRoot.ins.uiTips.Show(log, 99);
      return;
    }


    try
    {
      if (list.Count > 0)
      {

        log = $"读取到数据:{list.Count}条 开始导入数据库导入完成后会自动关闭";
        Debug.Log(log);
        UIRoot.ins.uiTips.Show(log, 99);
        foreach (TabContract contract in list)
        {
          AppUtil.Insert2DB<TabContract>(contract, AppConfig.tabKey, out long lastId);
        }

        log = $"导入数据完成即将关闭";
        Debug.Log(log);
        UIRoot.ins.uiTips.Show(log, 99);
        Timers.inst.Add(1f, 1, (object param) => {
          AppUtil.Quit();
        });
      }
    }
    catch(Exception ex)
    {
      log = $"导入数据库失败:{ex.ToString()}";
      Debug.LogError(log);
      AppUtil.AddLog(log);
      UIRoot.ins.uiTips.Show(log, 99);
    }

  }
}