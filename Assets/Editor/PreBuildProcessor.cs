using System.Collections;
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
  }
}