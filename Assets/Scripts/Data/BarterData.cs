using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 酒店消费明细
/// </summary>
public class BarterData
{
  /// <summary>
  /// 消费金额
  /// </summary>
  public float barter;

  /// <summary>
  /// 时间
  /// </summary>
  public int time;

  /// <summary>
  /// 消费备注
  /// </summary>
  public string remark;

  public static BarterData Crete(float barter, int bTime,string remark)
  {
    BarterData d = new BarterData();
    d.barter = barter;
    d.time = bTime;
    d.remark = remark;
    return d;
  }
  public string ToStr()
  {
    string res = $"barter:{barter},time:{time},remark:{remark}";
    return res;
  }
  public static BarterData Prase(string str)
  {
    if (!string.IsNullOrEmpty(str))
    {
      BarterData d = new BarterData();
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

  public static List<BarterData> DBStrToData(string str)
  {
    List<BarterData> res = new List<BarterData>();
    if (!string.IsNullOrEmpty(str))
    {
      string[] ary = str.Split("+");
      foreach (string str2 in ary)
      {
        BarterData d = BarterData.Prase(str2);
        if (d != null)
        {
          res.Add(d);
        }
      }
    }

    return res;
  }


  public static string ToDBStr(List<BarterData> pdList)
  {
    string res = "";
    if (pdList.Count > 0)
    {
      for (int i = 0; i < pdList.Count; i++)
      {
        res += pdList[i].ToStr();
        if (i < pdList.Count && i > 0)
        {
          res += "+";
        }
      }
    }
    return res;
  }
}