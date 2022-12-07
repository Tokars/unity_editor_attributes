using System;
using UnityEngine;

namespace OT.Attributes
{
    /// <summary>
    /// Slider Min. Max. unity inspector attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class MinMaxSliderAttribute : PropertyAttribute
    {
        public readonly float max;
        public readonly float min;

        public MinMaxSliderAttribute(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
    }
}