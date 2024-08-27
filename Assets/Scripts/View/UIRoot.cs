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
    UIObjectFactory.SetPackageItemExtension(UI_DetailItemLabel.URL, typeof(UIDetailItemLabelExt));
    UIObjectFactory.SetPackageItemExtension(UI_DetailItemTwoLabel.URL, typeof(UIDetailItemTwoLabelExt));
    UIObjectFactory.SetPackageItemExtension(UI_MainListItem.URL, typeof(UIMainListItemExt));
    UIObjectFactory.SetPackageItemExtension(UI_DetailItemProduct.URL, typeof(UIDetailItemProductExt));
    UIObjectFactory.SetPackageItemExtension(UI_DetailItemBarter.URL, typeof(UIDetailItemBarterExt));
    UIObjectFactory.SetPackageItemExtension(UI_DetailItemAccount.URL, typeof(UIDetailItemAccountExt));
    UIObjectFactory.SetPackageItemExtension(UI_DetailItemCity.URL, typeof(UIDetailItemCityExt));
    UIObjectFactory.SetPackageItemExtension(UI_DetailItemLine.URL, typeof(UIDetailItemLineExt));
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


  public void Init()
  {
    uiMain.Init();
    uiTips.Init();
    uiConfirm.Init();
    uiDetail.Init();
  }
}
