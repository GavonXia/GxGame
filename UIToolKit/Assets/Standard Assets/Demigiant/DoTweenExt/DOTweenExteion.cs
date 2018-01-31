using UnityEngine;
using UnityEngine.UI;

namespace DG.Tweening.GaExtention
{
    public static class DOTweenExteion
    {
        public static Tweener DOFade(this UISprite sprite, float endValue, float duration)
        {
            Debug.Log("CustomDoFade");
            return DOTween.To(sprite.AlphaGetter, sprite.AlphaSetter, endValue, duration);
        }

        public static Tweener DOFade(this UITexture sprite, float endValue, float duration)
        {
            Debug.Log("CustomDoFade");
            return DOTween.To(sprite.AlphaGetter, sprite.AlphaSetter, endValue, duration);
        }

        public static Tweener DOFade(this UIWidget sprite, float endValue, float duration)
        {
            Debug.Log("CustomDoFade");
            return DOTween.To(sprite.AlphaGetter, sprite.AlphaSetter, endValue, duration);
        }

        public static Tweener DOAnimation(this UIWidget sprite, float endValue, float duration)
        {
            Debug.Log("CustomDoFade");
            return DOTween.To(sprite.AlphaGetter, sprite.AlphaSetter, endValue, duration);
        }


        public static Tweener DOColor(this UISprite sprite, Color endValue, float duration)
        {
            Debug.Log("CustomDoColor");
            return DOTween.To(sprite.ColorGetter, sprite.ColorSetter, endValue, duration);
        }

        public static Tweener DOColor(this UITexture sprite, Color endValue, float duration)
        {
            Debug.Log("CustomDoColor");
            return DOTween.To(sprite.ColorGetter, sprite.ColorSetter, endValue, duration);
        }

        public static Tweener DOColor(this UIWidget sprite, Color endValue, float duration)
        {
            Debug.Log("CustomDoColor");
            return DOTween.To(sprite.ColorGetter, sprite.ColorSetter, endValue, duration);
        }

        private static float AlphaGetter(this UISprite image)
        {
            return image.alpha;
        }

        private static void AlphaSetter(this UISprite image, float alpha)
        {
            image.alpha = alpha;
        }

        private static float AlphaGetter(this UITexture image)
        {
            return image.alpha;
        }

        private static void AlphaSetter(this UIWidget image, float alpha)
        {
            image.alpha = alpha;
        }
        private static float AlphaGetter(this UIWidget image)
        {
            return image.alpha;
        }

        private static void AlphaSetter(this UITexture image, float alpha)
        {
            image.alpha = alpha;
        }

        private static Color ColorGetter(this UISprite image)
        {
            return image.color;
        }

        private static void ColorSetter(this UISprite image, Color color)
        {
            image.color = color;
        }

        private static Color ColorGetter(this UITexture image)
        {
            return image.color;
        }

        private static void ColorSetter(this UIWidget image, Color color)
        {
            image.color = color;
        }

        private static Color ColorGetter(this UIWidget image)
        {
            return image.color;
        }


        //case TargetTypeEx.UISprite:
        //                   _src.target = _src.GetComponent<UISprite>();
        //                   break;
        //               case TargetTypeEx.UITexture:
        //                   _src.target = _src.GetComponent<UITexture>();
        //                   break;
        //               case TargetTypeEx.UIWidget:
        //                   _src.target = _src.GetComponent<UIWidget>();
        //                   break;
    }
}