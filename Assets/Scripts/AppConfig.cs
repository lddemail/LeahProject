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
  /// <summary>
  /// ��ȡģ��·��
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
  //��������
  public static string GetProductName()
  {
    return $"{productName}_{version}";
  }

  public static void Init()
  {
    InitProvinceDic();
  }

  /// <summary>
  /// ������������
  /// </summary>
  public static Dictionary<int, int> adventSelectDic = new Dictionary<int, int>() {
      { 0,0},//ȫ��
      { 1,-1},//����
      { 2,30},
      { 3,60},
      { 4,90},
  };

  //ѡ����ʾ����ɫ #CCCCCC
  public static Color selectBgColor = new Color(0.8f, 0.8f, 0.8f, 1f);

  #region �¼�
  public const string UpdateQuery = "UpdateQuery";//��������²�ѯ
  public const string UpdateMainItem = "UpdateMainItem";//��ϸ������һ������
  #endregion

  public const string t_index = "t_index";

  public const string ALL = "ȫ��";
  public const string t_id = "t_id";
  /// <summary> �û�����Ĳ�Ʒ�б�(��Ʒ1+��Ʒ2) </summary>
  public const string t_products = "t_products";
  /// <summary> �Ƶ��Ӧ�ļ��� </summary>
  public const string t_group = "t_group";
  /// <summary> �Ƶ����� </summary>
  public const string t_hotelName = "t_hotelName";
  /// <summary> �Ƶ�Ʒ�� </summary>
  public const string t_brand = "t_brand";
  /// <summary> ԭFOLLOWUP </summary>
  public const string t_originalFollowup = "t_originalFollowup";
  /// <summary> ��SALES </summary>
  public const string t_newSales = "t_newSales";
  /// <summary> ��ͬԼ����֧����ʽ </summary>
  public const string t_payment = "t_payment";
  /// <summary> ǩԼ�Ĺ�˾�� </summary>
  public const string t_attribution = "t_attribution";
  /// <summary> �׷���ͬǩԼ���� </summary>
  public const string t_a_contract = "t_a_contract";
  /// <summary> ʡ </summary>
  public const string t_province = "t_province";
  /// <summary> �� </summary>
  public const string t_city = "t_city";
  /// <summary> ��ͬ�ڲ���� </summary>
  public const string t_interiorNo = "t_interiorNo";
  /// <summary> �ͻ����ĺ�ͬ��� </summary>
  public const string t_contractNo = "t_contractNo";
  /// <summary> Ƿ����=(��ͬ�ܶ�t_productsPrice-�����ܶ�t_totalAccount) </summary>
  public const string t_totalDebt = "t_totalDebt";
  /// <summary>  (��ͬ�ܶ�)���в�Ʒ���ܼ۸� </summary>
  public const string t_productsPrice = "t_productsPrice";
  /// <summary> �Ƶ������ܶ� </summary>
  public const string t_totalBarter = "t_totalBarter"; 
  /// <summary> ������ϸ </summary>
  public const string t_barter = "t_barter"; 
  /// <summary> �����ܶ� </summary>
  public const string t_totalAccount = "t_totalAccount";
  /// <summary> ������ϸ</summary>
  public const string t_accountRematk = "t_accountRematk";

  public const string RefreshUI = "RefreshUI";
  public const string ProductTemplateName = "��Ʒģ��.txt";
  public const string HotelTemplateName = "�Ƶ�ģ��.txt";
  public const string HotelGroupTemplateName = "�Ƶ꼯��ģ��.txt";
  public const string HotelBrandTemplateName = "�Ƶ�Ʒ��ģ��.txt";
  public const string PaymentTemplateName = "֧����ʽģ��.txt";
  public const string SignedTemplateName = "ǩԼ��˾ģ��.txt";
  public const string SalesTemplateName = "Salesģ��.txt";
  public const string A_SignedTemplateName = "�׷�ǩԼ��˾ģ��.txt";

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

  //ʡ������
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

  //����ѡ��
  public static List<string> adventBtns = new List<string> { "ȫ����ʾ","�ѹ���" ,"30�쵽��","60�쵽��","90�쵽��" };

  //��ͬ�ֶ�ת����
  public static Dictionary<string, string> fieldsNameDic = new Dictionary<string, string>
  {
    { t_id,"ID"},
    { t_hotelName,"�Ƶ�����"},
    { t_group,"�Ƶ꼯��"},
    { t_brand,"�Ƶ�Ʒ��"},
    { t_province,"ʡ"},
    { t_city,"��"},
    { t_originalFollowup,"ԭFOLLOWUP"},
    { t_newSales,"��SALES"},
    { t_interiorNo,"�ڲ����"},
    { t_contractNo,"�ⲿ���"},
    { t_payment,"֧����ʽ"},
    { t_attribution,"ǩԼ��˾"},
    { t_a_contract,"�׷�����"},
    { t_products,"��Ʒ�б�"},
    { t_productsPrice,"��ͬ�ܶ�"},
    { t_barter,"������ϸ"},
    { t_totalBarter,"�����ܶ�"},
    { t_accountRematk,"������ϸ"},
    { t_totalAccount,"�����ܶ�"},
    { t_totalDebt,"Ƿ����"}
  };
}
