using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(MoveableLineSpike))]
public class MoveableLineSpikeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUILayout.Label("Чтобы вращать линию по оси Z, вращай точку");
        base.DrawDefaultInspector();        
        MyDrawing.DrawInspector();
    }

    void OnSceneGUI()
    {
        if (Application.isPlaying)
            return;
        MoveableLineSpike spike = (MoveableLineSpike)target;
        MyDrawing.Update();
        MyDrawing.DrawDotLines(spike.gameObject);
        if (MyDrawing.autoDot)
            spike.dot = MyDrawing.minDot;
        EditorUtility.SetDirty(spike);
    }
}
