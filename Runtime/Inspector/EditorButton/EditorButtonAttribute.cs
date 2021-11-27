// Initial Concept by http://www.reddit.com/user/zaikman
// Revised by http://www.reddit.com/user/quarkism

using UnityEngine;

namespace Tok.CustomAttributes
{
    /// <summary>
    /// Stick this on a method
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class EditorButtonAttribute : PropertyAttribute
    {
    }
}