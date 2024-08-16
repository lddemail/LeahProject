using Basics;
using FairyGUI;
using FairyGUI.Utils;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
public class UIMainListItemExt : UI_MainListItem
{
  public override void ConstructFromXML(XML xml)
  {
    base.ConstructFromXML(xml);

  }

  public void SetData(TabContract tabC)
  {
    data = tabC;

    for (int i = 0; i < AppConfig.mainTitles.Count; i++)
    {
      string title = AppConfig.mainTitles[i];
      object val = tabC.GetPropertyValue(title);
      string valStr = val.ToString();
      GTextField gText = GetChild(title) as GTextField;
      gText.text = valStr;
    }
  }

}