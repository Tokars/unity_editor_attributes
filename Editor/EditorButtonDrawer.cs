using UnityEditor;
using UnityEngine;

namespace OT.Attributes.Editor
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    [CanEditMultipleObjects]
    public class EditorButtonDrawer : BaseEditorButtonDrawer<MonoBehaviour, EditorButtonAttribute>
    {
        private void OnEnable()
        {
            _t = target as MonoBehaviour;
        }
    }
}