using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace OT.Attributes.Editor
{
    public abstract class BaseEditorButtonDrawer<TTarget, TAttribute> : UnityEditor.Editor
        where TAttribute : PropertyAttribute
    {
        protected TTarget _t;


        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var methods = _t.GetType()
                .GetMembers(BindingFlags.Instance | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public |
                            BindingFlags.NonPublic)
                .Where(o => Attribute.IsDefined(o, typeof(TAttribute)));

            foreach (var memberInfo in methods)
            {
                if (GUILayout.Button(memberInfo.Name))
                {
                    var method = memberInfo as MethodInfo;
                    method.Invoke(_t, null);
                }
            }
        }
    }
}