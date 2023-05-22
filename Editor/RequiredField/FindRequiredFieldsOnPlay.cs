using UnityEditor;
using UnityEngine;

// ensure class initializer is called whenever scripts recompile
namespace OT.Attributes.Editor.RequiredField
{
    [InitializeOnLoad]
    [ExecuteAlways]
    public class FindRequiredFieldsOnPlay : MonoBehaviour
    {
        // register an event handler when the class is initialized
        static FindRequiredFieldsOnPlay()
        {
            EditorApplication.playModeStateChanged += OnPlayModeState;

            RequiredFieldFinder.SearchForAndErrorForRequiredFieldViolations();
        }

        private static void OnPlayModeState(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                if (RequiredFieldFinder.SearchForAndErrorForRequiredFieldViolations())
                    EditorApplication.ExitPlaymode();
            }
        }
    }
}
