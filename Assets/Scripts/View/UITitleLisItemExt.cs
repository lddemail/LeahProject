using Basics;
using FairyGUI.Utils;
using UnityEditor;
using UnityEngine;

public class UITitleLisItemExt : UI_TitleLisItem
{
  public override void ConstructFromXML(XML xml)
  {
    base.ConstructFromXML(xml);

  }

  public void SetData(string name)
  {
    m_title.text = name;
  }
}