using Basics;
using FairyGUI.Utils;
using FairyGUI;
using System.Collections;
using UnityEngine;
using System;

/// <summary>
/// 到账
/// </summary>
public class UIDetailItemAccountExt : UI_DetailItemAccount
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

    int index = m_InputLabBarter.m_InputLab.text.LastIndexOf(".");
    if (index == (m_InputLabBarter.m_InputLab.text.Length - 1))
    {
      return;
    }

    AccountData ad = data as AccountData;
    decimal.TryParse(m_InputLabBarter.m_InputLab.text,out ad.barter);
    ad.time = AppUtil.StringToTime(m_InputLabTime.m_InputLab.text);
    ad.remark = m_InputLabRemark.m_InputLab.text;

    _changeCallBack?.Invoke();
  }
  private Action _changeCallBack;
  public void SetChangeCallBack(Action changeCallBack)
  {
    _changeCallBack = changeCallBack;
  }

  public void SetData(AccountData ad)
  {
    data = ad;

    m_InputLabBarter.m_Title.text = "到账金额";

    if (ad.time < 1) ad.time = AppUtil.GetNowUnixTime();

    RefreshUI();
  }

  public void RefreshUI()
  {
    AccountData ad = data as AccountData;
    m_InputLabBarter.m_InputLab.text = ad.barter.ToString();
    m_InputLabTime.m_InputLab.text = AppUtil.TimeToString(ad.time);
    m_InputLabRemark.m_InputLab.text = ad.remark;
  }

  public AccountData GetAccountData()
  {
    AccountData res = data as AccountData;
    return res;
  }
}