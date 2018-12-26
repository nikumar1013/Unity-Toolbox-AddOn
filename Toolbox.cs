// Nikhil Kumar
// Date Completed: 12/25/18

using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.EventSystems;

class Toolbox : EditorWindow {

    // Boolean instance variable for controlling Create as Child functionality 
    static bool isChildActive;

    // Initializes the program
    void Start() {}

    // Creates the window frame for the toolbox
    [MenuItem("Window/Toolbox #%t")]
    static void Initialize() {
        Toolbox window = (Toolbox)EditorWindow.GetWindow(typeof(Toolbox), false, "Toolbox");
        window.minSize = new Vector2(475, 290);
        window.Show();
        window.Start();
    }

    // Update is called once per frame
    void OnGUI() {
        // Creates style and content object variables to allow for customization of the created window
        GUIStyle style = GUI.skin.GetStyle("minibutton");
        GUIContent content = new GUIContent();
        // Sets the dimensions of the window
        style.padding = new RectOffset(1, 1, 0, 0);
        style.overflow = new RectOffset(0, 0, 2, 4);
        style.fixedHeight = 30f;
        style.imagePosition = ImagePosition.ImageAbove;
        // Creates the first row of buttons (3D Object controls)
        Set3DObjectRow(content);
        Create3DObjectButtons(style, content);
        SetCreateAsChildToggle();
        // Creates the second row of buttons (Lighting controls)
        SetLightingRow(content);
        CreateLightingButtons(style, content);
        // Creates the third row of buttons (UI controls)
        SetUIRow(content);
        CreateUIButtons(style, content);
        // Createst the fourth row of buttons (Project Settings controls)
        SetProjectSettingsRow(content);
        CreateProjectSettingsButtons(style, content);
	}

    // Creates a label and customizes settings for the 3D Object buttons
    static void Set3DObjectRow(GUIContent content) {
        content.text = "3D Objects:";
        content.tooltip = "";
        content.image = null;
        GUILayout.Label(content, EditorStyles.boldLabel);
        GUILayout.Space(4);
    }

    // Creates a label and customizes settings for the Lighting buttons
    static void SetLightingRow(GUIContent content) {
        content.text = "Lighting:";
        content.tooltip = "";
        content.image = null;
        GUILayout.Label(content, EditorStyles.boldLabel);
        GUILayout.Space(4);
    }

    // Creates a label and customizes settings for the UI buttons
    static void SetUIRow(GUIContent content) {
        content.text = "UI:";
        content.tooltip = "";
        content.image = null;
        GUILayout.Label(content, EditorStyles.boldLabel);
        GUILayout.Space(4);
    }

    // Creates a label and customizes settings for the Project Settings buttons
    static void SetProjectSettingsRow(GUIContent content) {
        content.text = "Project Settings:";
        content.tooltip = "";
        content.image = null;
        GUILayout.Label(content, EditorStyles.boldLabel);
        GUILayout.Space(4);
    }

    // Creates and implements functionality for the 3D Object Buttons
    static void Create3DObjectButtons(GUIStyle style, GUIContent content) {
        // Starts a new row in the window
        EditorGUILayout.BeginHorizontal();
        // Array of strings representing each button
        string[] objectLabels = { "Cube", "Sphere", "Cylinder", "Capsule", "Plane" };
        // Loop to create the correct number of buttons
        for (int i = 0; i < objectLabels.Length; i++) {
            // Sets the title of each button
            content.text = objectLabels[i];
            content.tooltip = objectLabels[i];
            // Creates the Cube button
            if (i == 0) {
                content.image = AssetPreview.GetMiniTypeThumbnail(typeof(BoxCollider)) as Texture2D;
                if (GUILayout.Button(content, style, GUILayout.MinWidth(24))) {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    // Method call to create the Cube as a child of the current selection if desired
                    CreateAsChild(cube);
                }
            // Creates the Sphere button
            } else if (i == 1) {
                content.image = AssetPreview.GetMiniTypeThumbnail(typeof(SphereCollider)) as Texture2D;
                if (GUILayout.Button(content, style, GUILayout.MinWidth(24))) {
                    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    // Method call to create the Sphere as a child of the current selection if desired
                    CreateAsChild(sphere);
                }
            // Creates the Cylinder button
            } else if (i == 2) {
                content.image = AssetPreview.GetMiniTypeThumbnail(typeof(CapsuleCollider)) as Texture2D;
                if (GUILayout.Button(content, style, GUILayout.MinWidth(24))) {
                    GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                    // Method call to create the Cylinder as a child of the current selection if desired
                    CreateAsChild(cylinder);
                }
            // Creates the Capsule button
            } else if (i == 3) {
                content.image = AssetPreview.GetMiniTypeThumbnail(typeof(CapsuleCollider)) as Texture2D;
                if (GUILayout.Button(content, style, GUILayout.MinWidth(24))) {
                    GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                    // Method call to create the Capsule as a child of the current selection if desired
                    CreateAsChild(capsule);
                }
            // Creates the Plane button
            } else {
                content.image = AssetPreview.GetMiniTypeThumbnail(typeof(MeshCollider)) as Texture2D;
                if (GUILayout.Button(content, style, GUILayout.MinWidth(24))) {
                    GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
                    // Method call to create the Plane as a child of the current selection if desired
                    CreateAsChild(plane);
                }
            }
        }
        // Method call to create the 3D text button (functionality is slightly different from the ones above)
        Create3DTextButton(style, content);
    }

    // Creates the 3D Text Button as part of the row of 3D Object buttons
    static void Create3DTextButton(GUIStyle style, GUIContent content) {
        // Sets the title of the button
        content.text = "3D Text";
        content.tooltip = "3D Text";
        content.image = AssetPreview.GetMiniTypeThumbnail(typeof(TextMesh)) as Texture2D;
        // Creates a default text to appear in the engine when this button is clicked
        if (GUILayout.Button(content, style, GUILayout.MinWidth(24))) {
            GameObject text = new GameObject("New Text");
            text.transform.localPosition = Vector3.zero;
            TextMesh someTextMesh = text.AddComponent<TextMesh>();
            someTextMesh.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            someTextMesh.text = "Hello World";
            text.GetComponent<Renderer>().material = someTextMesh.font.material;
            // Method call to create the 3D Text as a child of the current selection if desired
            CreateAsChild(text);
        }
        // Ends the current row in the window
        GUILayout.EndHorizontal();
    }

    // Allows the users to choose whether or not they want new 3D Objects to be created as children of the current selection
    static void SetCreateAsChildToggle() {
        GUILayout.Space(4f);
        GUILayout.BeginHorizontal();
        isChildActive = GUILayout.Toggle(isChildActive, "  Create as child of current selection");
        GUILayout.EndHorizontal();
        EditorGUILayout.Space();
    }

    // Creates and implements functionality for the Lighting Buttons
    static void CreateLightingButtons(GUIStyle style, GUIContent content) {
        // Starts a new row in the window
        EditorGUILayout.BeginHorizontal();
        // Array of strings representing each button
        string[] lightLabels = { "Directional Light", "Point Light", "Spotlight", "Area Light" };
        // Loop to create the correct number of buttons
        for (int i = 0; i < lightLabels.Length; i++) {
            // Sets the title of each button
            content.text = lightLabels[i];
            content.tooltip = lightLabels[i];
            // Creates the Directional Light button
            if (i == 0)
                content.image = Resources.Load<Texture>("Lights/Directional");
            // Creates the Point Light button
            else if (i == 1)
                content.image = AssetPreview.GetMiniTypeThumbnail(typeof(Light)) as Texture2D;
            // Creates the Spotlight button
            else if (i == 2)
                content.image = Resources.Load<Texture>("Lights/Spotlight");
            // Creates the Area Light button
            else 
                content.image = Resources.Load<Texture>("Lights/Area");
            // Executes the correct command in Unity based on which button is clicked
            if (GUILayout.Button(content, style, GUILayout.MinWidth(97.5f)))
                EditorApplication.ExecuteMenuItem("GameObject/Light/" + lightLabels[i]);
        }
        // Ends the current row in the window
        EditorGUILayout.EndHorizontal();
    }

    // Creates and implements functionality for the UI control Buttons
    static void CreateUIButtons(GUIStyle style, GUIContent content) {
        // Starts a new row in the window
        EditorGUILayout.BeginHorizontal();
        // Array of strings representing each button
        string[] buttonLabels = { "Text", "Image", "Button", "Toggle", "Slider", "Scrollbar", "Dropdown" };
        // Loop to create the correct number of buttons
        for (int i = 0; i < buttonLabels.Length; i++) {
            // Sets the title of each button
            content.text = buttonLabels[i];
            content.tooltip = buttonLabels[i];
            // Creates the Text button
            if (i == 0)
                content.image = AssetPreview.GetMiniTypeThumbnail(typeof(Text)) as Texture2D;
            // Creates the Image button
            else if (i == 1)
                content.image = AssetPreview.GetMiniTypeThumbnail(typeof(Image)) as Texture2D;
            // Creates the Button button
            else if (i == 2)
                content.image = AssetPreview.GetMiniTypeThumbnail(typeof(Button)) as Texture2D;
            // Creates the Toggle button
            else if (i == 3)
                content.image = AssetPreview.GetMiniTypeThumbnail(typeof(Toggle)) as Texture2D;
            // Creates the Slider button
            else if (i == 4)
                content.image = AssetPreview.GetMiniTypeThumbnail(typeof(Slider)) as Texture2D;
            // Creates the Scrollbar button
            else if (i == 5)
                content.image = AssetPreview.GetMiniTypeThumbnail(typeof(Scrollbar)) as Texture2D;
            // Creates the Dropdown button
            else
                content.image = AssetPreview.GetMiniTypeThumbnail(typeof(Dropdown)) as Texture2D;
            // Executes the correct command in Unity based on which button is clicked
            if (GUILayout.Button(content, style, GUILayout.MinWidth(55.5f))) {
                EditorApplication.ExecuteMenuItem("GameObject/UI/" + buttonLabels[i]);
                // Creates an EventSystem in the engine if one does not exist already
                if (!EventSystemExists()) {
                    GameObject eventSystem = Instantiate(Resources.Load("Objects/EventSystem", typeof(GameObject))) as GameObject;
                    eventSystem.name = "EventSystem";
                }
            }
        }
        // Ends the current row in the window
        EditorGUILayout.EndHorizontal();
    }

    // Creates and implements functionality for the Project Settings buttons
    static void CreateProjectSettingsButtons(GUIStyle style, GUIContent content) {
        // Starts a new row in the window
        EditorGUILayout.BeginHorizontal();
        // Array of strings representing each button
        string[] projectLabels = { "Input", "Audio", "Time", "Player", "Physics", "Graphics" };
        // Loop to create the correct number of buttons
        for (int i = 0; i < projectLabels.Length; i++) {
            // Sets the title of each button
            content.text = projectLabels[i];
            content.tooltip = projectLabels[i];
            // Creates the Input button
            if (i == 0)
                content.image = EditorGUIUtility.Load("icons/d_movetool.png") as Texture2D;
            // Creates the Audio button
            else if (i == 1)
                content.image = AssetPreview.GetMiniTypeThumbnail(typeof(AudioSource)) as Texture2D;
            // Creates the Time button
            else if (i == 2)
                content.image = AssetPreview.GetMiniTypeThumbnail(typeof(Animation)) as Texture2D;
            // Creates the Player button
            else if (i == 3)
                content.image = AssetPreview.GetMiniTypeThumbnail(typeof(PlayerSettings)) as Texture2D;
            // Creates the Physics button
            else if (i == 4)
                content.image = AssetPreview.GetMiniTypeThumbnail(typeof(ConstantForce2D)) as Texture2D;
            // Creates the Graphics button
            else
                content.image = AssetPreview.GetMiniTypeThumbnail(typeof(GraphicRaycaster)) as Texture2D;
            // Executes the correct command in Unity based on which button is clicked
            if (GUILayout.Button(content, style, GUILayout.MinWidth(65)))
                EditorApplication.ExecuteMenuItem("Edit/Project Settings/" + projectLabels[i]);
        }
        // Ends the current row in the window
        EditorGUILayout.EndHorizontal();
    }

    // Implements the functionality for allowing the user to create a new 3D Object as a child of the current selection
    static void CreateAsChild(GameObject obj) {
        if (isChildActive) {
            //Don't make a child if no object is selected
            if (Selection.activeObject != null) {
                obj.transform.parent = Selection.activeTransform;
            }
        }
    }

    // Determines if an EventSystem exists
    static bool EventSystemExists() {
        // Creates an array of all the active GameObjects
        GameObject[] foundObjects = FindObjectsOfType<GameObject>();
        // Loops through the array of gameObjects
        foreach (GameObject element in foundObjects) {
            // Checks to see if each object in the array has an EventSystem or not
            if (element.name == "EventSystem") {
                Component foundEventSystem = element.GetComponent(typeof(EventSystem));
                Component foundInputModule = element.GetComponent(typeof(StandaloneInputModule));
                if (foundEventSystem != null && foundInputModule != null) {
                    return true;
                }
            }
        }
        return false;
    }
}