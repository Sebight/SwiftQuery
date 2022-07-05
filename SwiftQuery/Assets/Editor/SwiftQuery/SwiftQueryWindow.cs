using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SwiftQueryWindow : EditorWindow
{
    [MenuItem("SQ/SwiftQueryWizard")]
    static void Init()
    {
        SwiftQueryWindow window = (SwiftQueryWindow) EditorWindow.GetWindow(typeof(SwiftQueryWindow));
        window.Show();
    }

    int selectedTab = 0;
    private SQConfig tempConfig = new SQConfig();

    void OnGUI()
    {
        selectedTab = GUILayout.Toolbar(selectedTab, new string[] {"Query", "Settings"});

        switch (selectedTab)
        {
            case 0:
                RenderQueryTab();
                break;
            case 1:
                RenderSettingsTab();
                break;
        }
    }

    //TODO: Warn to apply changes.
    private void RenderSettingsTab()
    {
        GUILayout.Label("Settings", EditorStyles.boldLabel);
        GUILayout.Label("Config content: ");
        var config = SQConfigLoader.LoadConfig();
        if (config == null)
        {
            EditorGUILayout.HelpBox("Config not found", MessageType.Error);
            if (GUILayout.Button("Create config"))
            {
                SQConfigLoader.CreateConfig();
            }
        }

        //Config content

        GUILayout.BeginHorizontal();
        GUILayout.Label("Browser path");
        if (GUILayout.Button("Select"))
        {
            tempConfig.BrowserPath = EditorUtility.OpenFilePanel("Select browser", "", "");
        }
        GUILayout.EndHorizontal();


        //Save config
        if (GUILayout.Button("Save config"))
        {
            SQConfigLoader.SaveConfig(tempConfig);
            tempConfig = new SQConfig();
        }
    }

    private void RenderQueryTab()
    {
        GUILayout.Label("Query", EditorStyles.boldLabel);
        if (GUILayout.Button("Open browser window"))
        {
            //Start browser on config path
            var config = SQConfigLoader.LoadConfig();
            if (config == null)
            {
                EditorUtility.DisplayDialog("Error", "Config not found", "OK");
                return;
            }
            var browserPath = config.BrowserPath;
            if (string.IsNullOrEmpty(browserPath))
            {
                EditorUtility.DisplayDialog("Error", "Browser path not found", "OK");
                return;
            }
            System.Diagnostics.Process.Start("https://www.google.com/search?q=to be or not to be");
        }
    }
}