using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("get key : A");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("get key down : A");
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            Debug.Log("get key up : A");
        }

        if (Input.GetMouseButton(0))
        {
            Debug.Log("GetMouseButton : left");
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("GetMouseButtonDown : left");
        }

        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("GetMouseButtonUp : left");
        }
    }
}
