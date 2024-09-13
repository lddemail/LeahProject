using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AppConfig
{
  public static string productName = "LP";
  public static string version = "1.1";

  public static string tabKey = "t_id";

  private static string dataPath;
  //db路径
  public static string GetDBPath()
  {
    string[] ary = version.Split(".");
    string name = $"DB_{ary[0]}.db";
    string path = Path.Combine(GetDataPath(), name);
    Debug.Log($"GetDBPath:{path}");
    return path;
  }
  public static string GetLogPath()
  {
    string name = $"{productName}.log";
    string path = Path.Combine(GetDataPath(), name);
    Debug.Log($"GetLogPath:{path}");
    return path;
  }
  /// <summary>
  /// 获取模版路径
  /// </summary>
  /// <returns></returns>
  public static string GetTemplatePath(string name)
  {
    string path = Path.Combine(GetDataPath(), name);
    return path;
  }
  public static string GetDataPath()
  {
    if(string.IsNullOrEmpty(dataPath))
    {
      string rootPath = Directory.GetParent(Application.dataPath).FullName;
      dataPath = Path.Combine(rootPath, "Data");
    }
    if (!Directory.Exists(dataPath))
    {
      Directory.CreateDirectory(dataPath);
    }
    return dataPath;
  }
  //工程名字
  public static string GetProductName()
  {
    return $"{productName}_{version}";
  }

  public static void Init()
  {
    InitProvinceDic();
  }

  /// <summary>
  /// 临期下拉配置
  /// </summary>
  public static Dictionary<int, int> adventSelectDic = new Dictionary<int, int>() {
      { 0,0},//全部
      { 1,-1},//过期
      { 2,30},
      { 3,60},
      { 4,90},
  };

  //选中提示背景色 #CCCCCC
  public static Color selectBgColor = new Color(0.8f, 0.8f, 0.8f, 1f);

  #region 事件
  public const string UpdateQuery = "UpdateQuery";//主界面从新查询
  public const string UpdateMainItem = "UpdateMainItem";//更细主界面一条数据
  #endregion

  public const string t_index = "t_index";

  public const string ALL = "全部";
  public const string t_id = "t_id";
  /// <summary> 用户购买的产品列表(产品1+产品2) </summary>
  public const string t_products = "t_products";
  /// <summary> 酒店对应的集团 </summary>
  public const string t_group = "t_group";
  /// <summary> 酒店名字 </summary>
  public const string t_hotelName = "t_hotelName";
  /// <summary> 酒店品牌 </summary>
  public const string t_brand = "t_brand";
  /// <summary> 原FOLLOWUP </summary>
  public const string t_originalFollowup = "t_originalFollowup";
  /// <summary> 新SALES </summary>
  public const string t_newSales = "t_newSales";
  /// <summary> 合同约定的支付方式 </summary>
  public const string t_payment = "t_payment";
  /// <summary> 签约的公司名 </summary>
  public const string t_attribution = "t_attribution";
  /// <summary> 甲方合同签约名称 </summary>
  public const string t_a_contract = "t_a_contract";
  /// <summary> 省 </summary>
  public const string t_province = "t_province";
  /// <summary> 市 </summary>
  public const string t_city = "t_city";
  /// <summary> 合同内部编号 </summary>
  public const string t_interiorNo = "t_interiorNo";
  /// <summary> 客户看的合同编号 </summary>
  public const string t_contractNo = "t_contractNo";
  /// <summary> 欠款金额=(合同总额t_productsPrice-到账总额t_totalAccount) </summary>
  public const string t_totalDebt = "t_totalDebt";
  /// <summary>  (合同总额)所有产品的总价格 </summary>
  public const string t_productsPrice = "t_productsPrice";
  /// <summary> 酒店消费总额 </summary>
  public const string t_totalBarter = "t_totalBarter"; 
  /// <summary> 消费明细 </summary>
  public const string t_barter = "t_barter"; 
  /// <summary> 到账总额 </summary>
  public const string t_totalAccount = "t_totalAccount";
  /// <summary> 到账明细</summary>
  public const string t_accountRematk = "t_accountRematk";

  public const string RefreshUI = "RefreshUI";
  public const string ProductTemplateName = "产品模版.txt";
  public const string HotelTemplateName = "酒店模版.txt";
  public const string HotelGroupTemplateName = "酒店集团模版.txt";
  public const string HotelBrandTemplateName = "酒店品牌模版.txt";
  public const string PaymentTemplateName = "支付方式模版.txt";
  public const string SignedTemplateName = "签约公司模版.txt";
  public const string SalesTemplateName = "Sales模版.txt";
  public const string A_SignedTemplateName = "甲方签约公司模版.txt";

  public static List<string> mainTitles = new List<string>() {
    t_id,
    t_hotelName,
    t_group,
    t_interiorNo,
    t_products,
    t_productsPrice,
    t_totalAccount,
    t_totalDebt
  };

  //省，市区
  private static List<string> _provinceList;
  public static List<string> provinceList
  {
    get { 
      if(_provinceList == null)
      {
        _provinceList = new List<string>();
        foreach (string key in cityDic.Keys)
        {
          _provinceList.Add(key);
        }
      }
      return _provinceList;
    }
  }
  public static Dictionary<string, List<string>> cityDic = new Dictionary<string, List<string>>();
  public static void InitProvinceDic()
  {
    cityDic.Add("上海市",new List<string>() { "上海市" });
    cityDic.Add("浙江省", new List<string>() {
    "杭州市",
    "宁波",
    "温州",
    "嘉兴",
    "湖州",
    "绍兴",
    "金华",
    "衢州",
    "舟山",
    "台州",
    "丽水"
    });
    cityDic.Add("江苏省", new List<string>() {
    "南京市",
    "苏州市",
    "无锡市",
    "镇江市",
    "常州市",
    "南通市",
    "徐州市",
    "盐城市",
    "连云港市",
    "泰州市",
     "扬州市",
    "淮安市",
    "宿迁市"
    });
  }

  //临期选项
  public static List<string> adventBtns = new List<string> { "全部显示","已过期" ,"30天到期","60天到期","90天到期" };

  //合同字段转中文
  public static Dictionary<string, string> fieldsNameDic = new Dictionary<string, string>
  {
    { t_id,"ID"},
    { t_hotelName,"酒店名字"},
    { t_group,"酒店集团"},
    { t_brand,"酒店品牌"},
    { t_province,"省"},
    { t_city,"市"},
    { t_originalFollowup,"原FOLLOWUP"},
    { t_newSales,"新SALES"},
    { t_interiorNo,"内部编号"},
    { t_contractNo,"外部编号"},
    { t_payment,"支付方式"},
    { t_attribution,"签约公司"},
    { t_a_contract,"甲方名称"},
    { t_products,"产品列表"},
    { t_productsPrice,"合同总额"},
    { t_barter,"消费明细"},
    { t_totalBarter,"消费总额"},
    { t_accountRematk,"到账明细"},
    { t_totalAccount,"到账总额"},
    { t_totalDebt,"欠款金额"}
  };
}
