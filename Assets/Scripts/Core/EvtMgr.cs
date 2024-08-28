using UnityEditor;
using UnityEngine;
using FairyGUI;

public class EvtMgr : EventDispatcher
{



  private static EvtMgr ins;
  private static bool isInit = false;
  public static void Init()
  {
    if (isInit) return;

    isInit = true;
    ins = new EvtMgr();
  }

  /// <summary>
  /// 带参数
  /// </summary>
  /// <param name="strType"></param>
  /// <param name="callback"></param>
  public static void Add(string strType, EventCallback1 callback)
  {
    ins.AddEventListener(strType, callback);
  }

  /// <summary>
  /// 不带参数
  /// </summary>
  /// <param name="strType"></param>
  /// <param name="callback"></param>
  public static void Add(string strType, EventCallback0 callback)
  {
    ins.AddEventListener(strType, callback);
  }
  /// <summary>
  /// 
  /// </summary>
  /// <param name="strType"></param>
  /// <param name="callback"></param>
  public static void Remove(string strType, EventCallback1 callback)
  {
    ins.RemoveEventListener(strType, callback);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="strType"></param>
  /// <param name="callback"></param>
  public static void Remove(string strType, EventCallback0 callback)
  {
    ins.RemoveEventListener(strType, callback);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="strType"></param>
  /// <returns></returns>
  public static void Dispatch(string strType)
  {
    ins.DispatchEvent(strType);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="strType"></param>
  /// <param name="data"></param>
  /// <returns></returns>
  public static void Dispatch(string strType, object data)
  {
    ins.DispatchEvent(strType, data);
  }

}