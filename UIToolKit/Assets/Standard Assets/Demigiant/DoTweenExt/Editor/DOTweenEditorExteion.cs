using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DG.Tweening.GaExtention
{
    public static class DOTweenEditorExteion
    {
        private static Texture2D m_texture;
        private static bool m_canGetTexture = true;

        public static void InspectorLogo()
        {
            if (m_texture == null)
            //if (m_texture == null && m_canGetTexture)
            {
                m_canGetTexture = false;
                string[] resAssets = AssetDatabase.FindAssets("DOTweenExteion.dll t:Sprite");
                if (resAssets != null && resAssets.Length > 0)
                {
                    string resPath = AssetDatabase.GUIDToAssetPath(resAssets[0]);
                    m_texture = AssetDatabase.LoadAssetAtPath(resPath, typeof(Texture2D)) as Texture2D;
                    if (m_texture != null)
                    {
                        m_canGetTexture = true;
                    }
                }
            }
            if (m_texture)
            {
                GUILayout.Box(m_texture, GUILayout.Width(120), GUILayout.Height(23));
                // GUI.DrawTexture(new Rect(0, 0, m_texture.width, m_texture.height), m_texture);
            }
        }

    }

}