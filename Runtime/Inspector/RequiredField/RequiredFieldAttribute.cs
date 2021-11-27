using System;
using UnityEngine;

namespace Tok.CustomAttributes
{
    /// <summary> Indicates field by color in Inspector. </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class RequiredFieldAttribute : PropertyAttribute
    {
    }
}