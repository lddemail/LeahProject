using UnityEngine;
using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.IO;


#region demo
//������Ϊsqlite4unity�����ݿ�
//sql = new SQLiteHelper("data source=" + Application.dataPath + "/game.db");

//������Ϊtable1�����ݱ�
//sql.CreateTable("table1", new string[] { "ID", "Name", "Age", "Email" }, new string[] { "INTEGER", "TEXT", "INTEGER", "TEXT" });

//������������
//sql.InsertValues("table1", new string[] { "'1'", "'����'", "'22'", "'Zhang3@163.com'" });
//sql.InsertValues("table1", new string[] { "'2'", "'����'", "'25'", "'Li4@163.com'" });

//�������ݡ���Name="����"�ļ�¼�е�Name��Ϊ"Zhang3"
//sql.UpdateValues("table1", new string[] { "Name" }, new string[] { "'Zhang3'" }, "Name", "=", "'����'");

//����3������
//sql.InsertValues("table1", new string[] { "3", "'����'", "25", "'Wang5@163.com'" });
//sql.InsertValues("table1", new string[] { "4", "'����'", "26", "'Wang5@163.com'" });
//sql.InsertValues("table1", new string[] { "5", "'����'", "27", "'Wang5@163.com'" });

//ɾ��Name="����"��Age=26�ļ�¼,DeleteValuesOR��������
//sql.DeleteValuesAND("table1", new string[] { "Name", "Age" }, new string[] { "=", "=" }, new string[] { "'����'", "'26'" });

//��ȡ���ű�
//SqliteDataReader reader = sql.ReadFullTable("table1");
//while (reader.Read())
//{
//  //��ȡID
//  Debug.Log(reader.GetInt32(reader.GetOrdinal("ID")));
//  //��ȡName
//  Debug.Log(reader.GetString(reader.GetOrdinal("Name")));
//  //��ȡAge
//  Debug.Log(reader.GetInt32(reader.GetOrdinal("Age")));
//  //��ȡEmail
//  Debug.Log(reader.GetString(reader.GetOrdinal("Email")));
//}

//��ȡ���ݱ���Age>=25��ȫ����¼��ID��Name
//reader = sql.ReadTable("table1", new string[] { "ID", "Name" }, new string[] { "Age" }, new string[] { ">=" }, new string[] { "'25'" });
//while (reader.Read())
//{
//  //��ȡID
//  Debug.Log(reader.GetInt32(reader.GetOrdinal("ID")));
//  //��ȡName
//  Debug.Log(reader.GetString(reader.GetOrdinal("Name")));
//}

//�Լ�����SQL,ɾ�����ݱ���ȫ��Name="����"�ļ�¼
//sql.ExecuteQuery("DELETE FROM table1 WHERE NAME='����'");

//�ر����ݿ�����
//sql.CloseConnection();


//****************************************************************demo2

//������
//string[] colNames = new string[] { "name", "password" };
//string[] colTypes = new string[] { "string", "string" };
//CreateTable("user", colNames, colTypes);

//ʹ�÷��ʹ������ݱ�
//CreateTable<T4>();

// �������������ض����ֶ�
//foreach (var item in SelectData("user", new string[] { "name" }, new string[] { "password", "123456" }))
//{
//    Debug.Log(item);
//}

//��������
//UpdataData("user", new string[] {"name", "yyy"}, new string[] { "name" ,"wxy" });

// ɾ������
//DeleteValues("user", new string[] { "name","wxyqq" });


//��ѯ����
//foreach (var item in GetDataBySqlQuery("T4", new string[] { "name" }))
//{
//    Debug.Log(item);
//}
//foreach (var item in GetDataBySqlQuery("user",new string[] { "name" }))
//{
//  Debug.Log(item);
//}

//��������
//string[] values = new string[] { "3", "33", "333" };
//InsertValues("T4", values);

//ʹ�÷��Ͳ������
//T4 t = new T4(2, "22", "222");
//Insert<T4>(t);


#endregion


public class SQLiteHelper
{
  /// <summary>
  /// ���ݿ����Ӷ���
  /// </summary>
  private SqliteConnection dbConnection;

  /// <summary>
  /// SQL�����
  /// </summary>
  private SqliteCommand dbCommand;

  /// <summary>
  /// ���ݶ�ȡ����
  /// </summary>
  private SqliteDataReader dataReader;

  /// <summary>
  /// ���캯��   
  /// </summary>
  /// <param name="connectionString">���ݿ������ַ���</param>
  public SQLiteHelper(string connectionString)
  {
    try
    {
      if(CheckDBExists(connectionString))
      {
        Debug.Log("Open db:" + connectionString);
        connectionString = "data source=" + connectionString;
        //�������ݿ�����
        dbConnection = new SqliteConnection(connectionString);
        //�����ݿ�
        dbConnection.Open();
      }
    }
    catch (Exception e)
    {
      Debug.Log(e.ToString());
    }
  }

  /// <summary>
  /// ִ��SQL����
  /// </summary>
  /// <returns>The query.</returns>
  /// <param name="queryString">SQL�����ַ���</param>
  public SqliteDataReader ExecuteQuery(string queryString)
  {
    dbCommand = dbConnection.CreateCommand();
    dbCommand.CommandText = queryString;
    dataReader = dbCommand.ExecuteReader();
    return dataReader;
  }

  /// <summary>
  /// ִ��SQL������Ƿ�ɹ�
  /// </summary>
  /// <returns>The query.</returns>
  /// <param name="queryString">SQL�����ַ���</param>
  public bool ExecuteNonQuery(string queryString)
  {
    dbCommand = dbConnection.CreateCommand();
    dbCommand.CommandText = queryString;
    // ִ�в������
    int rowsAffected = dbCommand.ExecuteNonQuery();
    // �ж��Ƿ����ɹ�
    return rowsAffected > 0;
  }

  /// <summary>
  /// ����ר��
  /// </summary>
  /// <param name="queryString"></param>
  /// <returns></returns>
  public bool InsertExecuteNonQuery(string queryString,out long lastId)
  {
    dbCommand = dbConnection.CreateCommand();
    dbCommand.CommandText = queryString;
    // ִ�в������
    int rowsAffected = dbCommand.ExecuteNonQuery();
    // �ж��Ƿ����ɹ�
    bool isOK = rowsAffected > 0;
    lastId = -1;
    if (isOK)
    {
      //��ȡ���������е�����ֵ
      dbCommand.CommandText = "SELECT last_insert_rowid()";
      lastId = (long)dbCommand.ExecuteScalar();
    }
    return isOK;
  }

  /// <summary>
  /// �ر����ݿ�����
  /// </summary>
  public void CloseConnection()
  {
    //����Command
    if (dbCommand != null)
    {
      dbCommand.Cancel();
    }
    dbCommand = null;

    //����Reader
    if (dataReader != null)
    {
      dataReader.Close();
    }
    dataReader = null;

    //����Connection
    if (dbConnection != null)
    {
      dbConnection.Close();
    }
    dbConnection = null;
  }

  /// <summary>
  /// ɾ����
  /// </summary>
  /// <param name="tableName"></param>
  /// <returns></returns>
  public SqliteDataReader DeleteTable(string tableName)
  {
    string sql = "DROP TABLE " + tableName;
    return ExecuteQuery(sql);
  }

  /// <summary>
  /// ��ȡ�������ݱ�
  /// </summary>
  /// <returns>The full table.</returns>
  /// <param name="tableName">���ݱ�����</param>
  public SqliteDataReader ReadFullTable(string tableName)
  {
    string queryString = "SELECT * FROM " + tableName;
    return ExecuteQuery(queryString);
  }

  /// <summary>
  /// ��ָ�����ݱ��в�������
  /// </summary>
  /// <returns>The values.</returns>
  /// <param name="tableName">���ݱ�����</param>
  /// <param name="values">�������ֵ</param>
  public SqliteDataReader InsertValues(string tableName, string[] values)
  {
    //��ȡ���ݱ����ֶ���Ŀ
    int fieldCount = ReadFullTable(tableName).FieldCount;
    //����������ݳ��Ȳ������ֶ���Ŀʱ�����쳣
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
    Debug.Log($"��������:{queryString}");
    return ExecuteQuery(queryString);
  }

  /// <summary>
  /// ��������
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
      //ͨ������õ������ֵ
      if (!string.IsNullOrEmpty(key) && field.Name != key)
      {
        sql += $"{field.Name},";
      }
    }
    sql = sql.TrimEnd(',') + ")";

    sql += " values (";
    foreach (var field in fields)
    {
      //ͨ������õ������ֵ
      if (!string.IsNullOrEmpty(key) && field.Name != key)
      {
        sql += $"'{type.GetField(field.Name).GetValue(t)}',";
      }
    }
    sql = sql.TrimEnd(',') + ");";

    bool isOk = InsertExecuteNonQuery(sql, out lastId);
    string log = $"��������:lastId:{lastId}  {sql}";
    UILog.AddLog(log);
    return isOk;
  }


  /// <summary>
  /// ����ָ�����ݱ��ڵ�����
  /// </summary>
  /// <returns>The values.</returns>
  /// <param name="tableName">���ݱ�����</param>
  /// <param name="colNames">�ֶ���</param>
  /// <param name="colValues">�ֶ�����Ӧ������</param>
  /// <param name="key">�ؼ���</param>
  /// <param name="value">�ؼ�����Ӧ��ֵ</param>
  public bool UpdateValues(string tableName, string[] colNames, string[] colValues, string key, string operation, string value)
  {
    //���ֶ����ƺ��ֶ���ֵ����ȷӦʱ�����쳣
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
    Debug.Log($"��������:{queryString}");
    return ExecuteNonQuery(queryString);
    //return null;
  }

  /// <summary>
  /// ��������
  /// </summary>
  /// <param name="tableName"></param>
  /// <param name="values">��Ҫ�޸ĵ�����</param>
  /// <param name="conditions">�޸ĵ�����</param>
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
    Debug.Log($"��������:{sql}");
    return ExecuteNonQuery(sql);
  }

  /// <summary>
  /// ɾ��ָ�����ݱ��ڵ�����
  /// </summary>
  /// <returns>The values.</returns>
  /// <param name="tableName">���ݱ�����</param>
  /// <param name="colNames">�ֶ���</param>
  /// <param name="colValues">�ֶ�����Ӧ������</param>
  public SqliteDataReader DeleteValuesOR(string tableName, string[] colNames, string[] operations, string[] colValues)
  {
    //���ֶ����ƺ��ֶ���ֵ����ȷӦʱ�����쳣
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
  /// ɾ��ָ�����ݱ��ڵ�����
  /// </summary>
  /// <returns>The values.</returns>
  /// <param name="tableName">���ݱ�����</param>
  /// <param name="colNames">�ֶ���</param>
  /// <param name="colValues">�ֶ�����Ӧ������</param>
  public SqliteDataReader DeleteValuesAND(string tableName, string[] colNames, string[] operations, string[] colValues)
  {
    //���ֶ����ƺ��ֶ���ֵ����ȷӦʱ�����쳣
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
  /// ɾ������
  /// </summary>
  /// <param name="tableName"></param>
  /// <param name="conditions">��ѯ����</param>
  /// <returns></returns>
  public bool DeleteValues(string tableName, string[] conditions)
  {
    string sql = "delete from " + tableName + " where (";
    for (int i = 0; i < conditions.Length - 1; i += 2)
    {
      sql += conditions[i] + "='" + conditions[i + 1] + "' and ";
    }
    sql = sql.Substring(0, sql.Length - 4) + ");";
    Debug.Log($"ɾ������:{sql}");
    return ExecuteNonQuery(sql);
  }

  /// <summary>
  /// �������ݱ�
  /// </summary> +
  /// <returns>The table.</returns>
  /// <param name="tableName">���ݱ���</param>
  /// <param name="colNames">�ֶ���</param>
  /// <param name="colTypes">�ֶ�������</param>
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
  /// ������(ʹ�÷���)
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public void CreateTable<T>(string key="")
  {
    var type = typeof(T);
    string sql = string.Format("create Table {0}( ", type.Name);

    //�������T��˽�ж���GetFields����null
    var fields = type.GetFields();
    for (int i = 0; i < fields.Length; i++)
    {
      sql += " " + fields[i].Name + " " + CS2DB(fields[i].FieldType);
      if(!string.IsNullOrEmpty(key) && fields[i].Name == key)
      {
        sql += " PRIMARY KEY AUTOINCREMENT";
        Debug.Log($"��������:{key}");
      }
      sql += ",";
    }

    //�������֧��˽�ж���
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
  /// CSת��ΪDB���
  /// </summary>
  /// <param name="type">c#���ֶε����</param>
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
  /// ��ѯ����
  /// </summary>
  /// <param name="tableName">���ݱ���</param>
  /// <param name="values">��Ҫ��ѯ������</param>
  /// <param name="fields">��ѯ������</param>
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


    //���ڲ鿴��ӡ
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
  /// ��ѯ����
  /// </summary>
  /// <param name="tableName">����</param>
  /// <param name="fields">��Ҫ��������</param>
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
  /// �����ֶ�
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
  /// ��ѯ���Ƿ����
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
    Debug.Log($"[��ѯ���Ƿ����] sql={sql}   tableExists:{tableExists}");
    return tableExists;
  }

  /// <summary>
  /// ������ݿ��Ƿ����
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
