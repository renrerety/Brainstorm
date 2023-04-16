using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AutomatedTesting))]
public class AutomatedTestingEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AutomatedTesting script = (AutomatedTesting)target;
        
        if (GUILayout.Button("Start automated testing"))
        {
            script.StartAutomatedTesting();
        }

        if (GUILayout.Button("Stop automated testing"))
        {
            script.isTesting = false;
        }
    }
}
