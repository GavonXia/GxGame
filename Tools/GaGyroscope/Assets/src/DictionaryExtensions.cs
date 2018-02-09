using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class DictionaryExtensions
{
    public static void ForceListAdd<K, V, T>(this Dictionary<K, V> dict, K key, T value, bool acceptSame = false) where V : List<T>, new()
    {
        bool shouldNewAdd = false;
        if (dict.ContainsKey(key))
        {
            V list = dict[key];
            if (list == null)
            {
                dict.Remove(key);
                shouldNewAdd = true;
            }
            else
            {
                if (acceptSame)
                {
                    list.Add(value);
                }
                else if (!list.Contains(value))
                {
                    list.Add(value);
                }
            }
        }
        else
        {
            shouldNewAdd = true;
        }
        if (shouldNewAdd)
        {
            V list = new V();
            list.Add(value);
            dict.Add(key, list);
        }
    }
    
    public static void ForceAdd<K, V>(this Dictionary<K, V> dict, K key, V value)
    {
        if (dict.ContainsKey(key))
        {
            dict[key] = value;
        }
        else
        {
            dict.Add(key, value);
        }
    }

    public static void ForceRemove<K, V>(this Dictionary<K, V> dict, K key)
    {
        if (dict.ContainsKey(key))
        {
            dict.Remove(key);
        }
    }

    public static void SafeAdd<K, V>(this Dictionary<K, V> dict, K key, V value, bool errorOnDuplicate, string contextInformation)
    {
        if (dict.ContainsKey(key))
        {
            if (errorOnDuplicate)
            {
                Debug.LogError(string.Concat(new object[]
                {
                    "Error: Attempt to add a key \"",
                    key,
                    "\" to dictionary failed because it already existed. (CONTEXT: ",
                    contextInformation,
                    ")"
                }).ToString());
            }
            return;
        }
        dict.Add(key, value);
    }
    public static V SafeGet<K, V>(this Dictionary<K, V> dict, K key, V defaultValue, bool errorOnNotFound, string contextInformation)
    {
        if (!dict.ContainsKey(key))
        {
            if (errorOnNotFound)
            {
                Debug.LogError(string.Concat(new object[]
                {
                    "Error: Attempt to get a key \"",
                    key,
                    "\" from dictionary failed because it doesn't exist. (CONTEXT: ",
                    contextInformation,
                    ")"
                }).ToString());
            }
            return defaultValue;
        }
        return dict[key];
    }
    public static string PrintKeys<K, V>(this Dictionary<K, V> dict)
    {
        string text = "{";
        foreach (KeyValuePair<K, V> current in dict)
        {
            text = text + current.Key + ", ";
        }
        if (dict.Count > 0)
        {
            text = text.Substring(0, text.Length - 1);
        }
        text += "}";
        return text;
    }
    public static string PrintValues<K, V>(this Dictionary<K, V> dict)
    {
        string text = "{";
        foreach (KeyValuePair<K, V> current in dict)
        {
            text = text + current.Value + ", ";
        }
        if (dict.Count > 0)
        {
            text = text.Substring(0, text.Length - 1);
        }
        text += "}";
        return text;
    }
    public static string PrintKeyValuePairs<K, V>(this Dictionary<K, V> dict)
    {
        string text = "{";
        foreach (KeyValuePair<K, V> current in dict)
        {
            string text2 = text;
            text = string.Concat(new object[]
            {
                text2,
                current.Key,
                ":",
                current.Value,
                ", "
            });
        }
        if (dict.Count > 0)
        {
            text = text.Substring(0, text.Length - 1);
        }
        text += "}";
        return text;
    }
    public static Dictionary<string, object> MergeDictionaries(Dictionary<string, object> dictA, Dictionary<string, object> dictB)
    {
        return dictA.Concat(
            from kvp in dictB
            where !dictA.ContainsKey(kvp.Key)
            select kvp).ToDictionary((KeyValuePair<string, object> kvp) => kvp.Key, (KeyValuePair<string, object> kvp) => kvp.Value);
    }
    public static Dictionary<NK, NV> CastDictionary<NK, NV, OK, OV>(this Dictionary<OK, OV> dict) where NK : class where NV : class where OK : class where OV : class
    {
        Dictionary<NK, NV> dictionary = new Dictionary<NK, NV>();
        foreach (KeyValuePair<OK, OV> current in dict)
        {
            NK nK = current.Key as NK;
            NV nV = current.Key as NV;
            if (nK == null || (nV == null && current.Value != null))
            {
                Debug.LogError("Cannot cast dictionary types");
                return dictionary;
            }
            dictionary.Add(nK, nV);
        }
        return dictionary;
    }
    public static void AddKeyValuePair<K, V>(this Dictionary<K, V> dict, KeyValuePair<K, V> pair, bool errorOnDuplicate, string contextInformation)
    {
        if (dict.ContainsKey(pair.Key))
        {
            if (errorOnDuplicate)
            {
                Debug.LogError(string.Concat(new object[]
                {
                    "Error: Attempt to add a key value pair with key \"",
                    pair.Key,
                    "\" to dictionary failed because it already existed. (CONTEXT: ",
                    contextInformation,
                    ")"
                }).ToString());
            }
            return;
        }
        dict.Add(pair.Key, pair.Value);
    }
}
