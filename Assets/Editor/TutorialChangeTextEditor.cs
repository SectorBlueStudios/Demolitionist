using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TutorialChangeText))]
public class TutorialChangeTextEditor : Editor
{    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TutorialChangeText myScript = (TutorialChangeText)target;
        if (GUILayout.Button("Create Numbers"))
        {
            myScript.InitiateChangeNames();
        }

        if (GUILayout.Button("Clear Numbers"))
        {
            myScript.InitiateChangeNamesClear();
        }
    }
}
