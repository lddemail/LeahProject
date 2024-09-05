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

/// <summary>
/// 产品状态
/// </summary>
public enum EmProductType
{
  None,
  /// <summary>
  /// 过期
  /// </summary>
  Expire,
  /// <summary>
  /// 警告
  /// </summary>
  Warning,
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

        SetAllTabContractFiels(AppConfig.t_hotelName, tc.t_hotelName);
        SetAllTabContractFiels(AppConfig.t_group, tc.t_group);
        SetAllTabContractFiels(AppConfig.t_brand, tc.t_brand);
        SetAllTabContractFiels(AppConfig.t_originalFollowup, tc.t_originalFollowup);
        SetAllTabContractFiels(AppConfig.t_newSales, tc.t_newSales);
        SetAllTabContractFiels(AppConfig.t_payment, tc.t_payment);
        SetAllTabContractFiels(AppConfig.t_a_contract, tc.t_a_contract);
        SetAllTabContractFiels(AppConfig.t_attribution, tc.t_attribution);

        List<ProductData> pdList = tc.GetProductList();
        foreach (ProductData pd in pdList)
        {
          SetAllTabContractFiels(AppConfig.t_products, pd.name);
        }
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
  /// 更新一条清单
  /// </summary>
  /// <param name="data"></param>
  public static bool UpdateTabContract(TabContract data)
  {
    bool res = false;
    string log = "";
    bool isOk = AppUtil.Update2DB<TabContract>(data);
    if(isOk)
    {
      log = $"{data.t_id} 更新成功";
      res = true;
      EvtMgr.Dispatch(Evt.UpdateMainItem, data);
    }
    else
    {
      log = $"{data.t_id} 更新失败!";
    }
    Debug.Log(log);
    UIRoot.ins.uiTips.Show(log);
    return res;
  }

  /// <summary>
  /// 添加一条清单
  /// </summary>
  public static bool AddTabContract(TabContract data)
  {
    bool res = false;
    string log = "";
    bool isOk = AppUtil.Insert2DB<TabContract>(data, AppConfig.tabKey,out long lastId);
    if(isOk)
    {
      if(lastId >= 0) data.t_id = (int)lastId;
      allTabContract.Add(data);
      OrderAllTabContract();
      EvtMgr.Dispatch(Evt.UpdateQuery);
      log = $"{data.t_id} 新增入库完成";
      res = true;
    }
    else
    {
      log = $"{data.t_id} 入库失败";
    }
    Debug.Log(log);
    UIRoot.ins.uiTips.Show(log);
    return res;
  }
  /// <summary>
  /// 删除一条清单
  /// </summary>
  public static bool DelTabContract(int id)
  {
    bool res = false;
    TabContract tab = allTabContract.Find(x => x.t_id == id);
    if(tab != null)
    {
      string log = "";
      if(tab.t_id <= 0)
      {
        log = $"数据id:{tab.t_id} 不正确 删除失败,如果是新加的数据需要从新打开软件才行";
      }
      else
      {
        bool isOk = AppUtil.Delete2DB<TabContract>(tab);
        if (isOk)
        {
          allTabContract.Remove(tab);
          OrderAllTabContract();
          EvtMgr.Dispatch(Evt.UpdateQuery);
          log = $"{tab.t_id} 删除成功";
          res = true;
        }
        else
        {
          log = $"{tab.t_id} 删除失败!";
        }
      }

      Debug.Log(log);
      UIRoot.ins.uiTips.Show(log);
    }
    return res;
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