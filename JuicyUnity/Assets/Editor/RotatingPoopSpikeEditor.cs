using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(RotatingPoopSpike))]
public class RotatingPoopSpikeEditor : Editor
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
        RotatingPoopSpike spike = (RotatingPoopSpike)target;
        MyDrawing.Update();
        MyDrawing.DrawDotLines(spike.gameObject);
        if (MyDrawing.autoDot)
            spike.dot = MyDrawing.minDot;
        spike.GetComponent<LineRenderer>().SetPosition(0, spike.transform.position);
        spike.GetComponent<LineRenderer>().SetPosition(1, MyDrawing.minDot.transform.position);
        EditorUtility.SetDirty(spike);
    }
}
