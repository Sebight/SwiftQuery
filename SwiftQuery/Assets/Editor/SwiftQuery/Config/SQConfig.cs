using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SupportedSearchEngine
{
    Google,
    DuckDuckGo,
    Bing,
    StackOverflow
}

public class SQConfig
{
    public string BrowserPath;
    public SupportedSearchEngine SearchEngine;
}
