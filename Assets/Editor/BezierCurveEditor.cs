using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BezierCurve))]
public class BezierCurveEditor : Editor
{
    private BezierCurve bezierCurve;
    private Transform transform;
    private Quaternion rotation;

    private const int nLineSteps = 30;

    private void OnSceneGUI()
    {
        bezierCurve = target as BezierCurve;

        transform = bezierCurve.transform;
        rotation = Tools.pivotRotation == PivotRotation.Local ? transform.rotation : Quaternion.identity;

        Handles.color = Color.gray;
        Vector3 p0 = ShowPoint(0);
        for(int i = 1; i < bezierCurve.points.Length; ++i)
        {
            Vector3 p1 = ShowPoint(i);
            Handles.DrawLine(p0, p1);
            p0 = p1;
        }

        Handles.color = Color.red;
        Vector3 lineStartPosition = bezierCurve.GetPoint(0);
        for (int i = 1; i <= nLineSteps; ++i)
        {
            Vector3 lineEndPosition = bezierCurve.GetPoint(i / (float)nLineSteps);
            Handles.DrawLine(lineStartPosition, lineEndPosition);
            lineStartPosition = lineEndPosition;
        }
    }

    private Vector3 ShowPoint(int nIndex)
    {
        Vector3 point = transform.TransformPoint(bezierCurve.points[nIndex]);

        EditorGUI.BeginChangeCheck();

        point = Handles.DoPositionHandle(point, rotation);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(bezierCurve, "Move Point");
            EditorUtility.SetDirty(bezierCurve);
            bezierCurve.points[nIndex] = transform.InverseTransformPoint(point);
        }

        return point;
    }
}
