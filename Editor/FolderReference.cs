using UnityEditor;

namespace OT.Attributes.Editor
{
    [System.Serializable]
    public class FolderReference
    {
        public string GUID;

        public string Path => AssetDatabase.GUIDToAssetPath(GUID);
    }
}