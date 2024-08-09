using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using FairyGUI;
using Main;

public class AppStart : MonoBehaviour
{
    private void Awake()
    {
      AppConfig.Init();
      AppUtil.Init();
    }
    void Start()
    {
      AppData.Init();
      UIRoot.ins.Init();

    //AppUtil.Update2DB<TabContract>(con);
    //con.Insert2DB();
    //AppUtil.db.AddColumn<string>(AppConfig.tab_ContractData, "val");
    //AppUtil.db.CreateTable<TabContract>();

    //AppUtil.ReadAll4DB<TabContract>(AppConfig.TabContract);
    //TabContract.ReadAll4DB();

    StartCoroutine(RunApp());
  }

    private IEnumerator RunApp()
    {
       yield return null;

    }

    void Update()
    {
        
    }
}
