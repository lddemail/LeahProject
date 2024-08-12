using FairyGUI;
using Main;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    {
      if (_uiMain == null)
      {
        _uiMain = new UIMain();
        _uiMain.ui = transform.Find("MainPanel").GetComponent<UIPanel>().ui;
      }
      return _uiMain;
    }
  }


  public void Init()
  {
      MainBinder.BindAll();
      uiMain.Init();
  }
}
