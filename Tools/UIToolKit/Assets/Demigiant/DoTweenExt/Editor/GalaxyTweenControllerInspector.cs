using System;
using System.Collections.Generic;
using System.IO;
using DG.DemiEditor;
using DG.DOTweenEditor.Core;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using GA = DG.Tweening.GaExtention;

#if DOTWEEN_TMP
    using TMPro;
#endif

namespace DG.DOTweenEditor
{
    [CustomEditor(typeof(GalaxyTweenController))]
    public class GalaxyTweenControllerInspector : ABSAnimationInspector
    {
        static readonly string[] _AutoType = new[] {
            "False","True","Auto"
        };

        GalaxyTweenController _src;

        void OnEnable()
        {
            _src = target as GalaxyTweenController;
            _src.ReInit();
        }

        override public void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.BeginVertical();
            GUIInit();
            GUIAuto();
            GuiGalaxyTweenControllers();
            GUILayout.EndVertical();
        }

        void GUIInit()
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Init", GUILayout.Width(90f)))
            {
                _src.ReInit();
            }
            GUILayout.EndHorizontal();
        }

        void GUIAuto()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("AutoPlay", GUILayout.Width(90f));
            _src.m_autoPlay = EditorGUILayout.Popup(_src.m_autoPlay, _AutoType);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("AutoKill", GUILayout.Width(90f));
            _src.m_autoKill = EditorGUILayout.Popup(_src.m_autoKill, _AutoType);
            GUILayout.EndHorizontal();
        }

        void GuiGalaxyTweenControllers()
        {
            if (_src.m_animationMap.Count > 0)
            {
                foreach (EAnimTrigger trigger in _src.m_animationMap.Keys)
                {
                    List<GalaxyDOTweenAnimation> animations = _src.m_animationMap[trigger];


                    DeGUILayout.BeginVBox(GetColorByEAnimTrigger(trigger));
                    switch (trigger)
                    {
                        case EAnimTrigger.Appear:
                            GuiGalaxyTweensAppear(animations); break;
                        case EAnimTrigger.Disappear:
                            GuiGalaxyTweensDisappear(animations); break;
                        case EAnimTrigger.Click:
                            GuiGalaxyTweensClick(animations); break;
                        case EAnimTrigger.Trigger:
                            GuiGalaxyTweensTrigger(animations); break;
                    }

                    DeGUILayout.EndVBox();
                }
            }
        }

        void GuiGalaxyTweensAppear(List<GalaxyDOTweenAnimation> animations)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Appear");
            GUILayout.EndHorizontal();

            foreach (GalaxyDOTweenAnimation anim in animations)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(anim.gameObject.name, GUILayout.Width(200f)))
                {
                    Selection.activeObject = anim;
                }
                GUILayout.EndHorizontal();
            }
        }

        void GuiGalaxyTweensDisappear(List<GalaxyDOTweenAnimation> animations)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Disappear");
            GUILayout.EndHorizontal();

            foreach (GalaxyDOTweenAnimation anim in animations)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(anim.gameObject.name, GUILayout.Width(200f)))
                {
                    Selection.activeObject = anim;
                }
                GUILayout.EndHorizontal();
            }
        }

        void GuiGalaxyTweensClick(List<GalaxyDOTweenAnimation> animations)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Click");
            GUILayout.EndHorizontal();

            foreach (GalaxyDOTweenAnimation anim in animations)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(anim.gameObject.name, GUILayout.Width(200f)))
                {
                    Selection.activeObject = anim;
                }
                GUILayout.EndHorizontal();
            }
        }

        void GuiGalaxyTweensTrigger(List<GalaxyDOTweenAnimation> animations)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Trigger");
            GUILayout.EndHorizontal();

            foreach (GalaxyDOTweenAnimation anim in animations)
            {
                GUILayout.BeginHorizontal();

                GUILayout.Label(anim.triggerStr, GUILayout.Width(100f));
                if (GUILayout.Button(anim.gameObject.name, GUILayout.Width(200f)))
                {
                    Selection.activeObject = anim;
                }
                GUILayout.EndHorizontal();
            }
        }

        readonly Color AppearColor = new Color(135f / 255f, 206f / 255f, 235f / 255f);
        readonly Color DisappearColor = new Color(188f / 255f, 143f / 255f, 143f / 255f);
        readonly Color ClickColor = new Color(154f / 255f, 205f / 255f, 50f / 255f);
        readonly Color TriggerColor = new Color(255f / 255f, 255f / 255f, 0);

        private Color GetColorByEAnimTrigger(EAnimTrigger trigger) {

            switch (trigger)
            {
                case EAnimTrigger.Appear:
                    return AppearColor; break;
                case EAnimTrigger.Disappear:
                    return DisappearColor; break;
                case EAnimTrigger.Click:
                    return ClickColor; break;
                case EAnimTrigger.Trigger:
                    return TriggerColor; break;
            }
            return TriggerColor;
        }
    }
}

