using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Core;
using System;
using System.Linq;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class CutsceneEditor : EditorWindow
{
    private static Cutscene windowScene;

    public static void ShowWindow(Cutscene scene)
    {
        windowScene = scene;
        var window = GetWindow<CutsceneEditor>();
        window.titleContent = new GUIContent("Cutscene Editor");
        window.minSize = new Vector2(800, 600);
    }

    private void OnEnable()
    {
        // Find the UXML document and add its contents to this window's tree
        VisualTreeAsset original = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/Windows/CutsceneEditor.uxml");
        TemplateContainer treeAsset = original.CloneTree();
        rootVisualElement.Add(treeAsset);

        // Find the .uss stylesheet and set this window to use it
        StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/Windows/CutsceneEditor.uss");
        rootVisualElement.styleSheets.Add(styleSheet);

        // Grab the cutscene from the static property. There might be a better way to do this, but the problem is OnEnable()
        // runs as soon as the GetWindow finishes. Let me know if you find a better way!
        cutscene = windowScene;

        // Find all classes we have that implement ICutsceneCommand and store them in a list
        commands = FindAllSceneCommands();

        // Finds or sets up all of the VisualElements we're going to need
        InitializeWindow();

        // Renders the contents of the window
        RenderWindow();
    }

    // General private fields
    private Cutscene cutscene;
    private List<ICutsceneCommand> commands;

    // Visual elements for the command selection area
    private Box commandBox;
    private ListView commandList;
    private IntegerField commandPosition;
    private Button addButton;
    private HelpBox helpBox;

    // Visual elements for the cutscene commands area
    private ScrollView scrollView;

    private void InitializeWindow()
    {
        commandBox = rootVisualElement.Query<Box>("command-list-box").First();
        commandList = rootVisualElement.Query<ListView>("command-list").First();
        commandPosition = rootVisualElement.Query<IntegerField>("position").First();
        addButton = rootVisualElement.Query<Button>("add-button").First();
        helpBox = new HelpBox();
        helpBox.text = "Position is invalid!";

        scrollView = rootVisualElement.Query<ScrollView>("scene-info-scroll").First();

        // Defines what happens when the Add Command button is clicked
        addButton.clicked += () =>
        {
            // Add the new command at the position in the list input by the user. The CreateCommand() returns a new
            // instance of the selected command so that if we add the same command multiple times it is a separate
            // instance each time.
            if (commandList.selectedItem != null)
                cutscene.InsertCommand(commandPosition.value, CreateCommand((ICutsceneCommand)commandList.selectedItem));

            // Redraw the window since it's different now. Mark the cutscene object as needing updating
            RenderWindow();
            EditorUtility.SetDirty(cutscene);
        };

        
    }

    private void RenderWindow()
    {
        // Draw the contents of the left column
        DisplayCommands();
        // Draw the contents of the right column
        DisplaySceneContents();
    }

    private void DisplayCommands()
    {
        // Get rid of anything that was there before
        commandBox.Clear();
        
        // Create the ListView that holds all of the command names and add it to the Box VisualElement
        commandList.makeItem = () => new Label();
        commandList.bindItem = (element, i) => (element as Label).text = commands[i].ToString();
        commandList.itemsSource = commands;
        commandList.fixedItemHeight = 16;
        commandList.selectionType = SelectionType.Single;
        commandList.Rebuild();
        commandBox.Add(commandList);

        // Set the position = to the end of the cutscene.Commands list so that new commands go to the bottom by default
        // Add the IntegerField to the window
        commandPosition.value = cutscene.Commands.Count;
        commandBox.Add(commandPosition);

        // Make sure the position is valid and show a help message if not
        if (commandPosition.value < 0 || commandPosition.value > cutscene.Commands.Count)
        {
            commandBox.Add(helpBox);
        }
        else
        {
            commandBox.Add(addButton);
        }   
    }

    private void DisplaySceneContents()
    {
        // clear any previous contents
        scrollView.Clear();

        // Get the Cutscene as a serialized object, we need this to find the serialized properties for
        // each command in the cutscene.Commands list, and to give those properties something to bind to
        SerializedObject cutsceneSerializedObject = new SerializedObject(cutscene);

        // For each command in the list, generate the VisualElement, consisting of the header, the
        // buttons for deleting it and moving it around, and all of the serialized properties
        for(int i = 0; i < cutscene.Commands.Count; i++)
        {
            VisualElement commandContainer = new VisualElement();

            // Generate the header with the command name and delete/up/down buttons
            VisualElement header = GenerateHeaderRow(i);
            commandContainer.Add(header);

            // Find the serialized property for the command we are currently dealing with
            SerializedProperty serializedCommand = cutsceneSerializedObject.FindProperty("commands").GetArrayElementAtIndex(i);
            List<SerializedProperty> alreadyDrawn = new List<SerializedProperty>();

            // Draw all the properties for that command and add them to the container
            foreach(SerializedProperty prop in serializedCommand)
            {
                if (prop.isArray && prop.propertyType == SerializedPropertyType.Generic)
                {
                    alreadyDrawn.Add(prop);
                    ListView listView = new ListView();
                    MakeUserFriendlyListView(listView);
                    listView.BindProperty(prop);
                    listView.headerTitle = prop.displayName;

                    commandContainer.Add(listView);
                }
                else if (!alreadyDrawn.Contains(prop))
                {
                    // Generate a property field for the property, bind it to the cutscene object, and add it to the container
                    PropertyField field = new PropertyField(prop);
                    field.Bind(cutsceneSerializedObject);
                    commandContainer.Add(field);
                }
            }

            // Now that we have the entire Visual Element for the command, add it to the ScrollView
            scrollView.Add(commandContainer);
        }
    }

    private void MakeUserFriendlyListView(ListView listView)
    {
        listView.showFoldoutHeader = true;
        listView.showAddRemoveFooter = true;
        listView.showBorder = true;
        listView.showBoundCollectionSize = false;
        listView.virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight;
        listView.reorderable = true;
        listView.reorderMode = ListViewReorderMode.Animated;
        listView.selectionType = SelectionType.Single;
    }

    // Generates the Delete, Up, and Down buttons for each command
    private VisualElement GenerateHeaderRow(int i)
    {
        VisualElement container = new VisualElement();
        container.AddToClassList("header-row");
        container.style.flexDirection = FlexDirection.Row; // makes them stack horizontally instead of vertically

        // Generate the command name and add it to the container
        Label label = new Label();
        label.text = $"({i}) " + cutscene.Commands[i].ToString();
        label.AddToClassList("command-header");
        if (i == 0) label.AddToClassList("first-command"); // this is used to get rid of the top-margin for the first command in the list
        container.Add(label);

        // Generate the delete button and define its click action
        Button deleteButton = new Button();
        deleteButton.text = "Delete";
        deleteButton.AddToClassList("delete-button"); // used for styling
        deleteButton.clicked += () =>
        {
            cutscene.RemoveAt(i);
            DisplaySceneContents();
        };
        container.Add(deleteButton);

        // Generate the up button and define its click action
        // Only draw the up button if it's not the first command
        if (i > 0)
        {
            Button upButton = new Button();
            upButton.text = "Up";
            upButton.AddToClassList("position-button"); // used for styling
            upButton.clicked += () =>
            {
                cutscene.SwapCommands(i-1, i);
                DisplaySceneContents();
            };
            container.Add(upButton);
        }

        // Generate the down button and define its click action
        // Only draw the down button if it's not the last command
        if (i < cutscene.Commands.Count - 1)
        {
            Button downButton = new Button();
            downButton.text = "Down";
            downButton.AddToClassList("position-button"); // used for styling
            downButton.clicked += () =>
            {
                cutscene.SwapCommands(i, i+1);
                DisplaySceneContents();
            };
            container.Add(downButton);
        }

        return container;
    }

    // Finds all classes that implement ICutsceneCommand
    private List<ICutsceneCommand> FindAllSceneCommands()
    {
        var type = typeof(ICutsceneCommand);

        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(assembly => type.IsAssignableFrom(assembly) && !assembly.IsInterface)
            .Select(type => Activator.CreateInstance(type) as ICutsceneCommand)
            .ToList();
    }

    // Creates a new instance of the passed in command
    private ICutsceneCommand CreateCommand(ICutsceneCommand command)
    {
        if (command is MoveCharacter)
            return new MoveCharacter();

        if (command is Wait)
            return new Wait();

        if (command is MovePlayer)
            return new MovePlayer();

        if (command is StartDialogue)
            return new StartDialogue();

        else
        {
            Debug.LogError($"Couldn't create instance of {command.ToString()}");
            return null;
        }
            
    }
}
