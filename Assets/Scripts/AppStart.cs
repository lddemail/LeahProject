using System.Collections;
using UnityEngine;

public class AppStart : MonoBehaviour
{
    private void Awake()
    {
        // ע�����ҳ�ṩ����
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        Screen.SetResolution(1920, 1080, false); // false ��ʾ����ģʽ

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
