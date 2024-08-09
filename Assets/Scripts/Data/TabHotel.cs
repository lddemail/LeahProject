using System.Collections;
using UnityEngine;

/// <summary>
/// 酒店表
/// </summary>
public class TabHotel : TabBase
{
  /// <summary>
  /// 酒店名字
  /// </summary>
  public string t_name;

  /// <summary>
  /// 酒店对应的集团
  /// </summary>
  public string t_group;

  /// <summary>
  /// 酒店品牌
  /// </summary>
  public string t_brand;

  /// <summary>
  /// 省
  /// </summary>
  public string t_province;

  /// <summary>
  /// 市
  /// </summary>
  public string t_city;


  public static TabHotel Create(string name,string group,string brand,string province,string city)
  {
    TabHotel data = new TabHotel();
    data.t_name = name;
    data.t_group = group;
    data.t_brand = brand;
    data.t_province = province;
    data.t_city = city;
    return data;
  }
}