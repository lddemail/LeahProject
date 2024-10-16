using System.Reflection;


/// <summary>
/// 酒店关联
/// </summary>
public class HotelRelevanceData
{
  /// <summary>
  /// 酒店名字
  /// </summary>
  public string t_hotelName = "";
  /// <summary>
  /// 酒店对应的集团
  /// </summary>
  public string t_group = "";
  /// <summary>
  /// 酒店品牌
  /// </summary>
  public string t_brand = "";
  /// <summary>
  /// 甲方合同签约名称
  /// </summary>
  public string t_a_contract = "";

  /// <summary>
  /// 省
  /// </summary>
  public string t_province = "";

  /// <summary>
  /// 市
  /// </summary>
  public string t_city = "";

  /// <summary>
  /// 
  /// </summary>
  /// <param name="str"></param>
  /// <returns></returns>
  public static HotelRelevanceData Create(string str)
  {
    string[] atrAry = str.Split(AppConfig.Split_2);
    if(atrAry.Length == 6)
    {
      HotelRelevanceData data = new HotelRelevanceData();
      data.t_hotelName = atrAry[0];
      data.t_group = atrAry[1];
      data.t_brand = atrAry[2];
      data.t_a_contract = atrAry[3];
      data.t_province = atrAry[4];
      data.t_city = atrAry[5];
      return data;
    }
    return null;
  }
  private FieldInfo[] GetFields()
  {
    return GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
  }
  /// <summary>
  /// 返回模版格式字符串
  /// </summary>
  /// <returns></returns>
  public string ToTemplateStr()
  {
    return $"{t_hotelName}#{t_group}#{t_brand}#{t_a_contract}#{t_province}#{t_city}";
  }
  /// <summary>
  /// 根据属性名获取值
  /// </summary>
  /// <param name="name"></param>
  /// <returns></returns>
  public object GetFieldVal(string name)
  {
    FieldInfo field = this.GetType().GetField(name);
    if (field != null)
    {
      return field.GetValue(this);
    }
    return null;
  }
  /// <summary>
  /// 获取城市
  /// </summary>
  /// <returns></returns>
  public string GetCityStr()
  {
    return $"{t_province}/{t_city}";
  }
}