using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using FairyGUI;

public class AppStart : MonoBehaviour
{
    private void Awake()
    {
        // ע�����ҳ�ṩ����
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
