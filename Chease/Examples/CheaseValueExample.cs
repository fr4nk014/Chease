using UnityEngine;
using System.Collections;
using Chease; // Make sure this namespace is included.
using System;
using Random = UnityEngine.Random;

public class CheaseValueExample : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;

    [SerializeField] private CheaseTween.EasingMode easingModeForPosition = CheaseTween.EasingMode.OutBounce;

    private void Start()
    {
        StartCoroutine(Animate());
    }

    float gui_tValue;
    float gui_lerpValue;

    IEnumerator Animate()
    {
        while (true)
        {
            // Method 1: Store a function

            float _t = 0f;
            float startX = targetTransform.position.x;
            float endX = Random.Range(-5f, 5f);

            // Store the function beforehand, to use it later
            Func<float, float> easingFunc = CheaseTween.GetEasingFunction(easingModeForPosition);

            while (_t < 1f)
            {
                _t += Time.deltaTime;

                //                v - Using the stored function here
                float lerpValue = easingFunc(_t);

                gui_tValue = _t;
                gui_lerpValue = lerpValue;

                float xPos = Mathf.LerpUnclamped(startX, endX, lerpValue);
                targetTransform.position = new Vector3(xPos, targetTransform.position.y, 0f);

                yield return null;
            }


            // Method 2: Use a function directly (Do NOT use GetEasingFunction for this)

            Vector3 startScale = targetTransform.localScale;
            Vector3 endScale = new Vector3(Random.Range(.5f, 3f), Random.Range(.5f, 3f), Random.Range(.5f, 3f));
            _t = 0f;
            while (_t < 1f)
            {
                _t += Time.deltaTime;

                // You can also use the functions directly if the transition type doesn't need to be dynamic (Optimal)
                float lerpValue = CheaseTween.Functions.Out.Elastic(_t);

                gui_tValue = _t;
                gui_lerpValue = lerpValue;

                targetTransform.localScale = Vector3.LerpUnclamped(startScale, endScale, lerpValue);

                yield return null;
            }
        }

    }

    void OnGUI()
    {
        GUILayout.Label($"Time = {gui_tValue.ToString("f2")}");
        GUILayout.Label($"Lerp = {gui_lerpValue.ToString("f2")}");
    }


}
