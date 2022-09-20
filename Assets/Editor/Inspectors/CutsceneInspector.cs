using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Core;

[CustomEditor(typeof(Cutscene))]
public class CutsceneInspector : Editor
{
    private Cutscene scene;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        scene = target as Cutscene;

        if (GUILayout.Button("Open Editor"))
        {
            CutsceneEditor.ShowWindow(scene);
        }
    }
}
