using System;
using System.Collections;
using Chease;
using UnityEngine;
public class CheaseAnimator : MonoBehaviour
{
    public CheaseTween.PresetType PresetType { get; private set; }
    Func<float, float> EaseFunc;
    Vector3 EndVector;
    float TransitionTime;
    bool UnscaledTime;

    public void InitAnimation(CheaseTween.PresetType type, Vector3 end, CheaseTween.EasingMode easing_mode, float transition_time, bool unscaled_time)
    {
        PresetType = type;
        EndVector = end;
        EaseFunc = CheaseTween.GetEasingFunction(easing_mode);
        TransitionTime = transition_time;
        UnscaledTime = unscaled_time;
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        Vector3 starting_vector = Vector3.zero;

        if (TransitionTime == 0f) TransitionTime = 0.0001f; // Divide by 0 prevention

        switch (PresetType)
        {
            case CheaseTween.PresetType.Position: starting_vector = transform.position; break;
            case CheaseTween.PresetType.LocalPosition: starting_vector = transform.localPosition; break;
            case CheaseTween.PresetType.Rotation: starting_vector = transform.eulerAngles; break;
            case CheaseTween.PresetType.LocalRotation: starting_vector = transform.localEulerAngles; break;
            case CheaseTween.PresetType.LocalScale: starting_vector = transform.localScale; break;
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
                case CheaseTween.PresetType.Position: transform.position = Vector3.LerpUnclamped(starting_vector, EndVector, lerp); break;
                case CheaseTween.PresetType.LocalPosition: transform.localPosition = Vector3.LerpUnclamped(starting_vector, EndVector, lerp); break;
                case CheaseTween.PresetType.Rotation: transform.rotation = Quaternion.SlerpUnclamped(Quaternion.Euler(starting_vector), Quaternion.Euler(EndVector), lerp); break;
                case CheaseTween.PresetType.LocalRotation: transform.localRotation = Quaternion.SlerpUnclamped(Quaternion.Euler(starting_vector), Quaternion.Euler(EndVector), lerp); break;
                case CheaseTween.PresetType.LocalScale: transform.localScale = Vector3.LerpUnclamped(starting_vector, EndVector, lerp); break;
                default: break;
            }

            yield return null;
        }
        Destroy(this);
    }
}