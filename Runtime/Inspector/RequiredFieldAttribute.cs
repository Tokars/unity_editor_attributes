using System;
using UnityEngine;

namespace OT.Attributes
{
    /// <summary> Indicates field by color in Inspector. </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class RequiredFieldAttribute : PropertyAttribute
    {
    }
}