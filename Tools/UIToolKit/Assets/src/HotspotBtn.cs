using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HotspotBtn : MonoBehaviour
{
    GalaxyTweenController m_controller;
    GameObject m_mainSpot;
    // Use this for initialization
    void Awake()
    {
        m_controller = transform.GetComponent<GalaxyTweenController>();
        m_mainSpot = transform.Find("Sprite").gameObject;

        UIEventListener.Get(m_mainSpot).onPress = OnPress;
        UIEventListener.Get(m_mainSpot).onDrag = OnDrag;
    }

    private Vector2 m_previousPos;
    private float m_timeInterval;
    private float m_timeMax = 0.5f;
    
    private bool m_onLongPress = false;
    private bool m_isPress = false;
    private bool m_isOpen = false;
    
    private void OnDrag(GameObject go, Vector2 delta)
    {
        if (delta != Vector2.zero)
        {
            Debug.Log(Time.frameCount + "  OnDragStart" + delta);
            ResetInterval();
        }
    }

    private void OnShow(bool isShow)
    {
        Debug.Log("isSHow  " + isShow);
        if (isShow)
        {
            m_controller.Play("show");
        }
        else {
            m_controller.Play("hide");
        }
    }

    private void OnPress(GameObject go, bool state)
    {
        m_isPress = state;

        ResetInterval();
        m_onLongPress = false;
        Debug.Log(Time.frameCount + "  " + state);
        if (state)
        {
            m_controller.Play("press_sprite");
            m_controller.Play("press");
        }
        else
        {
            if (!m_isOpen)
            {
                m_controller.RewindTrigger("hide");
                m_controller.RewindTrigger("show");
                m_controller.RewindTrigger("press");
                m_controller.PlayBackwardsTrigger("press_sprite");
            }
        }
    }

    private void OnLongPress()
    {
        m_onLongPress = true;
        Debug.Log(Time.frameCount + "  OnLongPress");

        m_isOpen = !m_isOpen;
        OnShow(m_isOpen);
    }

    private void Update()
    {
        if (m_isPress)
        {
            m_timeInterval -= Time.deltaTime;
            
            if (m_timeInterval <= 0 && !m_onLongPress)
            {
                OnLongPress();
            }
        }
    }

    private void ResetInterval()
    {
        m_timeInterval = m_timeMax;
    }
}
