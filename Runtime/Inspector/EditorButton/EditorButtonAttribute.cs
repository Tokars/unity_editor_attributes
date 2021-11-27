// Initial Concept by http://www.reddit.com/user/zaikman
// Revised by http://www.reddit.com/user/quarkism

using UnityEngine;

namespace Tok.CustomAttributes
{
    /// <summary> Simple button for method without parameters </summary>
    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class EditorButtonAttribute : PropertyAttribute
    {
    }
}