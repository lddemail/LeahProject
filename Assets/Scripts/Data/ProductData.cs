using System.Collections;
using UnityEngine;


/// <summary>
/// 产品
/// </summary>
[System.Serializable]
public class ProductData
{

  /// <summary>
  /// 产品名字
  /// </summary>
  public string t_name;

  /// <summary>
  /// 产品价格
  /// </summary>
  public float t_price;

  /// <summary>
  /// 开始时间
  /// </summary>
  public int t_formTime;

  /// <summary>
  /// 开始时间
  /// </summary>
  public int t_toTime;

  /// <summary>
  /// 产品备注
  /// </summary>
  public string t_remark;

  public static ProductData Crete(string name,float price,int formTime,int toTime, string remark)
  {
    ProductData d = new ProductData();
    d.t_name = name;
    d.t_price = price;
    d.t_formTime = formTime;
    d.t_toTime = toTime;
    d.t_remark = remark;
    return d;
  }

  public string ToJson()
  {
    string json = JsonUtility.ToJson(this);
    return json;
  }
}