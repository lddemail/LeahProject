using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
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

/// <summary>
/// 产品状态
/// </summary>
public enum EmProductType
{
  None,
  /// <summary>
  /// 过期
  /// </summary>
  Expire,
  /// <summary>
  /// 警告
  /// </summary>
  Warning,
}

public class AppData
{
  /// <summary>
  /// 当前编辑的订单
  /// </summary>
  public static TabContract currTc;
  /// <summary>
  /// 所有定单数据
  /// </summary>
  public static List<TabContract> allTabContract = new List<TabContract>();

  /// <summary>
  ///  获取所有订单字段的名字列表
  /// </summary>
  public static Dictionary<string, List<string>> allTabContractFiels = new Dictionary<string, List<string>>();

  /// <summary>
  ///  所有酒店管理数据
  /// </summary>
  public static Dictionary<int, HotelRelevanceTempData> allHotelRelevanceTempData = new Dictionary<int, HotelRelevanceTempData>();
  /// <summary>
  /// 所有支付方式数据
  /// </summary>
  public static Dictionary<int, PaymentTempData> allPaymentTempData = new Dictionary<int, PaymentTempData>();
  /// <summary>
  /// 所有签约公司数据
  /// </summary>
  public static Dictionary<int, SignedTempData> allSignedTempData = new Dictionary<int, SignedTempData>();

  public static void Init()
  {
    CheckTab();
    allTabContract = AppUtil.ReadAll4DB<TabContract>();
    OrderAllTabContract();
    CheckOrCreateTemp();
    ReadAllTemplates();
  }

  /// <summary>
  /// 读取全部模版
  /// </summary>
  public static void ReadAllTemplates()
  {

    //酒店关联模版
    allHotelRelevanceTempData.Clear();
    List<string> _tempList = AppUtil.ReadFromTxt(AppConfig.HotelRelevanceTemplateName);
    if (_tempList.Count > 2)
    {
      _tempList.Sort((x, y) => AppUtil.CompareFirstTwoCharacters(x, y));
    }
    int id = 0;
    foreach (string str in _tempList)
    {
      HotelRelevanceTempData hrd = HotelRelevanceTempData.Create(id, str);
      if (hrd != null)
      {
        allHotelRelevanceTempData.Add(id, hrd);
        id++;
      }
    }

    //支付方式模版
    allPaymentTempData.Clear();
    _tempList = AppUtil.ReadFromTxt(AppConfig.PaymentTemplateName);
    id = 0;
    foreach (string str in _tempList)
    {
      PaymentTempData ptd = PaymentTempData.Create(id, str);
      if (ptd != null)
      {
        allPaymentTempData.Add(id, ptd);
        id++;
      }
    }
    //签约公司模版
    allSignedTempData.Clear();
    _tempList = AppUtil.ReadFromTxt(AppConfig.SignedTemplateName);
    id = 0;
    foreach (string str in _tempList)
    {
      SignedTempData ptd = SignedTempData.Create(id, str);
      if (ptd != null)
      {
        allSignedTempData.Add(id, ptd);
        id++;
      }
    }
  }

  public static HotelRelevanceTempData GetHotelRelevanceData(TabContract currTc)
  {
    object hotelName = currTc.GetFieldVal(AppConfig.t_hotelName);
    return GetHotelRelevanceData(hotelName.ToString());
  }
  /// <summary>
  /// 根据酒店名字获取关联数据
  /// </summary>
  /// <param name="name"></param>
  /// <returns></returns>
  public static HotelRelevanceTempData GetHotelRelevanceData(string hotelName) 
  {
    Debug.Log($"GetHotelRelevanceData:hotelName={hotelName}");
    HotelRelevanceTempData data = null;
    foreach (HotelRelevanceTempData d in allHotelRelevanceTempData.Values)
    {
      if (hotelName.Contains(d.t_hotelName)) return d;
    }
    return data;
  }

  /// <summary>
  /// 排序总订单
  /// </summary>
  private static void OrderAllTabContract()
  {
    if (allTabContract != null)
    {
      Dictionary<string, int> hotelNamesDic = new Dictionary<string, int>();
      foreach (TabContract tc in allTabContract)
      {
        if (!hotelNamesDic.ContainsKey(tc.t_hotelName))
        {
          hotelNamesDic.Add(tc.t_hotelName, hotelNamesDic.Count);
        }
        tc.t_index = hotelNamesDic[tc.t_hotelName];

        SetAllTabContractFiels(AppConfig.t_hotelName, tc.t_hotelName);
        SetAllTabContractFiels(AppConfig.t_group, tc.t_group);
        SetAllTabContractFiels(AppConfig.t_brand, tc.t_brand);
        SetAllTabContractFiels(AppConfig.t_originalFollowup, tc.t_originalFollowup);
        SetAllTabContractFiels(AppConfig.t_newSales, tc.t_newSales);
        SetAllTabContractFiels(AppConfig.t_payment, tc.t_payment);
        SetAllTabContractFiels(AppConfig.t_a_contract, tc.t_a_contract);
        SetAllTabContractFiels(AppConfig.t_attribution, tc.t_attribution);

        List<ProductData> pdList = tc.GetProductList();
        foreach (ProductData pd in pdList)
        {
          SetAllTabContractFiels(AppConfig.t_products, pd.name);
        }
      }

      //OrderBy升序
      //ThenBy降序
      //allTabContract.OrderBy(x => x.t_index).ThenBy(x => x.t_index).ToList();
      allTabContract = allTabContract.OrderBy(x => x.t_index).ToList();
    }
  }

  /// <summary>
  /// 获取模版字符串
  /// </summary>
  /// <returns></returns>
  public static List<string> GetTempList(string tempName)
  {
    List<string> res = new List<string>();
    switch (tempName)
    {
      case AppConfig.HotelRelevanceTemplateName:
        foreach(var rtd in allHotelRelevanceTempData.Values)
        {
          if (!res.Contains(rtd.t_hotelName)) res.Add(rtd.t_hotelName);
        }
        break;
      case AppConfig.PaymentTemplateName:
        foreach (var ptd in allPaymentTempData.Values)
        {
          if (!res.Contains(ptd.t_Name)) res.Add(ptd.t_Name);
        }
        break;
      case AppConfig.SignedTemplateName:
        foreach (var std in allSignedTempData.Values)
        {
          if (!res.Contains(std.t_Name)) res.Add(std.t_Name);
        }
        break;
      default:
        break;
    }
    return res;
  }
  public static void RemoveTemp(string tempName,int id)
  {
    if (id < 0) return;

    switch (tempName)
    {
      case AppConfig.HotelRelevanceTemplateName:
        if (allHotelRelevanceTempData.ContainsKey(id)) allHotelRelevanceTempData.Remove(id);
        break;
      case AppConfig.SignedTemplateName:
        if (allSignedTempData.ContainsKey(id)) allSignedTempData.Remove(id);
        break;
      case AppConfig.PaymentTemplateName:
        if (allPaymentTempData.ContainsKey(id)) allPaymentTempData.Remove(id);
        break;
      default:

        break;
    }
  }

  public static void SaveAllTemp()
  {
    //酒店关联模版
    //AppUtil.WriteToTxt(AppConfig.HotelRelevanceTemplateName, _tempDic.Values.ToList());
    //支付方式模版
    //AppUtil.WriteToTxt(AppConfig.PaymentTemplateName, allTemplates[AppConfig.PaymentTemplateName]);
    //签约公司模版
    //AppUtil.WriteToTxt(AppConfig.SignedTemplateName, allTemplates[AppConfig.SignedTemplateName]);
  }

  /// <summary>
  /// 检查并制作模版
  /// </summary>
  public static void CheckOrCreateTemp()
  {
    //酒店关联模版
    List<string> _tempList = AppUtil.ReadFromTxt(AppConfig.HotelRelevanceTemplateName);
    if(_tempList.Count < 1)
    {
      Dictionary<string, string> _tempDic = new Dictionary<string, string>();
      foreach (TabContract tc in allTabContract)
      {
        if (!_tempDic.ContainsKey(tc.t_hotelName))
        {
          HotelRelevanceTempData _hrd = new HotelRelevanceTempData();
          _hrd.t_hotelName = tc.t_hotelName;
          _hrd.t_group = tc.t_group;
          _hrd.t_brand = tc.t_brand;
          _hrd.t_a_contract = tc.t_a_contract;
          _hrd.t_province = tc.t_province;
          _hrd.t_city = tc.t_city;
          _tempDic.Add(_hrd.t_hotelName, _hrd.ToTemplateStr());
        }
      }
      AppUtil.WriteToTxt(AppConfig.HotelRelevanceTemplateName, _tempDic.Values.ToList());
    }

    //支付方式模版
    _tempList = AppUtil.ReadFromTxt(AppConfig.PaymentTemplateName);
    if(_tempList.Count < 1)
    {
      AppUtil.WriteToTxt(AppConfig.PaymentTemplateName, allTabContractFiels[AppConfig.t_payment]);
    }
    //签约公司模版
    _tempList = AppUtil.ReadFromTxt(AppConfig.SignedTemplateName);
    if (_tempList.Count < 1)
    {
      AppUtil.WriteToTxt(AppConfig.SignedTemplateName, allTabContractFiels[AppConfig.t_attribution]);
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
  /// 临期数据个数(包含过期)
  /// </summary>
  public static int GetTabAdventCount(int day)
  {
    int res = 0;
    foreach (var tab in allTabContract)
    {
      bool isAdventTerm = tab.isAdventTerm(day);
      if (isAdventTerm)
      {
        res++;
      }
    }
    return res;
  }

  /// <summary>
  /// 更新一条清单
  /// </summary>
  /// <param name="data"></param>
  public static bool UpdateTabContract(TabContract data)
  {
    bool res = false;
    string log = "";
    bool isOk = AppUtil.Update2DB<TabContract>(data);
    if(isOk)
    {
      log = $"{data.t_id} 更新成功";
      res = true;
      EventMgr.Dispatch(AppConfig.UpdateMainItem, data);
    }
    else
    {
      log = $"{data.t_id} 更新失败!";
    }
    Debug.Log(log);
    UIRoot.ins.uiTips.Show(log);
    return res;
  }

  /// <summary>
  /// 添加一条清单
  /// </summary>
  public static bool AddTabContract(TabContract data)
  {
    bool res = false;
    string log = "";
    bool isOk = AppUtil.Insert2DB<TabContract>(data, AppConfig.tabKey,out long lastId);
    if(isOk)
    {
      if(lastId >= 0) data.t_id = (int)lastId;
      allTabContract.Add(data);
      OrderAllTabContract();
      EventMgr.Dispatch(AppConfig.UpdateQuery);
      log = $"{data.t_id} 新增入库完成";
      res = true;
    }
    else
    {
      log = $"{data.t_id} 入库失败";
    }
    Debug.Log(log);
    UIRoot.ins.uiTips.Show(log);
    return res;
  }
  /// <summary>
  /// 删除一条清单
  /// </summary>
  public static bool DelTabContract(int id)
  {
    bool res = false;
    TabContract tab = allTabContract.Find(x => x.t_id == id);
    if(tab != null)
    {
      string log = "";
      if(tab.t_id <= 0)
      {
        log = $"数据id:{tab.t_id} 不正确 删除失败,如果是新加的数据需要从新打开软件才行";
      }
      else
      {
        bool isOk = AppUtil.Delete2DB<TabContract>(tab);
        if (isOk)
        {
          allTabContract.Remove(tab);
          OrderAllTabContract();
          EventMgr.Dispatch(AppConfig.UpdateQuery);
          log = $"{tab.t_id} 删除成功";
          res = true;
        }
        else
        {
          log = $"{tab.t_id} 删除失败!";
        }
      }

      Debug.Log(log);
      UILog.AddLog(log);
      UIRoot.ins.uiTips.Show(log);
    }
    return res;
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