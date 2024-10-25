using System.Collections;
using System.IO;
using UnityEngine;

public class AppStart : MonoBehaviour
{
    public static AppStart ins;
    private void Awake()
    {
        ins = this;
        // ע�����ҳ�ṩ����
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        Screen.SetResolution(1920, 1080, false); // false ��ʾ����ģʽ

        Application.targetFrameRate = 90;
        QualitySettings.vSyncCount = 0;


    string dataPath = AppConfig.GetDataPath();
    string[] files = Directory.GetFiles(dataPath);
    if(files == null || files.Length < 1)
    {
      string dataPath2 = AppConfig.GetOldDataPath();
      AppUtil.CopyDirectory(dataPath2, dataPath);
      Debug.Log($"Copy:{dataPath2}->{dataPath}");
    }

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
