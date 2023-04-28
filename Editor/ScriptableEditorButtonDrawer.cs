using UnityEditor;
using UnityEngine;

namespace OT.Attributes.Editor
{
    [CustomEditor(typeof(ScriptableObject), true)]
    [CanEditMultipleObjects]
    public class
        ScriptableEditorButtonDrawer : BaseEditorButtonDrawer<ScriptableObject, ScriptableEditorButtonAttribute>
    {
        private void OnEnable()
        {
            _t = target as ScriptableObject;
        }
    }
}