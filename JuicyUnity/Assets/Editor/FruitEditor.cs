using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(Fruit))]
public class FruitEditor : Editor
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
        Fruit fruit = (Fruit)target;
        MyDrawing.Update();
        MyDrawing.DrawDotLines(fruit.gameObject);
        if (MyDrawing.autoDot)
            fruit.dot = MyDrawing.minDot;
        EditorUtility.SetDirty(fruit);
    }
}
