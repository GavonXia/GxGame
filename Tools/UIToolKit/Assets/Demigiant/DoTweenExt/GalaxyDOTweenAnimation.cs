﻿using DG.Tweening.GaExtention;
using System;
using System.Collections.Generic;
using DG.Tweening.Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using GA = DG.Tweening.GaExtention;

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
    //public class GalaxyDOTweenAnimation : DOTweenAnimation
    //{
    //    public BFViewAnimType _BFViewAnimType;


    //}
    public class GalaxyDOTweenAnimation : ABSAnimationComponent
    {
        // add
        public EAnimTrigger animTrigger;
        public string triggerStr;

        public float delay;
        public float duration = 1;
        public Ease easeType = Ease.OutQuad;
        public AnimationCurve easeCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
        public LoopType loopType = LoopType.Restart;
        public int loops = 1;
        public string id = "";
        public bool isRelative;
        public bool isFrom;
        public bool isIndependentUpdate = false;
        public bool autoKill = true;

        public bool isActive = true;
        public bool isValid;
        public Component target;
        public GA.DOTweenAnimationType animationType;
        public TargetTypeEx targetType;
        public TargetTypeEx forcedTargetType; // Used when choosing between multiple targets
        public bool autoPlay = true;
        public bool useTargetAsV3;

        public float endValueFloat;
        public Vector3 endValueV3;
        public Vector2 endValueV2;
        public Color endValueColor = new Color(1, 1, 1, 1);
        public string endValueString = "";
        public Rect endValueRect = new Rect(0, 0, 0, 0);
        public Transform endValueTransform;

        public bool optionalBool0;
        public float optionalFloat0;
        public int optionalInt0;
        public RotateMode optionalRotationMode = RotateMode.Fast;
        public ScrambleMode optionalScrambleMode = ScrambleMode.None;
        public string optionalString;

        bool _tweenCreated; // TRUE after the tweens have been created
        int _playCount = -1; // Used when calling DOPlayNext

        public string animName;
        public int animClipIndex;
        #region Unity Methods

        void Awake()
        {
            if (!isActive || !isValid) return;

            if (animationType != GA.DOTweenAnimationType.Move || !useTargetAsV3)
            {
                // Don't create tweens if we're using a RectTransform as a Move target,
                // because that will work only inside Start
                //CreateTween();
            }
        }

        void Start()
        {
            if (_tweenCreated || !isActive || !isValid) return;

            //CreateTween();
        }

        void OnDestroy()
        {
            if (tween != null && tween.IsActive()) tween.Kill();
            tween = null;
        }

        // Used also by DOTweenAnimationInspector when applying runtime changes and restarting
        public void CreateTween()
        {
            _tweenCreated = true;
            if (target == null)
            {
                Debug.LogWarning(string.Format("{0} :: This tween's target is NULL, because the animation was created with a DOTween Pro version older than 0.9.255. To fix this, exit Play mode then simply select this object, and it will update automatically", this.gameObject.name), this.gameObject);
                return;
            }

            if (forcedTargetType != TargetTypeEx.Unset) targetType = forcedTargetType;
            if (targetType == TargetTypeEx.Unset)
            {
                // Legacy DOTweenAnimation (made with a version older than 0.9.450) without stored targetType > assign it now
                targetType = TypeToDOTargetType(target.GetType());
            }

            switch (animationType)
            {
                case GA.DOTweenAnimationType.None:
                    break;
                case GA.DOTweenAnimationType.Move:
                    if (useTargetAsV3)
                    {
                        isRelative = false;
                        if (endValueTransform == null)
                        {
                            Debug.LogWarning(string.Format("{0} :: This tween's TO target is NULL, a Vector3 of (0,0,0) will be used instead", this.gameObject.name), this.gameObject);
                            endValueV3 = Vector3.zero;
                        }
                        else
                        {
                            if (targetType == TargetTypeEx.RectTransform)
                            {
                                RectTransform endValueT = endValueTransform as RectTransform;
                                if (endValueT == null)
                                {
                                    Debug.LogWarning(string.Format("{0} :: This tween's TO target should be a RectTransform, a Vector3 of (0,0,0) will be used instead", this.gameObject.name), this.gameObject);
                                    endValueV3 = Vector3.zero;
                                }
                                else
                                {
                                    RectTransform rTarget = target as RectTransform;
                                    if (rTarget == null)
                                    {
                                        Debug.LogWarning(string.Format("{0} :: This tween's target and TO target are not of the same type. Please reassign the values", this.gameObject.name), this.gameObject);
                                    }
                                    else
                                    {
                                        // Problem: doesn't work inside Awake (ararargh!)
                                        endValueV3 = DOTweenUtils46.SwitchToRectTransform(endValueT, rTarget);
                                    }
                                }
                            }
                            else endValueV3 = endValueTransform.position;
                        }
                    }
                    switch (targetType)
                    {
                        case TargetTypeEx.RectTransform:
                            tween = ((RectTransform)target).DOAnchorPos3D(endValueV3, duration, optionalBool0);
                            break;
                        case TargetTypeEx.Transform:
                            tween = ((Transform)target).DOMove(endValueV3, duration, optionalBool0);
                            break;
                        case TargetTypeEx.Rigidbody2D:
                            tween = ((Rigidbody2D)target).DOMove(endValueV3, duration, optionalBool0);
                            break;
                        case TargetTypeEx.Rigidbody:
                            tween = ((Rigidbody)target).DOMove(endValueV3, duration, optionalBool0);
                            break;
                    }
                    break;
                case GA.DOTweenAnimationType.LocalMove:
                    tween = transform.DOLocalMove(endValueV3, duration, optionalBool0);
                    break;
                case GA.DOTweenAnimationType.Rotate:
                    switch (targetType)
                    {
                        case TargetTypeEx.Transform:
                            tween = ((Transform)target).DORotate(endValueV3, duration, optionalRotationMode);
                            break;
                        case TargetTypeEx.Rigidbody2D:
                            tween = ((Rigidbody2D)target).DORotate(endValueFloat, duration);
                            break;
                        case TargetTypeEx.Rigidbody:
                            tween = ((Rigidbody)target).DORotate(endValueV3, duration, optionalRotationMode);
                            break;
                    }
                    break;
                case GA.DOTweenAnimationType.LocalRotate:
                    tween = transform.DOLocalRotate(endValueV3, duration, optionalRotationMode);
                    break;
                case GA.DOTweenAnimationType.Scale:
                    switch (targetType)
                    {
#if DOTWEEN_TK2D
                case TargetType.tk2dTextMesh:
                    tween = ((tk2dTextMesh)target).DOScale(optionalBool0 ? new Vector3(endValueFloat, endValueFloat, endValueFloat) : endValueV3, duration);
                    break;
                case TargetType.tk2dBaseSprite:
                    tween = ((tk2dBaseSprite)target).DOScale(optionalBool0 ? new Vector3(endValueFloat, endValueFloat, endValueFloat) : endValueV3, duration);
                    break;
#endif
                        default:
                            tween = transform.DOScale(optionalBool0 ? new Vector3(endValueFloat, endValueFloat, endValueFloat) : endValueV3, duration);
                            break;
                    }
                    break;
                case GA.DOTweenAnimationType.UIWidthHeight:
                    tween = ((RectTransform)target).DOSizeDelta(optionalBool0 ? new Vector2(endValueFloat, endValueFloat) : endValueV2, duration);
                    break;
                case GA.DOTweenAnimationType.Color:
                    isRelative = false;
                    switch (targetType)
                    {
                        case TargetTypeEx.SpriteRenderer:
                            tween = ((SpriteRenderer)target).DOColor(endValueColor, duration);
                            break;
                        case TargetTypeEx.Renderer:
                            tween = ((Renderer)target).material.DOColor(endValueColor, duration);
                            break;
                        case TargetTypeEx.Image:
                            tween = ((Image)target).DOColor(endValueColor, duration);
                            break;
                        case TargetTypeEx.UISprite:
                            tween = ((UISprite)target).DOColor(endValueColor, duration);
                            break;
                        case TargetTypeEx.UITexture:
                            tween = ((UITexture)target).DOColor(endValueColor, duration);
                            break;
                        case TargetTypeEx.UIWidget:
                            tween = ((UIWidget)target).DOColor(endValueColor, duration);
                            break;
                        case TargetTypeEx.Text:
                            tween = ((Text)target).DOColor(endValueColor, duration);
                            break;
                        case TargetTypeEx.Light:
                            tween = ((Light)target).DOColor(endValueColor, duration);
                            break;
#if DOTWEEN_TK2D
                case TargetType.tk2dTextMesh:
                    tween = ((tk2dTextMesh)target).DOColor(endValueColor, duration);
                    break;
                case TargetType.tk2dBaseSprite:
                    tween = ((tk2dBaseSprite)target).DOColor(endValueColor, duration);
                    break;
#endif
#if DOTWEEN_TMP
                case TargetType.TextMeshProUGUI:
                    tween = ((TextMeshProUGUI)target).DOColor(endValueColor, duration);
                    break;
                case TargetType.TextMeshPro:
                    tween = ((TextMeshPro)target).DOColor(endValueColor, duration);
                    break;
#endif
                    }
                    break;
                case GA.DOTweenAnimationType.Fade:
                    isRelative = false;
                    switch (targetType)
                    {
                        case TargetTypeEx.SpriteRenderer:
                            tween = ((SpriteRenderer)target).DOFade(endValueFloat, duration);
                            break;
                        case TargetTypeEx.Renderer:
                            tween = ((Renderer)target).material.DOFade(endValueFloat, duration);
                            break;
                        case TargetTypeEx.Image:
                            tween = ((Image)target).DOFade(endValueFloat, duration);
                            break;
                        case TargetTypeEx.UISprite:
                            tween = ((UISprite)target).DOFade(endValueFloat, duration);
                            break;
                        case TargetTypeEx.UITexture:
                            tween = ((UITexture)target).DOFade(endValueFloat, duration);
                            break;
                        case TargetTypeEx.UIWidget:
                            tween = ((UIWidget)target).DOFade(endValueFloat, duration);
                            break;
                        case TargetTypeEx.Text:
                            tween = ((Text)target).DOFade(endValueFloat, duration);
                            break;
                        case TargetTypeEx.Light:
                            tween = ((Light)target).DOIntensity(endValueFloat, duration);
                            break;
                        case TargetTypeEx.CanvasGroup:
                            tween = ((CanvasGroup)target).DOFade(endValueFloat, duration);
                            break;
#if DOTWEEN_TK2D
                case TargetType.tk2dTextMesh:
                    tween = ((tk2dTextMesh)target).DOFade(endValueFloat, duration);
                    break;
                case TargetType.tk2dBaseSprite:
                    tween = ((tk2dBaseSprite)target).DOFade(endValueFloat, duration);
                    break;
#endif
#if DOTWEEN_TMP
                case TargetType.TextMeshProUGUI:
                    tween = ((TextMeshProUGUI)target).DOFade(endValueFloat, duration);
                    break;
                case TargetType.TextMeshPro:
                    tween = ((TextMeshPro)target).DOFade(endValueFloat, duration);
                    break;
#endif
                    }
                    break;
                case GA.DOTweenAnimationType.Text:
                    switch (targetType)
                    {
                        case TargetTypeEx.Text:
                            tween = ((Text)target).DOText(endValueString, duration, optionalBool0, optionalScrambleMode, optionalString);
                            break;
#if DOTWEEN_TK2D
                case TargetType.tk2dTextMesh:
                    tween = ((tk2dTextMesh)target).DOText(endValueString, duration, optionalBool0, optionalScrambleMode, optionalString);
                    break;
#endif
#if DOTWEEN_TMP
                case TargetType.TextMeshProUGUI:
                    tween = ((TextMeshProUGUI)target).DOText(endValueString, duration, optionalBool0, optionalScrambleMode, optionalString);
                    break;
                case TargetType.TextMeshPro:
                    tween = ((TextMeshPro)target).DOText(endValueString, duration, optionalBool0, optionalScrambleMode, optionalString);
                    break;
#endif
                    }
                    break;
                case GA.DOTweenAnimationType.PunchPosition:
                    switch (targetType)
                    {
                        case TargetTypeEx.RectTransform:
                            tween = ((RectTransform)target).DOPunchAnchorPos(endValueV3, duration, optionalInt0, optionalFloat0, optionalBool0);
                            break;
                        case TargetTypeEx.Transform:
                            tween = ((Transform)target).DOPunchPosition(endValueV3, duration, optionalInt0, optionalFloat0, optionalBool0);
                            break;
                    }
                    break;
                case GA.DOTweenAnimationType.PunchScale:
                    tween = transform.DOPunchScale(endValueV3, duration, optionalInt0, optionalFloat0);
                    break;
                case GA.DOTweenAnimationType.PunchRotation:
                    tween = transform.DOPunchRotation(endValueV3, duration, optionalInt0, optionalFloat0);
                    break;
                case GA.DOTweenAnimationType.ShakePosition:
                    switch (targetType)
                    {
                        case TargetTypeEx.RectTransform:
                            tween = ((RectTransform)target).DOShakeAnchorPos(duration, endValueV3, optionalInt0, optionalFloat0, optionalBool0);
                            break;
                        case TargetTypeEx.Transform:
                            tween = ((Transform)target).DOShakePosition(duration, endValueV3, optionalInt0, optionalFloat0, optionalBool0);
                            break;
                    }
                    break;
                case GA.DOTweenAnimationType.ShakeScale:
                    tween = transform.DOShakeScale(duration, endValueV3, optionalInt0, optionalFloat0);
                    break;
                case GA.DOTweenAnimationType.ShakeRotation:
                    tween = transform.DOShakeRotation(duration, endValueV3, optionalInt0, optionalFloat0);
                    break;
                case GA.DOTweenAnimationType.CameraAspect:
                    tween = ((Camera)target).DOAspect(endValueFloat, duration);
                    break;
                case GA.DOTweenAnimationType.CameraBackgroundColor:
                    tween = ((Camera)target).DOColor(endValueColor, duration);
                    break;
                case GA.DOTweenAnimationType.CameraFieldOfView:
                    tween = ((Camera)target).DOFieldOfView(endValueFloat, duration);
                    break;
                case GA.DOTweenAnimationType.CameraOrthoSize:
                    tween = ((Camera)target).DOOrthoSize(endValueFloat, duration);
                    break;
                case GA.DOTweenAnimationType.CameraPixelRect:
                    tween = ((Camera)target).DOPixelRect(endValueRect, duration);
                    break;
                case GA.DOTweenAnimationType.CameraRect:
                    tween = ((Camera)target).DORect(endValueRect, duration);
                    break;
                case GA.DOTweenAnimationType.Animation:
                    tween = ((Animation)target).DOAnimation(animName, duration);
                    break;
                case GaExtention.DOTweenAnimationType.Animator:
                    tween = ((Animator)target).DOAnimation(animName, duration);
                    break;
            }

            if (tween == null) return;

            if (isFrom)
            {
                ((Tweener)tween).From(isRelative);
            }
            else
            {
                tween.SetRelative(isRelative);
            }
            tween.SetTarget(this.gameObject).SetDelay(delay).SetLoops(loops, loopType).SetAutoKill(autoKill)
                .OnKill(() => tween = null);
            if (isSpeedBased) tween.SetSpeedBased();
            if (easeType == Ease.INTERNAL_Custom) tween.SetEase(easeCurve);
            else tween.SetEase(easeType);
            if (!string.IsNullOrEmpty(id)) tween.SetId(id);
            tween.SetUpdate(isIndependentUpdate);

            if (hasOnStart)
            {
                if (onStart != null) tween.OnStart(onStart.Invoke);
            }
            else onStart = null;
            if (hasOnPlay)
            {
                if (onPlay != null) tween.OnPlay(onPlay.Invoke);
            }
            else onPlay = null;
            if (hasOnUpdate)
            {
                if (onUpdate != null) tween.OnUpdate(onUpdate.Invoke);
            }
            else onUpdate = null;
            if (hasOnStepComplete)
            {
                if (onStepComplete != null) tween.OnStepComplete(onStepComplete.Invoke);
            }
            else onStepComplete = null;
            if (hasOnComplete)
            {
                if (onComplete != null) tween.OnComplete(onComplete.Invoke);
            }
            else onComplete = null;
            if (hasOnRewind)
            {
                if (onRewind != null) tween.OnRewind(onRewind.Invoke);
            }
            else onRewind = null;

            if (autoPlay) tween.Play();
            else tween.Pause();

            if (hasOnTweenCreated && onTweenCreated != null) onTweenCreated.Invoke();
        }

        #endregion

        #region Public Methods

        // These methods are here so they can be called directly via Unity's UGUI event system

        public override void DOPlay()
        {
            Tween t = this.tween;
            if (t != null && t.IsInitialized()) t.Play();
        }

        public override void DOPlayBackwards()
        {
            Tween t = this.tween;
            if (t != null) t.PlayBackwards();
        }

        public override void DOPlayForward()
        {
            Tween t = this.tween;
            if (t != null) t.PlayForward();
        }

        public override void DOPause()
        {
            DOTween.Pause(this.gameObject);
        }

        public override void DOTogglePause()
        {
            DOTween.TogglePause(this.gameObject);
        }

        public void DORewindEx()
        {
            _playCount = -1;
            Tween t = this.tween;
            if (t != null && t.IsInitialized()) t.Rewind();
        } 

        public override void DORewind()
        {
            _playCount = -1;
            // Rewind using Components order (in case there are multiple animations on the same property)
            GalaxyDOTweenAnimation[] anims = this.gameObject.GetComponents<GalaxyDOTweenAnimation>();
            for (int i = anims.Length - 1; i > -1; --i)
            {
                Tween t = anims[i].tween;
                if (t != null && t.IsInitialized()) anims[i].tween.Rewind();
            }
            // DOTween.Rewind(this.gameObject);
        }

        /// <summary>
        /// Restarts the tween
        /// </summary>
        /// <param name="fromHere">If TRUE, re-evaluates the tween's start and end values from its current position.
        /// Set it to TRUE when spawning the same DOTweenAnimation in different positions (like when using a pooling system)</param>
        public override void DORestart(bool fromHere = false)
        {
            _playCount = -1;
            if (tween == null)
            {
                if (Debugger.logPriority > 1) Debugger.LogNullTween(tween); return;
            }
            if (fromHere && isRelative) ReEvaluateRelativeTween();
            DOTween.Restart(this.gameObject);
        }

        public override void DOComplete()
        {
            DOTween.Complete(this.gameObject);
        }

        public override void DOKill()
        {
            DOTween.Kill(this.gameObject);
            tween = null;
        }

        #region Specifics

        public void DOPlayById(string id)
        {
            DOTween.Play(this.gameObject, id);
        }
        public void DOPlayAllById(string id)
        {
            DOTween.Play(id);
        }

        public void DOPauseAllById(string id)
        {
            DOTween.Pause(id);
        }

        public void DOPlayBackwardsById(string id)
        {
            DOTween.PlayBackwards(this.gameObject, id);
        }
        public void DOPlayBackwardsAllById(string id)
        {
            DOTween.PlayBackwards(id);
        }

        public void DOPlayForwardById(string id)
        {
            DOTween.PlayForward(this.gameObject, id);
        }
        public void DOPlayForwardAllById(string id)
        {
            DOTween.PlayForward(id);
        }

        public void DOPlayNext()
        {
            GalaxyDOTweenAnimation[] anims = this.GetComponents<GalaxyDOTweenAnimation>();
            while (_playCount < anims.Length - 1)
            {
                _playCount++;
                GalaxyDOTweenAnimation anim = anims[_playCount];
                if (anim != null && anim.tween != null && !anim.tween.IsPlaying() && !anim.tween.IsComplete())
                {
                    anim.tween.Play();
                    break;
                }
            }
        }

        public void DORewindAndPlayNext()
        {
            _playCount = -1;
            DOTween.Rewind(this.gameObject);
            DOPlayNext();
        }

        public void DORestartById(string id)
        {
            _playCount = -1;
            DOTween.Restart(this.gameObject, id);
        }
        public void DORestartAllById(string id)
        {
            _playCount = -1;
            DOTween.Restart(id);
        }

        /// <summary>
        /// Returns the tweens created by this DOTweenAnimation, in the same order as they appear in the Inspector (top to bottom)
        /// </summary>
        public List<Tween> GetTweens()
        {
            //            return DOTween.TweensByTarget(this.gameObject);

            List<Tween> result = new List<Tween>();
            GalaxyDOTweenAnimation[] anims = this.GetComponents<GalaxyDOTweenAnimation>();
            foreach (GalaxyDOTweenAnimation anim in anims) result.Add(anim.tween);
            return result;
        }

        #endregion

        #region Internal Static Helpers (also used by Inspector)

        public static TargetTypeEx TypeToDOTargetType(Type t)
        {
            string str = t.ToString();
            int dotIndex = str.LastIndexOf(".");
            if (dotIndex != -1) str = str.Substring(dotIndex + 1);
            if (str.IndexOf("Renderer") != -1 && (str != "SpriteRenderer")) str = "Renderer";
            return (TargetTypeEx)Enum.Parse(typeof(TargetTypeEx), str);
        }

        #endregion

        #endregion

        #region Private

        // Re-evaluate relative position of path
        void ReEvaluateRelativeTween()
        {
            if (animationType == GA.DOTweenAnimationType.Move)
            {
                ((Tweener)tween).ChangeEndValue(transform.position + endValueV3, true);
            }
            else if (animationType == GA.DOTweenAnimationType.LocalMove)
            {
                ((Tweener)tween).ChangeEndValue(transform.localPosition + endValueV3, true);
            }
        }

        #endregion
    }
}