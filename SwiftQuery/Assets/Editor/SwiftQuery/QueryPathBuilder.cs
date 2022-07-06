using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueryPathBuilder : MonoBehaviour
{
    public static string BuildQueryPath(SupportedSearchEngine searchEngine, string query)
    {
        string searchEngineDomain = "";
        //TODO: Find a better way to define these.
        switch (searchEngine)
        {
            case SupportedSearchEngine.Google:
                searchEngineDomain = "google.com/search";
                break;

            case SupportedSearchEngine.Bing:
                searchEngineDomain = "bing.com/search";
                break;
            
            case SupportedSearchEngine.DuckDuckGo:
                searchEngineDomain = "duckduckgo.com/";
                break;
            
            case SupportedSearchEngine.StackOverflow:
                searchEngineDomain = "stackoverflow.com/search";
                break;
        }
        return $"https://{searchEngineDomain}?q={query}";
    }
}