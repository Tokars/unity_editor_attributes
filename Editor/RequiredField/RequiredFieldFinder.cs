using System.Collections.Generic;
using OT.Attributes.Inspector.RequiredField;
using UnityEditor;
using UnityEngine;

namespace OT.Attributes.Editor.RequiredField
{
    /// <summary>
    /// NotNullFinder fires off checks for NotNull violations in the scene and asset database
    /// and reports their errors.
    /// </summary>
    public class RequiredFieldFinder : EditorWindow
    {
        private static bool outputLogs = false;

        /// <summary>
        /// Searchs for and error for not null violations in the scene and asset database
        /// </summary>
        public static bool SearchForAndErrorForRequiredFieldViolations()
        {
            // Debug.Log ("Searching for null NotNull fields");
            // Search for and error for prefabs with null RequireWire fields
            string[] guidsForAllGameObjects = AssetDatabase.FindAssets("t:GameObject");
            bool errorsFound = false;
            foreach (string guid in guidsForAllGameObjects)
            {
                Log("Loading GUID: " + guid);
                string pathToGameObject = AssetDatabase.GUIDToAssetPath(guid);

                Log("Loading Asset for guid at path: " + pathToGameObject);
                GameObject gameObject = (GameObject)AssetDatabase.LoadAssetAtPath(pathToGameObject, typeof(GameObject));

                errorsFound = errorsFound || ErrorForNullRequiredWiresOnGameObject(gameObject, pathToGameObject);
            }

            // Search the scene objects (only need root game objects since children will be searched)
            GameObject[] sceneGameObjects = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));
            List<GameObject> rootSceneGameObjects = new List<GameObject>();
            foreach (GameObject sceneGameObject in sceneGameObjects)
            {
                if (sceneGameObject.transform.parent == null)
                {
                    rootSceneGameObjects.Add(sceneGameObject);
                }
            }

            foreach (GameObject rootGameObjectInScene in rootSceneGameObjects)
            {
                errorsFound = errorsFound || ErrorForNullRequiredWiresOnGameObject(rootGameObjectInScene, "In current scene.");
            }

            return errorsFound;
            // Debug.Log ("NotNull search complete");
        }

        private static bool ErrorForNullRequiredWiresOnGameObject(GameObject gameObject, string pathToAsset)
        {
            bool errorsFound = false;
            List<RequiredFieldViolation> errorsOnGameObject = RequiredFieldChecker.FindErroringFields(gameObject);
            foreach (RequiredFieldViolation violation in errorsOnGameObject)
            {
                Debug.LogError(violation + $"\nPath: {pathToAsset} Type: <b>{violation.SourceMonoBehaviour.GetType().Name}</b>", violation.ErrorGameObject);
            }

            //Collect all objects with error
            if (errorsOnGameObject.Count > 0)
                RequiredFieldStaticTracker.AddInstanceID(gameObject.GetInstanceID());
            else
                RequiredFieldStaticTracker.RemoveInstanceID(gameObject.GetInstanceID());

            foreach (Transform child in gameObject.transform)
            {
                errorsFound = errorsFound || ErrorForNullRequiredWiresOnGameObject(child.gameObject, pathToAsset);
            }

            return errorsFound || errorsOnGameObject.Count > 0;
        }

        private static void Log(string log)
        {
            if (outputLogs == false)
            {
                return;
            }

            Debug.Log(log);
        }
    }
}