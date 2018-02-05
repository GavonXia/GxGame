using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class GuideMenu
{

    [MenuItem("Guide/Examples/TimestampToTime2", false, 51)]
    public static void TimeStampToTime2()
    {
        EditorWindow.GetWindow<TimestampWindow>().Show();
    }

    [MenuItem("Guide/Examples/TimestampToTime3", false, 52)]
    public static void TimeStampToTime3()
    {
        EditorWindow.GetWindow<TimestampWindow>().Show();
    }


    [MenuItem("Guide/Tools/TimestampToTime", false, 1)]
    public static void TimeStampToTime()
    {
        EditorWindow.GetWindow<TimestampWindow>().Show();
    }

}
