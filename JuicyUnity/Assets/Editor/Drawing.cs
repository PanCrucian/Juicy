using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// Помошнир редактору, рисование гизмо и линий
/// </summary>
public static class MyDrawing {
    public static Dot[] dots;
    public static Dot minDot;
    public static float minDist = Mathf.Infinity;
    public static float poiDistance = 12.5f;
    public static bool fold;
    public static bool autoDot = true;

    public static void DrawInspector()
    {
        fold = EditorGUILayout.Foldout(fold, "Помошник по точкам");
        if (fold)
        {
            autoDot = EditorGUILayout.Toggle("Авто точка", autoDot);
            GUILayout.Label("До ближайшей точки " + System.String.Format("{0}", minDist) + " юнитов", EditorStyles.boldLabel);
            MyDrawing.poiDistance = EditorGUILayout.FloatField("Радиус интереса:", poiDistance);
        }
    }

    public static void Update()
    {
        dots = GameObject.FindObjectsOfType<Dot>();
        minDot = dots[0];
        minDist = Mathf.Infinity;
    }

    public static void DrawDotLines(GameObject target)
    {
        List<Dot> poiDots = new List<Dot>();

        foreach (Dot d in dots)
        {
            if (target.Equals(d))
                continue;
            float distance = Vector3.Distance(target.transform.position, d.transform.position);
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
            float distance = Vector3.Distance(target.transform.position, d.transform.position);
            if (minDot.Equals(d))
            {
                Handles.color = Color.red;
                Handles.Label((target.transform.position - d.transform.position) * 0.5f + d.transform.position, distance.ToString());
                Handles.DrawLine(target.transform.position, d.transform.position);
            }
            else
            {
                Handles.color = Color.gray;
                Handles.Label((target.transform.position - d.transform.position) * 0.5f + d.transform.position, distance.ToString());
                Handles.DrawLine(target.transform.position, d.transform.position);
            }
        }
    }
}
