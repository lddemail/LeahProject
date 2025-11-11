using Basics;
using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// <summary>
/// 详情
/// </summary>
public class UIDetail : UIBase
{
  public enum EmItemType
  {
    //产品
    Product,
    //消费明细
    Barter,
    //到账明细
    Account
  }

  private Dictionary<EmItemType, List<GComponent>> _itemDic = new Dictionary<EmItemType, List<GComponent>>();
  PopupMenu itemPop = new PopupMenu();

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
  private void AddOrRemItemDic(EmItemType type,GComponent item, bool isAdd)
  {
    if (!_itemDic.ContainsKey(type)) _itemDic.Add(type,new List<GComponent>());

    if (isAdd)
    {
      if (!_itemDic[type].Contains(item)) _itemDic[type].Add(item);
    }
    else
    {
      if (_itemDic[type].Contains(item)) _itemDic[type].Remove(item);
    }
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
      AddDetailItemProduct().SetData(pd);
      UpdateDataToTc(EmItemType.Product);
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
      AddDetailItemBarter().SetData(bd);
      UpdateDataToTc(EmItemType.Barter);
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
      AddDetailItemAccount().SetData(ad);
      UpdateDataToTc(EmItemType.Account);
    }
  }


  private void BtnSaveHandler(EventContext context)
  {
    bool isOk = false;
    //入库
    if (isAddTab)
    {
      isOk = AppData.AddTabContract(AppData.currTc);
    }
    else
    {
      isOk = AppData.UpdateTabContract(AppData.currTc);
    }
    //if (isOk)
    //{
    //  Timers.inst.Add(0.5f, 1, (object param) => {
    //    Hide();
    //  });
    //}
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


  private UIDetailItemLineExt productLine;
  private UIDetailItemLineExt barterLine;
  private UIDetailItemLineExt accountLine;
  List<ObjectVal> objectVals;
  bool isAddTab = false;
  public override void Show(object obj = null)
  {
    if(obj != null && obj is TabContract)
    {
      var hotelR = AppData.GetHotelRelevanceData(obj as TabContract);
      if(hotelR == null)//缺少模版
      {
        UIRoot.ins.uiTips.Show($"缺少模版无法打开详情");
        return;
      }
    }

    base.Show();
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

    UIPanel.m_DetailList.RemoveChildrenToPool();
    if (AppData.currTc != null)
    {
      //酒店名 省市 
      AddDetailItemTwoLabel(AppConfig.t_hotelName, AppConfig.HotelRelevanceTemplateName, AppConfig.t_city, "");
      //酒店品牌  酒店集团 甲方名称
      AddDetailItemThreeLabel(AppConfig.t_brand, "", AppConfig.t_group, "", AppConfig.t_a_contract, "");
      //签约公司  支付方式
      AddDetailItemTwoLabel(AppConfig.t_attribution, AppConfig.SignedTemplateName, AppConfig.t_payment, AppConfig.PaymentTemplateName);
      //内部编号  外部编号 
      AddDetailItemTwoLabel(AppConfig.t_interiorNo, "", AppConfig.t_contractNo, "");
      //欠款
      AddDetailItemOneLabel(AppConfig.t_totalDebt, "");

      //产品
      productLine = AddDetailItemLine(AppConfig.t_productsPrice);
      List<ProductData> pdList = ProductData.DBStrToData(AppData.currTc.t_products);
      foreach (ProductData pd in pdList)
      {
        AddDetailItemProduct().SetData(pd);
      }
      //到账
      accountLine = AddDetailItemLine(AppConfig.t_totalAccount);
      List<AccountData> adList = AccountData.DBStrToData(AppData.currTc.t_accountRematk);
      foreach (AccountData ad in adList)
      {
        AddDetailItemAccount().SetData(ad);
      }
      //消费
      barterLine = AddDetailItemLine(AppConfig.t_totalBarter);
      List<BarterData> bdList = BarterData.DBStrToData(AppData.currTc.t_barter);
      foreach (BarterData bd in bdList)
      {
        AddDetailItemBarter().SetData(bd);
      }

      //UIPanel.m_DetailList.ResizeToFit();
    }
  }

  private void RefreshItemUI()
  {
    GObject[] gobs = UIPanel.m_DetailList.GetChildren();
    foreach (GObject gob in gobs)
    {
      MethodInfo RefreshUI = gob.GetType().GetMethod(AppConfig.RefreshUI);
      if(RefreshUI != null)
      {
        RefreshUI.Invoke(gob,null);
      }
    }
  }


  private UIDetailItemLineExt AddDetailItemLine(string name)
  {
    UIDetailItemLineExt item = UIPanel.m_DetailList.AddItemFromPool(UIDetailItemLineExt.URL) as UIDetailItemLineExt;
    item.SetData( name);
    return item;
  }

  private UIDetailItemCityExt AddDetailItemCity(string provinceName, string cityName, string name1, string template1)
  {
    UIDetailItemCityExt item = UIPanel.m_DetailList.AddItemFromPool(UIDetailItemCityExt.URL) as UIDetailItemCityExt;
    item.SetData(provinceName, cityName, name1, template1);
    return item;
  }

  private UIDetailItemOneLabelExt AddDetailItemOneLabel(string name1, string template1)
  {
    UIDetailItemOneLabelExt item = UIPanel.m_DetailList.AddItemFromPool(UIDetailItemOneLabelExt.URL) as UIDetailItemOneLabelExt;
    item.SetData(name1, template1);
    item.SetChangeCallBack(RefreshItemUI);
    return item;
  }
  private UIDetailItemTwoLabelExt AddDetailItemTwoLabel(string name1, string template1, string name2, string template2)
  {
    UIDetailItemTwoLabelExt item = UIPanel.m_DetailList.AddItemFromPool(UIDetailItemTwoLabelExt.URL) as UIDetailItemTwoLabelExt;
    item.SetData(name1, template1, name2, template2);
    item.SetChangeCallBack(RefreshItemUI);
    return item;
  }
  private UIDetailItemThreeLabelExt AddDetailItemThreeLabel(string name1, string template1,string name2, string template2, string name3, string template3)
  {
    UIDetailItemThreeLabelExt item = UIPanel.m_DetailList.AddItemFromPool(UIDetailItemThreeLabelExt.URL) as UIDetailItemThreeLabelExt;
    item.SetData(name1,template1,name2, template2, name3, template3);
    item.SetChangeCallBack(RefreshItemUI);
    return item;
  }

  /// <summary>
  /// 添加产品
  /// </summary>
  /// <param name="val"></param>
  private UIDetailItemProductExt AddDetailItemProduct()
  {
    UIDetailItemProductExt item = UIPanel.m_DetailList.GetFromPool(UIDetailItemProductExt.URL) as UIDetailItemProductExt;
    AddOrRemItemDic(EmItemType.Product, item, true);
    item.SetChangeCallBack(ProductChange);
    item.onRightClick.Set(MainItemRightClick);
    item.onRollOut.Set(MainItemRollOut);
    int childIndex = UIPanel.m_DetailList.GetChildIndex(productLine);
    UIPanel.m_DetailList.AddChildAt(item, childIndex + 1);
    return item;

  }

  private void ProductChange()
  {
    UpdateDataToTc(EmItemType.Product);
    RefreshItemUI();
  }

  /// <summary>
  /// 添加消费
  /// </summary>
  /// <param name="val"></param>
  private UIDetailItemBarterExt AddDetailItemBarter()
  {
    UIDetailItemBarterExt item = UIPanel.m_DetailList.GetFromPool(UIDetailItemBarterExt.URL) as UIDetailItemBarterExt;
    AddOrRemItemDic(EmItemType.Barter, item, true);
    item.SetChangeCallBack(BarterChange);
    item.onRightClick.Set(MainItemRightClick);
    item.onRollOut.Set(MainItemRollOut);
    int childIndex = UIPanel.m_DetailList.GetChildIndex(barterLine);
    UIPanel.m_DetailList.AddChildAt(item, childIndex + 1);
    return item;
  }
  private void BarterChange()
  {
    UpdateDataToTc(EmItemType.Barter);
    RefreshItemUI();
  }


  /// <summary>
  /// 添加到账明细
  /// </summary>
  private UIDetailItemAccountExt AddDetailItemAccount()
  {
    UIDetailItemAccountExt item = UIPanel.m_DetailList.GetFromPool(UIDetailItemAccountExt.URL) as UIDetailItemAccountExt;
    AddOrRemItemDic(EmItemType.Account, item, true);
    item.SetChangeCallBack(AccountChange);
    item.onRightClick.Set(MainItemRightClick);
    item.onRollOut.Set(MainItemRollOut);
    int childIndex = UIPanel.m_DetailList.GetChildIndex(accountLine);
    UIPanel.m_DetailList.AddChildAt(item, childIndex + 1);
    return item;


  }
  private void AccountChange()
  {
    UpdateDataToTc(EmItemType.Account);
    RefreshItemUI();
  }

  private void MainItemRollOut(EventContext context)
  {
    //GComponent obj = context.sender as GComponent;
    //GObject m_BtnDel = obj.GetChild("BtnDel");
    //if(m_BtnDel != null)
    //{
    //  m_BtnDel.visible = false;
    //}
  }

  private void MainItemRightClick(EventContext context)
  {
    GComponent obj = context.sender as GComponent;
    itemPop.ClearItems();
    itemPop.AddItem(AppConfig.Delete, () => {
      UIRoot.ins.uiConfirm.Show($"确定要删除吗?", () =>
      {
        RemoveItem(obj);
      });
    });
    itemPop.Show();
  }

  private void RemoveItem(GComponent obj)
  {
    if (obj is UIDetailItemProductExt)
    {
      AddOrRemItemDic(EmItemType.Product, obj, false);
      UpdateDataToTc(EmItemType.Product);
    }
    else if (obj is UIDetailItemBarterExt)
    {
      AddOrRemItemDic(EmItemType.Barter, obj, false);
      UpdateDataToTc(EmItemType.Barter);
    }
    else if (obj is UIDetailItemAccountExt)
    {
      AddOrRemItemDic(EmItemType.Account, obj, false);
      UpdateDataToTc(EmItemType.Account);
    }
 
    UIPanel.m_DetailList.RemoveChildToPool(obj);
  }
  private void UpdateDataToTc(EmItemType type)
  {
    List<GComponent> coms = _itemDic[type];
    if (type == EmItemType.Product)
    {
      List<ProductData> pdList = new List<ProductData>();
      foreach (GComponent item in coms)
      {
        pdList.Add(item.data as ProductData);
      }
      AppData.currTc.t_products = ProductData.ToDBStr(pdList);
    }
    else if (type == EmItemType.Barter)
    {
      List<BarterData> bdList = new List<BarterData>();
      foreach (GComponent item in coms)
      {
        bdList.Add(item.data as BarterData);
      }
      AppData.currTc.t_barter = BarterData.ToDBStr(bdList);
    }
    else if (type == EmItemType.Account)
    {
      List<AccountData> adList = new List<AccountData>();
      foreach (GComponent item in coms)
      {
        adList.Add(item.data as AccountData);
      }
      AppData.currTc.t_accountRematk = AccountData.ToDBStr(adList);
    }
    AppData.currTc.Compute();
  }


  public override void Hide()
  {
    UIPanel.m_DetailList.RemoveChildrenToPool();
    base.Hide();
  }

}