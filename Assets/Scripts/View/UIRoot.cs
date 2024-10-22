using Basics;
using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIRoot : MonoBehaviour
{
  public static void FguiBinder()
  {
    BasicsBinder.BindAll();
    UIObjectFactory.SetPackageItemExtension(UI_DetailItemLabel.URL, typeof(UIDetailItemOneLabelExt));
    UIObjectFactory.SetPackageItemExtension(UI_DetailItemTwoLabel.URL, typeof(UIDetailItemTwoLabelExt));
    UIObjectFactory.SetPackageItemExtension(UI_DetailItemThreeLabel.URL, typeof(UIDetailItemThreeLabelExt));
    UIObjectFactory.SetPackageItemExtension(UI_MainListItem.URL, typeof(UIMainListItemExt));
    UIObjectFactory.SetPackageItemExtension(UI_DetailItemProduct.URL, typeof(UIDetailItemProductExt));
    UIObjectFactory.SetPackageItemExtension(UI_DetailItemBarter.URL, typeof(UIDetailItemBarterExt));
    UIObjectFactory.SetPackageItemExtension(UI_DetailItemAccount.URL, typeof(UIDetailItemAccountExt));
    UIObjectFactory.SetPackageItemExtension(UI_DetailItemCity.URL, typeof(UIDetailItemCityExt));
    UIObjectFactory.SetPackageItemExtension(UI_DetailItemLine.URL, typeof(UIDetailItemLineExt));
    UIObjectFactory.SetPackageItemExtension(UI_InputTimeLabelComp.URL, typeof(UI_InputTimeLabelCompExt)); 
    UIObjectFactory.SetPackageItemExtension(UI_InputNumLabelComp.URL, typeof(UI_InputNumLabelCompExt)); 
    UIObjectFactory.SetPackageItemExtension(UI_InputDescLabelComp.URL, typeof(UI_InputDescLabelCompExt)); 
    UIObjectFactory.SetPackageItemExtension(UI_InputStrLabelComp.URL, typeof(UI_InputStrLabelCompExt)); 
    UIObjectFactory.SetPackageItemExtension(UI_InputComboxLabelComp.URL, typeof(UI_InputComboxLabelCompExt));
    UIObjectFactory.SetPackageItemExtension(UI_PaymentTempListItem.URL, typeof(UI_PaymentTempListItemExt));
    UIObjectFactory.SetPackageItemExtension(UI_SignedTempListItem.URL, typeof(UI_SignedTempListItemExt));
    UIObjectFactory.SetPackageItemExtension(UI_HotelRelevanceTempListItem.URL, typeof(UI_HotelRelevanceTempListItemExt));
    UIObjectFactory.SetPackageItemExtension(UI_UILogListItem.URL, typeof(UI_UILogListItemExt));
  }

  public static UIRoot ins;
  private void Awake()
  {
    ins = this;
  }

  private UIMain _uiMain;
  public UIMain uiMain
  {
    get
    { _uiMain ??= new UIMain(transform); return _uiMain; }
  }

  private UITips _uiTips;
  public UITips uiTips
  {
    get { _uiTips ??= new UITips(transform); return _uiTips; }
  }
  private UIConfirm _uiConfirm;
  public UIConfirm uiConfirm
  {
    get { _uiConfirm ??= new UIConfirm(transform); return _uiConfirm; }
  }

  private UIDetail _uiDetail;
  public UIDetail uiDetail
  {
    get { _uiDetail ??= new UIDetail(transform); return _uiDetail; }
  }

  private UITemplate _uiTemplate;
  public UITemplate uiTemplate
  {
    get { _uiTemplate ??= new UITemplate(transform); return _uiTemplate; }
  }
  private UILog _uiLog;
  public UILog uiLog
  {
    get { _uiLog ??= new UILog(transform); return _uiLog; }
  }

  public void Init()
  {
    UIConfig.popupMenu = "ui://Basics/MainPopupMenu";
    UIConfig.tooltipsWin = "ui://Basics/TipsLab";
    uiMain.Init();
    uiTips.Init();
    uiConfirm.Init();
    uiDetail.Init();
    uiTemplate.Init();
    uiLog.Init();
  }
}
