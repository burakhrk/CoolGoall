using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform point0, point1, point2;

    [SerializeField] private int curveResolution = 50; // Eğri çözünürlüğü
    [SerializeField] private float curveStrength = 1f; // Kavis miktarını kontrol eden değişken
    private Vector3[] positions;
    [SerializeField] GameController gameController;
    public bool isShoot = false;
    [SerializeField] GameObject dot;
    private List<GameObject> dotList = new List<GameObject>();

    private const float minY = 0.3f; // Y eksenindeki alt sınır

    private void Awake()
    {
        InitializeDots();
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
        foreach (var dot in dotList)
        {
            dot.SetActive(false);
        }
    }

    public void DisableDot(int index)
    {
        dotList[0].SetActive(false);
        dotList[index].SetActive(false);
    }

    private void Start()
    {
        lineRenderer.positionCount = curveResolution;
    }

    private void Update()
    {
        if (isShoot)
            return;

        DrawQuadraticCurve();
    }

    public void SetCurveResolution(int newResolution)
    {
        curveResolution = Mathf.Max(2, newResolution); // Minimum çözünürlük 2 olmalı
        InitializeDots();
    }

    public void SetCurveStrength(float newStrength)
    {
        curveStrength = newStrength;
    }

    private void InitializeDots()
    {
        // Dotları yeniden oluştur ve pozisyonları yeniden ata
        foreach (var dot in dotList)
        {
            Destroy(dot);
        }
        dotList.Clear();

        positions = new Vector3[curveResolution];
        for (int i = 0; i < curveResolution; i++)
        {
            var go = Instantiate(dot);
            go.transform.parent = gameController.transform;
            dotList.Add(go);
        }

        lineRenderer.positionCount = curveResolution;
    }

    void UpdateDotsPos()
    {
        for (int i = 0; i < curveResolution; i++)
        {
            dotList[i].transform.position = positions[i];
        }
    }

    public void DisableDotVisuals()
    {
        foreach (var dot in dotList)
        {
            dot.SetActive(false);
        }
    }

    public Vector3[] GetPath()
    {
        return positions;
    }

    void DrawQuadraticCurve()
    {
        // Kontrol noktası, başlangıç ve bitiş arasındaki orta noktadan sapma yapar
        Vector3 middlePoint = (point0.position + point2.position) / 2;
        Vector3 adjustedPoint1 = middlePoint + (point1.position - middlePoint) * curveStrength;

        for (int i = 0; i < curveResolution; i++)
        {
            float t = i / (float)(curveResolution - 1);
            Vector3 point = CalculateQuadraticBezierPoint(t, point0.position, adjustedPoint1, point2.position);

            // Y ekseninde minimum değeri kontrol et
            if (point.y < minY)
                point.y = minY;

            positions[i] = point;
        }
        lineRenderer.SetPositions(positions);
        UpdateDotsPos();
    }

    Vector3 CalculateLinearBezierPoint(float t, Vector3 p0, Vector3 p1)
    {
        return p0 + t * (p1 - p0);
    }

    Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
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
