using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform point0, point1,point2;
    private int numPoints = 50;
    private Vector3[] positions = new Vector3[50];


    private void Start()
    {
        lineRenderer.positionCount = numPoints-1;
    }
    private void Update()
    {
        DrawQuadraticCurve();

    }
    void DrawLinearCurve()
    {
        for (int i = 0; i < numPoints+1; i++)
        {
            float t = i / (float)numPoints;
            positions[i - 1] = CalculateLinearBezierPoint(t,point0.position,point1.position);
        }
        lineRenderer.SetPositions(positions);
    }
    void DrawQuadraticCurve()
    {
        for (int i = 0; i < numPoints; i++)
        {
            float t = i / (float)numPoints;
            if(i!=0)
            positions[i - 1] = CalculateQuadraticBezierPoint(t,point0.position,point1.position,point2.position);
        }
        lineRenderer.SetPositions(positions);

    }
    Vector3 CalculateLinearBezierPoint(float t, Vector3 p0, Vector3 p1 )
    {
        return p0 + t * (p1 - p0);
    }
    Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1,Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0; 
        p += 2 * u * t * p1;
        p += tt * p2;
        return p;

    }
}
