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
  public static void Init()
  {
    db = new SQLiteHelper(AppConfig.GetDBPath());

    AddFguiPackage("Main");
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
  /// 更新数据到DB
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="t"></param>
  public static void Update2DB<T>(T t)
  {
    List<string> keys = new List<string>();
    List<string> vals = new List<string>();
    FieldInfo[] fields = t.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
    object id = 0;
    foreach (FieldInfo field in fields)
    {
      string name = field.Name;
      object value = field.GetValue(t);
      if(name == "id")
      {
        id = value;
      }
      keys.Add(name);
      vals.Add($"'{value}'");

      Debug.Log($"Update2DB: {name}:{value}");
    }
    db.UpdateValues(t.GetType().Name, keys.ToArray(), vals.ToArray(), "id", "=", $"{id}");
  }

  /// <summary>
  /// 插入数据到DB
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="t"></param>
  public static void Insert2DB<T>(T t)
  {
    db.Insert<T>(t);
  }

  public static void Delete2DB<T>(T t)
  {
    TabBase tb = t as TabBase;
    db.DeleteValues(t.GetType().Name, new string[] { "id", $"{tb.t_id}" });
  }

  public static int StringToTime(string dateString)
  {
    if (string.IsNullOrEmpty(dateString)) return 0;

    if(dateString == "0") return 0;

    string format = "yyyy/MM/dd";
    dateString = dateString.Replace("0:00:00","").Trim();
    string[] ary = dateString.Split("/");
    try
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
    catch(Exception ex)
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

}