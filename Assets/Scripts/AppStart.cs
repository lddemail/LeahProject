using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using FairyGUI;

public class AppStart : MonoBehaviour
{
    private void Awake()
    {
    Application.targetFrameRate = 60;
    QualitySettings.vSyncCount = 0;

    AppConfig.Init();
        AppUtil.Init();
 
    }
    void Start()
    {
        AppData.Init();
        UIRoot.ins.Init();
        StartCoroutine(RunApp());
    }

    private IEnumerator RunApp()
    {
        yield return null;
       UIRoot.ins.uiMain.Show();
    }

    void Update()
    {
        
    }
}
