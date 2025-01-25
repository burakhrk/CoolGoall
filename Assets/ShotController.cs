using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ShotController : MonoBehaviour
{
     public TextMeshProUGUI feedbackText; // Feedback için UI Text referansı
    public Transform ball; // Topun referansı
    public Transform goal; // Kale referansı
     Transform player; // Oyuncunun referansı
    public float shotPower = 10f; // Şutun hızı
    public float perfectThreshold = 0.05f; // Perfect için 0.5'e yakınlık eşiği
    public float niceThreshold = 0.15f; // Nice için 0.5'e yakınlık eşiği
    public float directionThreshold = 0.8f; // Oyuncunun kaleye bakma yönü eşiği (dot product)

    private Vector3[] perfectTargets; // Perfect için hedef noktalar
    private Vector3[] niceTargets; // Nice için hedef noktalar
    private Vector3 missTarget; // Miss durumunda hedef
    bool IsGoal=false; 
    void Start()
    {
        player = transform; 
    }
    float distToCenter;
    bool isFacingGoal=false;
    public void ShowFeedBackText(float dist)
    {
        distToCenter = dist;

        // Oyuncunun yönü kaleye bakıyor mu?
          isFacingGoal = IsFacingGoal();
 
        if (isFacingGoal)
        {
            // Zamanlama ve yön uygunsa şut kalitesi hesaplanır
            if (distToCenter <= perfectThreshold)
            {
                feedbackText.text = "Perfect!";
                feedbackText.color = Color.green;
             }
            else if (distToCenter <= niceThreshold)
            {
                feedbackText.text = "Nice!";
                feedbackText.color = Color.yellow; 
            }
            else
            { 
                feedbackText.text = "Not Good!";
                feedbackText.color = Color.red; 
            }
        }
        else
        {
            // Oyuncu kaleye bakmıyorsa kötü şut
            feedbackText.text = "Bad Shot!";
            feedbackText.color = Color.red; 
        }
        feedbackText.gameObject.SetActive(true);
        StartCoroutine(FeedbackTextDisable());
    } 
    IEnumerator FeedbackTextDisable()
    {
        yield return new WaitForSeconds(2f);
        feedbackText.gameObject.SetActive(false);
    }
    public void GetReadyForShoot( Transform ballTrans)
    { 
        ball = ballTrans;
         Vector3 target;
         // Oyuncunun yönü kaleye bakıyor mu?
 
        if (isFacingGoal)
        {
            // Zamanlama ve yön uygunsa şut kalitesi hesaplanır
            if (distToCenter <= perfectThreshold)
            {
                perfectTargets = new Vector3[]
                  {
            new Vector3(- 2.49f-Random.Range(0,0.35f),2.47f,16.75f), // Sol üst
            new Vector3(2.49f+Random.Range(0,0.35f), 2.47f, 16.75f),  // Sağ üst
          //  new Vector3(goal.position.x - 3.35f, goal.position.y -2f, goal.position.z), // Sol üst
           // new Vector3(goal.position.x + 3.35f, goal.position.y -2f, goal.position.z)  // Sağ üst
                 };


                feedbackText.text = "Perfect!";
                feedbackText.color = Color.green;
                target = perfectTargets[Random.Range(0, perfectTargets.Length)]; // Perfect hedeflerinden biri
                IsGoal = true;
            }
            else if (distToCenter <= niceThreshold)
            {
                // Nice için farklı noktalar (kalenin alt köşeleri)
                niceTargets = new Vector3[]
                {
            new Vector3(2.46f- Random.Range(0,0.5f), 0.25f, 16.75f), // Sol alt
            new Vector3(-2.46f+Random.Range(0,0.5f),0.25f, 16.75f)  // Sağ alt

                };



                feedbackText.text = "Nice!";
                feedbackText.color = Color.yellow;
                target = niceTargets[Random.Range(0, niceTargets.Length)]; // Nice hedeflerinden biri
                IsGoal = true;
               
               
            }
            else
            {
                // Miss durumunda bir hedef (kalenin dışına gider)
                missTarget = new Vector3(goal.position.x+Random.Range(-5,5), goal.position.y + Random.Range(3,6), 17f+ Random.Range(3,6));
                feedbackText.text = "Not Good!";
                feedbackText.color = Color.red;
                target = missTarget; // Miss hedefi
                IsGoal = false;
            }
        }
        else
        {
            // Oyuncu kaleye bakmıyorsa kötü şut
            feedbackText.text = "Bad Shot!";
            feedbackText.color = Color.red;
            IsGoal=false;
            // Kale dışında rastgele bir hedef
            Vector3 direct;
            direct = transform.position - ball.transform.position;
            direct = direct *-3;
            direct.y = 0.25f;
            target = direct;
        }   

        ShootBall(target);
    }
    void ShootBall(Vector3 target)
    {
        Debug.Log("Shootball");
        ball.gameObject.GetComponent<DribblingBall>().ShootSent(target,IsGoal);
    }

    bool IsFacingGoal()
    {
        // Oyuncunun yönü ile kaleye olan yön arasındaki dot product hesaplanır
        Vector3 playerForward = player.forward.normalized; // Oyuncunun ileri yönü
        Vector3 toGoal = (goal.position - player.position).normalized; // Kaleye olan yön

        float dot = Vector3.Dot(playerForward, toGoal); 
        return (dot >= directionThreshold);
    }
}
