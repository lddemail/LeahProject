using Basics;
using FairyGUI.Utils;
using FairyGUI;
using System.Collections;
using UnityEngine;
using System;

/// <summary>
/// 消费
/// </summary>
public class UIDetailItemBarterExt : UI_DetailItemBarter
{

  public override void ConstructFromXML(XML xml)
  {
    base.ConstructFromXML(xml);
    m_InputLabBarter.m_InputLab.onChanged.Set(OnChangeCallBack);
    m_InputLabTime.m_InputLab.onChanged.Set(OnChangeCallBack);
    m_InputLabRemark.m_InputLab.onChanged.Set(OnChangeCallBack);
  }
  private void OnChangeCallBack(EventContext context)
  {
    if (string.IsNullOrEmpty(m_InputLabBarter.m_InputLab.text))
    {
      m_InputLabBarter.m_InputLab.text = "0";
    }

    BarterData bd = data as BarterData;
    bd.barter = float.Parse(m_InputLabBarter.m_InputLab.text);
    bd.time = AppUtil.StringToTime(m_InputLabTime.m_InputLab.text);
    bd.remark = m_InputLabRemark.m_InputLab.text;

    _changeCallBack?.Invoke();
  }
  public void SetData(BarterData bd)
  {
    data = bd;

    m_InputLabBarter.m_Title.text = "消费金额";

    if (bd.time < 1) bd.time = AppUtil.GetNowUnixTime();

    RefreshUI();
  }

  public void RefreshUI()
  {
    BarterData bd = data as BarterData;
    m_InputLabBarter.m_InputLab.text = bd.barter.ToString();
    m_InputLabTime.m_InputLab.text = AppUtil.TimeToString(bd.time);
    m_InputLabRemark.m_InputLab.text = bd.remark;
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