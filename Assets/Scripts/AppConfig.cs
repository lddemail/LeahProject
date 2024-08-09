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
    InitHotleList();
  }

  #region �Ƶ�����
  public static List<TabHotel> hotelList = new List<TabHotel>();
  public static void InitHotleList()
  {
    hotelList.Add(TabHotel.Create("Vocoǧ���������Ƶ�", "Local","", "�㽭ʡ", "������"));
    hotelList.Add(TabHotel.Create("�������Ĳ������Ƶ�", "Accor", "Pullman", "����ʡ", "������"));
    hotelList.Add(TabHotel.Create("���ݶ�ϲ�����������Է�Ƶ�", "Local", "Pullman", "����ʡ", "������"));
    hotelList.Add(TabHotel.Create("���ݸ�����Ƶ�", "Shangri-La", "Traders", "����ʡ", "������"));
    hotelList.Add(TabHotel.Create("���ݽ�̳��ɳ�Ƽ�������������Ƶ�", "Marriott", "", "����ʡ", "������"));
    hotelList.Add(TabHotel.Create("���ݽ�̳��ɳ�Ƽ�������������Ƶ�", "Marriott", "", "����ʡ", "������"));
  }
  #endregion

}
