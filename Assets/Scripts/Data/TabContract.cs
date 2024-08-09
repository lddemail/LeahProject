using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

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
  /// 酒店消费金额手动输入
  /// </summary>
  public float t_barter;
  /// <summary>
  /// 酒店消费金额备注
  /// </summary>
  public float t_barterRemark;

  /// <summary>
  /// 到账总额
  /// </summary>
  public float t_totalAccount;

  /// <summary>
  /// 到账明细
  /// </summary>
  public string t_accountRematk;

  /// <summary>
  /// 欠款金额=(合同总额t_productsPrice-到账总额t_totalAccount)
  /// </summary>
  public float t_totalDebt;
}