using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyControllerTest : MonoBehaviour
{
    GalaxyTweenController controller;

    // Use this for initialization
    void Start()
    {
        controller = transform.GetComponent<GalaxyTweenController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            controller.PlayTrigger("AA");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            controller.PlayBackwardsTrigger("AA");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            controller.RewindTrigger("AA");
        }
    }
}
