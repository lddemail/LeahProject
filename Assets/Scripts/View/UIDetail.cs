using Basics;
using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

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
    if (isOk)
    {
      Hide();
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

    InitUI();
  }


  private UIDetailItemLineExt productLine;
  private UIDetailItemLineExt barterLine;
  private UIDetailItemLineExt accountLine;
  private void InitUI()
  {
    UIPanel.m_DetailList.RemoveChildrenToPool();
    if (AppData.currTc != null)
    {
      AddDetailItemThreeLabel(AppConfig.t_hotelName, AppConfig.t_brand,AppConfig.t_attribution);

      AddDetailItemLabel(AppConfig.t_a_contract);

      AddDetailItemThreeLabel(AppConfig.t_originalFollowup, AppConfig.t_newSales, AppConfig.t_payment);

      AddDetailItemCity(AppConfig.t_province, AppConfig.t_city);

      AddDetailItemThreeLabel(AppConfig.t_interiorNo, AppConfig.t_contractNo, AppConfig.t_totalDebt);

      productLine = AddDetailItemLine(AppConfig.t_productsPrice);
      productLine.childIndex = UIPanel.m_DetailList.GetChildIndex(productLine);
      List<ProductData> pdList = ProductData.DBStrToData(AppData.currTc.t_products);
      foreach (ProductData pd in pdList)
      {
        AddDetailItemProduct().SetData(pd);
      }
     
      barterLine = AddDetailItemLine(AppConfig.t_totalBarter);
      barterLine.childIndex = UIPanel.m_DetailList.GetChildIndex(barterLine);
      List<BarterData> bdList = BarterData.DBStrToData(AppData.currTc.t_barter);
      foreach (BarterData bd in bdList)
      {
        AddDetailItemBarter().SetData(bd);
      }

      accountLine = AddDetailItemLine(AppConfig.t_totalAccount);
      accountLine.childIndex = UIPanel.m_DetailList.GetChildIndex(accountLine);
      List<AccountData> adList = AccountData.DBStrToData(AppData.currTc.t_accountRematk);
      foreach (AccountData ad in adList)
      {
        AddDetailItemAccount().SetData(ad);
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

  private void RefreshUI()
  {
  }

  private UIDetailItemLineExt AddDetailItemLine(string name)
  {
    UIDetailItemLineExt item = UIPanel.m_DetailList.AddItemFromPool(UIDetailItemLineExt.URL) as UIDetailItemLineExt;
    item.SetData( name);
    return item;
  }

  private UIDetailItemCityExt AddDetailItemCity(string provinceName, string cityName)
  {
    UIDetailItemCityExt item = UIPanel.m_DetailList.AddItemFromPool(UIDetailItemCityExt.URL) as UIDetailItemCityExt;
    item.SetData(provinceName, cityName);
    return item;
  }

  private UIDetailItemLabelExt AddDetailItemLabel(string name)
  {
    UIDetailItemLabelExt item = UIPanel.m_DetailList.AddItemFromPool(UIDetailItemLabelExt.URL) as UIDetailItemLabelExt;
    item.SetData(name);
    return item;
  }
  private UIDetailItemTwoLabelExt AddDetailItemTwoLabel(string name1, string name2)
  {
    UIDetailItemTwoLabelExt item = UIPanel.m_DetailList.AddItemFromPool(UIDetailItemTwoLabelExt.URL) as UIDetailItemTwoLabelExt;
    item.SetData(name1, name2);
    return item;
  }
  private UIDetailItemThreeLabelExt AddDetailItemThreeLabel(string name1, string name2, string name3)
  {
    UIDetailItemThreeLabelExt item = UIPanel.m_DetailList.AddItemFromPool(UIDetailItemThreeLabelExt.URL) as UIDetailItemThreeLabelExt;
    item.SetData(name1, name2, name3);
    return item;
  }

  List<UIDetailItemProductExt> itemProductList = new List<UIDetailItemProductExt>();
  private void AddToRemProductList(UIDetailItemProductExt item,bool isAdd)
  {
    if (isAdd)
    {
      if (!itemProductList.Contains(item)) itemProductList.Add(item);
    }
    else
    {
      if (itemProductList.Contains(item)) itemProductList.Remove(item);
    }
  }
  /// <summary>
  /// 展示产品
  /// </summary>
  /// <param name="val"></param>
  private UIDetailItemProductExt AddDetailItemProduct()
  {
    UIDetailItemProductExt item = UIPanel.m_DetailList.GetFromPool(UIDetailItemProductExt.URL) as UIDetailItemProductExt;
    AddToRemProductList(item,true);
    item.SetChangeCallBack(ProductChange);
    item.onRightClick.Set(MainItemRightClick);
    item.onRollOut.Set(MainItemRollOut);
    UIPanel.m_DetailList.AddChildAt(item, productLine.childIndex + 1);
    return item;

  }

  private void ProductChange()
  {
    UpdateDataToTc(EmItemType.Product);
    RefreshItemUI();
  }

  List<UIDetailItemBarterExt> itemBarterList = new List<UIDetailItemBarterExt>();
  private void AddToRemBarterList(UIDetailItemBarterExt item,bool isAdd)
  {
    if (isAdd)
    {
      if (!itemBarterList.Contains(item)) itemBarterList.Add(item);
    }
    else
    {
      if (itemBarterList.Contains(item)) itemBarterList.Remove(item);
    }
  }
  /// <summary>
  /// 展示消费
  /// </summary>
  /// <param name="val"></param>
  private UIDetailItemBarterExt AddDetailItemBarter()
  {
    UIDetailItemBarterExt item = UIPanel.m_DetailList.GetFromPool(UIDetailItemBarterExt.URL) as UIDetailItemBarterExt;
    AddToRemBarterList(item,true);
    item.SetChangeCallBack(BarterChange);
    item.onRightClick.Set(MainItemRightClick);
    item.onRollOut.Set(MainItemRollOut);
    UIPanel.m_DetailList.AddChildAt(item, barterLine.childIndex + 1);
    return item;
  }
  private void BarterChange()
  {
    UpdateDataToTc(EmItemType.Barter);
    RefreshItemUI();
  }


  List<UIDetailItemAccountExt> itemAccountList = new List<UIDetailItemAccountExt>();
  private void AddToRemAccountList(UIDetailItemAccountExt item,bool isAdd)
  {
    if (isAdd)
    {
      if (!itemAccountList.Contains(item)) itemAccountList.Add(item);
    }
    else
    {
      if (itemAccountList.Contains(item)) itemAccountList.Remove(item);
    }
  }
  /// <summary>
  /// 展示到账明细
  /// </summary>
  private UIDetailItemAccountExt AddDetailItemAccount()
  {
    UIDetailItemAccountExt item = UIPanel.m_DetailList.GetFromPool(UIDetailItemAccountExt.URL) as UIDetailItemAccountExt;
    AddToRemAccountList(item,true);
    item.SetChangeCallBack(AccountChange);
    item.onRightClick.Set(MainItemRightClick);
    item.onRollOut.Set(MainItemRollOut);
    UIPanel.m_DetailList.AddChildAt(item, accountLine.childIndex + 1);
    return item;


  }
  private void AccountChange()
  {
    UpdateDataToTc(EmItemType.Account);
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
          RemoveItem(obj);
        });
      });
    }
  }

  private void RemoveItem(GComponent obj)
  {
    if (obj is UIDetailItemProductExt)
    {
      AddToRemProductList(obj as UIDetailItemProductExt, false);
      UpdateDataToTc(EmItemType.Product);
    }
    else if (obj is UIDetailItemBarterExt)
    {
      AddToRemBarterList(obj as UIDetailItemBarterExt, false);
      UpdateDataToTc(EmItemType.Barter);
    }
    else if (obj is UIDetailItemAccountExt)
    {
      AddToRemAccountList(obj as UIDetailItemAccountExt, false);
      UpdateDataToTc(EmItemType.Account);
    }
 
    UIPanel.m_DetailList.RemoveChildToPool(obj);
  }
  private void UpdateDataToTc(EmItemType type)
  {
    if (type == EmItemType.Product)
    {
      List<ProductData> pdList = new List<ProductData>();
      foreach (UIDetailItemProductExt item in itemProductList)
      {
        pdList.Add(item.data as ProductData);
      }
      AppData.currTc.t_products = ProductData.ToDBStr(pdList);
    }
    else if (type == EmItemType.Barter)
    {
      List<BarterData> bdList = new List<BarterData>();
      foreach (UIDetailItemBarterExt item in itemBarterList)
      {
        bdList.Add(item.data as BarterData);
      }
      AppData.currTc.t_barter = BarterData.ToDBStr(bdList);
    }
    else if (type == EmItemType.Account)
    {
      List<AccountData> adList = new List<AccountData>();
      foreach (UIDetailItemAccountExt item in itemAccountList)
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
    UIPanel.visible = false;
  }

}