using UnityEditor;
using UnityEngine;

namespace OT.Attributes.Editor
{
    [CustomPropertyDrawer(typeof(FieldHelpAttribute))]
    public class FieldHelpAttributeDrawer : DecoratorDrawer
    {
        public override float GetHeight()
        {
            var helpBoxAttribute = attribute as FieldHelpAttribute;
            if (helpBoxAttribute == null) return base.GetHeight();
            var helpBoxStyle = (GUI.skin != null) ? GUI.skin.GetStyle("helpbox") : null;
            if (helpBoxStyle == null) return base.GetHeight();
            return Mathf.Max(40f,
                helpBoxStyle.CalcHeight(new GUIContent(helpBoxAttribute.msg), EditorGUIUtility.currentViewWidth) + 4);
        }

        public override void OnGUI(Rect position)
        {
            var helpBoxAttribute = attribute as FieldHelpAttribute;
            if (helpBoxAttribute == null) return;
            EditorGUI.HelpBox(position, helpBoxAttribute.msg, GetMessageType(helpBoxAttribute.msgType));
        }

        private MessageType GetMessageType(FieldHelpMessageType fieldHelpMessageType)
        {
            switch (fieldHelpMessageType)
            {
                default:
                case FieldHelpMessageType.None: return MessageType.None;
                case FieldHelpMessageType.Info: return MessageType.Info;
                case FieldHelpMessageType.Warning: return MessageType.Warning;
                case FieldHelpMessageType.Error: return MessageType.Error;
            }
        }
    }
}