using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityEventListener : MonoBehaviour
{
    MyUnityEvent uEvent = new MyUnityEvent();

    // Update is called once per frame
    void Update()
    {
        uEvent.AddListener(Test);

    }

    void Test()
    {

    }
}
