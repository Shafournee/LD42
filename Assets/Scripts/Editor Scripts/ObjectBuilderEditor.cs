using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PolygonColliderSnapper))]
public class ObjectBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PolygonColliderSnapper snapper = (PolygonColliderSnapper)target;
        if (GUILayout.Button("Snap Points"))
        {
            snapper.Snap();
        }
    }
}