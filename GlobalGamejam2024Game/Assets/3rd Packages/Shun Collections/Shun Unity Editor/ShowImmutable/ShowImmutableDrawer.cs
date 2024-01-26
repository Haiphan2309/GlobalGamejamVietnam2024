using UnityEditor;
using UnityEngine;

namespace Shun_Unity_Editor
{
    #if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ShowImmutableAttribute))]
    public class ShowImmutableDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginDisabledGroup(true);

            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                case SerializedPropertyType.Float:
                case SerializedPropertyType.Boolean:
                case SerializedPropertyType.String:
                case SerializedPropertyType.Enum:
                    EditorGUI.PropertyField(position, property, label);
                    break;

                case SerializedPropertyType.ObjectReference:
                    EditorGUI.ObjectField(position, property, label);
                    break;

                case SerializedPropertyType.Generic:
                    // Handle lists and serializable classes
                    EditorGUI.PropertyField(position, property, label, true);
                    break;
            }

            EditorGUI.EndDisabledGroup();
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
    }
    
    #endif
}