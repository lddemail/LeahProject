using Basics;
using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
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
    //UIPanel.m_BtnClose.onClick.Add(BtnCloseHandler);

    //UIPanel.m_BtnHotelTemp.onClick.Add(() => {
    //  ToggleTemp(EmTempType.HotelTemp);
    //});
    //UIPanel.m_BtnPaymentTemp.onClick.Add(() => {
    //  ToggleTemp(EmTempType.PaymentTemp);
    //});
    //UIPanel.m_BtnSignedTemp.onClick.Add(() => {
    //  ToggleTemp(EmTempType.SignedTemp);
    //});
    //UIPanel.m_BtnHotelRelevanceTemp.onClick.Add(() => {
    //  ToggleTemp(EmTempType.HotelRelevanceTemp);
    //});
  }


  private void BtnCloseHandler(EventContext context)
  {
    Hide();
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


    switch (currTempType)
    {
      case EmTempType.PaymentTemp:
        break;
      case EmTempType.SignedTemp:
        break;
      case EmTempType.HotelRelevanceTemp:
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