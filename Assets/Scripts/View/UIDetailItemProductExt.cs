using Basics;
using FairyGUI.Utils;
using FairyGUI;
using System.Collections;
using UnityEngine;

public class UIDetailItemProductExt : UI_DetailItemProduct
{

  public override void ConstructFromXML(XML xml)
  {
    base.ConstructFromXML(xml);

  }

  public void SetData(ProductData pdd)
  {
    data = pdd;
    m_InputLabName.text = pdd.name;
    m_InputLabPrice.text = pdd.price.ToString();
    m_InputLabfTime.text = AppUtil.TimeToString(pdd.fTime);
    m_InputLabtTime.text = AppUtil.TimeToString(pdd.tTime);
    m_InputLabRemark.text = pdd.remark;
  }
}