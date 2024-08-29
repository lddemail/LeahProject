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
    RefreshUI();
  }
  public void RefreshUI()
  {
    TabContract tabC = data as TabContract;
    m_selectBtn.selected = false;
    for (int i = 0; i < AppConfig.mainTitles.Count; i++)
    {
      string title = AppConfig.mainTitles[i];
      object val = tabC.GetFieldVal(title);
      GTextField gText = GetChild(title) as GTextField;
      gText.text = val.ToString();
    }
  }

  public bool isSelect
  {
    get { return m_selectBtn.selected; }
  }

}