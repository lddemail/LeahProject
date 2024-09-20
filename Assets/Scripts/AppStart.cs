using System.Collections;
using UnityEngine;

public class AppStart : MonoBehaviour
{
    private void Awake()
    {
        // 注册代码页提供程序
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        Screen.SetResolution(1920, 1080, false); // false 表示窗口模式

        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;

        EventMgr.Init();
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

    private void OnDestroy()
    {
    AppUtil.AppExit();
    }
}
