using UnityEditor;
using UnityEngine;

namespace OT.Attributes.Editor.RequiredField
{
    [InitializeOnLoad]
    public class RequiredFieldIconDrawer
    {
        static Texture texture;

        static RequiredFieldIconDrawer()
        {
            EditorApplication.hierarchyWindowItemOnGUI += EvaluateIcons;
            //texture = AssetDatabase.LoadAssetAtPath("Assets/Media/mine.png", typeof(Texture2D)) as Texture2D;
            var icon = EditorGUIUtility.IconContent("console.erroricon");
            texture = icon.image;
        }

        private static void EvaluateIcons(int instanceId, Rect selectionRect)
        {
            GameObject go = EditorUtility.InstanceIDToObject(instanceId) as GameObject;
            if (go == null) return;

            if (RequiredFieldStaticTracker.ContainsInstanceID(instanceId))
                DrawIcon(selectionRect);
        }

        private static void DrawIcon(Rect rect)
        {
            Rect r = new Rect(rect.x + rect.width - 16f, rect.y, 16f, 16f);
            GUI.DrawTexture(r, texture);
        }
    }
}
