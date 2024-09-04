using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 到账明细
/// </summary>
public class AccountData:DataBase
{ 
  /// <summary>
   /// 金额
   /// </summary>
  public float barter;

  /// <summary>
  /// 时间
  /// </summary>
  public int time;

  /// <summary>
  /// 备注
  /// </summary>
  public string remark;

  public static AccountData Crete(float barter, int time, string remark)
  {
 

    AccountData d = new AccountData();
    d.barter = barter;
    d.time = time;
    d.remark = remark;
    return d;
  }
  public bool isNull()
  {
    if (barter <= 0 || time <= 0) return true;

    return false;
  }
  public bool IsSame(AccountData ad)
  {
    return barter == ad.barter && time == ad.time && remark == ad.remark;
  }
  public string ToStr()
  {
    string res = $"barter:{barter},time:{time},remark:{remark}";
    return res;
  }
  public string ToExportStr()
  {
    string res = $"barter:{barter},time:{AppUtil.TimeToString(time)},remark:{remark}";
    return res;
  }
  public static AccountData Prase(string str)
  {
    if (!string.IsNullOrEmpty(str))
    {
      AccountData d = new AccountData();
      string[] ary = str.Split(",");
      foreach (string s in ary)
      {
        string[] ary2 = s.Split(":");
        switch (ary2[0])
        {
          case "barter":
           d.barter = float.Parse(ary2[1]);
            break;
          case "time":
            d.time = int.Parse(ary2[1]);
            break;
          case "remark":
            d.remark = ary2[1];
            break;
          default:
            Debug.LogError($"{s} 没解析");
            break;
        }
      }
      return d;
    }
    return null;
  }

  public static List<AccountData> DBStrToData(string str)
  {
    List<AccountData> res = new List<AccountData>();
    if (!string.IsNullOrEmpty(str))
    {
      string[] ary = str.Split("+");
      foreach (string str2 in ary)
      {
        AccountData d = AccountData.Prase(str2);
        if (d != null)
        {
          res.Add(d);
        }
      }
    }

    return res;
  }

  public static string ToDBStr(List<AccountData> pdList)
  {
    string res = "";
    if (pdList.Count > 0)
    {
      for (int i = 0; i < pdList.Count; i++)
      {
        res += pdList[i].ToStr();
        if (i < pdList.Count -1 )
        {
          res += "+";
        }
      }
    }
    return res;
  }
}