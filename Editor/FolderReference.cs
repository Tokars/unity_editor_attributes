using UnityEditor;

/*
 * @author: Docboy
 * https://forum.unity.com/threads/what-is-a-serializable-asset-type-for-folder.608875/#post-6447746
 */
namespace OT.Attributes.Editor
{
    [System.Serializable]
    public class FolderReference
    {
        public string GUID;

        public string Path => AssetDatabase.GUIDToAssetPath(GUID)+"/";
    }
}