using Basics;
using FairyGUI.Utils;
using FairyGUI;
using System.Collections;
using UnityEngine;

public class UIDetailItemAccountExt : UI_DetailItemAccount
{

  public override void ConstructFromXML(XML xml)
  {
    base.ConstructFromXML(xml);

  }

  public void SetData(AccountData ad)
  {
    data = ad;
    m_InputLabBarter.text = ad.barter.ToString();
    m_InputLabTime.text = AppUtil.TimeToString(ad.time);
    m_InputLabRemark.text = ad.remark;
  }
}