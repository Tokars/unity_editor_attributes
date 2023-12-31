﻿using UnityEditor;
using UnityEngine;

namespace OT.Attributes.Editor
{
    /*
     * @author: malabarMCB
     * https://github.com/malabarMCB/unity-readonly-inspector-properties
     */
    [CustomPropertyDrawer(typeof(ReadOnlyInInspectorAttribute))]
    public class ReadOnlyInInspectorDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
        {
            string valueStr;

            switch (prop.propertyType)
            {
                case SerializedPropertyType.Integer:
                    valueStr = prop.intValue.ToString();
                    break;

                case SerializedPropertyType.Boolean:
                    valueStr = prop.boolValue.ToString();
                    break;

                case SerializedPropertyType.Float:
                    valueStr = prop.floatValue.ToString("0.00000");
                    break;

                case SerializedPropertyType.String:
                    valueStr = prop.stringValue;
                    break;

                case SerializedPropertyType.Enum:
                    valueStr = prop.enumNames[prop.enumValueIndex];
                    break;

                case SerializedPropertyType.Vector2:
                    valueStr = prop.vector2Value.ToString();
                    break;

                case SerializedPropertyType.Vector3:
                    valueStr = prop.vector3Value.ToString();
                    break;

                case SerializedPropertyType.Vector4:
                    valueStr = prop.vector4Value.ToString();
                    break;

                default:
                    valueStr = "(not supported)";
                    break;
            }

            EditorGUI.LabelField(position, label.text, valueStr);
        }
    }
}