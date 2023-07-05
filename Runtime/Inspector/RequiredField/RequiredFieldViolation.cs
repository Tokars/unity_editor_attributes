using System.Reflection;
using UnityEngine;

namespace OT.Attributes
{
    /// <summary>
    /// Required Field violation represents data for objects that do not have their required fields
    /// assigned.
    /// </summary>
    public class RequiredFieldViolation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredFieldViolation"/> class.
        /// </summary>
        /// <param name="fieldInfo">Field info that describes the required field.</param>
        /// <param name="sourceMB">Source MonoBehavior that contains the field.</param>
        public RequiredFieldViolation(FieldInfo fieldInfo, MonoBehaviour sourceMB)
        {
            FieldInfo = fieldInfo;
            SourceMonoBehaviour = sourceMB;
            ErrorGameObject = sourceMB.gameObject;
        }

        /// <summary>
        /// Gets or sets the field info associated with the RequiredField attribute.
        /// </summary>
        /// <value>The field info.</value>
        public FieldInfo FieldInfo { get; set; }

        /// <summary>
        /// Gets or sets the game object that contains the component with the violation.
        /// </summary>
        /// <value>The erroring game object.</value>
        public GameObject ErrorGameObject { get; set; }

        /// <summary>
        /// Gets or sets the MonoBehavior that contains the violation.
        /// </summary>
        /// <value>The source mono behaviour.</value>
        public MonoBehaviour SourceMonoBehaviour { get; set; }

        /// <summary>
        /// Gets the full path to the erroring game object, including parents.
        /// </summary>
        /// <value>The full name.</value>
        public string FullName
        {
            get
            {
                Transform currentParent = this.ErrorGameObject.transform.parent;
                string fullName = this.ErrorGameObject.name;
                while (currentParent != null)
                {
                    fullName = currentParent.gameObject.name + "/" + fullName;
                    currentParent = currentParent.transform.parent;
                }

                return fullName;
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the current 
        /// <see cref="RequiredFieldViolation"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents the current 
        /// <see cref="RequiredFieldViolation"/>.</returns>
        public override string ToString()
        {
            return
                $"[{nameof(RequiredFieldViolation)}: Field=<b><color=red>{this.FieldInfo.Name}</color></b>, FullName=<b>{this.FullName}</b>]";
        }
    }
}