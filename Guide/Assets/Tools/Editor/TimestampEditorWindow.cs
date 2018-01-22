using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TimestampWindow : EditorWindow
{
    private string m_timestamp = string.Empty;

    private string m_time = string.Empty;

    private long m_timeLong = 0;

    private void OnGUI()
    {
        GUILayout.BeginVertical();
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.BeginHorizontal();
                {
                    m_timestamp = EditorTools.TextField(m_timestamp, GUILayout.Width(this.position.width - 80f));
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("Clear", GUILayout.Width(80f)))
                    {
                        m_timestamp = string.Empty;
                    }
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginVertical();
            {
                GUILayout.Label(m_time);
            }
            GUILayout.EndVertical();

            GUILayout.Space(10f);

            GUILayout.BeginVertical();
            {
                string compressMessage = string.Empty;
                MessageType compressMessageType = MessageType.None;
                GetInformationMessage(out compressMessage, out compressMessageType);
                EditorGUILayout.HelpBox(compressMessage, compressMessageType);
            }
            GUILayout.EndVertical();
        }
        GUILayout.EndVertical();
    }

    private void GetInformationMessage(out string compressMessage, out MessageType compressMessageType)
    {
        compressMessage = string.Empty;
        compressMessageType = MessageType.None;
        m_time = string.Empty;
        if (string.IsNullOrEmpty(m_timestamp))
        {
            compressMessage = "Timestamp is null or empty";
            compressMessageType = MessageType.Warning;
        }
        else
        {
            if (!long.TryParse(m_timestamp, out m_timeLong))
            {
                compressMessage = "Timestamp format Error";
                compressMessageType = MessageType.Error;
            }
            else
            {
                compressMessage = "OK";
                m_time = GetDateTime(m_timeLong).ToString("dd/MM/yyyy HH:mm:ss");
            }
        }
    }

    private DateTime GetDateTime(long timeStamp)
    {
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long lTime = ((long)timeStamp * 10000000);
        TimeSpan toNow = new TimeSpan(lTime);
        DateTime targetDt = dtStart.Add(toNow);
        return targetDt;
    }
}