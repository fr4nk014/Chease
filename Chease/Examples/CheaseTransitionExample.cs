using UnityEngine;

using Chease; // Make sure this namespace is included.

public class CheaseTransitionExample : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;

    [Header("User Setting")]
    [SerializeField] private CheaseTween.TransitionData userSetting = new(CheaseTween.TransformationType.Position, Vector3.one, CheaseTween.EasingMode.OutBack, 1f, 0f, true);


    float userTimeScale = 1f;

    private void UserSetting()
    {
        CheaseTween.TweenTransform(targetTransform, userSetting);
    }

    private void RandomPosition()
    {
        // Giving settings as parameters, instead of settings
        Vector3 randomPosition = 3f * Random.insideUnitSphere;
        CheaseTween.TweenTransform
        (
            targetTransform,
            CheaseTween.TransformationType.Position,
            randomPosition,
            CheaseTween.EasingMode.InOutBack,
            1f
        );
    }
    private void RandomScale()
    {
        Vector3 randomScale = new Vector3(
            Random.Range(.5f, 1.5f),
            Random.Range(.5f, 1.5f),
            Random.Range(.5f, 1.5f)
        );

        // Giving settings as a new transition data
        CheaseTween.TransitionData transitionData = new CheaseTween.TransitionData(
            CheaseTween.TransformationType.LocalScale,
            randomScale,
            CheaseTween.EasingMode.OutElastic,
            1.5f,
            0f,
            false
        );

        // Now we just pass the data in
        CheaseTween.TweenTransform(targetTransform, transitionData);
    }
    private void RandomRotation()
    {
        Vector3 randomRotation = new Vector3(
            Random.Range(-90f, 90f),
            Random.Range(-90f, 90f),
            Random.Range(-90f, 90f)
        );

        // Giving settings as a new transition data
        CheaseTween.TransitionData transitionData = new CheaseTween.TransitionData(
            CheaseTween.TransformationType.Rotation,
            randomRotation,
            CheaseTween.EasingMode.InOutBack,
            1f,
            0f,
            false
        );

        // Now we just pass the data in
        CheaseTween.TweenTransform(targetTransform, transitionData);
    }


    void OnGUI()
    {
        GUILayout.Label("Setting Funtion");
        GUILayout.Label("(Edit this in the inspector)");

        if (GUILayout.Button("Animate with Setting"))
        {
            UserSetting();
        }
        if (GUILayout.Button("Center"))
        {
            CheaseTween.TweenTransform(targetTransform, CheaseTween.TransformationType.Position, Vector3.zero, CheaseTween.EasingMode.InOutBack, 1f, 0f, false);
            CheaseTween.TweenTransform(targetTransform, CheaseTween.TransformationType.LocalScale, Vector3.one, CheaseTween.EasingMode.OutElastic, 1f, 0f, false);
            CheaseTween.TweenTransform(targetTransform, CheaseTween.TransformationType.Rotation, Vector3.zero, CheaseTween.EasingMode.InOutQuadratic, 1f, 0f, false);
        }

        GUILayout.Space(30f);
        GUILayout.Label("(Or try these)");
        GUILayout.Label("Random Functions");

        if (GUILayout.Button("Random Position"))
        {
            RandomPosition();
        }
        if (GUILayout.Button("Random Scale"))
        {
            RandomScale();
        }
        if (GUILayout.Button("Random Rotation"))
        {
            RandomRotation();
        }
        if (GUILayout.Button("Random Everything"))
        {
            RandomPosition();
            RandomScale();
            RandomRotation();
        }


        GUILayout.Space(30f);
        GUILayout.Label($"Timescale = {userTimeScale.ToString("f2")}");
        userTimeScale = GUILayout.HorizontalSlider(userTimeScale, .1f, 2f);
        if (GUILayout.Button("Reset"))
        {
            userTimeScale = 1f;
        }
        Time.timeScale = userTimeScale;
    }
}
