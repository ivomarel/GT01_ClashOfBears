using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Lint))]
public class LintEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //We get the 'value' field from our Lint object
        SerializedProperty valueProp = property.FindPropertyRelative("value");
        //We set the 'value' field by drawing an IntField EditorGUI
        valueProp.intValue = EditorGUI.IntField(position, property.name, valueProp.intValue);
    }
}
