using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace OT.Attributes.Editor.RequiredField
{
    /// <summary>
    /// This class is responsible for checking objects for RequiredField violations.
    /// </summary>
    public static class RequiredFieldChecker
    {
        /// <summary>
        /// Finds the erroring NotNull fields on a GameObject.
        /// </summary>
        /// <returns>The erroring fields.</returns>
        /// <param name="sourceObject">Source object.</param>
        public static List<RequiredFieldViolation> FindErroringFields(GameObject sourceObject)
        {
            List<RequiredFieldViolation> erroringFields = new List<RequiredFieldViolation>();
            MonoBehaviour[] monobehaviours = sourceObject.GetComponents<MonoBehaviour>();
            for (int i = 0; i < monobehaviours.Length; i++)
            {
                try
                {
                    if (MonoBehaviourHasErrors(monobehaviours[i]))
                    {
                        List<RequiredFieldViolation> violationsOnMonoBehaviour = FindErroringFields(monobehaviours[i]);
                        erroringFields.AddRange(violationsOnMonoBehaviour);
                    }
                }
                catch (System.ArgumentNullException)
                {
                    // TODO: Handle missing monobehaviours
                }
            }

            return erroringFields;
        }

        private static List<RequiredFieldViolation> FindErroringFields(MonoBehaviour sourceMb)
        {
            if (sourceMb == null)
            {
                throw new System.ArgumentNullException("MonoBehaviour is null. It likely references" +
                                                       " a script that's been deleted.");
            }

            List<RequiredFieldViolation> erroringFields = new List<RequiredFieldViolation>();

            // Add null NotNull fields
            List<FieldInfo> notNullFields =
                ReflectionUtility.GetFieldsWithAttributeFromType<RequiredFieldAttribute>(
                    sourceMb.GetType(),
                    BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo notNullField in notNullFields)
            {
                object fieldObject = notNullField.GetValue(sourceMb);
                if (fieldObject == null || fieldObject.Equals(null))
                {
                    erroringFields.Add(new RequiredFieldViolation(notNullField, sourceMb));
                }
            }

            // Remove RequiredFieldViolations for prefabs with IgnorePrefab
            bool isObjectAPrefab = PrefabUtility.GetPrefabAssetType(sourceMb.gameObject) != PrefabAssetType.NotAPrefab;
            List<RequiredFieldViolation> violationsToIgnore = new List<RequiredFieldViolation>();
            if (isObjectAPrefab)
            {
                // Find all violations that should be overlooked.
                foreach (RequiredFieldViolation errorField in erroringFields)
                {
                    FieldInfo fieldInfo = errorField.FieldInfo;
                    foreach (Attribute attribute in Attribute.GetCustomAttributes(fieldInfo))
                    {
                        if (attribute.GetType() == typeof(RequiredFieldAttribute))
                        {
                            if (((RequiredFieldAttribute) attribute).IgnorePrefab)
                            {
                                violationsToIgnore.Add(errorField);
                            }
                        }
                    }
                }

                foreach (RequiredFieldViolation violation in violationsToIgnore)
                {
                    erroringFields.Remove(violation);
                }
            }

            return erroringFields;
        }

        private static bool MonoBehaviourHasErrors(MonoBehaviour mb)
        {
            return FindErroringFields(mb).Count > 0;
        }
    }
}