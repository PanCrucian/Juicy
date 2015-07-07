using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
[CanEditMultipleObjects]
[CustomEditor(typeof(SurpriseSpike))]
public class SurpriseSpikeEditor : Editor
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
        SurpriseSpike spike = (SurpriseSpike)target;
        MyDrawing.Update();
        MyDrawing.DrawDotLines(spike.gameObject);
        if (MyDrawing.autoDot)
            spike.dot = MyDrawing.minDot;
        EditorUtility.SetDirty(spike);
    }
}
