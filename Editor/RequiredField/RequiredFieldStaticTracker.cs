using System.Collections.Generic;
using UnityEditor;

namespace OT.Attributes.Editor.RequiredField
{
    [InitializeOnLoad]
    public static class RequiredFieldStaticTracker
    {
        static HashSet<int> instanceIDs = new HashSet<int>();

        public static void AddInstanceID(int id)
        {
            instanceIDs.Add(id);
        }

        public static void RemoveInstanceID(int id)
        {
            instanceIDs.Remove(id);
        }

        public static bool ContainsInstanceID(int id)
        {
            return instanceIDs.Contains(id);
        }

    }
}
