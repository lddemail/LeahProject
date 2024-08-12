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
