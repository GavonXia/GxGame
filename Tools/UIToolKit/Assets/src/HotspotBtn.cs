using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HotspotBtn : MonoBehaviour
{
    readonly static Vector2 NORMAL_WIDGET_SIZE = new Vector2(140, 140);
    readonly static Vector2 OPEN_WIDGET_SIZE = new Vector2(300, 300);

    UIDragObject m_dragObj;
    UIWidget m_targetWidget;
    GalaxyTweenController m_controller;
    GameObject m_mainSpot;

    private Vector2 m_previousPos;
    private float m_timeInterval;
    private float m_timeMax = 0.5f;

    private bool m_onLongPress = false;
    private bool m_isPress = false;
    private bool m_isOpen = false;

    // Use this for initialization
    void Awake()
    {
        m_controller = transform.GetComponentInChildren<GalaxyTweenController>();
        m_mainSpot = transform.Find("Root/Sprite").gameObject;
        m_targetWidget = transform.GetComponent<UIWidget>();
        m_dragObj = transform.GetComponentInChildren<UIDragObject>();

        UIEventListener.Get(m_mainSpot).onPress = OnPress;
        UIEventListener.Get(m_mainSpot).onDrag = OnDrag;
        SetWidgetSize(false);
    }

    private void OnDrag(GameObject go, Vector2 delta)
    {
        if (delta != Vector2.zero)
        {
            ResetInterval();
        }
    }

    private void OnShow(bool isShow)
    {
        if (isShow)
        {
            SetWidgetSize(true);
            m_controller.Play("show");
            m_dragObj.ForceRefresh(true);
        }
        else
        {
            SetWidgetSize(false);
            m_controller.Play("hide");
            m_dragObj.ForceRefresh(false);
        }
    }

    private void OnPress(GameObject go, bool state)
    {
        m_isPress = state;

        ResetInterval();
        m_onLongPress = false;

        if (state)
        {
            if (!m_isOpen)
            {
                m_controller.RewindTrigger("show");

                m_controller.Play("press_sprite");
                m_controller.Play("press");
            }
            else
            {
                m_controller.RewindTrigger("hide");
            }
        }
        else
        {
            if (!m_isOpen)
            {
                m_controller.RewindTrigger("press");
                m_controller.PlayBackwardsTrigger("press_sprite");
            }
        }
    }

    private void OnLongPress()
    {
        m_onLongPress = true;
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

    private void SetWidgetSize(bool isOpen)
    {
        if (!isOpen)
        {
            m_targetWidget.width = (int)NORMAL_WIDGET_SIZE.x;
            m_targetWidget.height = (int)NORMAL_WIDGET_SIZE.y;
        }
        else
        {
            m_targetWidget.width = (int)OPEN_WIDGET_SIZE.x;
            m_targetWidget.height = (int)OPEN_WIDGET_SIZE.y;
        }
    }

}
