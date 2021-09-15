# Unity-Built-In-Attributes
A list of built in Unity Attributes.
* [Property Inspector](#property-inspector)
* [Serialization](#serialization)
* [Component Related](#component-related)


```c#
[HideInInspector][SerializeField] public int score;
// can be
[HideInInspector, SerializeField] public int score;
```

This is not a complete list of all the attributes available.

# Property Inspector
[Header](https://docs.unity3d.com/ScriptReference/HeaderAttribute.html): 
Shows a label in the inspector.
```c#
[Header("Stats")]
public int health = 100;
public float speed = 0f;
[Header("Items")]
public int ammo = 10;
```

[Space](https://docs.unity3d.com/ScriptReference/SpaceAttribute.html): 
Adds space between inspector elements.
```c#
public float item1 = 0f;
[Space(10)]
public float item2 = 0f;
```

[Tooltip](https://docs.unity3d.com/ScriptReference/TooltipAttribute.html): 
Shows a tooltip on mouse over.
```c#
[Tooltip("The games score.")] public int score = 0;
```

[HideInInspector](https://docs.unity3d.com/ScriptReference/HideInInspector.html): 
Stops showing the property in the inspector.
```c#
[HideInInspector] public bool reset = false;
```

[Range](https://docs.unity3d.com/ScriptReference/RangeAttribute.html): 
Limit the range of a float or int.
```c#
[Range(0, 100)] public float speed = 2f;
```

[Min](https://docs.unity3d.com/ScriptReference/MinAttribute.html): 
Limit minimum value of float or int.
```c#
[Min(1.0f)] public float speed = 2.0;
```

[TextArea](https://docs.unity3d.com/ScriptReference/TextAreaAttribute.html): 
Creates flexible scrollable text area.
```c#
[TextArea] public string description = "";
```

[Multiline](https://docs.unity3d.com/ScriptReference/MultilineAttribute.html): 
Show more than one lines.
```c#
[Multiline(4)] public string description = "";
```

[ColorUsage](https://docs.unity3d.com/ScriptReference/ColorUsageAttribute.html): 
Attribute used to configure the usage of the ColorField and Color Picker for a color.
```c#
[ColorUsage(true, true)] public Color color = Color.red;
```

[GradientUsage](https://docs.unity3d.com/ScriptReference/GradientUsageAttribute.html): 
Use on a gradient to configure the GradientField.
```c#
[GradientUsage(true)] public Gradient gradient;
```

[InspectorName](https://docs.unity3d.com/ScriptReference/InspectorNameAttribute.html): 
Use this attribute on enum value declarations to change the display name shown in the Inspector.
```c#
public enum ModelImporterIndexFormat
{
    Auto = 0,
    [InspectorName("16 bits")]
    UInt16 = 1,
    [InspectorName("32 bits")]
    UInt32 = 2,
}
```

# Serialization
[SerializeField](https://docs.unity3d.com/ScriptReference/SerializeField.html): 
Serialize a private field to show it in the inspector.
```c#
[SerializeField] private int score;
```

[Serializable](https://docs.unity3d.com/ScriptReference/Serializable.html): 
Make a class Serializable so it will be visible in the inspector.
```c#
[System.Serializable]
public class ClassA
{
    public int score = 10;
    public Color color = Color.red;
}
```

[FormerlySerializedAs](https://docs.unity3d.com/ScriptReference/Serialization.FormerlySerializedAsAttribute.html): 
If you changed the name of a serialized property, you can set this to the old name, so save data will still work.
```c#
[FormerlySerializedAs("value")] private string m_value;
```

# Component Related
[RequireComponent](https://docs.unity3d.com/ScriptReference/RequireComponent.html): 
The RequireComponent attribute automatically adds required components as dependencies.
```c#
[RequireComponent(typeof(RigidBody))]
[RequireComponent(typeof(Component1), typeof(Component2), typeof(Component3))]  // You can enter multiple components into attribute.
public class ClassA : MonoBehaviour
{
}
```

[ExecuteInEditMode](https://docs.unity3d.com/ScriptReference/ExecuteInEditMode.html): 
Will call MonoBehaviour methods like Update and OnEnable while in EditMode.
```c#
[ExecuteInEditMode]
public class ClassA : MonoBehaviour
{
}
```

[SelectionBase](https://docs.unity3d.com/ScriptReference/SelectionBaseAttribute.html): 
Will select the top most parent of the GameObject when a sub object is selected in the editor.
```c#
[SelectionBase]
public class ClassA : MonoBehaviour
{
}
```