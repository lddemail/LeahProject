using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static ExcelSheet;

/// <summary>
/// 合同清单
/// </summary>
public class TabContract
{
  /// <summary>
  /// 唯一id
  /// </summary>
  public int t_id;
  /// <summary>
  /// 排序用
  /// </summary>
  public int t_index;
  /// <summary>
  /// 酒店名字
  /// </summary>
  public string t_hotelName ="";
  /// <summary>
  /// 酒店对应的集团
  /// </summary>
  public string t_group = "";
  /// <summary>
  /// 酒店品牌
  /// </summary>
  public string t_brand = "";

  /// <summary>
  /// 省
  /// </summary>
  public string t_province = "";

  /// <summary>
  /// 市
  /// </summary>
  public string t_city = "";
  /// <summary>s
  /// 原FOLLOWUP
  /// </summary>
  public string t_originalFollowup = "";
  /// <summary>
  /// 新SALES
  /// </summary>
  public string t_newSales = "";
  /// <summary>
  /// 合同内部编号
  /// </summary>
  public string t_interiorNo = "";
  /// <summary>
  /// 客户看的合同编号
  /// </summary>
  public string t_contractNo = "";
  /// <summary>
  /// 合同约定的支付方式
  /// </summary>
  public string t_payment = "";
  /// <summary>
  /// 签约的公司名
  /// </summary>
  public string t_attribution = "";
  /// <summary>
  /// 甲方合同签约名称
  /// </summary>
  public string t_a_contract = "";

  /// <summary>
  /// 用户购买的产品列表(产品1+产品2)
  /// </summary>
  public string t_products = "";
  /// <summary>
  /// (合同总额)所有产品的总价格
  /// </summary>
  public float t_productsPrice;

  /// <summary>
  /// 酒店消费明细(时间,金额,备注)
  /// </summary>
  public string t_barter = "";
  /// <summary>
  /// 酒店消费总额
  /// </summary>
  public float t_totalBarter;

  /// <summary>
  /// 到账明细(时间,金额,备注)
  /// </summary>
  public string t_accountRematk = "";
  /// <summary>
  /// 到账总额
  /// </summary>
  public float t_totalAccount;

  /// <summary>
  /// 欠款金额=(合同总额t_productsPrice-到账总额t_totalAccount)
  /// </summary>
  public float t_totalDebt;


  /// <summary>
  /// 导出格式
  /// </summary>
  /// <returns></returns>
  public List<ObjectVal> GetExportObjs()
  {
    List<ObjectVal> vals = new List<ObjectVal>();

    string name = "";
    string value = "";
    FieldInfo[] fields = GetFields();
    foreach (FieldInfo field in fields)
    {
      name = field.Name;
      value = "";
      switch (name)
      {
        case AppConfig.t_products:
          List<ProductData> pdList = GetProductList();
          for(int i=0;i< pdList.Count;i++)
          {
            value += pdList[0].ToExportStr();
            if (i < pdList.Count - 1)
            {
              value += "+";
            }
          }
          break;
        case AppConfig.t_barter:
          List<BarterData> btList = GetBarterList();
          for (int i = 0; i < btList.Count; i++)
          {
            value += btList[0].ToExportStr();
            if (i < btList.Count - 1)
            {
              value += "+";
            }
          }
          break;
        case AppConfig.t_accountRematk:
          List<AccountData> adList = GetAccountList();
          for (int i = 0; i < adList.Count; i++)
          {
            value += adList[0].ToExportStr();
            if (i < adList.Count - 1)
            {
              value += "+";
            }
          }
          break;
        default:
          object valueDB = field.GetValue(this);
          value = valueDB == null ? "" : valueDB.ToString();
          break;
      }
      if(name != AppConfig.t_index)
      {
        vals.Add(new ObjectVal(name, value));
      }
    }

    return vals;
  }

  /// <summary>
  /// 覆盖数据
  /// </summary>
  public void Cover(TabContract tc)
  {
    FieldInfo[] fields = GetFields();
    foreach (FieldInfo field in fields)
    {
      string name = field.Name;
      object valueDB = field.GetValue(tc);
      if(valueDB != null)
      {
        field.SetValue(this, valueDB);
      }
    }
    Debug.Log($"从DB覆盖到本地:{tc.t_id}");
  }


  /// <summary>
  /// 覆盖数据
  /// </summary>
  public void CoverOVS(List<ObjectVal> ovs)
  {
    FieldInfo[] fields = GetFields();
    foreach (FieldInfo field in fields)
    {
      string name = field.Name;
      int index = ovs.FindIndex(x => x.name == name);
      if(index>= 0)
      {
        Debug.Log($"CoverValue:{name}->{ovs[index].val}");
        field.SetValue(this, ovs[index].val);
      }
    }

  }


  /// <summary>
  /// 根据属性名获取值
  /// </summary>
  /// <param name="name"></param>
  /// <returns></returns>
  public object GetFieldVal(string name)
  {
    if (name == AppConfig.t_newSales) return "Leah Zhang";

    FieldInfo field = this.GetType().GetField(name);
    if(field != null)
    {
      return field.GetValue(this);
    }
    return null;
  }
  /// <summary>
  /// 根据属性名设置值
  /// </summary>
  /// <param name="name"></param>
  /// <returns></returns>
  public void SetFieldVal(string name,object val)
  {
    FieldInfo[] fields = GetFields();
    foreach (FieldInfo field in fields)
    {
      string fieldName = field.Name;
      if (fieldName == name)
      {
        field.SetValue(this,val);
        Debug.Log($"SetFieldVal:{fieldName}={val}");
        break;
      }
    }
  }

  /// <summary>
  /// 获取产品列表
  /// </summary>
  /// <returns></returns>
  public List<ProductData> GetProductList()
  {
    List<ProductData> list = ProductData.DBStrToData(t_products);
    return list;
  }

  /// <summary>
  /// 删除一个产品
  /// </summary>
  /// <param name="pd"></param>
  /// <returns></returns>
  public bool RemProduct(ProductData pd)
  {
    List<ProductData> list = GetProductList();
    ProductData rpd = list.Find(x => x.IsSame(pd) == true);
    if(rpd != null)
    {
      list.Remove(rpd);
      t_products = ProductData.ToDBStr(list);
      Compute();
      return true;
    }
    return false;
  }
  /// <summary>
  /// 添加一个产品
  /// </summary>
  /// <param name="pd"></param>
  /// <returns></returns>
  public bool AddProduct(ProductData pd)
  {
    List<ProductData> list = GetProductList();
    if (list == null) list = new List<ProductData>();
    list.Add(pd);
    t_products = ProductData.ToDBStr(list);
    Compute();
    return true;
  }

  public List<BarterData> GetBarterList()
  {
    List<BarterData> list = BarterData.DBStrToData(t_barter);
    return list;
  }
  /// <summary>
  /// 添加一个消费
  /// </summary>
  /// <param name="db"></param>
  /// <returns></returns>
  public bool AddBarter(BarterData db)
  {
    List<BarterData> list = GetBarterList();
    if (list == null) list = new List<BarterData>();
    list.Add(db);
    t_barter = BarterData.ToDBStr(list);
    Compute();
    return true;
  }
  /// <summary>
  /// 删除一个消费
  /// </summary>
  /// <param name="db"></param>
  /// <returns></returns>
  public bool RemBarter(BarterData db)
  {
    List<BarterData> list = GetBarterList();
    BarterData rpd = list.Find(x => x.IsSame(db) == true);
    if (rpd != null)
    {
      list.Remove(rpd);
      t_barter = BarterData.ToDBStr(list);
      Compute();
      return true;
    }
    return false;
  }
  public List<AccountData> GetAccountList()
  {
    List<AccountData> list = AccountData.DBStrToData(t_accountRematk);
    return list;
  }
  /// <summary>
  /// 添加一个到账
  /// </summary>
  /// <param name="db"></param>
  /// <returns></returns>
  public bool AddAccount(AccountData ad)
  {
    List<AccountData> list = GetAccountList();
    if (list == null) list = new List<AccountData>();
    list.Add(ad);
    t_accountRematk = AccountData.ToDBStr(list);
    Compute();
    return true;
  }
  /// <summary>
  /// 删除一个到账
  /// </summary>
  /// <param name="db"></param>
  /// <returns></returns>
  public bool RemAccount(AccountData ad)
  {
    List<AccountData> list = GetAccountList();
    AccountData rpd = list.Find(x => x.IsSame(ad) == true);
    if (rpd != null)
    {
      list.Remove(rpd);
      t_accountRematk = AccountData.ToDBStr(list);
      Compute();
      return true;
    }
    return false;
  }


  private FieldInfo[] GetFields()
  {
    return GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
  }

  public List<ObjectVal> GetObjectVals()
  {
    List<ObjectVal> vals = new List<ObjectVal>();
    FieldInfo[] fields = GetFields();
    foreach (FieldInfo field in fields)
    {
      string name = field.Name;
      object value = field.GetValue(this);
      vals.Add(new ObjectVal(name, value));

      Debug.Log($"GetFiledDic: {name}:{value}");
    }
    return vals;
  }

  /// <summary>
  /// 获取距离当前时间最近的产品
  /// </summary>
  /// <returns></returns>
  public ProductData GetRecentlyPD()
  {
    ProductData resPD = null;
    List<ProductData> list = GetProductList();
    if (list != null)
    {
      int unixTime = AppUtil.GetNowUnixTime();
      int time = 0;
      foreach (ProductData pd in list)
      {
        if (pd.tTime >= 0)
        {
          int pT = pd.tTime - unixTime;
          if(time == 0 || pT <= time)
          {
            time = pT;
            resPD = pd;
          }
        }
      }
    }
    return resPD;
  }

  /// <summary>
  /// 是否要到期
  /// </summary>
  /// <param name="day"></param>
  /// <returns></returns>
  public bool isAdventTerm(int day =0)
  {
    if (day == 0) return true;

    //包含销售合同的不用处理是否到期
    if (t_products.Contains(AppConfig.SalesContract)) return false;

    bool res = false;
    List<ProductData> list = GetProductList();
    if (list != null)
    {
      int d = 3600 * 24 * day;
      int unixTime = AppUtil.GetNowUnixTime();
      foreach (ProductData pd in list)
      {
        if(pd.tTime > 0)
        {
          int pT = pd.tTime - unixTime;
          if(day < 0)//过期 
          {
            if (pT < 0)
            {
              res = true;
              break;
            }
          }
          else 
          {
            if (pT >= 0 && pT <= d)
            {
              res = true;
              break;
            }
          }
        }
      }
    }
    return res;
  }

  /// <summary>
  /// 模糊匹配酒店名字
  /// </summary>
  /// <param name="name"></param>
  /// <returns></returns>
  public bool isDimHotelNameTerm(string name)
  {
    bool res = name == AppConfig.ALL || t_hotelName.Contains(name);
    return res;
  }

  /// <summary>
  /// 全匹配酒店名字
  /// </summary>
  /// <param name="name"></param>
  /// <returns></returns>
  public bool isHotelNameTerm(string name)
  {
    bool res = name == AppConfig.ALL || t_hotelName == name;
    return res;
  }
  public bool isGroupTerm(string name)
  {
    bool res = name == AppConfig.ALL || t_group == name;
    return res;
  }
  /// <summary>
  /// 是否包含该产品
  /// </summary>
  /// <param name="name"></param>
  /// <returns></returns>
  public bool isProductTerm(string name)
  {
    bool res = name == AppConfig.ALL || t_products.Contains(name);
    return res;
  }

  public int GetProductExpirationTime() { return _ProductExpirationTime; }
  private int _ProductExpirationTime = 0;
  /// <summary> 实施计算产品的到期时间用于排序 </summary>
  public void ComputeProductExpirationTime()
  {
    _ProductExpirationTime = 0;
    ProductData pd = GetRecentlyPD();
    if (pd != null)
    {
      pd.GetProductType(out _ProductExpirationTime);
    }
    Debug.Log($"ComputeProductExpirationTime: {t_hotelName}:{_ProductExpirationTime}");
  }

  public void Compute()
  {
    // (合同总额)所有产品的总价格
    t_productsPrice = 0;
    List<ProductData> pddList = GetProductList();
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
    decimal dl = (decimal)t_productsPrice - (decimal)t_totalAccount;
    t_totalDebt = (float)dl;
    if (t_totalDebt > 0 && t_totalDebt < 1) t_totalDebt = 0;

    Debug.Log($"Compute:t_productsPrice={t_productsPrice} t_totalBarter={t_totalBarter} t_totalAccount={t_totalAccount} t_totalDebt={t_totalDebt}");
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
          //产品
          string products = IsEmpty(val.val);
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
          //价格
          string price = Find(vals, "AMOUNT");
          ProductData pd = ProductData.Crete(products, float.Parse(price), fTime, tTime, productsRem);
          pdList.Add(pd);
          string t_products = ProductData.ToDBStr(pdList);
          d.t_products = t_products;
          //d.t_productsPrice = val.val;这个是计算出来的
          break;
        case "Barter":
          string t_barter = IsEmpty(val.val);
          BarterData bData = BarterData.Crete(float.Parse(t_barter),0,"0");
          d.t_barter = bData.ToStr();
          break;
        case "A/R Remark"://到账明细2022.9.20收252006.11；2022.11.23收226805.5；2023.3.1收25200.61
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