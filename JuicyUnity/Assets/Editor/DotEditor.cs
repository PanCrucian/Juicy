using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(Dot))]
public class DotEditor : Editor {
    public bool fold;
    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();
        fold = EditorGUILayout.Foldout(fold, "Дистанция до точек");
        if (fold)
        {
            GUILayout.Label("До ближайшей точки " + System.String.Format("{0}", MyDrawing.minDist) + " юнитов", EditorStyles.boldLabel);
            MyDrawing.poiDistance = EditorGUILayout.FloatField("Радиус интереса:", MyDrawing.poiDistance);
        }
    }

    void OnSceneGUI()
    {
        Dot dot = (Dot)target;
        MyDrawing.Update();
        MyDrawing.DrawDotLines(dot.gameObject);        
    }
}
