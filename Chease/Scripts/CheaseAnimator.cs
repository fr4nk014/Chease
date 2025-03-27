using System;
using System.Collections;
using Chease;
using UnityEngine;
public class CheaseAnimator : MonoBehaviour
{
    public bool HasStarted = false;
    public CheaseTween.TransformationType PresetType { get; private set; }
    Func<float, float> EaseFunc;
    Vector3 EndVector;
    float TransitionTime;
    float Delay;
    bool UnscaledTime;

    public void InitAnimation(CheaseTween.TransformationType type, Vector3 end, CheaseTween.EasingMode easing_mode, float transition_time, float delay, bool unscaled_time)
    {
        PresetType = type;
        EndVector = end;
        EaseFunc = CheaseTween.GetEasingFunction(easing_mode);
        TransitionTime = transition_time;
        Delay = delay;
        UnscaledTime = unscaled_time;
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        if (UnscaledTime) yield return new WaitForSecondsRealtime(Delay);
        else yield return new WaitForSeconds(Delay);

        CheaseAnimator[] old_movers = transform.GetComponents<CheaseAnimator>();
        foreach (CheaseAnimator m in old_movers)
        {
            if (m.HasStarted && m.PresetType == PresetType && m != this) GameObject.Destroy(m);
        }
        HasStarted = true;
        Vector3 starting_vector = Vector3.zero;

        if (TransitionTime == 0f) TransitionTime = 0.0001f; // Divide by 0 prevention

        switch (PresetType)
        {
            case CheaseTween.TransformationType.Position: starting_vector = transform.position; break;
            case CheaseTween.TransformationType.LocalPosition: starting_vector = transform.localPosition; break;
            case CheaseTween.TransformationType.Rotation: starting_vector = transform.eulerAngles; break;
            case CheaseTween.TransformationType.LocalRotation: starting_vector = transform.localEulerAngles; break;
            case CheaseTween.TransformationType.LocalScale: starting_vector = transform.localScale; break;
            default: break;
        }


        float elapsed = 0f;
        while (elapsed < TransitionTime)
        {
            elapsed += UnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            float _t = elapsed / TransitionTime;

            if (_t > 1f) _t = 1f;

            float lerp = EaseFunc(_t);

            switch (PresetType)
            {
                case CheaseTween.TransformationType.Position: transform.position = Vector3.LerpUnclamped(starting_vector, EndVector, lerp); break;
                case CheaseTween.TransformationType.LocalPosition: transform.localPosition = Vector3.LerpUnclamped(starting_vector, EndVector, lerp); break;
                case CheaseTween.TransformationType.Rotation: transform.rotation = Quaternion.SlerpUnclamped(Quaternion.Euler(starting_vector), Quaternion.Euler(EndVector), lerp); break;
                case CheaseTween.TransformationType.LocalRotation: transform.localRotation = Quaternion.SlerpUnclamped(Quaternion.Euler(starting_vector), Quaternion.Euler(EndVector), lerp); break;
                case CheaseTween.TransformationType.LocalScale: transform.localScale = Vector3.LerpUnclamped(starting_vector, EndVector, lerp); break;
                default: break;
            }

            yield return null;
        }
        Destroy(this);
    }
}