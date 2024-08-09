using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class PostBuildProcessor :IPostprocessBuildWithReport
{
  public int callbackOrder { get => 0; }

  public void OnPostprocessBuild(BuildReport report)
  {
  
  }

}
