using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class SQConfigLoader : MonoBehaviour
{
    public static readonly string SQ_CONFIG_PATH = Application.dataPath + "/Editor/SwiftQuery/Config/SQConfig.json";

    public static SQConfig LoadConfig()
    {
        if (File.Exists(SQ_CONFIG_PATH))
        {
            string json = File.ReadAllText(SQ_CONFIG_PATH);
            SQConfig config = JsonConvert.DeserializeObject<SQConfig>(json);
            return config;
        }
        else
        {
            return null;
        }
    }

    public static void CreateConfig()
    {
        string json = JsonConvert.SerializeObject(new SQConfig());
        File.WriteAllText(SQ_CONFIG_PATH, json);
    }
    
    public static void SaveConfig(SQConfig config)
    {
        string json = JsonConvert.SerializeObject(config);
        File.WriteAllText(SQ_CONFIG_PATH, json);
    }
}