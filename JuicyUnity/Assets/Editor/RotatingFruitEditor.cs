using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
[CanEditMultipleObjects]
[CustomEditor(typeof(RotatingFruit))]
public class RotatingFruitEditor : Editor
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
        RotatingFruit fruit = (RotatingFruit)target;
        MyDrawing.Update();
        MyDrawing.DrawDotLines(fruit.gameObject);
        if (MyDrawing.autoDot)
            fruit.dot = MyDrawing.minDot;
        EditorUtility.SetDirty(fruit);
    }
}
