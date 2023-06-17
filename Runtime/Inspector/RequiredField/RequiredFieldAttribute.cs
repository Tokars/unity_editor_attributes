using UnityEngine;

namespace OT.Attributes.Inspector.RequiredField
{
    /// <summary>
    /// Not Null Attribute will error in the Editor if an object field has not been assigned.
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Field)]
    public class RequiredFieldAttribute : PropertyAttribute
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Attributes"/> should ignore prefabs.
        /// Use this when your object reqiures a reference to an object in the scene, such as a spawn point.
        /// </summary>
        /// <value><c>true</c> if ignore prefab; otherwise, <c>false</c>.</value>
        public bool IgnorePrefab { get; set; }
    }
}