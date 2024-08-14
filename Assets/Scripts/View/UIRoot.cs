using Basics;
using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIRoot : MonoBehaviour
{
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

  public void Init()
  {
    BasicsBinder.BindAll();
    uiMain.Init();
    uiTips.Init();
    uiConfirm.Init();
  }
}
