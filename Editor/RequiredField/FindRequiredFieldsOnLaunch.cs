using UnityEditor;
using UnityEngine;

namespace OT.Attributes.Editor.RequiredField
{
    /// <summary>
    /// Class that contains logic to find RequiredField violations when pressing Play from the Editor
    /// </summary>
    [InitializeOnLoad]
    public class FindRequiredFieldsOnLaunch
    {
        static FindRequiredFieldsOnLaunch()
        {
            if (Debug.isDebugBuild)
            {
                // Searching on first launch seemed to execute before references were wired up on scene objects.
                //EditorApplication.update += RunOnce;
            }
        }

        private static void RunOnce()
        {
            EditorApplication.update -= RunOnce;
            RequiredFieldFinder.SearchForAndErrorForRequiredFieldViolations();
        }
    }
}