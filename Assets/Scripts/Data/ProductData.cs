using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 产品
/// </summary>
public class ProductData: DataBase
{

  /// <summary>
  /// 产品名字
  /// </summary>
  public string name;

  /// <summary>
  /// 产品价格
  /// </summary>
  public float price;

  /// <summary>
  /// 开始时间
  /// </summary>
  public int fTime;

  /// <summary>
  /// 结束时间
  /// </summary>
  public int tTime;

  /// <summary>
  /// 产品备注
  /// </summary>
  public string remark;

  public static ProductData Crete(string name,float price,int formTime,int toTime, string remark)
  {
    ProductData d = new ProductData();
    d.name = name;
    d.price = price;
    d.fTime = formTime;
    d.tTime = toTime;
    d.remark = remark;
    return d;
  }

  /// <summary>
  /// 到期状态
  /// </summary>
  /// <param name="day"></param>
  /// <returns></returns>
  public string GetAdventStr()
  {
    int unixTime = AppUtil.GetNowUnixTime();
    int pT = tTime - unixTime;
    if(pT < 0)
    {
      return "过期";
    }
    else {
      int d = AppUtil.GetDayByUnixTime(pT);
      return $"{d}天内到期";
    }
  }


  public bool isNull()
  {
    if (string.IsNullOrEmpty(name) || price <= 0 || fTime <= 0 || tTime <= 0) return true;

    return false;
  }

  public bool IsSame(ProductData pd)
  {
    return name == pd.name && price == pd.price && fTime == pd.fTime && tTime == pd.tTime && remark == pd.remark;
  }

  public string ToStr()
  {
    string res = $"name:{name},price:{price},fTime:{fTime},tTime:{tTime},remark:{remark}";
    return res;
  }
  public static ProductData Prase(string str)
  {
    if(!string.IsNullOrEmpty(str))
    {
      ProductData d = new ProductData();
      string[] ary = str.Split(",");
      foreach(string s in ary)
      {
        string[] ary2 = s.Split(":");
        switch(ary2[0])
        {
          case "name":
            d.name = ary2[1];
            break;
          case "price":
            d.price = float.Parse(ary2[1]);
            break;
          case "fTime":
            d.fTime = int.Parse(ary2[1]);
            break;
          case "tTime":
            d.tTime = int.Parse(ary2[1]);
            break;
          case "remark":
            d.remark = ary2[1];
            break;
          default:
            //Debug.LogError($"{s} 没解析");
            break;
        }
      }
      return d;
    }
    return null;
  }

  public string ToJson()
  {
    string json = JsonUtility.ToJson(this);
    return json;
  }

  public static List<ProductData> DBStrToData(string str)
  {
    List<ProductData> res = new List<ProductData>();
    if(!string.IsNullOrEmpty(str))
    {
      string[] ary = str.Split("+");
      foreach(string str2 in ary)
      {
        ProductData d = ProductData.Prase(str2);
        if(d != null)
        {
          res.Add(d);
        }
      }
    }

    return res;
  }

  public static string ToDBStr(List<ProductData> pdList)
  {
    string res = "";
    if (pdList.Count > 0)
    {
      for (int i = 0; i < pdList.Count; i++)
      {
        res += pdList[i].ToStr();
        if (i < pdList.Count-1)
        {
          res += "+";
        }
      }
    }
    return res;
  }
}