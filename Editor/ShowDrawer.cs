using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace DarkNaku.Attribute
{
    [CustomPropertyDrawer(typeof(ShowAttribute))]
    public class ShowDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var showAttribute = (ShowAttribute)attribute;

            if (CheckCondition(showAttribute, property))
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var showAttribute = (ShowAttribute)attribute;

            if (CheckCondition(showAttribute, property))
            {
                return EditorGUI.GetPropertyHeight(property, label, true);
            }
            else
            {
                return 0f;
            }
        }

        private bool CheckCondition(ShowAttribute showAttribute, SerializedProperty property)
        {
            object targetObject = property.serializedObject.targetObject;

            var targetType = targetObject.GetType();
            string conditionName = showAttribute.Condition;
            object[] parameters = showAttribute.Parameters;

            var parameterTypes = new System.Type[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                parameterTypes[i] = parameters[i].GetType();
            }

            FieldInfo fieldInfo = targetType.GetField(conditionName,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (fieldInfo != null && fieldInfo.FieldType == typeof(bool))
            {
                return (bool)fieldInfo.GetValue(targetObject);
            }

            PropertyInfo propertyInfo = targetType.GetProperty(conditionName,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (propertyInfo != null && propertyInfo.PropertyType == typeof(bool))
            {
                return (bool)propertyInfo.GetValue(targetObject, null);
            }

            MethodInfo methodInfo = targetType.GetMethod(conditionName,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                null, parameterTypes, null);

            if (methodInfo != null && methodInfo.ReturnType == typeof(bool))
            {
                try
                {
                    return (bool)methodInfo.Invoke(targetObject, parameters);
                }
                catch (Exception e)
                {
                    Debug.LogWarning(
                        $"[ShowAttribute] CheckCondition : Error invoking method '{conditionName}' with parameters. {e.Message}");
                    throw;
                }
            }

            Debug.LogWarning(
                $"[ShowAttribute] CheckCondition : Could not find a boolean field, property, or parameterless method named '{conditionName}' in {targetType.Name}");

            return true;
        }
    }
}