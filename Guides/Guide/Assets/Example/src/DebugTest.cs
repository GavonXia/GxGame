using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTest : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        Debug.Log("Log");
        Debug.LogWarning("LogWarning");
        Debug.LogError("LogError");

        Debug.Log("Log", gameObject);
        Debug.LogFormat(gameObject, "GameObject Name : {0}", gameObject.name);
    }
}
