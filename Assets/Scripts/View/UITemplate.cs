using Basics;
using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

/// <summary>
/// 模版
/// </summary>
public class UITemplate : UIBase
{
  private List<PaymentTempData> PaymentTempData; 
  private List<SignedTempData> SignedTempData;
  private List<HotelRelevanceTempData> HotelRelevanceTempData;
  private string currTempType = "";

  PopupMenu itemPop = new PopupMenu();

  public UITemplate(Transform tf)
  {
    ui = UIRoot.ins.uiMain.UIPanel.m_UITemplate;
  }

  public UI_UITemplate UIPanel
  {
    get { return ui as UI_UITemplate; }
  }

  public override void Init()
  {
    UIPanel.m_BtnClose.onClick.Add(BtnCloseHandler);
    UIPanel.m_BtnSave.onClick.Add(BtnSaveHandler);

    UIPanel.m_BtnPaymentTemp.onClick.Add(() =>
    {
      ToggleTemp(AppConfig.PaymentTemplateName);
    });
    UIPanel.m_BtnSignedTemp.onClick.Add(() =>
    {
      ToggleTemp(AppConfig.SignedTemplateName);
    });
    UIPanel.m_BtnHotelRelevanceTemp.onClick.Add(() =>
    {
      ToggleTemp(AppConfig.HotelRelevanceTemplateName);
    });

    UIPanel.m_PaymentTempList.itemRenderer = PaymentTempList;
    UIPanel.m_PaymentTempList.SetVirtual();

    UIPanel.m_SignedTempList.itemRenderer = SignedTempListRender;
    UIPanel.m_SignedTempList.SetVirtual();

    UIPanel.m_HotelRelevanceTempList.itemRenderer = HotelRelevanceTempListRender;
    UIPanel.m_HotelRelevanceTempList.SetVirtual();
  }
  private void PaymentTempList(int index, GObject item)
  {
    var data = PaymentTempData[index];
    var _item = item as UI_PaymentTempListItemExt;
    _item.SetData(data);
    _item.onRightClick.Set(MainItemRightClick);
    //_item.onRollOut.Set(MainItemRollOut);
  }
  private void SignedTempListRender(int index, GObject item)
  {
    var data = SignedTempData[index];
    var _item = item as UI_SignedTempListItemExt;
    _item.SetData(data);
    _item.onRightClick.Set(MainItemRightClick);
    //_item.onRollOut.Set(MainItemRollOut);
  }
  private void HotelRelevanceTempListRender(int index, GObject item)
  {
    var data = HotelRelevanceTempData[index];
    var _item = item as UI_HotelRelevanceTempListItemExt;
    _item.SetData(data);
    _item.onRightClick.Set(MainItemRightClick);
    //_item.onRollOut.Set(MainItemRollOut);
  }
  private void MainItemRightClick(EventContext context)
  {
    GComponent obj = context.sender as GComponent;
    itemPop.ClearItems();
    itemPop.AddItem(AppConfig.Delete, () => {

      string tips = "";
      int id = -1;
      switch (currTempType)
      {
        case AppConfig.PaymentTemplateName:
          tips = (obj.data as PaymentTempData).t_Name;
          id = (obj.data as PaymentTempData).t_id;
          break;
        case AppConfig.SignedTemplateName:
          tips = (obj.data as SignedTempData).t_Name;
          id = (obj.data as SignedTempData).t_id;
          break;
        case AppConfig.HotelRelevanceTemplateName:
          tips = (obj.data as HotelRelevanceTempData).t_hotelName;
          id = (obj.data as HotelRelevanceTempData).t_id;
          break;
      }
      UIRoot.ins.uiConfirm.Show($"确定要删除:{tips}吗?", () =>
      {
        AppData.RemoveTemp(currTempType, id);
      });
    });
    itemPop.Show();
  }

  private void BtnCloseHandler(EventContext context)
  {
    UIRoot.ins.uiConfirm.Show($"如果没有点击保存所有修将被还原.", () => {

      Hide();
    });
  }
  private void BtnSaveHandler(EventContext context)
  {
    //保存
    AppData.SaveAllTemp();
    UIRoot.ins.uiTips.Show($"模版保存成功");
  }
  public override void Show(object obj = null)
  {

    UIPanel.visible = true;
    currTempType = "";
    InitUI();
    ToggleTemp(AppConfig.PaymentTemplateName);
  }

  private void InitUI()
  {
  
  }

  /// <summary>
  /// 切换模版
  /// </summary>
  private void ToggleTemp(string tempType)
  {
    if (currTempType == tempType) return;

    currTempType = tempType;

    UIPanel.m_PaymentTempList.visible = currTempType == AppConfig.PaymentTemplateName;
    UIPanel.m_SignedTempList.visible = currTempType == AppConfig.SignedTemplateName;
    UIPanel.m_HotelRelevanceTempList.visible = currTempType == AppConfig.HotelRelevanceTemplateName;

    switch (currTempType)
    {
      case AppConfig.PaymentTemplateName:
        PaymentTempData = AppData.allPaymentTempData.Values.ToList();
        UIPanel.m_PaymentTempList.numItems = PaymentTempData.Count;
        break;
      case AppConfig.SignedTemplateName:
        SignedTempData = AppData.allSignedTempData.Values.ToList();
        UIPanel.m_SignedTempList.numItems = SignedTempData.Count;
        break;
      case AppConfig.HotelRelevanceTemplateName:
        HotelRelevanceTempData = AppData.allHotelRelevanceTempData.Values.ToList();
        UIPanel.m_HotelRelevanceTempList.numItems = HotelRelevanceTempData.Count;
        break;
    }
  }

  private void RefreshItemUI()
  {
    //GObject[] gobs = UIPanel.m_DetailList.GetChildren();
    //foreach (GObject gob in gobs)
    //{
    //  MethodInfo RefreshUI = gob.GetType().GetMethod(AppConfig.RefreshUI);
    //  if (RefreshUI != null)
    //  {
    //    RefreshUI.Invoke(gob, null);
    //  }
    //}
  }

  public override void RefreshUI()
  {
  }

  public override void Hide()
  {
    //UIPanel.m_DetailList.RemoveChildrenToPool();
    UIPanel.visible = false;
    AppData.ReadAllTemplates();
    UIRoot.ins.uiTips.Show($"模版刷新成功");
  }

}