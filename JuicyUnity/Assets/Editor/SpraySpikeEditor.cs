using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(SpraySpike))]
public class SpraySpikeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUILayout.Label("Чтобы вращать этот шип по оси Z, вращай точку");
        base.DrawDefaultInspector();        
        MyDrawing.DrawInspector();
    }

    void OnSceneGUI()
    {
        if (Application.isPlaying)
            return;
        SpraySpike spike = (SpraySpike)target;
        MyDrawing.Update();
        MyDrawing.DrawDotLines(spike.gameObject);
        if (MyDrawing.autoDot)
            spike.dot = MyDrawing.minDot;
        EditorUtility.SetDirty(spike);
    }
}
