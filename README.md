# Chease
Easy to use solution for tweening in Unity.

![CheaseShowcase](https://github.com/user-attachments/assets/6e859586-36a5-4965-8538-2a7aca6c30fd)

## About
Chease aims to make tweening (or easing) in Unity as easy as possible. Offering both single-line automated transform animations (position, rotation, scale) and direct access to the functions themselves for manipulating any values.

Available easing methods:
- Linear (no easing)
- InBack OutBack, InOutBack
- InBounce, OutBounce, InOutBounce
- InCircular, OutCircular, InOutCircular
- InCubic, OutCubic, InOutCubic
- InElastic, OutElastic, InOutElastic
- InExponential, OutExponential, InOutExponential
- InQuadratic, OutQuadratic, InOutQuadratic
- InQuartic, OutQuartic, InOutQuartic
- InQuintic, OutQuintic, InOutQuintic,
- InSine, OutSine, InOutSine

Check out [easings](https://easings.net) for a handy visualization of them in action.

## How to Install
Download the latest release and just drag and drop it in to Unity. You can choose to not include the 'Examples' folder.


## How to Use

The unitypackage includes examples which you can study.

Make sure you include the namespace "Chease" in the scripts you use Chease in. 
```cs
using Chease;
```

### Automatic Transform Tweening
You can move, rotate or scale any transform automatically with a single line:
```cs
CheaseTween.TweenTransform(targetTransform, settings);
```


There are 2 options for the automated movement.

#### Method 1: TransitionData class
```cs
// Creating a new transition data object
CheaseTween.TransitionData transitionData = new CheaseTween.TransitionData
(
	CheaseTween.TransformationType.LocalScale,
	new Vector3(1f, 2f, 3f),
	CheaseTween.EasingMode.OutElastic,
	1.5f,
	false
);

// Moving the object with the data
CheaseTween.TweenTransform(targetTransform, transitionData);
```
TransitionData is serialized so you can also set the settings in the inspector.
![SerializedData_Inspector](https://github.com/user-attachments/assets/88fe5b3e-2870-4e80-8237-88435bb10595)


#### Method 2: Directly inputting the settings
```cs
CheaseTween.TweenTransform(targetTransform, CheaseTween.PresetType.Position, randomPosition, CheaseTween.EasingMode.InOutBack, 1f, false);
```

### Using functions manually
It is also easy to use the functions manually. This is suitable for more robust applications where any value needs to change.

Example use in a Coroutine:
#### Method 1: Storing the function
```cs
using System;
using Chease;

float _t = 0f;
float startX = 0f;
float endX = 5f;

// Store the function beforehand, to use it later
Func<float, float> easingFunc = CheaseTween.GetEasingFunction(CheaseTween.EasingMode.OutCircular);

while (_t < 1f)
{
	_t += Time.deltaTime;

	// Using the stored function
	float lerpValue = easingFunc(_t);

	float xPos = Mathf.LerpUnclamped(startX, endX, lerpValue);
	targetTransform.position = new Vector3(xPos, targetTransform.position.y, 0f);

	yield return null;
}
```
#### Method 2: Accessing the functions directly
```cs
using Chease;

Vector3 startScale = Vector3.one;
Vector3 endScale = new Vector3(3f, 2f, 1f);
_t = 0f;

while (_t < 1f)
{
 	_t += Time.deltaTime;

	// Using the function directly
	float lerpValue = CheaseTween.Functions.Out.Elastic(_t);

	targetTransform.localScale = Vector3.LerpUnclamped(startScale, endScale, lerpValue);
	yield return null;
}
```
