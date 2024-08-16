using Basics;
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
    if (tc != null)
    {
      ProductData pd = new ProductData();
      tc.AddProduct(pd);
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
    if (tc != null)
    {
      BarterData bd = new BarterData();
      tc.AddBarter(bd);
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
    if (tc != null)
    {
      AccountData ad = new AccountData();
      tc.AddAccount(ad);
      RefreshUI();
    }
  }


  private void BtnSaveHandler(EventContext context)
  {
    //入库
    if(isAddTab)
    {
      AppData.AddTabContract(tc);
      UIRoot.ins.uiTips.Show($"{tc.t_hotelName} 新增入库完成");
    }
    else
    {
      AppData.UpdateTabContract(tc);
      UIRoot.ins.uiTips.Show($"{tc.t_hotelName} 数据库更新完成");
    }
    Hide();
  }

  private void BtnCloseHandler(EventContext context)
  {
    UIRoot.ins.uiConfirm.Show($"如果没有点击保存所有修将被还原.", () => {
      if (tc != null && tc.t_id > 0)
      {
        AppData.DBCoverLocalById(tc.t_id);
      }
      Hide();
    });
  }

  List<ObjectVal> objectVals;
  TabContract tc;
  bool isAddTab = false;
  public override void Show(object obj = null)
  {
    if(obj != null)
    {
      isAddTab = false;
      tc = obj as TabContract;
    }
    else
    {
      isAddTab = true;
      tc = new TabContract();
    }
    UIPanel.visible = true;

    RefreshUI();
  }

  private void RefreshUI()
  {
    UIPanel.m_DetailList.RemoveChildrenToPool();
    if (tc != null)
    {
      objectVals = tc.GetObjectVals();
      foreach (ObjectVal val in objectVals)
      {
        switch (val.name)
        {
          case "t_hotelName":
          case "t_group":
          case "t_brand":
          case "t_province":
          case "t_city":
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
      item.onRightClick.Set(MainItemRightClick);
      item.onRollOut.Set(MainItemRollOut);
    }
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
      item.onRightClick.Set(MainItemRightClick);
      item.onRollOut.Set(MainItemRollOut);
    }
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
      item.onRightClick.Set(MainItemRightClick);
      item.onRollOut.Set(MainItemRollOut);
    }
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
      isOk = tc.RemProduct(pd);
    }
    else if(bd != null)
    {
      isOk = tc.RemBarter(bd);
    }
    else if (ad != null)
    {
      isOk = tc.RemAccount(ad);
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
  }

}