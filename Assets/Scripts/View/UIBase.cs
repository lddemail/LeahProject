using FairyGUI;
using System;
using UnityEngine;
public class UIBase:MonoBehaviour
{
    public GComponent ui;

    public virtual void Init()
    {

    }


    public virtual void Show(object obj=null)
    {
      if (ui != null) ui.visible = true;
    }

    public virtual void Hide()
    {
      if (ui != null) ui.visible = false;
    }

    public virtual void RefreshUI()
    {
    
    }
}
