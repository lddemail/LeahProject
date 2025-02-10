using System.Reflection;


/// <summary>
/// 支付方式模版
/// </summary>
public class PaymentTempData
{
  /// <summary>
  /// 唯一id
  /// </summary>
  public int t_id;
  /// <summary>
  /// 名字
  /// </summary>
  public string t_Name = "";


  /// <summary>
  /// 
  /// </summary>
  /// <param name="str"></param>
  /// <returns></returns>
  public static PaymentTempData Create(int id, string str)
  {
    PaymentTempData data = new PaymentTempData();
    data.t_id = id;
    data.t_Name = str;
    return data;
  }

  private FieldInfo[] GetFields()
  {
    return GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
  }
  /// <summary>
  /// 返回模版格式字符串
  /// </summary>
  /// <returns></returns>
  public string ToTemplateStr()
  {
    return t_Name;
  }
  /// <summary>
  /// 返回模版管理格式字符串
  /// </summary>
  /// <returns></returns>
  public string ToTemplateShowStr()
  {
    return t_Name;
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

}