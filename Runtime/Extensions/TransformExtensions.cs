using System.Collections;
using UnityEngine;

namespace Botaemic.Core.Extensions
{
    public static class TransformExtensions
    {
        /// <summary>
        /// Return a Vector3, the direction from source to destination
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static Vector3 DirectionTo(this Transform source, Vector3 destination)
        {
            return source.position.DirectionTo(destination);
        }

        /// <summary>
        /// Return a normalized Vector3, the direction from source to destination
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static Vector3 NormalDirectionTo(this Transform source, Vector3 destination)
        {
            return source.position.NormalDirectionTo(destination);
        }

        /// <summary>
        /// Return a Vector3, the direction from source to destination
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static Vector3 DirectionTo(this Transform source, Transform destination)
        {
            return source.DirectionTo(destination.position);
        }

        /// <summary>
        /// Return a normalized Vector3, the direction from source to destination
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static Vector3 NormalDirectionTo(this Transform source, Transform destination)
        {
            return source.NormalDirectionTo(destination.position);
        }

        /// <summary>
        /// Return a float. the distance between to points
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static Vector3 DirectionTo(this Transform source, GameObject destination)
        {
            return source.DirectionTo(destination.transform.position);
        }

        /// <summary>
        /// Return a normalized Vector3, the direction from source to destination
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static Vector3 NormalDirectionTo(this Transform source, GameObject destination)
        {
            return source.NormalDirectionTo(destination.transform.position);
        }

        /// <summary>
        /// Return a float. the distance between to points
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static float DistanceTo(this Transform source, Vector3 destination)
        {
            return source.position.DistanceTo(destination);
        }

        /// <summary>
        /// Return a float. the distance between to points
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static float DistanceTo(this Transform source, Transform destination)
        {
            return source.position.DistanceTo(destination.position);
        }

        /// <summary>
        /// Return a float. the distance between to points
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static float DistanceTo(this Transform source, GameObject destination)
        {
            return source.DistanceTo(destination.transform);
        }

        public static void ScaleTo(
            this Transform transform,
            float size,
            MonoBehaviour mono,
            float duration,
            System.Action<Transform> callback = null)
        {
            mono.StartCoroutine(GrowTransformCoroutine(transform, size, duration, callback));
        }


        private static IEnumerator GrowTransformCoroutine(Transform transform, float size, float duration, System.Action<Transform> callback)
        {
            float start = Time.time;
            Vector3 currentScale = transform.localScale;
            Vector3 desiredScale = Vector3.one * size;
            Vector3 growStep = desiredScale - currentScale;
            while (Time.time <= start + duration)
            {
                float step = Mathf.Clamp01((Time.time - start) / duration);

                Vector3 growFactor = growStep * step;
                transform.localScale = currentScale + growFactor;
                yield return new WaitForEndOfFrame();
            }

            transform.localScale = desiredScale;

            callback?.Invoke(transform);
            yield return null;
        }

    }
}
