﻿using FairyGUI;
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
    string dbPath = AppConfig.GetDBPath();
    string dir = Path.GetDirectoryName(dbPath);
    if(!Directory.Exists(dir))
    {
      Directory.CreateDirectory(dir);
    }
    db = new SQLiteHelper(dbPath);

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
    db.Insert<T>(t.GetType().Name, t);
  }

  public static void Delete2DB<T>(T t)
  {
    TabBase tb = t as TabBase;
    db.DeleteValues(t.GetType().Name, new string[] { "id", $"{tb.id}" });
  }

}