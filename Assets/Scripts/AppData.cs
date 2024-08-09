using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppData
{
  /// <summary>
  /// 所有清单数据
  /// </summary>
  public static List<TabContract> allTabContract = new List<TabContract>();

  public static void Init()
  {
    CheckTab();

    allTabContract = AppUtil.ReadAll4DB<TabContract>();
  }

  private static void CheckTab()
  {
    bool isExist = false;
    isExist = AppUtil.db.CheckTabExists("TabContract");
    if(!isExist)
    {
      AppUtil.db.CreateTable<TabContract>();
      Debug.Log($"创建表:TabContract");
    }
  }

  /// <summary>
  /// 添加一条清单
  /// </summary>
  public static void AddTabContract(TabContract data)
  {
    TabContract tab = allTabContract.Find(x => x.id == data.id);
    if (tab == null)
    {
      allTabContract.Add(data);
      AppUtil.Insert2DB<TabContract>(tab);
    }
  }
  /// <summary>
  /// 删除一条清单
  /// </summary>
  public static void DelTabContract(int id)
  {
    TabContract tab = allTabContract.Find(x => x.id == id);
    if(tab != null)
    {
      allTabContract.Remove(tab);
      AppUtil.Delete2DB<TabContract>(tab);
    }
  }

}