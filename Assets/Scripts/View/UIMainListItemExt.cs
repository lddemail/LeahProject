﻿using Basics;
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
        List<ProductData> pdList = tabC.GetProductList();
        string products = "";
        EmProductType ptype = EmProductType.None;
        foreach (ProductData pd in pdList)
        {
          products += pd.ToMainShowStr();
          if(pd.GetProductType(out int day) == EmProductType.Warning)
          {
            ptype = EmProductType.Warning;
          }
        }
        gText.text = products;
        if(ptype == EmProductType.Warning)
        {
          //反向替换酒店名字为警告颜色
          (GetChild(AppConfig.t_hotelName) as GTextField).text = AppUtil.GetColorStrByType(ptype,tabC.t_hotelName);
        }
      }
      else
      {
        object val = tabC.GetFieldVal(title);
        gText.text = val.ToString();
      }
    }
  }

  public bool isSelect
  {
    get { return m_selectBtn.selected; }
  }

}