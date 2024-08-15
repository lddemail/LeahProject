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

  public static List<string> mainTitles = new List<string>() {
    "id",
    "hotelName",
    "group",
    "brand",
    "province",
    "city",
    "originalFollowup",
    "newSales",
    "interiorNo"
  };

  //省，市区

  public static string[] provinceAry = new string[] { "上海市", "浙江省", "江苏省" };
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

}
