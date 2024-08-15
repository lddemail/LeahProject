using Basics;
using FairyGUI;
using FairyGUI.Utils;
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
      object val = tabC.GetPropertyValue($"t_{title}");
      string valStr = val.ToString();
      switch (i)
      {
        case 0:
          m_t0.text = valStr;
          break;
        case 1:
          m_t1.text = valStr;
          break;
        case 2:
          m_t2.text = valStr;
          break;
        case 3:
          m_t3.text = valStr;
          break;
        case 4:
          m_t4.text = valStr;
          break;
        case 5:
          m_t5.text = valStr;
          break;
        case 6:
          m_t6.text = valStr;
          break;
        case 7:
          m_t7.text = valStr;
          break;
        case 8:
          m_t8.text = valStr;
          break;
      }
    }
  }

}