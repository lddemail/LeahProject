using FairyGUI;
using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

public class AppUtil
{
  public static SQLiteHelper db;
  private static StreamWriter logWriter;
  public static void Init()
  {
    db = new SQLiteHelper(AppConfig.GetDBPath());

    if(!Application.isEditor)
    {
      logWriter = new StreamWriter(AppConfig.GetLogPath(), true);
      logWriter.AutoFlush = true;
      logWriter.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} version:[{AppConfig.version}]");
    }
    //AddFguiPackage("Main");
  }

  private static void AddFguiPackage(string name)
  {
    UIPackage.AddPackage($"UI/{name}");
  }

  /// <summary>
  /// 从DB读取整张表
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="tabName"></param>
  /// <returns></returns>
  public static List<T> ReadAll4DB<T>() where T: new()
  {
    var Ttype = typeof(T);
    List<T> res = new List<T>();
    SqliteDataReader reader = db.ReadFullTable(Ttype.Name);
    while (reader.Read())
    {
      T data = new T();
      res.Add(data);

      FieldInfo[] fields = data.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
      foreach (FieldInfo field in fields)
      {
        string name = field.Name;
        Type type = field.FieldType;
        object value = null;
        if (type == typeof(int) || type == typeof(long))
        {
          value = reader.GetInt32(reader.GetOrdinal(name));
        }
        else if (type == typeof(double) || type == typeof(float))
        {
          value = reader.GetFloat(reader.GetOrdinal(name));
        }
        else
        {
          value = reader.GetString(reader.GetOrdinal(name));
        }
        if (value != null)
        {
          field.SetValue(data, value);
          //Debug.Log($"{name}: {value}");
        }
      }
    }
    return res;
  }

  /// <summary>
  /// 从DB读取覆盖本地
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="tabName"></param>
  /// <returns></returns>
  public static T Read4DBById<T>(int id) where T : new()
  {
    T data = new T();
    var Ttype = typeof(T);
    SqliteDataReader reader = db.SelectData(Ttype.Name, new string[] { "*" }, new string[] { AppConfig.tabKey, id.ToString() });
    while (reader.Read())
    {
      FieldInfo[] fields = data.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
      foreach (FieldInfo field in fields)
      {
        string name = field.Name;
        Type type = field.FieldType;
        object value = null;
        if (type == typeof(int) || type == typeof(long))
        {
          value = reader.GetInt32(reader.GetOrdinal(name));
        }
        else if (type == typeof(double) || type == typeof(float))
        {
          value = reader.GetFloat(reader.GetOrdinal(name));
        }
        else
        {
          value = reader.GetString(reader.GetOrdinal(name));
        }
        if (value != null)
        {
          field.SetValue(data, value);
          //Debug.Log($"{name}: {value}");
        }
      }
    }
    return data;
  }
  /// <summary>
  /// 更新数据到DB
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="t"></param>
  public static bool Update2DB<T>(T t)
  {
    List<string> keys = new List<string>();
    List<string> vals = new List<string>();
    FieldInfo[] fields = t.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
    object id = 0;
    foreach (FieldInfo field in fields)
    {
      string name = field.Name;
      object value = field.GetValue(t);
      if(name == AppConfig.tabKey)
      {
        id = value;
      }
      keys.Add(name);
      vals.Add($"'{value}'");

      Debug.Log($"Update2DB: {name}:{value}");
    }
    return db.UpdateValues(t.GetType().Name, keys.ToArray(), vals.ToArray(), AppConfig.tabKey, "=", $"{id}");
  }

  /// <summary>
  /// 插入数据到DB
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="t"></param>
  public static bool Insert2DB<T>(T t, string key,out long lastId)
  {
    return db.Insert<T>(t,key,out lastId);
  }

  public static bool Delete2DB<T>(T t)
  {
    TabContract tb = t as TabContract;
    return db.DeleteValues(t.GetType().Name, new string[] { AppConfig.tabKey, $"{tb.t_id}" });
  }

  public static int StringToTime(string dateString)
  {
    if (string.IsNullOrEmpty(dateString)) return 0;

    if(dateString == "0") return 0;

    string format = "yyyy-MM-dd";
    dateString = dateString.Replace("0:00:00","").Trim();
    string[] ary = dateString.Split("/");
    try
    {
      if (ary.Length > 0)
      {
        if (ary[1].Length == 1 && ary[2].Length == 1)
        {
          format = "yyyy/M/d";
        }
        else if (ary[1].Length == 1)
        {
          format = "yyyy/M/dd";
        }
        else if (ary[2].Length == 1)
        {
          format = "yyyy/MM/d";
        }
        else
        {
          format = "yyyy/MM/dd";
        }
      }
      else
      {
        ary = dateString.Split("-");
        if(ary.Length > 0)
        {
          format = "yyyy-MM-dd";
        }
      }
    }
    catch (Exception ex)
    {
      Debug.Log(dateString + ex.ToString());
    }

    DateTime dateTime;
    long unixTimestamp = 0;
    // 尝试解析字符串为 DateTime 对象
    if (DateTime.TryParseExact(dateString, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dateTime))
    {
      // 将 DateTime 转换为 Unix 时间戳
      unixTimestamp = ((DateTimeOffset)dateTime).ToUnixTimeSeconds();
      Debug.Log($"Unix Timestamp:{dateString}->{unixTimestamp}");
    }
    else
    {
      Debug.LogError($"error:Invalid date format:{dateString}");
    }
    return (int)unixTimestamp;
  }

  public static string TimeToString(int timestamp)
  {
    if (timestamp == 0) return "0";
    // 将时间戳转换为 DateTimeOffset
    DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timestamp);
    // 将 DateTimeOffset 转换为本地时间的 DateTime
    DateTime dateTime = dateTimeOffset.LocalDateTime;
    // 将 DateTime 转换为字符串
    string dateString = dateTime.ToString("yyyy-MM-dd");
    return dateString;
  }

  /// <summary>
  /// 获取当前系统时间时间戳秒
  /// </summary>
  /// <returns></returns>
  public static int GetNowUnixTime()
  {
    DateTime now = DateTime.Now;
    // 转换为 Unix 时间戳
    DateTimeOffset dateTimeOffset = new DateTimeOffset(now);
    long unixTimestamp = dateTimeOffset.ToUnixTimeSeconds();
    // 输出 Unix 时间戳
    //Debug.Log("当前时间的 Unix 时间戳: " + unixTimestamp);
    return (int)unixTimestamp;
  }

  /// <summary>
  /// 时间戳转成成天
  /// </summary>
  /// <returns></returns>
  public static int GetDayByUnixTime(int time)
  {
    int dayTime = 3600 * 24;
    if (time <= dayTime) return 1;

    float fTime = (float)time / dayTime;
    int res = (int)Math.Ceiling(fTime);
    return res;
  }
  /// <summary>
  /// 退出app
  /// </summary>
  public static void Quit()
  {
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
  }

  public static void AddLog(string log)
  {
    if (logWriter == null) return;
    logWriter.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} [{log}]");
  }

  public static int GetIndexByList(List<string> list,string val)
  {
    int index = list.FindIndex(x => x == val);
    if (index < 0) index = 0;
    return index;
  }

  public static bool GetInputLabEnabled(string fieldName)
  {
    switch (fieldName)
    {
      case AppConfig.t_productsPrice:
      case AppConfig.t_totalBarter:
      case AppConfig.t_totalAccount:
      case AppConfig.t_totalDebt:
        return false;
      default:
        return true;
    }
  }

}