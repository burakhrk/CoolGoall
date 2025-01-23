using TMPro;
using UnityEngine;
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

    void Start()
    {
        player = transform; 
    }
    float distToCenter;

    public void ShowFeedBackText(float dist)
    {
        distToCenter = dist;

        // Oyuncunun yönü kaleye bakıyor mu?
        bool isFacingGoal = IsFacingGoal();

        if (isFacingGoal)
        {
            // Zamanlama ve yön uygunsa şut kalitesi hesaplanır
            if (distToCenter <= perfectThreshold)
            {
                perfectTargets = new Vector3[]
                  {
            new Vector3(goal.position.x - 1f, goal.position.y + 1f, goal.position.z), // Sol üst
            new Vector3(goal.position.x + 1f, goal.position.y + 1f, goal.position.z)  // Sağ üst
                 };


                feedbackText.text = "Perfect!";
                feedbackText.color = Color.green;
             }
            else if (distToCenter <= niceThreshold)
            {
                // Nice için farklı noktalar (kalenin alt köşeleri)
                niceTargets = new Vector3[]
                {
            new Vector3(goal.position.x - 1f, goal.position.y - 1f, goal.position.z), // Sol alt
            new Vector3(goal.position.x + 1f, goal.position.y - 1f, goal.position.z)  // Sağ alt
                };



                feedbackText.text = "Nice!";
                feedbackText.color = Color.yellow; 

            }
            else
            {
                // Miss durumunda bir hedef (kalenin dışına gider)
                missTarget = new Vector3(goal.position.x, goal.position.y + 2f, goal.position.z + 2f);
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
    }
    public void GetReadyForShoot( Transform ballTrans)
    {
        ball = ballTrans;
         Vector3 target;
         // Oyuncunun yönü kaleye bakıyor mu?
        bool isFacingGoal = IsFacingGoal();

        if (isFacingGoal)
        {
            // Zamanlama ve yön uygunsa şut kalitesi hesaplanır
            if (distToCenter <= perfectThreshold)
            {
                perfectTargets = new Vector3[]
                  {
            new Vector3(goal.position.x - 1f, goal.position.y + 1f, goal.position.z), // Sol üst
            new Vector3(goal.position.x + 1f, goal.position.y + 1f, goal.position.z)  // Sağ üst
                 };


                feedbackText.text = "Perfect!";
                feedbackText.color = Color.green;
                target = perfectTargets[Random.Range(0, perfectTargets.Length)]; // Perfect hedeflerinden biri
            }
            else if (distToCenter <= niceThreshold)
            {
                // Nice için farklı noktalar (kalenin alt köşeleri)
                niceTargets = new Vector3[]
                {
            new Vector3(goal.position.x - 1f, goal.position.y - 1f, goal.position.z), // Sol alt
            new Vector3(goal.position.x + 1f, goal.position.y - 1f, goal.position.z)  // Sağ alt
                };



                feedbackText.text = "Nice!";
                feedbackText.color = Color.yellow;
                target = niceTargets[Random.Range(0, niceTargets.Length)]; // Nice hedeflerinden biri

               
               
            }
            else
            {
                // Miss durumunda bir hedef (kalenin dışına gider)
                missTarget = new Vector3(goal.position.x, goal.position.y + 2f, goal.position.z + 2f);
                feedbackText.text = "Not Good!";
                feedbackText.color = Color.red;
                target = missTarget; // Miss hedefi
            }
        }
        else
        {
            // Oyuncu kaleye bakmıyorsa kötü şut
            feedbackText.text = "Bad Shot!";
            feedbackText.color = Color.red;

            // Kale dışında rastgele bir hedef
            target = new Vector3(
                ball.position.x + Random.Range(-2f, 2f),
                ball.position.y + Random.Range(-1f, 1f),
                ball.position.z - 2f
            );
        }

        ShootBall(target);
    }
    void ShootBall(Vector3 target)
    {
        ball.gameObject.GetComponent<DribblingBall>().ShootSent(target);
    }

    bool IsFacingGoal()
    {
        // Oyuncunun yönü ile kaleye olan yön arasındaki dot product hesaplanır
        Vector3 playerForward = player.forward.normalized; // Oyuncunun ileri yönü
        Vector3 toGoal = (goal.position - player.position).normalized; // Kaleye olan yön

        float dot = Vector3.Dot(playerForward, toGoal);

        // Eğer dot product belirlenen eşikten büyükse, oyuncu kaleye bakıyordur
        return dot >= directionThreshold;
    }
}
