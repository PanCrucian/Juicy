using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
[CanEditMultipleObjects]
[CustomEditor(typeof(Spike))]
public class SpikeEditor : Editor
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
        Spike spike = (Spike)target;
        MyDrawing.Update();
        MyDrawing.DrawDotLines(spike.gameObject);
        if (MyDrawing.autoDot)
            spike.dot = MyDrawing.minDot;
        EditorUtility.SetDirty(spike);
    }
}
