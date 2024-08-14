using JetBrains.Annotations;
using Mono.Data.Sqlite;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static ExcelSheet;

/// <summary>
/// 合同清单
/// </summary>
public class TabContract:TabBase
{
  /// <summary>
  /// 排序用
  /// </summary>
  public int t_index;
  /// <summary>
  /// 酒店名字
  /// </summary>
  public string t_hotelName;
  /// <summary>
  /// 酒店对应的集团
  /// </summary>
  public string t_group;
  /// <summary>
  /// 酒店品牌
  /// </summary>
  public string t_brand;

  /// <summary>
  /// 省
  /// </summary>
  public string t_province;

  /// <summary>
  /// 市
  /// </summary>
  public string t_city;
  /// <summary>s
  /// 原FOLLOWUP
  /// </summary>
  public string t_originalFollowup;
  /// <summary>
  /// 新SALES
  /// </summary>
  public string t_newSales;
  /// <summary>
  /// 合同内部编号
  /// </summary>
  public string t_interiorNo;
  /// <summary>
  /// 客户看的合同编号
  /// </summary>
  public string t_contractNo;
  /// <summary>
  /// 合同约定的支付方式
  /// </summary>
  public string t_payment;
  /// <summary>
  /// 签约的公司名
  /// </summary>
  public string t_attribution;
  /// <summary>
  /// 甲方合同签约名称
  /// </summary>
  public string t_a_contract;

  /// <summary>
  /// 用户购买的产品列表(产品1+产品2)
  /// </summary>
  public string t_products;
  /// <summary>
  /// (合同总额)所有产品的总价格
  /// </summary>
  public float t_productsPrice;

  /// <summary>
  /// 酒店消费明细(时间,金额,备注)
  /// </summary>
  public string t_barter;
  /// <summary>
  /// 酒店消费总额
  /// </summary>
  public float t_totalBarter;

  /// <summary>
  /// 到账明细(时间,金额,备注)
  /// </summary>
  public string t_accountRematk;
  /// <summary>
  /// 到账总额
  /// </summary>
  public float t_totalAccount;

  /// <summary>
  /// 欠款金额=(合同总额t_productsPrice-到账总额t_totalAccount)
  /// </summary>
  public float t_totalDebt;

  
  public List<ObjectVal> GetObjectVals()
  {
    List<ObjectVal> vals = new List<ObjectVal>();
    FieldInfo[] fields = GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
    foreach (FieldInfo field in fields)
    {
      string name = field.Name;
      object value = field.GetValue(this);
      vals.Add(new ObjectVal(name, value));

      Debug.Log($"GetFiledDic: {name}:{value}");
    }
    return vals;
  }


  public bool isAdvent(int day =0)
  {
    if (day == 0) return true;

    int d = 3600 * 24 * day;
    int unixTime = AppUtil.GetNowUnixTime();
    List<ProductData> list = ProductData.DBStrToData(t_products);
    if(list != null)
    {
      foreach(ProductData pd in list)
      {
        if(pd.tTime > 0)
        {
          int pT = pd.tTime - unixTime;
          if (pT <= d)
          {
            return true;
          }
        }
      }
    }
    return false;
  }

  public void Compute()
  {
    // (合同总额)所有产品的总价格
    t_productsPrice = 0;
    List<ProductData> pddList = ProductData.DBStrToData(t_products);
    foreach (ProductData pdd in pddList)
    {
      t_productsPrice += pdd.price;
    }

    //酒店消费总额
    t_totalBarter = 0;
    List<BarterData> btdList = BarterData.DBStrToData(t_barter);
    foreach (BarterData btd in btdList)
    {
      t_totalBarter += btd.barter;
    }

    //到账总额
    t_totalAccount = 0;
    List<AccountData> atdList = AccountData.DBStrToData(t_accountRematk);
    foreach (AccountData atd in atdList)
    {
      t_totalAccount += atd.barter;
    }

    //欠款金额=(合同总额t_productsPrice-到账总额t_totalAccount)
    t_totalDebt = t_productsPrice - t_totalAccount;

    Debug.Log($"Compute:t_productsPrice={t_productsPrice} t_totalBarter={t_totalBarter} t_totalAccount={t_totalAccount} t_totalDebt={t_totalDebt}");
  }

  public object GetPropertyValue(string propertyName)
  {
    FieldInfo field = this.GetType().GetField(propertyName);
    return field.GetValue(this);
  }


  private static string IsEmpty(string str)
  {
    return string.IsNullOrEmpty(str) ? "0": str;
  }
  private static string Find(List<ObjVal> vals ,string name)
  {
    ObjVal obj = vals.Find(x => x.name == name);
    string res = IsEmpty(obj.val);
    return res;
  }
  /// <summary>
  /// excel导入原始数据
  /// </summary>
  /// <param name="index"></param>
  /// <param name="vals"></param>
  /// <returns></returns>
  public static TabContract Create(int index, List<ObjVal> vals)
  {
    TabContract d = new TabContract();

    foreach(ObjVal val in vals)
    {
      switch (val.name)
      {
        case "NAME_C":
          d.t_hotelName = IsEmpty(val.val);
          break;
        case "GROUP":
          d.t_group = IsEmpty(val.val);
          break;
        case "BRAND":
          d.t_brand = IsEmpty(val.val);
          break;
        case "State  City":
          string city = IsEmpty(val.val);
          string[] cityAry = city.Split("省");
          if(cityAry.Length == 2)
          {
            d.t_province = cityAry[0] + "省";
            d.t_city = cityAry[1];
          }
          else
          {
            d.t_province = cityAry[0];
            d.t_city = cityAry[0];
          }
          break;
        case "原FOLLOWUP":
          d.t_originalFollowup = IsEmpty(val.val);
          break;
        case "新SALES":
          d.t_newSales = IsEmpty(val.val);
          break;
        case "Internal No.":
          d.t_interiorNo = IsEmpty(val.val);
          break;
        case "Contract No.":
          d.t_contractNo = IsEmpty(val.val);
          break;
        case "PAYMENT":
          d.t_payment = IsEmpty(val.val);
          break;
        case "ATTRBUTION":
          d.t_attribution = IsEmpty(val.val);
          break;
        case "CONTRACT":
          d.t_a_contract = IsEmpty(val.val);
          break;
        case "PRODUCT":
          List<ProductData> pdList = new List<ProductData>();
          string products = IsEmpty(val.val);
          //产品
          string[] productsAry = products.Split("+");
          if(productsAry != null && productsAry.Length > 0)
          {
            //时间
            string fromTime = Find(vals, "FROM");
            fromTime = fromTime.Replace("?", "");
            fromTime = fromTime.Replace("？", "");
            int fTime = AppUtil.StringToTime(fromTime);
            string toTime = Find(vals, "TO");
            toTime = toTime.Replace("?", "");
            toTime = toTime.Replace("？", "");
            int tTime = AppUtil.StringToTime(toTime);
            //备注
            string productsRem = Find(vals, "PRODUCT REMARK");

            foreach (string str in productsAry)
            {
              ProductData pd = ProductData.Crete(str,0, fTime, tTime, productsRem);
              pdList.Add(pd);
            }
          }
          string t_products = ProductData.ToDBStr(pdList);
          d.t_products = t_products;
          //d.t_productsPrice = val.val;这个是计算出来的
          break;
        case "Barter":
          string t_barter = IsEmpty(val.val);
          BarterData bData = BarterData.Crete(float.Parse(t_barter),0,"0");
          d.t_barter = bData.ToStr();
          break;
        case "A/R Remark"://2022.9.20收252006.11；2022.11.23收226805.5；2023.3.1收25200.61
          string Remark = IsEmpty(val.val);
          string[] RemarkAry = Remark.Split("；");
          List<AccountData> aList = new List<AccountData>();
          foreach(string rem in RemarkAry)
          {
            if(!string.IsNullOrEmpty(rem))
            {
             string[] remAry = rem.Split("收");
              if(remAry != null && remAry.Length == 2)
              {
                string time = remAry[0].Trim().Replace(".","/");
                int _time = AppUtil.StringToTime(time);
                float _pri = 0;
                float.TryParse(remAry[1].Trim(),out _pri);
                AccountData aData = AccountData.Crete(_pri, _time,"");
                aList.Add(aData);
              }
            }
          }
          d.t_accountRematk = AccountData.ToDBStr(aList);
          break;
        default:
          if(val.name != "FROM" && val.name != "TO" && val.name != "PRODUCT REMARK")
          {
            Debug.Log($"没有处理:{val.name}:{val.val}");
          }
          break;
      }

    }

    return d;
  }
}