using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


public struct ObjectVal
{
  public string name;
  public object val;
  public ObjectVal(string _name, object _val)
  {
    name = _name;
    val = _val;
  }
}

public class AppData
{
  /// <summary>
  /// 当前编辑的订单
  /// </summary>
  public static TabContract currTc;
  /// <summary>
  /// 所有定单数据
  /// </summary>
  public static List<TabContract> allTabContract = new List<TabContract>();

  /// <summary>
  ///  获取所有订单字段的名字列表
  /// </summary>
  public static Dictionary<string, List<string>> allTabContractFiels = new Dictionary<string, List<string>>();

  public static void Init()
  {
    CheckTab();
    allTabContract = AppUtil.ReadAll4DB<TabContract>();
    OrderAllTabContract();
  }

  /// <summary>
  /// 排序总订单
  /// </summary>
  private static void OrderAllTabContract()
  {
    if (allTabContract != null)
    {
      Dictionary<string, int> hotelNamesDic = new Dictionary<string, int>();
      foreach (TabContract tc in allTabContract)
      {
        if (!hotelNamesDic.ContainsKey(tc.t_hotelName))
        {
          hotelNamesDic.Add(tc.t_hotelName, hotelNamesDic.Count);
        }
        tc.t_index = hotelNamesDic[tc.t_hotelName];

        SetAllTabContractFiels("t_hotelName", tc.t_hotelName);
        SetAllTabContractFiels("t_group", tc.t_group);
        SetAllTabContractFiels("t_brand", tc.t_brand);
        SetAllTabContractFiels("t_originalFollowup", tc.t_originalFollowup);
        SetAllTabContractFiels("t_newSales", tc.t_newSales);
        SetAllTabContractFiels("t_payment", tc.t_payment);
        SetAllTabContractFiels("t_a_contract", tc.t_a_contract);
        SetAllTabContractFiels("t_attribution", tc.t_attribution);

      }

      //OrderBy升序
      //ThenBy降序
      //allTabContract.OrderBy(x => x.t_index).ThenBy(x => x.t_index).ToList();
      allTabContract = allTabContract.OrderBy(x => x.t_index).ToList();
    }
  }

  private static void SetAllTabContractFiels(string keyName, string val)
  {
    if(!allTabContractFiels.ContainsKey(keyName))
    {
      allTabContractFiels.Add(keyName,new List<string>());
    }
    if (!allTabContractFiels[keyName].Contains(val))
    {
      allTabContractFiels[keyName].Add(val);
    }
  }
  private static void CheckTab()
  {
    bool isExist = false;
    isExist = AppUtil.db.CheckTabExists("TabContract");
    if(!isExist)
    {
      AppUtil.db.CreateTable<TabContract>(AppConfig.tabKey);
      Debug.Log($"创建表:TabContract");
      
    }
  }

  /// <summary>
  /// 跟新一条清单
  /// </summary>
  /// <param name="data"></param>
  public static void UpdateTabContract(TabContract data)
  {
    AppUtil.Update2DB<TabContract>(data);
  }

  /// <summary>
  /// 添加一条清单
  /// </summary>
  public static void AddTabContract(TabContract data)
  {
    string log = "";
    bool isOk = AppUtil.Insert2DB<TabContract>(data, AppConfig.tabKey);
    if(isOk)
    {
      allTabContract.Add(data);
      OrderAllTabContract();
      UIRoot.ins.uiMain.QueryByTerm();
      log = $"{data.t_hotelName} 新增入库完成";
    }
    else
    {
      log = $"{data.t_hotelName} 入库失败";
    }
    Debug.Log(log);
    UIRoot.ins.uiTips.Show(log);
  }
  /// <summary>
  /// 删除一条清单
  /// </summary>
  public static void DelTabContract(int id)
  {
    TabContract tab = allTabContract.Find(x => x.t_id == id);
    if(tab != null)
    {
      string log = "";
      bool isOk = AppUtil.Delete2DB<TabContract>(tab);
      if(isOk)
      {
        allTabContract.Remove(tab);
        OrderAllTabContract();
        UIRoot.ins.uiMain.QueryByTerm();
        log = $"{tab.t_hotelName} 删除成功";
      }
      else
      {
        log = $"{tab.t_hotelName} 删除失败!";
      }
      Debug.Log(log);
      UIRoot.ins.uiTips.Show(log);
    }
  }

  /// <summary>
  /// 从数据库拿数据覆盖本地
  /// </summary>
  public static void DBCoverLocalById(int id)
  {
    TabContract tab = allTabContract.Find(x => x.t_id == id);
    if (tab != null)
    {
      TabContract tabDB =AppUtil.Read4DBById<TabContract>(id);
      if(tabDB != null && tabDB.t_id > 0)
      {
        tab.Cover(tabDB);
      }
    }
  }
}