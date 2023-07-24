using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform point0, point1,point2;
    private int numPoints = 50;
    private Vector3[] positions = new Vector3[50];
  [SerializeField]  GameController gameController;
    public bool isShoot = false;
    [SerializeField] GameObject dot;
    List<GameObject> dotList= new List<GameObject>();
    
    private void Awake()
    {
        for (int i = 0; i < numPoints-1; i++)
        {
            var go = Instantiate(dot);
            go.transform.parent = gameController.transform;
            dotList.Add(go);
        }
    }
    private void OnEnable()
    {
        gameController.OnGameEnd += GameEnd;
    }
    private void OnDisable()
    {
        gameController.OnGameEnd -= GameEnd;

    }
    void GameEnd()
    {
 
        for (int i = 0; i < numPoints - 1; i++)
        {
            dotList[i].SetActive(false);
        }
    }
    public void DisableDot(int index)
    {
        dotList[0].SetActive(false);
        dotList[index].SetActive(false);
    }
    private void Start()
    {
        lineRenderer.positionCount = numPoints-1;
    }
    private void Update()
    {
        if (isShoot)
        {

            return;
        }

        DrawQuadraticCurve();

    }
    void UpdateDotsPos()
    {
        for (int i = 0; i < numPoints-1; i++)
        {
            dotList[i].transform.position = positions[i];
        }
    }
   public Vector3[] GetPath()
    {
        return positions;
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
        UpdateDotsPos();
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
