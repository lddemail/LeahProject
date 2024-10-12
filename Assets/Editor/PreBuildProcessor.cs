using System.Collections;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class PreBuildProcessor : IPreprocessBuildWithReport
{
  public int callbackOrder { get => 0; }

  public void OnPreprocessBuild(BuildReport report)
  {
    PlayerSettings.productName = AppConfig.GetProductName();
    PlayerSettings.bundleVersion = AppConfig.version;

    SetInnoSetup();
  }

  private void SetInnoSetup()
  {
    string InnoSetupPath = Application.dataPath.Replace("Assets", "InnoSetup");
    string SetipPath = Path.Combine(InnoSetupPath, "Setip.iss");
    if(File.Exists(SetipPath))
    {
      string oldMyAppVersion="";
      string[] lines = File.ReadAllLines(SetipPath);
      foreach(string line in lines)
      {
        if(line.Contains("#define MyAppVersion"))
        {
          oldMyAppVersion = line;//"#define MyAppVersion \"1.7\"
          break;
        }
      }

      if(!string.IsNullOrEmpty(oldMyAppVersion))
      {
        string newMyAppVersion = $"#define MyAppVersion '{AppConfig.version}'";
        string val = File.ReadAllText(SetipPath);
        val = val.Replace(oldMyAppVersion, newMyAppVersion);
        File.WriteAllText(SetipPath,val);
      }
    }
  }
}