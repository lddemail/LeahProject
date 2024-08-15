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

  private static Dictionary<string, int> hotelNamesDic = new Dictionary<string, int>();
  public static void Init()
  {
    CheckTab();

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
      }

      //OrderBy升序
      //ThenBy降序
      //allTabContract.OrderBy(x => x.t_index).ThenBy(x => x.t_index).ToList();
      allTabContract.OrderBy(x => x.t_index).ToList();
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
  /// 添加一条清单
  /// </summary>
  public static void AddTabContract(TabContract data)
  {
    allTabContract.Add(data);
    AppUtil.Insert2DB<TabContract>(data);
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