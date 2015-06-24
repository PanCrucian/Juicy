using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(RotatingSpike))]
public class RotatingSpikeEditor : Editor
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
        RotatingSpike spike = (RotatingSpike)target;
        MyDrawing.Update();
        MyDrawing.DrawDotLines(spike.gameObject);
        if (MyDrawing.autoDot)
            spike.dot = MyDrawing.minDot;
        spike.GetComponent<LineRenderer>().SetPosition(0, spike.transform.position);
        spike.GetComponent<LineRenderer>().SetPosition(1, MyDrawing.minDot.transform.position);
        EditorUtility.SetDirty(spike);
    }
}
