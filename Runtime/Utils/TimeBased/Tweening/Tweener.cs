using System;
using System.Collections;
using UnityEngine;

namespace UtilsToolbox.Utils.TimeBased.Tweening
{
    public static class Tweener
    {
        #region PUBLIC METHODS
        public static TweenHandle TweenScale(MonoBehaviour caller, TweenTimeMode mode, GameObject target, Vector2 startScale,
            Vector2 finishScale, float duration, Action onFinish = null)
        {
            return StartTween(caller, Tween_Coroutine(mode, duration,
                tickDelta => target.transform.localScale = Vector3.Lerp(startScale, finishScale, tickDelta),
                () =>
                {
                    target.transform.localScale = finishScale;
                    onFinish?.Invoke();
                })
            );
        }

        public static TweenHandle TweenAlpha(MonoBehaviour caller, TweenTimeMode mode, CanvasGroup target, float startAlpha,
            float finishAlpha, float duration, Action onFinish = null)
        {
            return StartTween(caller, Tween_Coroutine(mode, duration,
                tickDelta => target.alpha = Mathf.Lerp(startAlpha, finishAlpha, tickDelta),
                () =>
                {
                    target.alpha = finishAlpha;
                    onFinish?.Invoke();
                })
            );
        }

        public static TweenHandle TweenColor(MonoBehaviour caller, TweenTimeMode mode, UnityEngine.UI.Graphic target,
            Color startColor, Color finishColor, float duration, Action onFinish = null)
        {
            return StartTween(caller, Tween_Coroutine(mode, duration,
                tickDelta => target.color = Color.Lerp(startColor, finishColor, tickDelta),
                () =>
                {
                    target.color = finishColor;
                    onFinish?.Invoke();
                })
            );
        }

        public static TweenHandle TweenAnchoredPosition(MonoBehaviour caller, TweenTimeMode mode, RectTransform target,
            Vector2 startPosition, Vector2 finishPosition, float duration, Action onFinish = null)
        {
            return StartTween(caller, Tween_Coroutine(mode, duration,
                tickDelta => target.anchoredPosition = Vector2.Lerp(startPosition, finishPosition, tickDelta),
                () =>
                {
                    target.anchoredPosition = finishPosition;
                    onFinish?.Invoke();
                })
            );
        }

        public static TweenHandle TweenSprite(MonoBehaviour caller, TweenTimeMode mode, UnityEngine.UI.Image image,
            Sprite[] sprites, float frameDuration, Action onFinish = null)
        {
            int totalFrames = sprites.Length;
            float duration = totalFrames * frameDuration;

            return StartTween(caller, Tween_Coroutine(mode, duration,
                tickDelta =>
                {
                    int currentFrame = Mathf.FloorToInt(tickDelta * duration / frameDuration);
                    currentFrame = Mathf.Clamp(currentFrame, 0, totalFrames - 1);
                    image.sprite = sprites[currentFrame];
                },
                () => onFinish?.Invoke())
            );
        }

        public static TweenHandle Tween(MonoBehaviour caller, TweenTimeMode mode, float duration, Action<float> onTick,
            Action onFinish = null)
        {
            return StartTween(caller, Tween_Coroutine(mode, duration,
                tickDelta => onTick?.Invoke(tickDelta),
                () => onFinish?.Invoke())
            );
        }

        public static void InterruptTween(TweenHandle tweenHandle, Action onInterrupted = null)
        {
            if (tweenHandle is { IsActive: true })
            {
                tweenHandle.Interrupt();
                onInterrupted?.Invoke();
            }
        }
        #endregion

        #region PRIVATE METHODS
        private static TweenHandle StartTween(MonoBehaviour caller, IEnumerator coroutine)
        {
            if (caller == null)
            {
                return null;
            }

            Coroutine unityCoroutine = caller.StartCoroutine(coroutine);
            return new TweenHandle(caller, unityCoroutine);
        }

        private static IEnumerator Tween_Coroutine(TweenTimeMode mode, float duration, Action<float> onTick,
            Action onFinish = null)
        {
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                onTick?.Invoke(elapsedTime / duration);

                elapsedTime += mode switch
                {
                    TweenTimeMode.ScaledTime => Time.deltaTime,
                    TweenTimeMode.UnscaledTime => Time.unscaledDeltaTime,
                    _ => Time.deltaTime
                };

                yield return null;
            }

            onFinish?.Invoke();
        }
        #endregion
    }
}