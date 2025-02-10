using Basics;
using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
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
  List<string> productList;

  PopupMenu mainPop;//菜单项
  PopupMenu itemPop;
  public override void Init()
  {
    mainPop = new PopupMenu();
    mainPop.AddItem(AppConfig.Inport_Excel, _clickMenu);
    mainPop.AddItem(AppConfig.Export_Excel, _clickMenu);
    mainPop.AddItem(AppConfig.Export_Data, _clickMenu);
    mainPop.AddItem(AppConfig.Inport_Data, _clickMenu);
    mainPop.AddItem(AppConfig.Open_Data, _clickMenu);
    mainPop.AddItem(AppConfig.Show_Log, _clickMenu);
    mainPop.AddItem(AppConfig.Manage_Template, _clickMenu);

    itemPop = new PopupMenu();

    UIPanel.m_BtnMainPop.onClick.Add(() => {
      mainPop.Show(UIPanel.m_BtnMainPop);
    });
    UIPanel.m_BtnAdd.onClick.Add(BtnAddHandler);

    UIPanel.m_mainList.itemRenderer = MainListRender;
    UIPanel.m_mainList.SetVirtual();

    UIPanel.m_title_product.onChanged.Add(Title_ProductChange);
    UIPanel.m_title_hotelName.onChanged.Add(Title_HotelNameChange);

    EventMgr.Add(AppConfig.UpdateQuery, QueryByTerm);
    EventMgr.Add(AppConfig.UpdateMainItem, UpdateMainItem);

    UIPanel.m_VersionTxt.text = $"版本号:{AppConfig.version}";

  }
  private void _clickMenu(EventContext context)
  {
    GObject itemObject = (GObject)context.data;
    switch(itemObject.text)
    {
      case AppConfig.Inport_Excel:
        BtnAddHotelHandler();
        break;
      case AppConfig.Export_Excel:
        BtnSaveExcelHandler();
        break;
      case AppConfig.Export_Data:
        BtnExportDataHandler();
        break;
      case AppConfig.Inport_Data:
        BtnInportDataHandler();
        break;
      case AppConfig.Open_Data:
        AppUtil.OpenFolder(AppConfig.GetDataPath());
        break;
      case AppConfig.Show_Log:
        UIRoot.ins.uiLog.Show();
        break;
      case AppConfig.Manage_Template:
        UIRoot.ins.uiTemplate.Show();
        break;
      default://模版
        //string tempPath = AppConfig.GetTemplatePath(itemObject.text);
        //AppUtil.OpenFolder(tempPath);
        break;
    }
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

  private void Title_ProductChange(EventContext context)
  {
    AppConfig.adventSelectDic.TryGetValue(UIPanel.m_title_product.selectedIndex, out adventTerm);
    QueryByTerm();
  }
  private void Title_HotelNameChange(EventContext context)
  {
    hotelNameTerm = UIPanel.m_title_hotelName.text;
    if (string.IsNullOrEmpty(hotelNameTerm)) hotelNameTerm = AppConfig.ALL;
    QueryByTerm();
  }

  /// <summary>
  /// 添加数据
  /// </summary>
  /// <param name="context"></param>
  private void BtnAddHandler(EventContext context)
  {
    UIRoot.ins.uiDetail.Show();
  }

  int adventTerm = 0;
  string hotelNameTerm = AppConfig.ALL;


  /// <summary>
  /// 设置查询条件查询
  /// </summary>
  /// <param name="_adventTerm"></param>
  /// <param name="_hotelNameTerm"></param>
  public void SetQuery(int _adventTerm, string _hotelNameTerm)
  {
    adventTerm = _adventTerm;
    foreach(int key in AppConfig.adventSelectDic.Keys)
    {
      if (AppConfig.adventSelectDic[key] == adventTerm)
      {
        UIPanel.m_title_product.selectedIndex = key;
        break;
      }
    }

    hotelNameTerm = _hotelNameTerm;
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
      bool isHotelNameTerm = tab.isDimHotelNameTerm(hotelNameTerm);
      //bool isGroupTerm = tab.isGroupTerm(groupTerm);
      //bool isProductTerm = tab.isProductTerm(groupTerm);
      if (isAdventTerm && isHotelNameTerm)
      {
        _currTabContracts.Add(tab);
      }
    }
    //选择了临期筛选的话按照临期排序
    if (adventTerm > 0)
    {
      foreach (var tab in _currTabContracts)
      {
        tab.ComputeProductExpirationTime();
      }

      //排序从小到大
      _currTabContracts.Sort((x, y) => x.GetProductExpirationTime().CompareTo(y.GetProductExpirationTime()));
    }

    int count = _currTabContracts == null ? 0 : _currTabContracts.Count;
    UIRoot.ins.uiTips.Show($"检索到{count}条数据");
    Debug.Log($"检索条件 :{adventTerm},{hotelNameTerm} 检索到{count}条数据");
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
    //UI_MainListItem obj = context.sender as UI_MainListItem;
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
    itemPop.ClearItems();
    itemPop.AddItem(AppConfig.Details, () => {
      UIRoot.ins.uiDetail.Show(tc);//详情
    });
    itemPop.AddItem(AppConfig.Delete, () => {
      UIRoot.ins.uiConfirm.Show($"确定要删除:ID:{tc.t_id}:{tc.t_hotelName}吗?", () =>
      {
        bool isOk = AppData.DelTabContract(tc.t_id);
        if (isOk)
        {
          _currTabContracts.Remove(tc);
          RefreshUI();
        }
      });
    });
    itemPop.Show();
  }

  public override void Show(object obj=null)
  {
    base.Show();
    if (AppData.allTabContractFiels.Count == 0)
    {
      UIRoot.ins.uiTips.Show($"没有合同数据或者缺少数据库");
      return;
    }

    UIPanel.m_title_product.items = AppConfig.adventBtns.ToArray();
    UIPanel.m_title_product.selectedIndex = 0;

    QueryByTerm();

    //弹窗提示临期
    int day = 90;
    int count = AppData.GetTabAdventCount(day);
    if(count > 0)
    {
      string message = AppUtil.GetColorStrByType(EmProductType.None,$"{count}份合同将在{day}天");
      UIRoot.ins.uiConfirm.Show($"有{message}内到期或已过期.点击确定查看这些合同", () => {
        SetQuery(day,AppConfig.ALL);
      });
    }
  }

  public override void RefreshUI()
  {
    int count = _currTabContracts == null ? 0 : _currTabContracts.Count;
    UIPanel.m_mainList.numItems = count;
  }

  public override void Hide()
  {
    base.Hide();
  }

  private void BtnInportDataHandler()
  {
    var paths = SFB.StandaloneFileBrowser.OpenFilePanel("导入Data", "", "zip", false);
    if (paths.Length > 0)
    {
      UILog.Log($"导入Data:{paths[0]}");
      AppUtil.UnCompressFolder(paths[0], AppConfig.GetDataPath());

      UIRoot.ins.uiTips.Show("导入Data成功即将关闭APP", 99);
      Timers.inst.Add(3f, 1, (object param) => {
        AppUtil.Quit();
      });
    }
  }
  private void BtnExportDataHandler()
  {
    string exportName = "Export_Data";
    string dataPath = AppConfig.GetDataPath();
    string lp_data = dataPath.Replace("Data", exportName);
    try
    {
      AppUtil.CopyDirectory(dataPath, lp_data);
      string zipFilePath = SFB.StandaloneFileBrowser.SaveFilePanel("导出LP数据", "", exportName, "zip");
      if (!string.IsNullOrEmpty(zipFilePath))
      {
        // 压缩文件夹
        AppUtil.CompressFolder(lp_data, zipFilePath);
      }
    }
    catch (Exception ex)
    {
      UILog.Log($"Export_Data:{ex.ToString()}");
      Debug.LogError(ex);
    }
    finally
    {
      if (Directory.Exists(lp_data))
      {
        Directory.Delete(lp_data, true);
      }
    }
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
      UILog.Log(log);
      UIRoot.ins.uiTips.Show(log, 99);
      return;
    }


    try
    {
      if (list.Count > 0)
      {

        log = $"读取到数据:{list.Count}条 开始导入数据库导入完成后会自动关闭";
        UIRoot.ins.uiTips.Show(log, 99);
        foreach (TabContract contract in list)
        {
          AppUtil.Insert2DB<TabContract>(contract, AppConfig.tabKey, out long lastId);
        }

        log = $"导入数据完成即将关闭";
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
      UILog.Log(log);
      UIRoot.ins.uiTips.Show(log, 99);
    }

  }
}