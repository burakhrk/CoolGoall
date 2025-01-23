using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DribblingBall : MonoBehaviour
{
   public  bool hasOwner=false;
    public Transform ball; // Topun referansı
     public float shotDuration = 1f; // Şutun hedefe ulaşma süresi
    public AnimationCurve heightCurve; // Yükseklik eğrisi

    private Vector3 startPoint; // Topun vurulduğu pozisyon
    private Vector3 controlPoint; // Bezier kontrol noktası
    private Vector3 endPoint; // Hedef pozisyon
    private float shotTimer; // Şut zamanlayıcısı
    private bool isShooting = false; // Şutun aktif olup olmadığını kontrol eder
    public void BallReceived()
    {
        hasOwner = true;
    }
   
    private void LateUpdate()
    {
        if (!hasOwner)
        {
            if (transform.position.y > 0.25f)
            {
                transform.position = new Vector3(transform.position.x, 0.25f, transform.position.z);
            }
        }
    }
    private void Update()
    {
        if (isShooting)
        {
            shotTimer += Time.deltaTime / shotDuration;
            if (shotTimer > 1f)
            {
                shotTimer = 1f;
                isShooting = false; // Şut bitti
            }

            // Bezier eğrisi üzerinde topun pozisyonunu hesapla
            Vector3 curvePosition = CalculateBezierPoint(shotTimer, startPoint, controlPoint, TargetPos);
            ball.position = curvePosition;
            if(curvePosition==TargetPos)
            {
                GetComponent<Rigidbody>().isKinematic = false;
            }
        }

    }
    Vector3 TargetPos;
    public void ShootSent(Vector3 Target)
    {
        ball = transform;
        TargetPos = Target;
             // Şutun başlangıç pozisyonunu ve hedefini ayarla
            startPoint = ball.position;
         
            // Kontrol noktasını belirle (topun kalkacağı yüksekliği belirler)
            controlPoint = new Vector3(
                (startPoint.x + endPoint.x) / 2,
                Mathf.Max(startPoint.y, endPoint.y) + heightCurve.Evaluate(0.5f) * 5f, // Yüksekliği heightCurve'e göre belirle
                (startPoint.z + endPoint.z) / 2
            );

            // Zamanlayıcıyı ve şut durumunu başlat
            shotTimer = 0f;
            isShooting = true;
      
        
    }
    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        // Quadratic Bezier eğrisi formülü: (1 - t)^2 * P0 + 2 * (1 - t) * t * P1 + t^2 * P2
        float u = 1f - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 point = (uu * p0) + (2 * u * t * p1) + (tt * p2);
        return point;
    }
} 
