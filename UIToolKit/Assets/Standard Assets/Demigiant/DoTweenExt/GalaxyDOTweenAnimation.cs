// Author: Daniele Giardini - http://www.demigiant.com
// Created: 2015/03/12 15:55

using UnityEngine;
using DG.Tweening;

#if DOTWEEN_TMP
	using TMPro;
#endif

#pragma warning disable 1591
namespace DG.Tweening
{
    /// <summary>
    /// Attach this to a GameObject to create a tween
    /// </summary>
    [AddComponentMenu("DOTween/Galaxy DOTween Animation")]
    public class GalaxyDOTweenAnimation : DOTweenAnimation
    {
        public BFViewAnimType _BFViewAnimType;


    }

    public enum BFViewAnimType
    {

    }

}