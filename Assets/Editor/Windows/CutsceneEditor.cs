using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Core;
using System;
using System.Linq;
using UnityEngine.UIElements;

public class CutsceneEditor : EditorWindow
{
    private static Cutscene windowScene;

    public static void ShowWindow(Cutscene scene)
    {
        windowScene = scene;
        GetWindow<CutsceneEditor>();
    }

    private void OnEnable()
    {
        VisualTreeAsset original = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/Windows/CutsceneEditor.uxml");
        TemplateContainer treeAsset = original.CloneTree();
        rootVisualElement.Add(treeAsset);

        StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/Windows/CutsceneEditor.uss");
        Debug.Log(styleSheet);
        rootVisualElement.styleSheets.Add(styleSheet);

        scene = windowScene;
        commands = FindAllSceneCommands();
        
        RenderWindow();
    }

    private Cutscene scene;
    private List<ICutsceneCommand> commands;
    private ListView commandList;
    private ICutsceneCommand selectedCommand => (ICutsceneCommand)commandList.selectedItem;


    private void RenderWindow()
    {
        DisplayCommands();
        DisplaySceneContents();
    }

    private void DisplayCommands()
    {
        Box commandListBox = rootVisualElement.Query<Box>("command-list-box").First();
        commandList = rootVisualElement.Query<ListView>("command-list").First();
        Button addButton = rootVisualElement.Query<Button>("add-button").First();

        commandList.makeItem = () => new Label();
        commandList.bindItem = (element, i) => (element as Label).text = commands[i].ToString();

        commandList.itemsSource = commands;
        commandList.itemHeight = 16;
        commandList.selectionType = SelectionType.Single;

        commandList.Refresh();

        addButton.clicked += () =>
        {
            if (selectedCommand != null)
                scene.AddCommand(selectedCommand);

            DisplaySceneContents();
            EditorUtility.SetDirty(scene);
        };
    }

    private void DisplaySceneContents()
    {
        Box cutsceneArea = rootVisualElement.Query<Box>("scene").First();
        cutsceneArea.Clear();

        foreach(ICutsceneCommand command in scene.Commands)
        {
            VisualElement renderedCommand = RenderCommand(command);
            cutsceneArea.Add(renderedCommand);
        }
    }

    private VisualElement RenderCommand(ICutsceneCommand command)
    {
        VisualElement container = new VisualElement();

        Label label = new Label();
        label.text = command.ToString();
        label.AddToClassList("command-header");
        container.Add(label);

        Label label2 = new Label();
        label2.text = "Test";
        container.Add(label2);

        return container;
    }

    private List<ICutsceneCommand> FindAllSceneCommands()
    {
        var type = typeof(ICutsceneCommand);

        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(assembly => type.IsAssignableFrom(assembly) && !assembly.IsInterface)
            .Select(type => Activator.CreateInstance(type) as ICutsceneCommand)
            .ToList();
    }
}
