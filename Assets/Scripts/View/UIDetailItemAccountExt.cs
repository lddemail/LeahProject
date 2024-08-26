using Basics;
using FairyGUI.Utils;
using FairyGUI;
using System.Collections;
using UnityEngine;
using System;

public class UIDetailItemAccountExt : UI_DetailItemAccount
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

    AccountData ad = data as AccountData;
    ad.barter = float.Parse(m_InputLabBarter.text);
    ad.time = AppUtil.StringToTime(m_InputLabTime.text);
    ad.remark = m_InputLabRemark.text;

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
    RefreshUI();
  }

  public void RefreshUI()
  {
    AccountData ad = data as AccountData;
    m_InputLabBarter.text = ad.barter.ToString();
    m_InputLabTime.text = AppUtil.TimeToString(ad.time);
    m_InputLabRemark.text = ad.remark;
  }

  public AccountData GetAccountData()
  {
    AccountData res = data as AccountData;
    return res;
  }
}