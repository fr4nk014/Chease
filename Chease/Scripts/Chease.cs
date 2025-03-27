using System;
using System.Collections.Generic;
using UnityEngine;
namespace Chease
{
    public static class CheaseTween
    {
        [Serializable]
        public class TransitionData
        {
            public TransformationType _Type;
            public Vector3 _EndVector;
            public EasingMode _EasingType;
            public float _TransitionDuration;
            public float _Delay;
            public bool _UnscaledTime;

            /// <summary>
            /// Transition settings for automated transform manipulation.
            /// </summary>
            public TransitionData(TransformationType _type, Vector3 _endvector, EasingMode _easingtype, float _duration, float _delay, bool _unscaledTime)
            {
                _Type = _type;
                _EndVector = _endvector;
                _EasingType = _easingtype;
                _TransitionDuration = _duration;
                _Delay = _delay;
                _UnscaledTime = _unscaledTime;
            }
        }

        /// <summary>
        /// Method of easing.
        /// </summary>
        public enum EasingMode
        {
            Linear,
            InBack, OutBack, InOutBack,
            InBounce, OutBounce, InOutBounce,
            InCircular, OutCircular, InOutCircular,
            InCubic, OutCubic, InOutCubic,
            InElastic, OutElastic, InOutElastic,
            InExponential, OutExponential, InOutExponential,
            InQuadratic, OutQuadratic, InOutQuadratic,
            InQuartic, OutQuartic, InOutQuartic,
            InQuintic, OutQuintic, InOutQuintic,
            InSine, OutSine, InOutSine
        }

        /// <summary>
        /// Part of transform to manipulate.
        /// </summary>
        public enum TransformationType
        {
            Position,
            Rotation,
            LocalPosition,
            LocalRotation,
            LocalScale
        }

        public static void TweenTransform(Transform targetTransform, TransformationType type, Vector3 end_vector, EasingMode easing_mode, float transition_time, float delay = 0f, bool unscaled_time = false)
        {
            AnimateTransformTween(type, targetTransform, end_vector, easing_mode, transition_time, delay, unscaled_time);
        }
        public static void TweenTransform(Transform targetTransform, TransitionData settings)
        {
            AnimateTransformTween(settings._Type, targetTransform, settings._EndVector, settings._EasingType, settings._TransitionDuration, settings._Delay, settings._UnscaledTime);
        }

        private static void AnimateTransformTween(TransformationType type, Transform targetTransform, Vector3 end_position, EasingMode easing_mode, float transition_time, float delay, bool unscaled_time = false)
        {
            if (targetTransform == null)
            {
                Debug.LogError("CheaseTween: Target transform was null.");
                return;
            }
            GameObject g = targetTransform.gameObject;

            CheaseAnimator mover = g.AddComponent<CheaseAnimator>();
            mover.InitAnimation(type, end_position, easing_mode, transition_time, delay, unscaled_time);
        }

        public static Func<float, float> GetEasingFunction(EasingMode mode)
        {
            return easingMap.TryGetValue(mode, out var func) ? func : null;
        }

        private static readonly Dictionary<EasingMode, Func<float, float>> easingMap =
        new Dictionary<EasingMode, Func<float, float>>()
        {
            { EasingMode.Linear, Functions.Linear },

            { EasingMode.InSine, Functions.In.Sine },
            { EasingMode.OutSine, Functions.Out.Sine },
            { EasingMode.InOutSine, Functions.InOut.Sine },

            { EasingMode.InCubic, Functions.In.Cubic },
            { EasingMode.OutCubic, Functions.Out.Cubic },
            { EasingMode.InOutCubic, Functions.InOut.Cubic },

            { EasingMode.InQuintic, Functions.In.Quintic },
            { EasingMode.OutQuintic, Functions.Out.Quintic },
            { EasingMode.InOutQuintic, Functions.InOut.Quintic },

            { EasingMode.InCircular, Functions.In.Circular },
            { EasingMode.OutCircular, Functions.Out.Circular },
            { EasingMode.InOutCircular, Functions.InOut.Circular },

            { EasingMode.InElastic, Functions.In.Elastic },
            { EasingMode.OutElastic, Functions.Out.Elastic },
            { EasingMode.InOutElastic, Functions.InOut.Elastic },

            { EasingMode.InQuadratic, Functions.In.Quadratic },
            { EasingMode.OutQuadratic, Functions.Out.Quadratic },
            { EasingMode.InOutQuadratic, Functions.InOut.Quadratic },

            { EasingMode.InQuartic, Functions.In.Quartic },
            { EasingMode.OutQuartic, Functions.Out.Quartic },
            { EasingMode.InOutQuartic, Functions.InOut.Quartic },

            { EasingMode.InExponential, Functions.In.Exponential },
            { EasingMode.OutExponential, Functions.Out.Exponential },
            { EasingMode.InOutExponential, Functions.InOut.Exponential },

            { EasingMode.InBack, Functions.In.Back },
            { EasingMode.OutBack, Functions.Out.Back },
            { EasingMode.InOutBack, Functions.InOut.Back },

            { EasingMode.InBounce, Functions.In.Bounce },
            { EasingMode.OutBounce, Functions.Out.Bounce },
            { EasingMode.InOutBounce, Functions.InOut.Bounce },
        };

        public static class Functions
        {
            public static float Linear(float t)
            {
                return t;
            }

            public static class In
            {
                public static float Sine(float t)
                {
                    return 1f - Mathf.Cos((t * Mathf.PI) / 2f);
                }
                public static float Cubic(float t)
                {
                    return t * t * t;
                }
                public static float Quintic(float t)
                {
                    return t * t * t * t * t;
                }
                public static float Circular(float t)
                {
                    return 1f - Mathf.Sqrt(1f - Mathf.Pow(t, 2f));
                }
                public static float Elastic(float t)
                {
                    const float n = (2f * Mathf.PI) / 3f;

                    return t == 0 ? 0 : t == 1f ? 1f : -Mathf.Pow(2f, 10f * t - 10f) * Mathf.Sin((t * 10f - 10.75f) * n);
                }
                public static float Quadratic(float t)
                {
                    return t * t;
                }
                public static float Quartic(float t)
                {
                    return t * t * t * t;
                }
                public static float Exponential(float t)
                {
                    return t == 0 ? 0 : Mathf.Pow(2f, 10f * t - 10f);
                }
                public static float Back(float t)
                {
                    const float n1 = 1.70158f;
                    const float n2 = n1 + 1;

                    return n2 * t * t * t - n1 * t * t;
                }
                public static float Bounce(float t)
                {
                    return 1 - Out.Bounce(1f - t);
                }
            }
            public static class Out
            {
                public static float Sine(float t)
                {
                    return Mathf.Sin((t * Mathf.PI) / 2f);
                }
                public static float Cubic(float t)
                {
                    return 1f - Mathf.Pow(1f - t, 3f);
                }
                public static float Quintic(float t)
                {
                    return 1f - Mathf.Pow(1f - t, 5f);
                }
                public static float Circular(float t)
                {
                    return Mathf.Sqrt(1 - Mathf.Pow(t - 1f, 2f));
                }
                public static float Elastic(float t)
                {
                    const float n = (2f * Mathf.PI) / 3f;

                    return t == 0 ? 0 : t == 1 ? 1 : Mathf.Pow(2, -10 * t) * Mathf.Sin((t * 10f - 0.75f) * n) + 1f;
                }
                public static float Quadratic(float t)
                {
                    return 1f - (1f - t) * (1f - t);
                }
                public static float Quartic(float t)
                {
                    return 1f - Mathf.Pow(1f - t, 4f);
                }
                public static float Exponential(float t)
                {
                    return t == 1f ? 1f : 1f - Mathf.Pow(2f, -10f * t);
                }
                public static float Back(float t)
                {
                    const float n1 = 1.70158f;
                    const float n2 = n1 + 1;

                    return 1f + n2 * Mathf.Pow(t - 1f, 3f) + n1 * Mathf.Pow(t - 1f, 2f);
                }
                public static float Bounce(float t)
                {
                    const float n1 = 7.5625f;
                    const float n2 = 2.75f;

                    if (t < 1 / n2)
                    {
                        return n1 * t * t;
                    }
                    else if (t < 2 / n2)
                    {
                        return n1 * (t -= 1.5f / n2) * t + 0.75f;
                    }
                    else if (t < 2.5f / n2)
                    {
                        return n1 * (t -= 2.25f / n2) * t + 0.9375f;
                    }
                    else
                    {
                        return n1 * (t -= 2.625f / n2) * t + 0.984375f;
                    }
                }
            }
            public static class InOut
            {
                public static float Sine(float t)
                {
                    return -(Mathf.Cos(Mathf.PI * t) - 1f) / 2f;
                }
                public static float Cubic(float t)
                {
                    return t < 0.5f ? 4f * t * t * t : 1f - Mathf.Pow(-2f * t + 2f, 3f) / 2f;
                }
                public static float Quintic(float t)
                {
                    return t < 0.5f ? 16f * t * t * t * t * t : 1f - Mathf.Pow(-2f * t + 2f, 5f) / 2f;
                }
                public static float Circular(float t)
                {
                    return t < 0.5 ? (1 - Mathf.Sqrt(1 - Mathf.Pow(2f * t, 2f))) / 2f : (Mathf.Sqrt(1f - Mathf.Pow(-2f * t + 2f, 2f)) + 1f) / 2f;
                }
                public static float Elastic(float t)
                {
                    const float n = (2f * Mathf.PI) / 4.5f;

                    return t == 0 ? 0 : t == 1f ? 1 : t < 0.5f ? -(Mathf.Pow(2f, 20f * t - 10f) * Mathf.Sin((20f * t - 11.125f) * n)) / 2f
                      : (Mathf.Pow(2f, -20f * t + 10f) * Mathf.Sin((20f * t - 11.125f) * n)) / 2f + 1f;
                }
                public static float Quadratic(float t)
                {
                    return t < 0.5f ? 2f * t * t : 1f - Mathf.Pow(-2f * t + 2f, 2f) / 2f;
                }
                public static float Quartic(float t)
                {
                    return t < 0.5f ? 8f * t * t * t * t : 1f - Mathf.Pow(-2f * t + 2f, 4f) / 2f;
                }
                public static float Exponential(float t)
                {
                    return t == 0 ? 0 : t == 1 ? 1 : t < 0.5 ? Mathf.Pow(2, 20 * t - 10) / 2 : (2 - Mathf.Pow(2, -20 * t + 10)) / 2;
                }
                public static float Back(float t)
                {
                    const float n1 = 1.70158f;
                    const float n2 = n1 * 1.525f;

                    return t < 0.5f ? (Mathf.Pow(2 * t, 2) * ((n2 + 1) * 2 * t - n2)) / 2
                      : (Mathf.Pow(2 * t - 2, 2) * ((n2 + 1) * (t * 2 - 2) + n2) + 2) / 2;
                }
                public static float Bounce(float t)
                {
                    return t < 0.5 ? (1 - Out.Bounce(1 - 2 * t)) / 2 : (1 + Out.Bounce(2 * t - 1)) / 2;
                }
            }
        }

    }
}
