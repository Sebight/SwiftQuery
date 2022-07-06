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
    private string searchQuery = "";

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
        
        GUILayout.BeginHorizontal();
        GUILayout.Label("Search engine");
        tempConfig.SearchEngine = (SupportedSearchEngine) EditorGUILayout.EnumPopup(tempConfig.SearchEngine); 
        GUILayout.EndHorizontal();

        //Save config
        if (GUILayout.Button("Save config"))
        {
            SQConfigLoader.SaveConfig(tempConfig);
            var oldSearchEngine = tempConfig.SearchEngine;
            tempConfig = new SQConfig();
            tempConfig.SearchEngine = oldSearchEngine;
        }
    }

    private void RenderQueryTab()
    {
        GUILayout.Label("Query", EditorStyles.boldLabel);
        searchQuery = EditorGUILayout.TextField("Search query", searchQuery);
        if (GUILayout.Button("Open browser window"))
        {
            //Start browser on config path
            var config = SQConfigLoader.LoadConfig();
            // if (config == null)
            // {
            //     EditorUtility.DisplayDialog("Error", "Config not found", "OK");
            //     return;
            // }
            // var browserPath = config.BrowserPath;
            // if (string.IsNullOrEmpty(browserPath))
            // {
            //     EditorUtility.DisplayDialog("Error", "Browser path not found", "OK");
            //     return;
            // }
            var queryString = QueryPathBuilder.BuildQueryPath(config.SearchEngine, searchQuery);
            System.Diagnostics.Process.Start(queryString);
        }
    }
}