using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Bezier
{
    public static Vector3 GetPoint(Vector3[] points, float t)
    {
        // 多次线性插值
        //return Vector3.Lerp(Vector3.Lerp(p0, p1, t), Vector3.Lerp(p1, p2, t), t);

        if (points.Length == 2)
        {
            Vector3 p0 = points[0];
            Vector3 p1 = points[1];
            // 线性Bezier公式
            return p0 + (p1 - p0) * t;
        }
        else if (points.Length == 3)
        {
            Vector3 p0 = points[0];
            Vector3 p1 = points[1];
            Vector3 p2 = points[2];

            // 二次方Bezier公式
            float u = 1 - t;
            return u * u * p0 + 2 * t * u * p1 + t * t * p2;
        }
        else if (points.Length == 4)
        {
            // 三次方Bezier公式
            Vector3 p0 = points[0];
            Vector3 p1 = points[1];
            Vector3 p2 = points[2];
            Vector3 p3 = points[3];

            float u = 1 - t;
            return u * u * u * p0 + 3 * u * u * t * p1 + 3 * u * t * t * p2 + t * t * t * p3;
        }

        return new Vector3();
    }
}
