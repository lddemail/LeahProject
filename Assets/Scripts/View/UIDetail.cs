﻿using Basics;
using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDetail : UIBase
{
  public UIDetail(Transform tf)
  {
    ui = UIRoot.ins.uiMain.UIPanel.m_UIDetail;
  }

  public UI_UIDetail UIPanel
  {
    get { return ui as UI_UIDetail; }
  }

  public override void Init()
  {
    UIPanel.m_BtnClose.onClick.Add(BtnCloseHandler);
    UIPanel.m_BtnSave.onClick.Add(BtnSaveHandler);
    UIPanel.m_BtnAddProduct.onClick.Add(BtnAddProductHandler);
    UIPanel.m_BtnAddBarter.onClick.Add(BtnAddBarterHandler);
    UIPanel.m_BtnAddAccount.onClick.Add(BtnAddAccountHandler);
  }
  /// <summary>
  /// 添加产品
  /// </summary>
  /// <param name="context"></param>
  /// <exception cref="NotImplementedException"></exception>
  private void BtnAddProductHandler(EventContext context)
  {
    if (AppData.currTc != null)
    {
      ProductData pd = new ProductData();
      AppData.currTc.AddProduct(pd);
      RefreshUI();
    }
  }
  /// <summary>
  /// 添加消费
  /// </summary>
  /// <param name="context"></param>
  /// <exception cref="NotImplementedException"></exception>
  private void BtnAddBarterHandler(EventContext context)
  {
    if (AppData.currTc != null)
    {
      BarterData bd = new BarterData();
      AppData.currTc.AddBarter(bd);
      RefreshUI();
    }
  }
  /// <summary>
  /// 添加到账
  /// </summary>
  /// <param name="context"></param>
  /// <exception cref="NotImplementedException"></exception>
  private void BtnAddAccountHandler(EventContext context)
  {
    if (AppData.currTc != null)
    {
      AccountData ad = new AccountData();
      AppData.currTc.AddAccount(ad);
      RefreshUI();
    }
  }



  /// <summary>
  /// 更新所有数据
  /// </summary>
  private void UpdateData()
  {
    List<AccountData> adList = new List<AccountData>();
    List<BarterData> bdList = new List<BarterData>();
    List<ProductData> pddList = new List<ProductData>();
    List<ObjectVal> obs = new List<ObjectVal>();
    GObject[] gobs = UIPanel.m_DetailList.GetChildren();
    foreach (GObject gob in gobs)
    {
      if (gob is UIDetailItemAccountExt)
      {
        UIDetailItemAccountExt Account = gob as UIDetailItemAccountExt;
        AccountData ad = Account.GetAccountData();
        if (!ad.isNull()) adList.Add(ad);
      }
      else if (gob is UIDetailItemBarterExt)
      {
        UIDetailItemBarterExt Barter = gob as UIDetailItemBarterExt;
        BarterData bd = Barter.GetBarterData();
        if (!bd.isNull()) bdList.Add(bd);
      }
      else if (gob is UIDetailItemCityExt)
      {
        UIDetailItemCityExt city = gob as UIDetailItemCityExt;
        AppData.currTc.t_province = city.GetProvince();
        AppData.currTc.t_city = city.GetCity();
      }
      else if (gob is UIDetailItemProductExt)
      {
        UIDetailItemProductExt Product = gob as UIDetailItemProductExt;
        ProductData pd = Product.GetProductData();
        if (!pd.isNull()) pddList.Add(pd);
      }
      else if (gob is UIDetailItemLabelExt)
      {
        UIDetailItemLabelExt label = gob as UIDetailItemLabelExt;
        ObjectVal ov = label.GetOV();
        switch (ov.name)
        {
           case "t_productsPrice":
           case "t_totalBarter":
           case "t_totalAccount":
           case "t_totalDebt":
            break;
          default:
            obs.Add(ov);
            break;

        }
    
      }
    }
    AppData.currTc.t_accountRematk = AccountData.ToDBStr(adList);
    AppData.currTc.t_barter = BarterData.ToDBStr(bdList);
    AppData.currTc.t_products = ProductData.ToDBStr(pddList);
    AppData.currTc.CoverOVS(obs);
    AppData.currTc.Compute();
  
  }

  private void BtnSaveHandler(EventContext context)
  {
    UpdateData();
    //入库
    if (isAddTab)
    {
      AppData.AddTabContract(AppData.currTc);
      UIRoot.ins.uiTips.Show($"{AppData.currTc.t_hotelName} 新增入库完成");
    }
    else
    {
      AppData.UpdateTabContract(AppData.currTc);
      UIRoot.ins.uiTips.Show($"{AppData.currTc.t_hotelName} 数据库更新完成");
    }
  }

  private void BtnCloseHandler(EventContext context)
  {
    UIRoot.ins.uiConfirm.Show($"如果没有点击保存所有修将被还原.", () => {
      if (AppData.currTc != null && AppData.currTc.t_id > 0)
      {
        AppData.DBCoverLocalById(AppData.currTc.t_id);
      }
      Hide();
    });
  }

  List<ObjectVal> objectVals;
  bool isAddTab = false;
  public override void Show(object obj = null)
  {
    if(obj != null)
    {
      isAddTab = false;
      AppData.currTc = obj as TabContract;
    }
    else
    {
      isAddTab = true;
      AppData.currTc = new TabContract();
    }
    UIPanel.visible = true;

    RefreshUI();
  }

  private void RefreshItemUI()
  {
    UpdateData();

    GObject[] gobs = UIPanel.m_DetailList.GetChildren();
    foreach (GObject gob in gobs)
    {
      if (gob is UIDetailItemAccountExt)
      {
        UIDetailItemAccountExt Account = gob as UIDetailItemAccountExt;
        Account.RefreshUI();
      }
      else if (gob is UIDetailItemBarterExt)
      {
        UIDetailItemBarterExt Barter = gob as UIDetailItemBarterExt;
        Barter.RefreshUI();
      }
      else if (gob is UIDetailItemCityExt)
      {
        UIDetailItemCityExt city = gob as UIDetailItemCityExt;
      }
      else if (gob is UIDetailItemProductExt)
      {
        UIDetailItemProductExt Product = gob as UIDetailItemProductExt;
        Product.RefreshUI();
      }
      else if (gob is UIDetailItemLabelExt)
      {
        UIDetailItemLabelExt label = gob as UIDetailItemLabelExt;
        label.RefreshUI();
      }
    }
  }

  private void RefreshUI()
  {
    UIPanel.m_DetailList.RemoveChildrenToPool();
    if (AppData.currTc != null)
    {
      objectVals = AppData.currTc.GetObjectVals();
      foreach (ObjectVal val in objectVals)
      {
        switch (val.name)
        {
          case "t_hotelName":
          case "t_group":
          case "t_brand":
            AddDetailItemLabel(val);
            break;
          case "t_province":
          case "t_city":
            AddDetailItemCity(val);
            break;
          case "t_originalFollowup":
          case "t_newSales":
          case "t_interiorNo":
          case "t_contractNo":
          case "t_payment":
          case "t_attribution":
          case "t_a_contract":
            AddDetailItemLabel(val);
            break;
          case "t_products":
            AddDetailItemProduct(val);
            break;
          case "t_productsPrice":
            AddDetailItemLabel(val);
            break;
          case "t_barter":
            AddDetailItemBarter(val);
            break;
          case "t_totalBarter":
            AddDetailItemLabel(val);
            break;
          case "t_accountRematk":
            AddDetailItemAccount(val);
            break;
          case "t_totalAccount":
            AddDetailItemLabel(val);
            break;
          case "t_totalDebt":
            AddDetailItemLabel(val);
            break;
          default:
            Debug.Log($"没有处理:{val.name}");
            break;
        }

      }
    }
  }

  private UIDetailItemCityExt cityItem;
  private void AddDetailItemCity(ObjectVal val)
  {
    if(cityItem == null)
    {
      cityItem = UIPanel.m_DetailList.AddItemFromPool(UI_DetailItemCity.URL) as UIDetailItemCityExt;
    }
    cityItem.SetData(val);
  }

  private void AddDetailItemLabel(ObjectVal val)
  {
    UIDetailItemLabelExt item = UIPanel.m_DetailList.AddItemFromPool(UI_DetailItemLabel.URL) as UIDetailItemLabelExt;
    item.SetData(val);
  }
 /// <summary>
 /// 展示产品
 /// </summary>
 /// <param name="val"></param>
  private void AddDetailItemProduct(ObjectVal val)
  {
    List<ProductData> pddList = ProductData.DBStrToData(val.val.ToString());
    foreach (ProductData pdd in pddList)
    {
      UIDetailItemProductExt item = UIPanel.m_DetailList.AddItemFromPool(UI_DetailItemProduct.URL) as UIDetailItemProductExt;
      item.SetData(pdd);
      item.SetChangeCallBack(ProductChange);
      item.onRightClick.Set(MainItemRightClick);
      item.onRollOut.Set(MainItemRollOut);
    }
  }
  private void ProductChange()
  {
    RefreshItemUI();
  }
  /// <summary>
  /// 展示消费
  /// </summary>
  /// <param name="val"></param>
  private void AddDetailItemBarter(ObjectVal val)
  {
    List<BarterData> list = BarterData.DBStrToData(val.val.ToString());
    foreach(BarterData d in list)
    {
      UIDetailItemBarterExt item = UIPanel.m_DetailList.AddItemFromPool(UI_DetailItemBarter.URL) as UIDetailItemBarterExt;
      item.SetData(d);
      item.SetChangeCallBack(BarterChange);
      item.onRightClick.Set(MainItemRightClick);
      item.onRollOut.Set(MainItemRollOut);
    }
  }
  private void BarterChange()
  {
    RefreshItemUI();
  }
  /// <summary>
  /// 展示到账明细
  /// </summary>
  private void AddDetailItemAccount(ObjectVal val)
  {
    List<AccountData> list = AccountData.DBStrToData(val.val.ToString());
    foreach (AccountData d in list)
    {
      UIDetailItemAccountExt item = UIPanel.m_DetailList.AddItemFromPool(UI_DetailItemAccount.URL) as UIDetailItemAccountExt;
      item.SetData(d);
      item.SetChangeCallBack(AccountChange);
      item.onRightClick.Set(MainItemRightClick);
      item.onRollOut.Set(MainItemRollOut);
    }
  }
  private void AccountChange()
  {
    RefreshItemUI();
  }

  private void MainItemRollOut(EventContext context)
  {
    GComponent obj = context.sender as GComponent;
    GObject m_BtnDel = obj.GetChild("BtnDel");
    if(m_BtnDel != null)
    {
      m_BtnDel.visible = false;
    }
  }

  private void MainItemRightClick(EventContext context)
  {
    GComponent obj = context.sender as GComponent;
    GObject m_BtnDel = obj.GetChild("BtnDel");
    if(m_BtnDel != null)
    {
      m_BtnDel.visible = true;
      m_BtnDel.x = obj.displayObject.GlobalToLocal(context.inputEvent.position).x - 60;
      m_BtnDel.onClick.Set(() => {
        UIRoot.ins.uiConfirm.Show($"确定要删除吗?", () => {
          RemoveData(obj.data);
        });
      });
    }
  }
  private void RemoveData(object data)
  {
    ProductData pd = data as ProductData;
    BarterData bd = data as BarterData;
    AccountData ad = data as AccountData;
    bool isOk = false;
    if (pd != null)
    {
      isOk = AppData.currTc.RemProduct(pd);
    }
    else if(bd != null)
    {
      isOk = AppData.currTc.RemBarter(bd);
    }
    else if (ad != null)
    {
      isOk = AppData.currTc.RemAccount(ad);
    }
    if(isOk)
    {
      Debug.Log("删除刷新UI");
      RefreshUI();
    }
  }

  public override void Hide()
  {
    UIPanel.m_DetailList.RemoveChildrenToPool();
    UIPanel.visible = false;
    cityItem = null;
  }

}