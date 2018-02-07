using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DG.Tweening
{

    public enum EAnimTrigger
    {
        None,
        Disappear,
        Appear,
        Click,
        Trigger,
    }

    public class GalaxyTweenController : MonoBehaviour
    {
        public Transform m_target;

        // 0 no 1 yes 2 auto
        public int m_autoKill = 0;
        public int m_autoPlay = 0;

        [SerializeField]
        public List<GalaxyDOTweenAnimation> m_animationList;
        [SerializeField]
        public Dictionary<EAnimTrigger, List<GalaxyDOTweenAnimation>> m_animationMap = new Dictionary<EAnimTrigger, List<GalaxyDOTweenAnimation>>();
        [SerializeField]
        public Dictionary<string, List<GalaxyDOTweenAnimation>> m_animationTriggerMap = new Dictionary<string, List<GalaxyDOTweenAnimation>>();

        private void Awake()
        {
            if (m_target == null)
            {
                m_target = transform;
            }
            m_animationList = new List<GalaxyDOTweenAnimation>(m_target.GetComponentsInChildren<GalaxyDOTweenAnimation>(true));
            m_animationMap = new Dictionary<EAnimTrigger, List<GalaxyDOTweenAnimation>>();
            m_animationTriggerMap = new Dictionary<string, List<GalaxyDOTweenAnimation>>();

            foreach (GalaxyDOTweenAnimation anim in m_animationList)
            {
                bool isCustom = true;
                bool isTrue = true;
                GetBoolByInt(m_autoKill,out isTrue, out isCustom);
                if (isCustom)
                {
                    anim.autoKill = isTrue;
                }
                GetBoolByInt(m_autoPlay, out isTrue, out isCustom);
                if (isCustom)
                {
                    anim.autoPlay = isTrue;
                }

                anim.CreateTween();

                m_animationMap.ForceListAdd(anim.animTrigger, anim);

                if (anim.animTrigger == EAnimTrigger.Trigger)
                {
                    if (!string.IsNullOrEmpty(anim.triggerStr))
                    {
                        m_animationTriggerMap.ForceListAdd(anim.triggerStr, anim);
                    }
                }
            }
        }

        private void GetBoolByInt(int num, out bool isTrue, out bool isCustom)
        {
            isCustom = true;
            isTrue = true;
            switch (num)
            {
                case 0:
                    isTrue = false; isCustom = false; break;
                case 1:
                    isTrue = true; isCustom = false; break;
                case 2:
                    isTrue = false; isCustom = true; break;
            }
        }

        public void ReInit()
        {
            Awake();
        }

        public void Play(string trigger)
        {
            PlayTrigger(trigger);
        }

        public void Play(EAnimTrigger trigger)
        {
            switch (trigger)
            {
                case EAnimTrigger.Appear:
                    PlayAppear();
                    break;
                case EAnimTrigger.Disappear:
                    PlayDisappear();
                    break;
                case EAnimTrigger.Click:
                    PlayClick();
                    break;
            }
        }

        public void PlayAppear()
        {

        }
        public void PlayDisappear()
        {

        }
        public void PlayClick()
        {

        }
        public void PlayTrigger(string trigger)
        {
            if (m_animationTriggerMap.ContainsKey(trigger))
            {
                List<GalaxyDOTweenAnimation> animations = m_animationTriggerMap[trigger];
                foreach (GalaxyDOTweenAnimation animtion in animations)
                {
                    animtion.DOPlayForward();
                }
            }
        }

        public void PlayBackwardsTrigger(string trigger)
        {
            if (m_animationTriggerMap.ContainsKey(trigger))
            {
                List<GalaxyDOTweenAnimation> animations = m_animationTriggerMap[trigger];
                foreach (GalaxyDOTweenAnimation animtion in animations)
                {
                    animtion.DOPlayBackwards();
                }
            }
        }

        public void RewindTrigger(string trigger)
        {
            if (m_animationTriggerMap.ContainsKey(trigger))
            {
                Component t = null;
                List<GalaxyDOTweenAnimation> animations = m_animationTriggerMap[trigger];
                foreach (GalaxyDOTweenAnimation animtion in animations)
                {
                    animtion.DORewindEx();
                }
            }
        }
    }
}