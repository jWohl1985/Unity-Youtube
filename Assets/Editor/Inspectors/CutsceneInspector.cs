using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Core;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;

[CustomEditor(typeof(Cutscene))]
public class CutsceneInspector : Editor
{
    private VisualElement container;
    private Action openEditor;

    public void OnEnable()
    {
        openEditor = () => CutsceneEditor.ShowWindow(target as Cutscene);
    }

    public override VisualElement CreateInspectorGUI()
    {
        container = new VisualElement();

        SerializedProperty property = serializedObject.FindProperty("trigger");
        PropertyField field = new PropertyField(property);
        container.Add(field);

        SerializedProperty oneTimeProperty = serializedObject.FindProperty("oneTimeOnly");
        PropertyField oneTimeField = new PropertyField(oneTimeProperty);
        container.Add(oneTimeField);

        Button button = new Button(openEditor) { text = "Open Editor" };
        container.Add(button);

        DisplaySceneCommands();

        return container;
    }

    private void DisplaySceneCommands()
    {
        foreach(ICutsceneCommand command in (target as Cutscene).Commands)
        {
            container.Add(new Label() { text = command.ToString() });
        }
    }
}
