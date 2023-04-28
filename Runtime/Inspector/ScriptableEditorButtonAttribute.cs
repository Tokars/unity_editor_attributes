using UnityEngine;

namespace OT.Attributes
{
    /// <summary> Scriptable Object simple button for method without parameters </summary>
    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class ScriptableEditorButtonAttribute : PropertyAttribute
    {
    }
}