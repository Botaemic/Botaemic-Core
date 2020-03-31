using System.Collections;
using UnityEngine;

namespace Botaemic.Core.Extensions
{
    public static class SpriteRendererExtension
    {
        public static void FadeOutSprite(this SpriteRenderer renderer, MonoBehaviour mono, float duration, System.Action<SpriteRenderer> callback = null)
        {
            mono.StartCoroutine(FadeOutSpriteCoroutine(renderer, duration, callback));
        }

        public static void FadeInSprite(this SpriteRenderer renderer, MonoBehaviour mono, float duration, System.Action<SpriteRenderer> callback = null)
        {
            mono.StartCoroutine(FadeInSpriteCoroutine(renderer, duration, callback));
        }

        private static IEnumerator FadeOutSpriteCoroutine(SpriteRenderer renderer, float duration, System.Action<SpriteRenderer> callback)
        {
            float start = Time.time;
            while (Time.time <= start + duration)
            {
                Color color = renderer.color;
                color.a = 1f - Mathf.Clamp01((Time.time - start) / duration);
                renderer.color = color;
                yield return new WaitForEndOfFrame();
            }

            Color finalColor = renderer.color;
            finalColor.a = 0f;
            renderer.color = finalColor;

            callback?.Invoke(renderer);
            yield return null;
        }

        private static IEnumerator FadeInSpriteCoroutine(SpriteRenderer renderer, float duration, System.Action<SpriteRenderer> callback)
        {
            float start = Time.time;
            while (Time.time <= start + duration)
            {
                Color color = renderer.color;
                color.a = 0f + Mathf.Clamp01((Time.time - start) / duration);
                renderer.color = color;
                yield return new WaitForEndOfFrame();
            }

            Color finalColor = renderer.color;
            finalColor.a = 1f;
            renderer.color = finalColor;

            callback?.Invoke(renderer);
            yield return null;
        }
    }
}
