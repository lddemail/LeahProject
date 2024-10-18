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
  public enum EmTempType
  {
    None,
    //支付模版
    PaymentTemp,
    //签约公司模版
    SignedTemp,
    //酒店关联
    HotelRelevanceTemp
  }

  private List<string> PaymentTempData; 
  private List<string> SignedTempData;
  private List<HotelRelevanceData> HotelRelevanceTempData;
  private EmTempType currTempType = EmTempType.None;

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
      ToggleTemp(EmTempType.PaymentTemp);
    });
    UIPanel.m_BtnSignedTemp.onClick.Add(() =>
    {
      ToggleTemp(EmTempType.SignedTemp);
    });
    UIPanel.m_BtnHotelRelevanceTemp.onClick.Add(() =>
    {
      ToggleTemp(EmTempType.HotelRelevanceTemp);
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
    string str= PaymentTempData[index];
    var _item = item as UI_PaymentTempListItemExt;
    _item.SetData(str);
    _item.onRightClick.Set(MainItemRightClick);
    //_item.onClick.Set(MainItemDoubleClick);
    //_item.onRollOut.Set(MainItemRollOut);
  }
  private void SignedTempListRender(int index, GObject item)
  {
    string str = SignedTempData[index];
    var _item = item as UI_SignedTempListItemExt;
    _item.SetData(str);
    _item.onRightClick.Set(MainItemRightClick);
    //_item.onClick.Set(MainItemDoubleClick);
    //_item.onRollOut.Set(MainItemRollOut);
  }
  private void HotelRelevanceTempListRender(int index, GObject item)
  {
    HotelRelevanceData data = HotelRelevanceTempData[index];
    var _item = item as UI_HotelRelevanceTempListItemExt;
    _item.SetData(data);
    _item.onRightClick.Set(MainItemRightClick);
    //_item.onClick.Set(MainItemDoubleClick);
    //_item.onRollOut.Set(MainItemRollOut);
  }
  private void MainItemRightClick(EventContext context)
  {
    GComponent obj = context.sender as GComponent;
    itemPop.ClearItems();
    itemPop.AddItem(AppConfig.Delete, () => {
      UIRoot.ins.uiConfirm.Show($"确定要删除:{obj.data}吗?", () =>
      {

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
  }
  public override void Show(object obj = null)
  {

    UIPanel.visible = true;

    InitUI();
    ToggleTemp(EmTempType.PaymentTemp);
  }

  private void InitUI()
  {
  
  }

  /// <summary>
  /// 切换模版
  /// </summary>
  private void ToggleTemp(EmTempType tempType)
  {
    if (currTempType == tempType) return;

    currTempType = tempType;

    UIPanel.m_PaymentTempList.visible = currTempType == EmTempType.PaymentTemp;
    UIPanel.m_SignedTempList.visible = currTempType == EmTempType.SignedTemp;
    UIPanel.m_HotelRelevanceTempList.visible = currTempType == EmTempType.HotelRelevanceTemp;

    switch (currTempType)
    {
      case EmTempType.PaymentTemp:
        PaymentTempData = AppData.GetTempList(AppConfig.PaymentTemplateName);
        UIPanel.m_PaymentTempList.numItems = PaymentTempData.Count;
        break;
      case EmTempType.SignedTemp:
        SignedTempData = AppData.GetTempList(AppConfig.SignedTemplateName);
        UIPanel.m_SignedTempList.numItems = SignedTempData.Count;
        break;
      case EmTempType.HotelRelevanceTemp:
        HotelRelevanceTempData = AppData.allHotelRelevances.Values.ToList();
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