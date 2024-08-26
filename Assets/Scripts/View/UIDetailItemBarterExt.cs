using Basics;
using FairyGUI.Utils;
using FairyGUI;
using System.Collections;
using UnityEngine;
using System;

public class UIDetailItemBarterExt : UI_DetailItemBarter
{

  public override void ConstructFromXML(XML xml)
  {
    base.ConstructFromXML(xml);
    m_InputLabBarter.onChanged.Set(OnChangeCallBack);
  }
  private void OnChangeCallBack(EventContext context)
  {
    if (string.IsNullOrEmpty(m_InputLabBarter.text))
    {
      m_InputLabBarter.text = "0";
    }

    BarterData bd = data as BarterData;
    bd.barter = float.Parse(m_InputLabBarter.text);
    bd.time = AppUtil.StringToTime(m_InputLabTime.text);
    bd.remark = m_InputLabRemark.text;

    _changeCallBack?.Invoke();
  }
  public void SetData(BarterData bd)
  {
    data = bd;
    RefreshUI();
  }

  public void RefreshUI()
  {
    BarterData bd = data as BarterData;
    m_InputLabBarter.text = bd.barter.ToString();
    m_InputLabTime.text = AppUtil.TimeToString(bd.time);
    m_InputLabRemark.text = bd.remark;
  }

  private Action _changeCallBack;
  public void SetChangeCallBack(Action changeCallBack)
  {
    _changeCallBack = changeCallBack;
  }

  public BarterData GetBarterData()
  {
    BarterData res = data as BarterData;
    return res;
  }
}