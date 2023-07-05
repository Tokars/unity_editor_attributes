using UnityEditor;
using UnityEngine;

namespace OT.Attributes.Editor.RequiredField
{
    /// <summary>
    /// Drawerer for fields with RequiredFieldAttribute assigned.
    /// </summary>
    [CustomPropertyDrawer(typeof(RequiredFieldAttribute))]
    public class RequiredFieldAttributeDrawer : PropertyDrawer
    {
        private int warningHeight = 30;
        private Color32 _errorColor = new Color32(255, 81, 49, 255);

        /// <summary>
        /// Gets the height that is passed into the rect in OnGUI.
        /// </summary>
        /// <returns>The property height.</returns>
        /// <param name="property">Serialized property.</param>
        /// <param name="label">Label for the property.</param>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // The height for the object assignment is just whatever Unity would
            // do by default.
            float objectReferenceHeight = base.GetPropertyHeight(property, label);
            float calculatedHeight = objectReferenceHeight;

            bool shouldAddWarningHeight = property.propertyType != SerializedPropertyType.ObjectReference ||
                                          IsNotWiredUp(property);
            if (shouldAddWarningHeight)
            {
                // When it's not wired up we add in additional height for the warning text.
                calculatedHeight += warningHeight;
            }

            return calculatedHeight;
        }

        /// <summary>
        /// Raises the GUI event and draws the property.
        /// </summary>
        /// <param name="position">Position for the property.</param>
        /// <param name="property">Serialized property.</param>
        /// <param name="label">Label for the property.</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Position is the DrawArea of the property to be rendered. Includes height from GetHeight()
            // BeginProperty used for objects that don't handle [SerializeProperty] attribute.
            EditorGUI.BeginProperty(position, label, property);

            if (IsNotWiredUp(property))
                GUI.color = _errorColor;

            //Add GameObject instance ID to mark it with icon in hierarchy

            // Calculate ObjectReference rect size
            Rect objectReferenceRect = position;

            // Use Unity's default height calculation for the reference rectangle
            float objectReferenceHeight = base.GetPropertyHeight(property, label);
            objectReferenceRect.height = objectReferenceHeight;
            BuildObjectField(objectReferenceRect, property, label);
            GUI.color = Color.white;

            // Calculate warning rectangle's size
            Rect warningRect = new Rect(
                position.x,
                objectReferenceRect.y + objectReferenceHeight,
                position.width,
                warningHeight);
            BuildWarningRectangle(warningRect, property);

            EditorGUI.EndProperty();

            if (GUI.changed)
            {
                var comp = property.serializedObject.targetObject as Component;

                if (comp == null)
                    return;

                EditorApplication.RepaintHierarchyWindow();

                if (IsNotWiredUp(property))
                    RequiredFieldStaticTracker.AddInstanceID(comp.gameObject.GetInstanceID());
                else
                    RequiredFieldStaticTracker.RemoveInstanceID(comp.gameObject.GetInstanceID());
            }
        }

        private bool IsNotWiredUp(SerializedProperty property)
        {
            if (IsPropertyRequiredFieldInSceneAndPrefab(property))
            {
                return false;
            }

            return property.objectReferenceValue == null;
        }

        private bool IsPropertyRequiredFieldInSceneAndPrefab(SerializedProperty property)
        {
            RequiredFieldAttribute myAttribute = (RequiredFieldAttribute) attribute;
            bool isPrefabAllowedNull = myAttribute.IgnorePrefab;
            return IsPropertyOnPrefab(property) && isPrefabAllowedNull;
        }

        private bool IsPropertyOnPrefab(SerializedProperty property)
        {
            return EditorUtility.IsPersistent(property.serializedObject.targetObject);
        }

        private void BuildObjectField(Rect drawArea, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.ObjectReference)
            {
                EditorGUI.PropertyField(drawArea, property, label);
                return;
            }

            if (IsPropertyRequiredFieldInSceneAndPrefab(property))
            {
                // Render Object Field for RequiredField (InScene) attributes on Prefabs.
                //label.text = "(*) " + label.text;
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.ObjectField(drawArea, property, label);
                EditorGUI.EndDisabledGroup();
            }
            else
            {
                //label.text = "* " + label.text;
                EditorGUI.ObjectField(drawArea, property, label);
            }
        }

        private void BuildWarningRectangle(Rect drawArea, SerializedProperty property)
        {
            if (property.propertyType != SerializedPropertyType.ObjectReference)
            {
                string warningString = "RequiredField attribute only valid on ObjectReference fields.";
                EditorGUI.HelpBox(drawArea, warningString, MessageType.Warning);
            }
            else if (IsNotWiredUp(property))
            {
                string warningString = "Missing object reference for RequiredField property.";
                EditorGUI.HelpBox(drawArea, warningString, MessageType.Error);
            }
        }
    }
}