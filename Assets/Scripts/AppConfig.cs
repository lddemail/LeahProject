using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AppConfig
{
  public static string productName = "LP";
  public static string version = "1.0";

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

  public static string ALL = "全部";
  public static string t_products = "t_products";
  public static string t_group = "t_group";
  public static string t_hotelName = "t_hotelName";
  public static string t_brand = "t_brand";
  public static string t_newSales = "t_newSales";
  public static string t_payment = "t_payment";
  public static string t_attribution = "t_attribution";
  public static string t_a_contract = "t_a_contract";
  public static string t_originalFollowup = "t_a_contract";
  public static string t_province = "t_province";
  public static string t_interiorNo = "t_interiorNo";
  public static string t_contractNo = "t_contractNo";
  public static string t_totalDebt = "t_totalDebt";
  public static string t_productsPrice = "t_productsPrice";
  public static string t_totalBarter = "t_totalBarter";
  public static string t_totalAccount = "t_totalAccount";

  public static string RefreshUI = "RefreshUI";

  public static List<string> mainTitles = new List<string>() {
    "t_id",
    "t_hotelName",
    "t_group",
    "t_interiorNo",
    "t_products",
    "t_productsPrice",
    "t_totalAccount",
    "t_totalDebt"
  };

  //省，市区
  public static List<string> provinceList = new List<string> { "上海市", "浙江省", "江苏省" };
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
    { "t_id","ID"},
    { "t_hotelName","酒店名字"},
    { "t_group","酒店集团"},
    { "t_brand","酒店品牌"},
    { "t_province","省"},
    { "t_city","市"},
    { "t_originalFollowup","原FOLLOWUP"},
    { "t_newSales","新SALES"},
    { "t_interiorNo","内部编号"},
    { "t_contractNo","外部编号"},
    { "t_payment","支付方式"},
    { "t_attribution","签约公司"},
    { "t_a_contract","甲方名称"},
    { "t_products","产品列表"},
    { "t_productsPrice","合同总额"},
    { "t_barter","消费明细"},
    { "t_totalBarter","消费总额"},
    { "t_accountRematk","到账明细"},
    { "t_totalAccount","到账总额"},
    { "t_totalDebt","欠款金额"}
  };
}
