using Basics;
using FairyGUI.Utils;
using FairyGUI;
using System.Collections;
using UnityEngine;

public class UIDetailItemBarterExt : UI_DetailItemBarter
{

  public override void ConstructFromXML(XML xml)
  {
    base.ConstructFromXML(xml);

  }

  public void SetData(BarterData bd)
  {
    data = bd;
    m_InputLabBarter.text = bd.barter.ToString();
    m_InputLabTime.text = AppUtil.TimeToString(bd.time);
    m_InputLabRemark.text = bd.remark;
  }
}