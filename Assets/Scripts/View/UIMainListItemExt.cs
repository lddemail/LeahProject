using Basics;
using FairyGUI;
using FairyGUI.Utils;
using System.Collections.Generic;
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
      GTextField gText = GetChild(title) as GTextField;
      if(title == AppConfig.t_products)
      {
        string products = "";
        ProductData pd = tabC.GetRecentlyPD();
        if(pd != null)
        {
          EmProductType ptype = pd.GetProductType(out int day);
          if (ptype == EmProductType.Warning || ptype == EmProductType.Expire)
          {
            //反向替换酒店名字为警告颜色
            (GetChild(AppConfig.t_hotelName) as GTextField).text = AppUtil.GetColorStrByType(ptype, tabC.t_hotelName);
          }
          products = pd.ToMainShowStr();
          int pdCount = tabC.GetProductList().Count;
          if (pdCount > 1)
          {
            products += $"(还有{pdCount}个产品未显示)";
          }
        }
        gText.text = products;
      }
      else if(title == AppConfig.t_totalDebt)
      {
        object val = tabC.GetFieldVal(title);
        float fval = float.Parse(val.ToString());
        if (fval < 0) fval = 0;
        gText.text = fval.ToString();
      }
      else
      {
        object val = tabC.GetFieldVal(title);
        gText.text = val.ToString();
      }
      SetTooltips(title, gText.text);
    }
  }

  private void SetTooltips(string title,string str)
  {
    GObject gob = GetChild(title+"_bg");
    if(gob != null)
    {
      gob.tooltips = str;
    }
  }

  public bool isSelect
  {
    get { return m_selectBtn.selected; }
  }

}