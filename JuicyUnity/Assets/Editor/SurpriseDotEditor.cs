using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
[CanEditMultipleObjects]
[CustomEditor(typeof(SurpriseDot))]
public class SurpriseDotEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();
        MyDrawing.DrawInspector();
    }

    void OnSceneGUI()
    {
        if (Application.isPlaying)
            return;
        SurpriseDot dot = (SurpriseDot)target;
        MyDrawing.Update();
        MyDrawing.DrawDotLines(dot.gameObject);
        EditorUtility.SetDirty(dot);
    }
}
