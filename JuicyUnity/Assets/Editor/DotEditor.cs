using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(Dot))]
public class DotEditor : Editor {
    Dot[] dots;
    float minDist = Mathf.Infinity;
    [Range(0,100f)]float poiDistance = 12.5f;
    Dot dot;

    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();
        GUILayout.Label("До ближайшей точки " + System.String.Format("{0}", minDist) + " юнитов", EditorStyles.boldLabel);
        poiDistance = EditorGUILayout.FloatField("Радиус интереса:", poiDistance);
    }

    void OnSceneGUI()
    {
        if (dots == null)
            dots = GameObject.FindObjectsOfType<Dot>();
        dot = (Dot)target;
        minDist = Mathf.Infinity;
        List<Dot> poiDots = new List<Dot>();
        Dot minDot = dot;

        foreach (Dot d in dots)
        {
            if (d == dot)
                continue;
            float distance = Vector3.Distance(dot.transform.position, d.transform.position);
            if (minDist > distance)
            {
                minDot = d;
                minDist = distance;
            }
            if (distance <= poiDistance)
            {
                poiDots.Add(d);
            }
        }
        foreach (Dot d in poiDots)
        {
            float distance = Vector3.Distance(dot.transform.position, d.transform.position);
            if (d == minDot)
            {
                Handles.color = Color.red;
                Handles.Label((dot.transform.position - d.transform.position) * 0.5f + d.transform.position, distance.ToString());
                Handles.DrawLine(dot.transform.position, d.transform.position);
            }
            else
            {
                Handles.color = Color.gray;
                Handles.Label((dot.transform.position - d.transform.position) * 0.5f + d.transform.position, distance.ToString());
                Handles.DrawLine(dot.transform.position, d.transform.position);
            }
        }
    }
}
