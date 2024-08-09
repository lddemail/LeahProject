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
  public void Init()
  {
    MainBinder.BindAll();
    UI_MainPanel.CreateInstance().Init();
  }
}
