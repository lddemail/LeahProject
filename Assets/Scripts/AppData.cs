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
    allTabContractFiels.Add("t_hotelName",new List<string>());
    allTabContractFiels.Add("t_group", new List<string>());
    allTabContractFiels.Add("t_brand", new List<string>());
    allTabContractFiels.Add("t_originalFollowup", new List<string>());
    allTabContractFiels.Add("t_newSales", new List<string>());
    allTabContractFiels.Add("t_payment", new List<string>());
    allTabContractFiels.Add("t_a_contract", new List<string>());

    Dictionary<string, int> hotelNamesDic = new Dictionary<string, int>();
    allTabContract = AppUtil.ReadAll4DB<TabContract>();
    if(allTabContract != null)
    {
      foreach(TabContract tc in allTabContract)
      {
        if(!hotelNamesDic.ContainsKey(tc.t_hotelName))
        {
          hotelNamesDic.Add(tc.t_hotelName, hotelNamesDic.Count -1);
        }
        tc.t_index = hotelNamesDic[tc.t_hotelName];

        SetAllTabContractFiels("t_hotelName", tc.t_hotelName);
        SetAllTabContractFiels("t_group", tc.t_group);
        SetAllTabContractFiels("t_brand", tc.t_brand);
        SetAllTabContractFiels("t_originalFollowup", tc.t_originalFollowup);
        SetAllTabContractFiels("t_newSales", tc.t_newSales);
        SetAllTabContractFiels("t_payment", tc.t_payment);
        SetAllTabContractFiels("t_a_contract", tc.t_a_contract);

      }

      //OrderBy升序
      //ThenBy降序
      //allTabContract.OrderBy(x => x.t_index).ThenBy(x => x.t_index).ToList();
      allTabContract.OrderBy(x => x.t_index).ToList();
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
    allTabContract.Add(data);
    AppUtil.Insert2DB<TabContract>(data, AppConfig.tabKey);
  }
  /// <summary>
  /// 删除一条清单
  /// </summary>
  public static void DelTabContract(int id)
  {
    TabContract tab = allTabContract.Find(x => x.t_id == id);
    if(tab != null)
    {
      allTabContract.Remove(tab);
      AppUtil.Delete2DB<TabContract>(tab);
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