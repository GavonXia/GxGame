using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DG.Tweening.GaExtention
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
        public List<GalaxyDOTweenAnimation> m_animationList = new List<GalaxyDOTweenAnimation>();
        public Transform m_target;

        private void Awake()
        {
            if (m_target == null) {
                m_target = transform;
            }
            m_animationList = new List<GalaxyDOTweenAnimation>(m_target.GetComponentsInChildren<GalaxyDOTweenAnimation>(true));
        }


    }

}