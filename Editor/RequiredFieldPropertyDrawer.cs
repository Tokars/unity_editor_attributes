using Tok.CustomAttributes;
using UnityEditor;
using UnityEngine;

namespace Tok.Editor.CustomAttributes
{
    [CustomPropertyDrawer(typeof(RequiredFieldAttribute))]
    public class RequiredFieldPropertyDrawer : PropertyDrawer
    {
        private Color32 _errorColor = new Color32(255, 81, 49, 255);
        private Color _regColor;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty prop = property.serializedObject.FindProperty(property.propertyPath);
            _regColor = GUI.color;
            if (IsNull(prop))
                GUI.color = _errorColor;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.color = _regColor;
        }

        private static bool IsNull(SerializedProperty property)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.String:
                    return string.IsNullOrEmpty(property.stringValue);
                case SerializedPropertyType.ObjectReference:
                    return property.objectReferenceValue == null;
                case SerializedPropertyType.AnimationCurve:
                    return property.animationCurveValue.keys.Length > 0;
                case SerializedPropertyType.ExposedReference:
                    return property.exposedReferenceValue == null;
                default:
                    return false;
            }
        }
    }
}