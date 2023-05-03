using UnityEngine;

namespace OT.Attributes.Inspector
{
    public class PreviewSpriteAttribute : PropertyAttribute
    {
        public readonly int AdditionalInspectorHeight;

        public PreviewSpriteAttribute(int additionalInspectorHeight = 10)
        {
            AdditionalInspectorHeight = additionalInspectorHeight;
        }
    }
}