using OfficeOpenXml;
using SFB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using static ExcelSheet;

#region demo
//var sheet = new ExcelSheet("test");
//sheet.Load("test");
//sheet[0, 0] = "1"; // 第一行第一列赋值为 1
//sheet[1, 2] = "2"; // 第二行第三列赋值为 2
//sheet.Save("test", "Sheet1", ExcelSheet.FileFormat.Csv);

//存:
//private ExcelSheet _sheet = new ExcelSheet();
//_sheet[0, 0] = "Hello World";
//_sheet[1, 2] = "123";
//_sheet.Save("test");
//取
//_sheet.Load("test");
//for (int i = 0; i < _sheet.RowCount; i++)
//{
//  for (int j = 0; j < _sheet.ColCount; j++)
//  {
//    var value = _sheet[i, j];
//    if (string.IsNullOrEmpty(value)) continue;
//    Debug.Log($"Sheet[{i}, {j}]: {value}");
//  }
//}
#endregion


public class ExcelHelper
{
    public static ExcelSheet ImportExcel()
    {
      var extensions = new[] {
        new ExtensionFilter("Excel", "xlsx" ),
      };

      string[] paths = StandaloneFileBrowser.OpenFilePanel("Load Excel", "", extensions, false);
      ExcelSheet es = null;
      if (paths.Length != 0)
      {
          es = new ExcelSheet();
          es.Load(paths[0], "合同清单");
      }
     return es;
  }

  public static bool SaveToExcel(List<TabContract> list)
  {
    if (list == null || list.Count == 0) return false;

    string time = DateTime.Now.ToString("yyyy_MM_dd");
    string defName = $"LP_{time}";
    string path = StandaloneFileBrowser.SaveFilePanel("导出Excel","", defName, "xlsx");
    if (string.IsNullOrEmpty(path)) return false;

    Debug.Log(path);
    List<ObjectVal> objs;
    ExcelSheet _sheet = new ExcelSheet();
    for (int i = 0; i < list.Count; i++)
    {
      objs = list[i].GetExportObjs();
      for (int k = 0; k < objs.Count; k++)
      {
        if (i == 0)
        {
          _sheet[0, k] = objs[k].name;
        }
        _sheet[i + 1, k] = objs[k].val.ToString();
      }
    }
    _sheet.Save(path, "合同清单");

    return true;
  }
}

/// <summary>
/// Excel 文件存储和读取器
/// </summary>
public partial class ExcelSheet
{
  private int _rowCount = 0; // 最大行数
  private int _colCount = 0; // 最大列数
  public int RowCount { get => _rowCount; }
  public int ColCount { get => _colCount; }
  private Dictionary<Index, string> _sheetDic = new Dictionary<Index, string>(); // 缓存当前数据的字典
  public struct Index
  {
    public int Row,Col;
    public Index(int row, int col)
    {
      Row = row;
      Col = col;
    }
  }

  /// <summary>
  /// 获取面向对象的数据格式
  /// </summary>
  /// <returns></returns>
  public Dictionary<int,List<ObjVal>> GetObjVal()
  {
    int index = 0;
    Dictionary<int, List<ObjVal>> res = new Dictionary<int, List<ObjVal>>();
    for (int r = 0; r < RowCount; r++)
    {
      for (int c = 0; c < ColCount; c++)
      {
        if(r >0)
        {
          index = r + 1;
          if (!res.ContainsKey(index))
          {
            res.Add(index, new List<ObjVal>());
          }
          res[index].Add(new ObjVal(this[0, c], this[r, c]));
        }
      }
    }
    return res;
  }

  public struct ObjVal
  {
    public string name;
    public string val;
    public ObjVal(string _name,string _val)
    {
      name = _name;
      val = _val;
    }
  }


  public ExcelSheet() { }
  public string this[int row, int col]
  {
    get
    {
      // 越界检查
      if (row >= _rowCount || row < 0)
        Debug.LogError($"ExcelSheet: Row {row} out of range!");
      if (col >= _colCount || col < 0)
        Debug.LogError($"ExcelSheet: Column {col} out of range!");

      // 不存在结果，则返回空字符串
      return _sheetDic.GetValueOrDefault(new Index(row, col), "");
    }
    set
    {
      _sheetDic[new Index(row, col)] = value;

      // 记录最大行数和列数
      if (row >= _rowCount) _rowCount = row + 1;
      if (col >= _colCount) _colCount = col + 1;
    }
  }

  /// <summary>
  /// 存储 Excel 文件
  /// </summary>
  /// <param name="fullPath">文件路径</param>
  /// <param name="sheetName">表名，如果没有指定表名，则使用文件名。若使用 csv 格式，则忽略此参数</param>
  public void Save(string fullPath, string sheetName = null)
  {
    var directory =  Path.GetDirectoryName(fullPath);
    if (!Directory.Exists(directory))
    { // 如果文件所在的目录不存在，则先创建目录
      Directory.CreateDirectory(directory);
    }

    if (fullPath.EndsWith(".xlsx"))
    {
      SaveAsXlsx(fullPath, sheetName);
    }
    else if (fullPath.EndsWith(".csv"))
    {
      SaveAsCsv(fullPath);
    }
    else
    {
      throw new ArgumentOutOfRangeException(fullPath);
    }

    Debug.Log($"ExcelSheet: Save sheet \"{fullPath}::{sheetName}\" successfully:{RowCount},{ColCount}");
  }

  /// <summary>
  /// 读取 Excel 文件
  /// </summary>
  /// <param name="fullPath">文件路径</param>
  /// <param name="sheetName">表名，如果没有指定表名，则使用文件名</param>
  public void Load(string fullPath, string sheetName = null)
  {
    // 清空当前数据
    Clear();
    if (!File.Exists(fullPath))
    { // 不存在文件，则报错
      Debug.LogError($"ExcelSheet: Can't find path \"{fullPath}\".");
      return;
    }
    if (fullPath.EndsWith(".xlsx"))
    {
      LoadFromXlsx(fullPath, sheetName);
    }
    else if (fullPath.EndsWith(".csv"))
    {
      LoadFromCsv(fullPath);
    }
    else
    {
      throw new ArgumentOutOfRangeException(fullPath);
    }
    Debug.Log($"ExcelSheet: Load sheet \"{fullPath}::{sheetName}\" successfully:{RowCount},{ColCount}");
  }
  private void SaveAsXlsx(string fullPath, string sheetName)
  {
    sheetName ??= Path.GetFileNameWithoutExtension(fullPath); // 如果没有指定表名，则使用文件名
    var directory = Path.GetDirectoryName(fullPath);
    if (!Directory.Exists(directory))
    { // 如果文件所在的目录不存在，则先创建目录
      Directory.CreateDirectory(directory);
    }

    var fileInfo = new FileInfo(fullPath);
    using var package = new ExcelPackage(fileInfo);

    if (!File.Exists(fullPath) ||                         // 不存在 Excel
        package.Workbook.Worksheets[sheetName] == null)
    { // 或者没有表，则添加表
      package.Workbook.Worksheets.Add(sheetName);       // 创建表时，Excel 文件也会被创建
    }

    var sheet = package.Workbook.Worksheets[sheetName];

    var cells = sheet.Cells;
    cells.Clear(); // 先清空数据

    foreach (var pair in _sheetDic)
    {
      var i = pair.Key.Row;
      var j = pair.Key.Col;
      cells[i + 1, j + 1].Value = pair.Value;
    }

    package.Save(); // 保存文件
  }

  private void SaveAsCsv(string fullPath)
  {
    using FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write);

    Index idx = new Index(0, 0);
    for (int i = 0; i < _rowCount; i++)
    {
      idx.Row = i;
      idx.Col = 0;

      // 写入第一个 value
      var value = _sheetDic.GetValueOrDefault(idx, "");
      if (!string.IsNullOrEmpty(value))
        fs.Write(Encoding.UTF8.GetBytes(value));

      // 写入后续 value，需要添加 ","
      for (int j = 1; j < _colCount; j++)
      {
        idx.Col = j;
        value = "," + _sheetDic.GetValueOrDefault(idx, "");
        fs.Write(Encoding.UTF8.GetBytes(value));
      }

      // 写入 "\n"
      fs.Write(Encoding.UTF8.GetBytes("\n"));
    }
  }

  private void LoadFromXlsx(string fullPath, string sheetName)
  {
    sheetName ??= Path.GetFileNameWithoutExtension(fullPath); // 如果没有指定表名，则使用文件名
    var fileInfo = new FileInfo(fullPath);
    using var package = new ExcelPackage(fileInfo);
    var sheet = package.Workbook.Worksheets[sheetName];
    if (sheet == null)
    { // 不存在表，则报错
      Debug.LogError($"ExcelSheet: Can't find sheet \"{sheetName}\" in file \"{fullPath}\"");
      return;
    }

    _rowCount = sheet.Dimension.Rows;
    _colCount = sheet.Dimension.Columns;

    ExcelRange cells = sheet.Cells;
    for (int i = 0; i < _rowCount; i++)
    {
      for (int j = 0; j < _colCount; j++)
      {
        var cell = cells[i + 1, j + 1];
        if(cell.Value != null)
        {
          var value = cell.Value.ToString();
          if (string.IsNullOrEmpty(value)) continue; // 有数据才记录
          _sheetDic.Add(new Index(i, j), value);
        }
      }
    }
  }

  private void LoadFromCsv(string fullPath)
  {
    // 读取文件
    string[] lines = File.ReadAllLines(fullPath); // 读取所有行
    for (int i = 0; i < lines.Length; i++)
    {
      string[] line = lines[i].Split(','); // 读取一行，逗号分割
      for (int j = 0; j < line.Length; j++)
      {
        if (line[j] != "") // 有数据才记录
          _sheetDic.Add(new Index(i, j), line[j]);
      }

      // 更新最大行数和列数
      _colCount = Mathf.Max(_colCount, line.Length);
      _rowCount = i + 1;
    }
  }

  public void Clear()
  {
    _sheetDic.Clear();
    _rowCount = 0;
    _colCount = 0;
  }
}
