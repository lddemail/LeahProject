using Basics;
using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

/// <summary>
/// 模版
/// </summary>
public class UITemplate : UIBase
{
  private bool isSave = false;
  private string currTempType = "";
  private Dictionary<string, List<GComponent>> _itemDic = new Dictionary<string, List<GComponent>>();

  PopupMenu itemPop = new PopupMenu();

  public UITemplate(Transform tf)
  {
    ui = UIRoot.ins.uiMain.UIPanel.m_UITemplate;
  }

  public UI_UITemplate UIPanel
  {
    get { return ui as UI_UITemplate; }
  }

  public override void Init()
  {
    UIPanel.m_BtnClose.onClick.Add(BtnCloseHandler);
    UIPanel.m_BtnAdd.onClick.Add(BtnAddHandler);
  }

  private void MainItemRightClick(EventContext context)
  {
    GComponent obj = context.sender as GComponent;
    itemPop.ClearItems();
    itemPop.AddItem(AppConfig.Delete, () => {

      string tips = "";
      int id = -1;
      switch (currTempType)
      {
        case AppConfig.HotelRelevanceTemplateName:
          tips = (obj.data as HotelRelevanceTempData).t_hotelName;
          id = (obj.data as HotelRelevanceTempData).t_id;
          break;
      }
      UIRoot.ins.uiConfirm.Show($"确定要删除:id:{id} {tips}吗?", () =>
      {
        RemoveItem(obj);
        AppData.RemoveTemp(currTempType, id);
        isSave = true;
      });
    });
    itemPop.Show();
  }

  private void BtnCloseHandler(EventContext context)
  {
    Hide();
  }
  private void BtnAddHandler(EventContext context)
  {
    // 使用 LINQ 查询最大 id 的 Data
    var maxIdData = AppData.allHotelRelevanceTempData.OrderByDescending(data => data.t_id).FirstOrDefault();
    HotelRelevanceTempData hrtd = new HotelRelevanceTempData();
    hrtd.t_id = maxIdData.t_id + 1;
    hrtd.t_hotelName = "酒店名";
    hrtd.t_group = "集团";
    hrtd.t_brand = "品牌";
    hrtd.t_a_contract = "甲方";
    hrtd.t_province = "省";
    hrtd.t_city = "市";
    AppData.AddTemp(AppConfig.HotelRelevanceTemplateName, hrtd);
    AddHotelRelevanceTempListItem().SetData(hrtd);
    UIPanel.m_HotelRelevanceTempList.ScrollToView(AppData.allHotelRelevanceTempData.Count-1,true);
    isSave = true;
  }
  public override void Show(object obj = null)
  {
    isSave = false;
    UIPanel.visible = true;
    currTempType = "";
    ToggleTemp(AppConfig.HotelRelevanceTemplateName);
  }

  /// <summary>
  /// 切换模版
  /// </summary>
  private void ToggleTemp(string tempType)
  {
    if (currTempType == tempType) return;

    currTempType = tempType;

    UIPanel.m_PaymentTempList.visible = currTempType == AppConfig.PaymentTemplateName;
    UIPanel.m_SignedTempList.visible = currTempType == AppConfig.SignedTemplateName;
    UIPanel.m_HotelRelevanceTempList.visible = currTempType == AppConfig.HotelRelevanceTemplateName;

    switch (currTempType)
    {
      case AppConfig.HotelRelevanceTemplateName:
        foreach(HotelRelevanceTempData hrtd in AppData.allHotelRelevanceTempData)
        {
          AddHotelRelevanceTempListItem().SetData(hrtd);
        }
        break;
    }
  }

  /// <summary>
  /// 添加酒店关联模版Item
  /// </summary>
  /// <returns></returns>
  private UI_HotelRelevanceTempListItemExt AddHotelRelevanceTempListItem()
  {
    UI_HotelRelevanceTempListItemExt item = UIPanel.m_HotelRelevanceTempList.GetFromPool(UI_HotelRelevanceTempListItemExt.URL) as UI_HotelRelevanceTempListItemExt;
    AddOrRemItemDic(AppConfig.HotelRelevanceTemplateName, item, true);
    UIPanel.m_HotelRelevanceTempList.AddChild(item);
    item.onRightClick.Set(MainItemRightClick);
    item.SetChangeCallBack(ChangeCallBack);
    return item;
  }

  private void ChangeCallBack()
  {
    isSave = true;
  }

  private void AddOrRemItemDic(string type, GComponent item, bool isAdd)
  {
    if (!_itemDic.ContainsKey(type)) _itemDic.Add(type, new List<GComponent>());

    if (isAdd)
    {
      if (!_itemDic[type].Contains(item)) _itemDic[type].Add(item);
    }
    else
    {
      if (_itemDic[type].Contains(item)) _itemDic[type].Remove(item);
    }
  }
  private void RemoveItem(GComponent obj)
  {
    AddOrRemItemDic(currTempType, obj, false);

    switch (currTempType)
    {
      case AppConfig.HotelRelevanceTemplateName:
        UIPanel.m_HotelRelevanceTempList.RemoveChildToPool(obj);
        break;
    }
  }

  private void RefreshItemUI()
  {
    //GObject[] gobs = UIPanel.m_DetailList.GetChildren();
    //foreach (GObject gob in gobs)
    //{
    //  MethodInfo RefreshUI = gob.GetType().GetMethod(AppConfig.RefreshUI);
    //  if (RefreshUI != null)
    //  {
    //    RefreshUI.Invoke(gob, null);
    //  }
    //}
  }

  public override void RefreshUI()
  {
  }

  public override void Hide()
  {
    if(isSave)
    {
      //从新保存模版
      AppData.SaveAllTemp();
      //从新读取模版
      AppData.ReadAllTemplates();
      UIRoot.ins.uiTips.Show($"模版刷新成功");
    }
    UIPanel.m_HotelRelevanceTempList.RemoveChildrenToPool();
    UIPanel.visible = false;
  }
}