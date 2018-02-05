using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DG.Tweening
{
    public enum TweenTrigger
    {
        None,
        Appear,
        Disappear,
        Trigger,
        Click
    }

    public class GalaxyTweenController : MonoBehaviour
    {
        public Transform m_target;

        public List<GalaxyDOTweenAnimation> m_animationList;
        public Dictionary<EAnimTrigger, List<GalaxyDOTweenAnimation>> m_animationMap = new Dictionary<EAnimTrigger, List<GalaxyDOTweenAnimation>>();

        private void Awake()
        {
            if (m_target == null)
            {
                m_target = transform;
            }
            m_animationList = new List<GalaxyDOTweenAnimation>(m_target.GetComponentsInChildren<GalaxyDOTweenAnimation>(true));

            foreach (GalaxyDOTweenAnimation anim in m_animationList)
            {
                m_animationMap.ForceListAdd(anim.animTrigger, anim);
            }
        }


    }

}