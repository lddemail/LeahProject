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
  PopupMenu templatePop;//模版项
  PopupMenu itemPop;
  public override void Init()
  {
    mainPop = new PopupMenu();
    mainPop.AddItem(AppConfig.Inport_Excel, _clickMenu);
    mainPop.AddItem(AppConfig.Expot_Excel, _clickMenu);
    mainPop.AddItem(AppConfig.Expot_Data, _clickMenu);

    templatePop = new PopupMenu();
    templatePop.AddItem(AppConfig.Update_Template, _clickMenu);
    templatePop.AddItem(AppConfig.ProductTemplateName, _clickMenu);
    templatePop.AddItem(AppConfig.HotelTemplateName, _clickMenu);
    templatePop.AddItem(AppConfig.HotelGroupTemplateName, _clickMenu);
    templatePop.AddItem(AppConfig.PaymentTemplateName, _clickMenu);
    templatePop.AddItem(AppConfig.SignedTemplateName, _clickMenu);
    templatePop.AddItem(AppConfig.SalesTemplateName, _clickMenu);
    templatePop.AddItem(AppConfig.A_SignedTemplateName, _clickMenu);
    templatePop.AddItem(AppConfig.HotelRelevanceTemplateName, _clickMenu);

    itemPop = new PopupMenu();

    UIPanel.m_BtnMainPop.onClick.Add(() => {
      mainPop.Show(UIPanel.m_BtnMainPop);
    });
    UIPanel.m_BtnAdd.onClick.Add(BtnAddHandler);

    UIPanel.m_BtnTemplate.onClick.Add(() => {
      templatePop.Show(UIPanel.m_BtnTemplate);
    });


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
      case AppConfig.Expot_Excel:
        BtnSaveExcelHandler();
        break;
      case AppConfig.Expot_Data:
        AppStart.ins.StartCoroutine(BtnExpotDataHandler());
        break;
      case AppConfig.Update_Template:
        AppData.ReadAllTemplates();
        UIRoot.ins.uiTips.Show($"模版刷新成功");
        break;
      default://模版
        string tempPath = AppConfig.GetTemplatePath(itemObject.text);
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(tempPath) { UseShellExecute = true });
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
    if(AppData.allTabContractFiels.Count == 0)
    {
      UIRoot.ins.uiTips.Show($"没有合同数据");
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
      string message = AppUtil.GetColorStrByType(EmProductType.Warning,$"{count}份合同将在{day}天");
      UIRoot.ins.uiConfirm.Show($"有{message}内到期或已过期.点击确定查看这些合同", () => {
        SetQuery(day,AppConfig.ALL);
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
  IEnumerator BtnExpotDataHandler()
  {
    // 选择要压缩的文件夹
    string dataPath = AppConfig.GetDataPath();
    string lp_data = dataPath.Replace("Data", "LP_Data");
    yield return null;
    try
    {
      AppUtil.CopyDirectory(dataPath, lp_data);
      // 打开保存文件对话框
      string zipFilePath = SFB.StandaloneFileBrowser.SaveFilePanel("导出LP数据", "", "LP_Data", "zip");
      if (!string.IsNullOrEmpty(zipFilePath))
      {
        // 压缩文件夹
        AppUtil.CompressFolder(lp_data, zipFilePath);
      }
    }
    catch (Exception ex)
    {
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