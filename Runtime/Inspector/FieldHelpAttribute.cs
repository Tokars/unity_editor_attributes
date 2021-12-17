using UnityEngine;

namespace OT.Attributes
{
    public enum FieldHelpMessageType
    {
        None,
        Info,
        Warning,
        Error
    }

    public class FieldHelpAttribute : PropertyAttribute
    {
        public string msg;
        public FieldHelpMessageType msgType;

        public FieldHelpAttribute(string msg, FieldHelpMessageType msgType = FieldHelpMessageType.None)
        {
            this.msg = msg;
            this.msgType = msgType;
        }
    }
}