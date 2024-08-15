using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using FairyGUI;

public class AppStart : MonoBehaviour
{
    private void Awake()
    {
        // 注册代码页提供程序
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;

        AppConfig.Init();
        AppUtil.Init();
        UIRoot.FguiBinder();


    }
    void Start()
    {
        UIRoot.ins.Init();
        AppData.Init();

        StartCoroutine(RunApp());
    }

    private IEnumerator RunApp()
    {
        yield return null;
       UIRoot.ins.uiMain.Show(null);
    }

    void Update()
    {
        
    }
}
