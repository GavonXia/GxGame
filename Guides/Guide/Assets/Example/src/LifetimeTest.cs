using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifetimeTest : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log(" Awake");
    }

    private void OnEnable()
    {
        Debug.Log(" OnEnable");
    }

    private void Start()
    {
        Debug.Log(" Start");
    }

    private void Update()
    {
        Debug.Log(" Update");
    }

    private void LateUpdate()
    {
        Debug.Log(" LateUpdate");
    }

    private void FixedUpdate()
    {
        Debug.Log(" FixedUpdate");
    }

    private void OnGUI()
    {
        Debug.Log(" OnGUI");
    }

    private void OnDisable()
    {
        Debug.Log(" OnDisable");
    }

    private void OnDestroy()
    {
        Debug.Log(" OnDestroy");
    }

    private void OnApplicationQuit()
    {
        Debug.Log(" OnApplicationQuit");
    }

}
