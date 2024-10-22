using UnityEngine;
using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.IO;


#region demo
//创建名为sqlite4unity的数据库
//sql = new SQLiteHelper("data source=" + Application.dataPath + "/game.db");

//创建名为table1的数据表
//sql.CreateTable("table1", new string[] { "ID", "Name", "Age", "Email" }, new string[] { "INTEGER", "TEXT", "INTEGER", "TEXT" });

//插入两条数据
//sql.InsertValues("table1", new string[] { "'1'", "'张三'", "'22'", "'Zhang3@163.com'" });
//sql.InsertValues("table1", new string[] { "'2'", "'李四'", "'25'", "'Li4@163.com'" });

//更新数据。将Name="张三"的记录中的Name改为"Zhang3"
//sql.UpdateValues("table1", new string[] { "Name" }, new string[] { "'Zhang3'" }, "Name", "=", "'张三'");

//插入3条数据
//sql.InsertValues("table1", new string[] { "3", "'王五'", "25", "'Wang5@163.com'" });
//sql.InsertValues("table1", new string[] { "4", "'王五'", "26", "'Wang5@163.com'" });
//sql.InsertValues("table1", new string[] { "5", "'王五'", "27", "'Wang5@163.com'" });

//删除Name="王五"且Age=26的记录,DeleteValuesOR方法相似
//sql.DeleteValuesAND("table1", new string[] { "Name", "Age" }, new string[] { "=", "=" }, new string[] { "'王五'", "'26'" });

//读取整张表
//SqliteDataReader reader = sql.ReadFullTable("table1");
//while (reader.Read())
//{
//  //读取ID
//  Debug.Log(reader.GetInt32(reader.GetOrdinal("ID")));
//  //读取Name
//  Debug.Log(reader.GetString(reader.GetOrdinal("Name")));
//  //读取Age
//  Debug.Log(reader.GetInt32(reader.GetOrdinal("Age")));
//  //读取Email
//  Debug.Log(reader.GetString(reader.GetOrdinal("Email")));
//}

//读取数据表中Age>=25的全部记录的ID和Name
//reader = sql.ReadTable("table1", new string[] { "ID", "Name" }, new string[] { "Age" }, new string[] { ">=" }, new string[] { "'25'" });
//while (reader.Read())
//{
//  //读取ID
//  Debug.Log(reader.GetInt32(reader.GetOrdinal("ID")));
//  //读取Name
//  Debug.Log(reader.GetString(reader.GetOrdinal("Name")));
//}

//自己定义SQL,删除数据表中全部Name="王五"的记录
//sql.ExecuteQuery("DELETE FROM table1 WHERE NAME='王五'");

//关闭数据库连接
//sql.CloseConnection();


//****************************************************************demo2

//创建表
//string[] colNames = new string[] { "name", "password" };
//string[] colTypes = new string[] { "string", "string" };
//CreateTable("user", colNames, colTypes);

//使用泛型创建数据表
//CreateTable<T4>();

// 根据条件查找特定的字段
//foreach (var item in SelectData("user", new string[] { "name" }, new string[] { "password", "123456" }))
//{
//    Debug.Log(item);
//}

//更新数据
//UpdataData("user", new string[] {"name", "yyy"}, new string[] { "name" ,"wxy" });

// 删除数据
//DeleteValues("user", new string[] { "name","wxyqq" });


//查询数据
//foreach (var item in GetDataBySqlQuery("T4", new string[] { "name" }))
//{
//    Debug.Log(item);
//}
//foreach (var item in GetDataBySqlQuery("user",new string[] { "name" }))
//{
//  Debug.Log(item);
//}

//插入数据
//string[] values = new string[] { "3", "33", "333" };
//InsertValues("T4", values);

//使用泛型插入对象
//T4 t = new T4(2, "22", "222");
//Insert<T4>(t);


#endregion


public class SQLiteHelper
{
  /// <summary>
  /// 数据库连接定义
  /// </summary>
  private SqliteConnection dbConnection;

  /// <summary>
  /// SQL命令定义
  /// </summary>
  private SqliteCommand dbCommand;

  /// <summary>
  /// 数据读取定义
  /// </summary>
  private SqliteDataReader dataReader;

  /// <summary>
  /// 构造函数   
  /// </summary>
  /// <param name="connectionString">数据库连接字符串</param>
  public SQLiteHelper(string connectionString)
  {
    try
    {
      if(CheckDBExists(connectionString))
      {
        Debug.Log("Open db:" + connectionString);
        connectionString = "data source=" + connectionString;
        //构造数据库连接
        dbConnection = new SqliteConnection(connectionString);
        //打开数据库
        dbConnection.Open();
      }
    }
    catch (Exception e)
    {
      Debug.Log(e.ToString());
    }
  }

  /// <summary>
  /// 执行SQL命令
  /// </summary>
  /// <returns>The query.</returns>
  /// <param name="queryString">SQL命令字符串</param>
  public SqliteDataReader ExecuteQuery(string queryString)
  {
    dbCommand = dbConnection.CreateCommand();
    dbCommand.CommandText = queryString;
    dataReader = dbCommand.ExecuteReader();
    return dataReader;
  }

  /// <summary>
  /// 执行SQL命令返回是否成功
  /// </summary>
  /// <returns>The query.</returns>
  /// <param name="queryString">SQL命令字符串</param>
  public bool ExecuteNonQuery(string queryString)
  {
    dbCommand = dbConnection.CreateCommand();
    dbCommand.CommandText = queryString;
    // 执行插入操作
    int rowsAffected = dbCommand.ExecuteNonQuery();
    // 判断是否插入成功
    return rowsAffected > 0;
  }

  /// <summary>
  /// 插入专用
  /// </summary>
  /// <param name="queryString"></param>
  /// <returns></returns>
  public bool InsertExecuteNonQuery(string queryString,out long lastId)
  {
    dbCommand = dbConnection.CreateCommand();
    dbCommand.CommandText = queryString;
    // 执行插入操作
    int rowsAffected = dbCommand.ExecuteNonQuery();
    // 判断是否插入成功
    bool isOK = rowsAffected > 0;
    lastId = -1;
    if (isOK)
    {
      //获取最近插入的行的主键值
      dbCommand.CommandText = "SELECT last_insert_rowid()";
      lastId = (long)dbCommand.ExecuteScalar();
    }
    return isOK;
  }

  /// <summary>
  /// 关闭数据库连接
  /// </summary>
  public void CloseConnection()
  {
    //销毁Command
    if (dbCommand != null)
    {
      dbCommand.Cancel();
    }
    dbCommand = null;

    //销毁Reader
    if (dataReader != null)
    {
      dataReader.Close();
    }
    dataReader = null;

    //销毁Connection
    if (dbConnection != null)
    {
      dbConnection.Close();
    }
    dbConnection = null;
  }

  /// <summary>
  /// 删除表
  /// </summary>
  /// <param name="tableName"></param>
  /// <returns></returns>
  public SqliteDataReader DeleteTable(string tableName)
  {
    string sql = "DROP TABLE " + tableName;
    return ExecuteQuery(sql);
  }

  /// <summary>
  /// 读取整张数据表
  /// </summary>
  /// <returns>The full table.</returns>
  /// <param name="tableName">数据表名称</param>
  public SqliteDataReader ReadFullTable(string tableName)
  {
    string queryString = "SELECT * FROM " + tableName;
    return ExecuteQuery(queryString);
  }

  /// <summary>
  /// 向指定数据表中插入数据
  /// </summary>
  /// <returns>The values.</returns>
  /// <param name="tableName">数据表名称</param>
  /// <param name="values">插入的数值</param>
  public SqliteDataReader InsertValues(string tableName, string[] values)
  {
    //获取数据表中字段数目
    int fieldCount = ReadFullTable(tableName).FieldCount;
    //当插入的数据长度不等于字段数目时引发异常
    if (values.Length != fieldCount)
    {
      throw new SqliteException("values.Length!=fieldCount");
    }

    string queryString = "INSERT INTO " + tableName + " VALUES (" + values[0];
    for (int i = 1; i < values.Length; i++)
    {
      queryString += ", " + values[i];
    }
    queryString += " )";
    Debug.Log($"插入数据:{queryString}");
    return ExecuteQuery(queryString);
  }

  /// <summary>
  /// 插入数据
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="t"></param>
  /// <returns></returns>
  public bool Insert<T>(T t,string key,out long lastId)
  {
    var type = typeof(T);
    var fields = type.GetFields();
    string sql = "INSERT INTO " + type.Name;
    //string sql = "INSERT INTO " + tabName + " values (";

    sql += " (";
    foreach (var field in fields)
    {
      //通过反射得到对象的值
      if (!string.IsNullOrEmpty(key) && field.Name != key)
      {
        sql += $"{field.Name},";
      }
    }
    sql = sql.TrimEnd(',') + ")";

    sql += " values (";
    foreach (var field in fields)
    {
      //通过反射得到对象的值
      if (!string.IsNullOrEmpty(key) && field.Name != key)
      {
        sql += $"'{type.GetField(field.Name).GetValue(t)}',";
      }
    }
    sql = sql.TrimEnd(',') + ");";

    bool isOk = InsertExecuteNonQuery(sql, out lastId);
    string log = $"插入数据:lastId:{lastId}  {sql}";
    UILog.AddLog(log);
    return isOk;
  }


  /// <summary>
  /// 更新指定数据表内的数据
  /// </summary>
  /// <returns>The values.</returns>
  /// <param name="tableName">数据表名称</param>
  /// <param name="colNames">字段名</param>
  /// <param name="colValues">字段名相应的数据</param>
  /// <param name="key">关键字</param>
  /// <param name="value">关键字相应的值</param>
  public bool UpdateValues(string tableName, string[] colNames, string[] colValues, string key, string operation, string value)
  {
    //当字段名称和字段数值不正确应时引发异常
    if (colNames.Length != colValues.Length)
    {
      throw new SqliteException("colNames.Length!=colValues.Length");
    }

    string queryString = "UPDATE " + tableName + " SET " + colNames[0] + "=" + colValues[0];
    for (int i = 1; i < colValues.Length; i++)
    {
      queryString += ", " + colNames[i] + "=" + colValues[i];
    }
    queryString += " WHERE " + key + operation + value;
    Debug.Log($"更新数据:{queryString}");
    return ExecuteNonQuery(queryString);
    //return null;
  }

  /// <summary>
  /// 更新数据
  /// </summary>
  /// <param name="tableName"></param>
  /// <param name="values">需要修改的数据</param>
  /// <param name="conditions">修改的条件</param>
  /// <returns></returns>
  public bool UpdateValues(string tableName, string[] values, string[] conditions)
  {
    string sql = "update " + tableName + " set ";
    for (int i = 0; i < values.Length - 1; i += 2)
    {
      sql += values[i] + "='" + values[i + 1] + "',";
    }
    sql = sql.TrimEnd(',') + " where (";
    for (int i = 0; i < conditions.Length - 1; i += 2)
    {
      sql += conditions[i] + "='" + conditions[i + 1] + "' and ";
    }
    sql = sql.Substring(0, sql.Length - 4) + ");";
    Debug.Log($"更新数据:{sql}");
    return ExecuteNonQuery(sql);
  }

  /// <summary>
  /// 删除指定数据表内的数据
  /// </summary>
  /// <returns>The values.</returns>
  /// <param name="tableName">数据表名称</param>
  /// <param name="colNames">字段名</param>
  /// <param name="colValues">字段名相应的数据</param>
  public SqliteDataReader DeleteValuesOR(string tableName, string[] colNames, string[] operations, string[] colValues)
  {
    //当字段名称和字段数值不正确应时引发异常
    if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
    {
      throw new SqliteException("colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
    }

    string queryString = "DELETE FROM " + tableName + " WHERE " + colNames[0] + operations[0] + colValues[0];
    for (int i = 1; i < colValues.Length; i++)
    {
      queryString += "OR " + colNames[i] + operations[0] + colValues[i];
    }
    return ExecuteQuery(queryString);
  }

  /// <summary>
  /// 删除指定数据表内的数据
  /// </summary>
  /// <returns>The values.</returns>
  /// <param name="tableName">数据表名称</param>
  /// <param name="colNames">字段名</param>
  /// <param name="colValues">字段名相应的数据</param>
  public SqliteDataReader DeleteValuesAND(string tableName, string[] colNames, string[] operations, string[] colValues)
  {
    //当字段名称和字段数值不正确应时引发异常
    if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
    {
      throw new SqliteException("colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
    }

    string queryString = "DELETE FROM " + tableName + " WHERE " + colNames[0] + operations[0] + colValues[0];
    for (int i = 1; i < colValues.Length; i++)
    {
      queryString += " AND " + colNames[i] + operations[i] + colValues[i];
    }
    return ExecuteQuery(queryString);
  }

  /// <summary>
  /// 删除数据
  /// </summary>
  /// <param name="tableName"></param>
  /// <param name="conditions">查询条件</param>
  /// <returns></returns>
  public bool DeleteValues(string tableName, string[] conditions)
  {
    string sql = "delete from " + tableName + " where (";
    for (int i = 0; i < conditions.Length - 1; i += 2)
    {
      sql += conditions[i] + "='" + conditions[i + 1] + "' and ";
    }
    sql = sql.Substring(0, sql.Length - 4) + ");";
    Debug.Log($"删除数据:{sql}");
    return ExecuteNonQuery(sql);
  }

  /// <summary>
  /// 创建数据表
  /// </summary> +
  /// <returns>The table.</returns>
  /// <param name="tableName">数据表名</param>
  /// <param name="colNames">字段名</param>
  /// <param name="colTypes">字段名类型</param>
  public SqliteDataReader CreateTable(string tableName, string[] colNames, string[] colTypes)
  {
    string queryString = "CREATE TABLE " + tableName + "( " + colNames[0] + " " + colTypes[0];
    for (int i = 1; i < colNames.Length; i++)
    {
      queryString += ", " + colNames[i] + " " + colTypes[i];
    }
    queryString += "  ) ";
    return ExecuteQuery(queryString);
  }

  /// <summary>
  /// 创建表(使用泛型)
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public void CreateTable<T>(string key="")
  {
    var type = typeof(T);
    string sql = string.Format("create Table {0}( ", type.Name);

    //这里如果T是私有对象GetFields返回null
    var fields = type.GetFields();
    for (int i = 0; i < fields.Length; i++)
    {
      sql += " " + fields[i].Name + " " + CS2DB(fields[i].FieldType);
      if(!string.IsNullOrEmpty(key) && fields[i].Name == key)
      {
        sql += " PRIMARY KEY AUTOINCREMENT";
        Debug.Log($"设置主键:{key}");
      }
      sql += ",";
    }

    //这里可以支持私有对象
    //var fields = type.GetProperties();
    //for (int i = 0; i < fields.Length; i++)
    //{
    //  sql += string.Format(" {0} {1},", fields[i].Name, CS2DB(fields[i].PropertyType));
    //}

    sql = sql.TrimEnd(',') + ")";
    Debug.Log($"[CreateTable] sql={sql}");
    ExecuteQuery(sql);
  }

  /// <summary>
  /// CS转化为DB类别
  /// </summary>
  /// <param name="type">c#中字段的类别</param>
  /// <returns></returns>
  string CS2DB(Type type)
  {
    string result = "TEXT";
    if (type == typeof(int) || type == typeof(long))
    {
      result = "INTEGER";
    }
    else if (type == typeof(string))
    {
      result = "TEXT";
    }
    else if (type == typeof(double) || type == typeof(float))
    {
      result = "REAL";
    }
    else if (type == typeof(DateTime))
    {
      result = "DATETIME";
    }
    else if (type == typeof(byte[]))
    {
      result = "BLOB";
    }
    Debug.Log($"[CS2DB] {type.ToString()} => {result}");
    return result;
  }

  /// <summary>
  /// Reads the table.
  /// </summary>
  /// <returns>The table.</returns>
  /// <param name="tableName">Table name.</param>
  /// <param name="items">Items.</param>
  /// <param name="colNames">Col names.</param>
  /// <param name="operations">Operations.</param>
  /// <param name="colValues">Col values.</param>
  public SqliteDataReader ReadTable(string tableName, string[] items, string[] colNames, string[] operations, string[] colValues)
  {
    string queryString = "SELECT " + items[0];
    for (int i = 1; i < items.Length; i++)
    {
      queryString += ", " + items[i];
    }
    queryString += " FROM " + tableName + " WHERE " + colNames[0] + " " + operations[0] + " " + colValues[0];
    for (int i = 0; i < colNames.Length; i++)
    {
      queryString += " AND " + colNames[i] + " " + operations[i] + " " + colValues[0] + " ";
    }
    return ExecuteQuery(queryString);
  }

  /// <summary>
  /// 查询数据
  /// </summary>
  /// <param name="tableName">数据表名</param>
  /// <param name="values">需要查询的数据</param>
  /// <param name="fields">查询的条件</param>
  /// <returns></returns>
  public SqliteDataReader SelectData(string tableName, string[] values, string[] fields)
  {
    string sql = "select " + values[0];
    for (int i = 1; i < values.Length; i++)
    {
      sql += " , " + values[i];
    }
    sql += " from " + tableName + " where( ";
    for (int i = 0; i < fields.Length - 1; i += 2)
    {
      sql += fields[i] + " =' " + fields[i + 1] + " 'and ";
    }
    sql = sql.Substring(0, sql.Length - 4) + ");";
    return ExecuteQuery(sql);


    //用于查看打印
    //List<string> list = new List<string>();
    //reader = ExecuteQuery(sql);

    //for (int i = 0; i < reader.FieldCount; i++)
    //{
    //    object obj = reader.GetValue(i);
    //    list.Add(obj.ToString());
    //}
    //return list;
  }
  /// <summary>
  /// 查询数据
  /// </summary>
  /// <param name="tableName">表名</param>
  /// <param name="fields">需要查找数据</param>
  /// <returns></returns>
  public List<String> GetDataBySqlQuery(string tableName, string[] fields)
  {
    //string queryString = "select " + fields[0];
    //for (int i = 1; i < fields.Length; i++)
    //{
    //    queryString += " , " + fields[i];
    //}
    //queryString += " from " + tableName;
    //return ExecuteQuery(queryString);

    List<string> list = new List<string>();
    string queryString = "SELECT * FROM " + tableName;
    dataReader = ExecuteQuery(queryString);
    while (dataReader.Read())
    {
      for (int i = 0; i < dataReader.FieldCount; i++)
      {
        object obj = dataReader.GetValue(i);
        list.Add(obj.ToString());
      }
    }
    return list;
  }

  /// <summary>
  /// 新增字段
  /// </summary>
  /// <param name="tableName"></param>
  /// <param name="fields"></param>
  public void AddColumn<T>(string tableName, string columnName)
  {
    string columnType = CS2DB(typeof(T));
    string sql = $"ALTER TABLE {tableName} ADD COLUMN {columnName} {columnType}";
    Debug.Log($"[AddColumn] sql={sql}");
    ExecuteQuery(sql);
  }

  /// <summary>
  /// 查询表是否存在
  /// </summary>
  /// <param name="tabName"></param>
  /// <returns></returns>
  public bool CheckTabExists(string tabName)
  {
    bool tableExists = false;
    string sql = $"SELECT Count(*) FROM sqlite_master WHERE type='table' AND name='{tabName}';";
    dbCommand = dbConnection.CreateCommand();
    dbCommand.CommandText = sql;
    long count = (long)dbCommand.ExecuteScalar();
    tableExists = (count > 0);
    Debug.Log($"[查询表是否存在] sql={sql}   tableExists:{tableExists}");
    return tableExists;
  }

  /// <summary>
  /// 检查数据库是否存在
  /// </summary>
  /// <returns></returns>
  public bool CheckDBExists(string path)
  {

    bool res = false;
    if(File.Exists(path))
    {
      res = true;
    }
    UILog.AddLog($"CheckDBExists:{path} isHave:{res}");
    return res;
  }
}
