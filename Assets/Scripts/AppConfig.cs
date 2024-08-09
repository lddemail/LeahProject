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
    InitHotleList();
  }

  #region 酒店数据
  public static List<TabHotel> hotelList = new List<TabHotel>();
  public static void InitHotleList()
  {
    hotelList.Add(TabHotel.Create("Voco千岛湖阳光大酒店", "Local","", "浙江省", "杭州市"));
    hotelList.Add(TabHotel.Create("常熟理文铂尔曼酒店", "Accor", "Pullman", "江苏省", "常熟市"));
    hotelList.Add(TabHotel.Create("常州都喜天丽富都青枫苑酒店", "Local", "Pullman", "江苏省", "常州市"));
    hotelList.Add(TabHotel.Create("常州富都大酒店", "Shangri-La", "Traders", "江苏省", "常州市"));
    hotelList.Add(TabHotel.Create("常州金坛金沙科技金融中心万豪酒店", "Marriott", "", "江苏省", "常州市"));
    hotelList.Add(TabHotel.Create("常州金坛金沙科技金融中心万豪酒店", "Marriott", "", "江苏省", "常州市"));
  }
  #endregion

}
