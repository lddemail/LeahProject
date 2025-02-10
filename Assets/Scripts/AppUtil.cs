using FairyGUI;
using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using UnityEngine;

public class AppUtil
{
  public static SQLiteHelper db;
  private static StreamWriter logWriter;
  public static void Init()
  {
    db = new SQLiteHelper(AppConfig.GetDBPath());

    //if(!Application.isEditor)
    //{
    //  logWriter = new StreamWriter(AppConfig.GetLogPath(), true);
    //  logWriter.AutoFlush = true;
    //  logWriter.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} version:[{AppConfig.version}]");
    //}
  }
  /// <summary>
  /// 删除数据文件
  /// </summary>
  public static void DelDB()
  {
    string path = AppConfig.GetDBPath();
    if(File.Exists(path))
    {
      File.Delete(path);
      Debug.Log("删除数据库:"+ path);
    }
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

      FieldInfo[] fields = data.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
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
      FieldInfo[] fields = data.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
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
    FieldInfo[] fields = t.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
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
    try
    {
      bool isOldType = dateString.Contains("/");
      string[] ary = dateString.Split("/");
      if (isOldType && ary.Length > 0)
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
      string log = $"{dateString} 数据不符合规范";
      UILog.Log(log);
      UIRoot.ins.uiTips.Show(log);
      throw new Exception(log);
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
      string log = $"{dateString} 数据不符合规范";
      UILog.Log(log);
      UIRoot.ins.uiTips.Show(log);
      throw new Exception(log);
    }
    return (int)unixTimestamp;
  }

  public static string TimeToString(int timestamp)
  {
    if (timestamp <= 0) return "0";
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
    //if (logWriter == null) return;
    //logWriter.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} [{log}]");
  }

  public static int GetIndexByList(List<string> list,string val)
  {
    int index = list.FindIndex(x => x == val);
    if (index < 0) index = 0;
    return index;
  }

  public static bool GetInputLab_SearchEnabled(string fieldName)
  {
    switch (fieldName)
    {
      case AppConfig.t_hotelName:
        return true;
      default:
        return false;
    }
  }

  public static bool GetInputLabEnabled(string fieldName)
  {
    switch (fieldName)
    {
      case AppConfig.t_interiorNo:
      case AppConfig.t_contractNo:
        return true;
      default:
        return false;
    }
  }

  public static string GetUBBColorStr(string color, string str)
  {
    return $"[color={color}]{str}[/color]";
  }

  /// <summary>
  /// 转成十六进制颜色
  /// </summary>
  /// <param name="color"></param>
  /// <param name="str"></param>
  /// <returns></returns>
  public static string GetUBBColorStr(Color color,string str)
  {
    // 将 Color 结构体转换为 0-255 范围内的 RGB 值
    int r = Mathf.RoundToInt(color.r * 255);
    int g = Mathf.RoundToInt(color.g * 255);
    int b = Mathf.RoundToInt(color.b * 255);
    // 转换为十六进制字符串并格式化为 UBB 语法
    return $"[color=#{r:X2}{g:X2}{b:X2}]{str}[/color]";
  }
  public static string GetColorStrByType(EmProductType type,string str)
  {
    switch (type)
    {
      case EmProductType.Expire:
        return GetUBBColorStr(Color.red, str);
      case EmProductType.Warning:
        return GetUBBColorStr(Color.yellow, str);
      default:
        return str;
    }
  }

  /// <summary>
  /// 写入txt
  /// </summary>
  /// <param name="list"></param>
  public static void WriteToTxt(string name,List<string> list)
  {
    if (list == null) return;

    List<string> saveList = new List<string>();
    foreach(string str in list)
    {
      if(!string.IsNullOrEmpty(str))
      {
        string _str = str.Trim();
        if (_str != "0" && !saveList.Contains(_str))
        {
          saveList.Add(_str);
        }
      }
    }
    string targetPath = AppConfig.GetTemplatePath(name);
    File.WriteAllLines(targetPath, saveList.ToArray());
  }

  /// <summary>
  /// 读取txt
  /// </summary>
  /// <param name="name"></param>
  /// <returns></returns>
  public static List<string> ReadFromTxt(string name)
  {
    List<string> res = new List<string>();
    string targetPath = AppConfig.GetTemplatePath(name);
    if (File.Exists(targetPath))
    {
      string[] lines = File.ReadAllLines(targetPath);
      if (lines != null && lines.Length > 0)
      {
        foreach (string line in lines)
        {
          if(!string.IsNullOrEmpty(line))
          {
            string _line = line.Trim();
            if(!res.Contains(_line)) res.Add(_line);
          }
        }
      }
    }
    return res;
  }

  /// <summary>
  /// 按照前2个汉字相同的规则排序
  /// </summary>
  /// <param name="x"></param>
  /// <param name="y"></param>
  /// <returns></returns>
  public static int CompareFirstTwoCharacters(string x, string y)
  {
    if (string.IsNullOrEmpty(x) || string.IsNullOrEmpty(y))
    {
      return string.Compare(x, y, StringComparison.Ordinal);
    }

    // 获取前两个字符
    string firstTwoCharsX = x.Length >= 2 ? x.Substring(0, 2) : x;
    string firstTwoCharsY = y.Length >= 2 ? y.Substring(0, 2) : y;

    // 先比较前两个字符
    int result = string.Compare(firstTwoCharsX, firstTwoCharsY, StringComparison.Ordinal);
    if (result != 0)
    {
      return result;
    }

    // 如果前两个字符相同，再比较整个字符串
    return string.Compare(x, y, StringComparison.Ordinal);
  }


  private static GTextField _textField = new GTextField();
  /// <summary>
  /// 根据字符串长度计算文本宽度
  /// </summary>
  /// <returns></returns>
  public static float GetTextWidthByStr(string str,int fontSize=22)
  {
    _textField.textFormat.size = fontSize;
    _textField.text = str;
    return _textField.textWidth;
  }


  public static void AppExit()
  {
    Debug.Log("APP退出");
    if (db != null) db.CloseConnection();
  }
  
  /// <summary>
  /// 压缩
  /// </summary>
  /// <param name="folderPath"></param>
  /// <param name="zipPath"></param>
  public static void CompressFolder(string folderPath, string zipPath)
  {
    if (Directory.Exists(folderPath))
    {
      // 删除已存在的 zip 文件，以防止异常
      if (File.Exists(zipPath))
      {
        File.Delete(zipPath);
      }

      // 压缩文件夹
      ZipFile.CreateFromDirectory(folderPath, zipPath);
      Debug.Log($"Folder '{folderPath}' has been compressed to '{zipPath}'");
    }
    else
    {
      Debug.LogError($"The folder '{folderPath}' does not exist.");
    }
  }

  /// <summary>
  /// 解压缩到目录
  /// </summary>
  public static void UnCompressFolder(string zipPath, string extractDir)
  {
    UILog.Log($"解压缩到目录:{zipPath}  {extractDir}");
    // 如果目标目录不存在，则创建
    if (!Directory.Exists(extractDir))
    {
      Directory.CreateDirectory(extractDir);
    }
    // 使用临时目录来避免直接覆盖
    string tempDirectory = Path.Combine(extractDir, "temp");
    if (Directory.Exists(tempDirectory))
    {
      Directory.Delete(tempDirectory, true);
    }
    Directory.CreateDirectory(tempDirectory);

    // 解压到临时目录
    ZipFile.ExtractToDirectory(zipPath, tempDirectory);

    // 移动文件并覆盖
    foreach (string file in Directory.GetFiles(tempDirectory, "*", SearchOption.AllDirectories))
    {
      string relativePath = file.Substring(tempDirectory.Length + 1);
      string destinationPath = Path.Combine(extractDir, relativePath);

      // 创建目录结构
      string destinationDir = Path.GetDirectoryName(destinationPath);
      if (!Directory.Exists(destinationDir))
      {
        Directory.CreateDirectory(destinationDir);
      }

      // 移动文件并覆盖
      File.Copy(file, destinationPath, true);
    }

    // 删除临时目录
    Directory.Delete(tempDirectory, true);
  }

  /// <summary>
  /// copy
  /// </summary>
  /// <param name="sourceDir"></param>
  /// <param name="destinationDir"></param>
  public static void CopyDirectory(string sourceDir, string destinationDir)
  {
    try
    {
      if (Directory.Exists(destinationDir))
      {
        Directory.Delete(destinationDir, true);
      }
      // Create the destination directory if it doesn't exist
      Directory.CreateDirectory(destinationDir);

      // Copy all the files
      foreach (var file in Directory.GetFiles(sourceDir))
      {
        string destFile = Path.Combine(destinationDir, Path.GetFileName(file));
        File.Copy(file, destFile, true);
      }

      // Copy all the subdirectories
      foreach (var directory in Directory.GetDirectories(sourceDir))
      {
        string destDir = Path.Combine(destinationDir, Path.GetFileName(directory));
        CopyDirectory(directory, destDir);
      }
    }
    catch(Exception ex)
    {
      UILog.Log($"CopyDirectory:{ex.ToString()}");
    }
  }

  /// <summary>
  /// 获取格式化后的显示内容
  /// </summary>
  /// <param name="currTc"></param>
  /// <param name="fieldName"></param>
  public static string GetFormatVal(TabContract currTc ,string fieldName)
  {
    string resVal="";
    object _obj = null;
    HotelRelevanceTempData _hrd = null;
    switch (fieldName)
    {
      case AppConfig.t_province:
      case AppConfig.t_city:
      case AppConfig.t_brand:
      case AppConfig.t_group:
      case AppConfig.t_a_contract:
        _hrd = AppData.GetHotelRelevanceData(currTc);
        if (_hrd != null)
        {
          if (fieldName == AppConfig.t_province || fieldName == AppConfig.t_city)
          {
            resVal = _hrd.GetCityStr();
          }
          else
          {
            _obj = _hrd.GetFieldVal(fieldName);
            resVal = _obj == null ? "" : _obj.ToString();
          }
        }
        break;
      case AppConfig.t_totalDebt:
        object productsPrice = currTc.GetFieldVal(AppConfig.t_productsPrice);
        object totalAccount = currTc.GetFieldVal(AppConfig.t_totalAccount);
        float price = (float)productsPrice - (float)totalAccount;
        resVal = $"合同总额:{productsPrice} - 到账:{totalAccount} = 欠款:{price}";
        break;
      default:
        _obj = currTc.GetFieldVal(fieldName);
        resVal = _obj == null ? "" : _obj.ToString();
        break;
    }
    return resVal;
  }

  /// <summary>
  /// 打开目录
  /// </summary>
  public static void OpenFolder(string path)
  {
    if (!Directory.Exists(path))
    {
      Debug.LogError($"目录不存在:{path}");
      return;
    }
    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(path) { UseShellExecute = true });
  }
}