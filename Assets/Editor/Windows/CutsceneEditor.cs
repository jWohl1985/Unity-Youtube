using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Core;
using System;
using System.Linq;

public class CutsceneEditor : EditorWindow
{
    private static Cutscene windowScene;

    public static void ShowWindow(Cutscene scene)
    {
        windowScene = scene;
        GetWindow<CutsceneEditor>();
    }

    private Cutscene scene;
    int tab = 0;


    public void OnGUI()
    {
        scene = windowScene;

        tab = GUILayout.Toolbar(tab, new string[] { "Scene", "Commands" });

        switch (tab)
        {
            case 0:
                RenderSceneCommands();
                break;
            case 1:
                RenderCommandList();
                break;
        }
    }

    private void RenderSceneCommands()
    {
        foreach(ICutsceneCommand command in scene.Commands)
        {
            GUILayout.Label(command.ToString());
        }
    }

    private void RenderCommandList()
    {
        IEnumerable<ICutsceneCommand> commands = FindAllSceneCommands();
        foreach(ICutsceneCommand command in commands)
        {
            if (GUILayout.Button(command.ToString()))
            {
                scene.AddCommand(command);
            }
        }
    }

    private IEnumerable<ICutsceneCommand> FindAllSceneCommands()
    {
        var type = typeof(ICutsceneCommand);

        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(assembly => type.IsAssignableFrom(assembly) && !assembly.IsInterface)
            .Select(type => Activator.CreateInstance(type) as ICutsceneCommand);
    }
}
