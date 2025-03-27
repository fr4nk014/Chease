# Chease
Easy to use solution for tweening in Unity.

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

### How to Install
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

