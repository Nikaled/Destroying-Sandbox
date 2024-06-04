using UnityEngine;
using UnityEditor;

public class EditModeFunctions : EditorWindow
{
    public BuildCellGenerator generator;

    [MenuItem("Window/EEEEEdit Mode Functions")]
    public static void ShowWindow()
    {
        GetWindow<EditModeFunctions>("Edit Mode Functions");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Run Function"))
        {
            FunctionToRun();
        }
    }

    private void FunctionToRun()
    {
        Debug.Log("The function ran.");
    }
}
