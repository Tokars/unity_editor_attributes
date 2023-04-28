using UnityEngine;

namespace OT.Attributes
{
    /// <summary> MonoBehavior Simple button for method without parameters </summary>
    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class EditorButtonAttribute : PropertyAttribute
    {
    }
}