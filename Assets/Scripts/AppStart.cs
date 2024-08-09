using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using FairyGUI;

public class AppStart : MonoBehaviour
{
    private void Awake()
    {
      AppUtil.Init();
    }
    void Start()
    {
      AppData.Init();
    TabContract con = new TabContract()
    {
      id = 2,
      pIndex = 2,
      name = "≤‚ ‘211",
      val = "≤‚ ‘val211"
    };
    //AppUtil.Update2DB<TabContract>(con);
    //con.Insert2DB();
    //AppUtil.db.AddColumn<string>(AppConfig.tab_ContractData, "val");
    //AppUtil.db.CreateTable<TabContract>();

    //AppUtil.ReadAll4DB<TabContract>(AppConfig.TabContract);
    //TabContract.ReadAll4DB();
    }

    void Update()
    {
        
    }
}
