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
  //db·��
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
  //��������
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

  //ʡ������

  public static string[] provinceAry = new string[] { "�Ϻ���", "�㽭ʡ", "����ʡ" };
  public static Dictionary<string, List<string>> cityDic = new Dictionary<string, List<string>>();
  public static void InitProvinceDic()
  {
    cityDic.Add("�Ϻ���",new List<string>() { "�Ϻ���" });
    cityDic.Add("�㽭ʡ", new List<string>() {
    "������",
    "����",
    "����",
    "����",
    "����",
    "����",
    "��",
    "����",
    "��ɽ",
    "̨��",
    "��ˮ"
    });
    cityDic.Add("����ʡ", new List<string>() {
    "�Ͼ���",
    "������",
    "������",
    "����",
    "������",
    "��ͨ��",
    "������",
    "�γ���",
    "���Ƹ���",
    "̩����",
     "������",
    "������",
    "��Ǩ��"
    });
  }

}
