using System;
using UnityEditor;
using UnityEngine;
using UtilsToolbox.PropertyAttributes;

namespace UtilsToolbox.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(AutoFindAttribute))]
    public sealed class AutoFindPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var autoFind = (AutoFindAttribute)attribute;

            EditorGUI.PropertyField(position, property, label);

            if (property.objectReferenceValue != null)
            {
                return;
            }

            if (property.propertyType != SerializedPropertyType.ObjectReference)
            {
                return;
            }

            Component component = property.serializedObject.targetObject as Component;
            if (component == null)
            {
                return;
            }

            Type fieldType = fieldInfo.FieldType;
            if (typeof(Component).IsAssignableFrom(fieldType) == false)
            {
                return;
            }

            Component found = autoFind.Mode switch
            {
                AutoFindMode.Self => component.GetComponent(fieldType),
                AutoFindMode.Children => component.GetComponentInChildren(fieldType, true),
                AutoFindMode.Parent => component.GetComponentInParent(fieldType, true),
                _ => null
            };

            if (found == null)
            {
                return;
            }

            property.objectReferenceValue = found;
            property.serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(component);
        }
    }
}