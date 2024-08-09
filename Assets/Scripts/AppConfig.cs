using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AppConfig
{
  public static string productName = "LP";
  public static string version = "1.0";
  public static string GetDBPath()
  {
    string[] ary = version.Split(".");
    string name = $"data_{ary[0]}.db";
    string path = Path.Combine(Application.persistentDataPath, "DB", name);
    return path;
  }

  public static string GetProductName()
  {
    return $"{productName}_{version}";
  }
}
