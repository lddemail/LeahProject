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
  //db·��
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
    "t_hotelName",
    "t_group",
    "t_brand",
    "t_province",
    "t_city",
    "t_originalFollowup",
    "t_newSales",
    "t_interiorNo",
    "t_contractNo",
    "t_payment",
    "t_attribution",
    "t_productsPrice",
    "t_totalBarter",
    "t_totalAccount",
    "t_totalDebt"
  };

  //ʡ������
  public static Dictionary<string, List<string>> provinceDic = new Dictionary<string, List<string>>();
  public static void InitProvinceDic()
  {
    provinceDic.Add("�Ϻ���",new List<string>() { "�Ϻ���" });
    provinceDic.Add("�㽭ʡ", new List<string>() {
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
    provinceDic.Add("����ʡ", new List<string>() {
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
