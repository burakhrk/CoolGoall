using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Ball : MonoBehaviour
{
     
    private Rigidbody rb;
    public Animator animPlayer;
     private int currentPos = 1;
    [SerializeField] private float speed;
    private bool hit = false;
       bool isKick = false;
    Vector3[] Path;
    GameController gameController;
    [SerializeField] Bezier bezier;
    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        rb = GetComponent<Rigidbody>();
    }
    public void Shoot(Vector3[] path)
    {
        Path = path;
        StartCoroutine(WaitForAnim());
    }
   IEnumerator WaitForAnim()
    {
        yield return new WaitForSeconds(1f);
        isKick = true;

    }
    private void Update()
    {
        if(isKick&&!hit)
        FollowToRoute();
    }

    public void FollowToRoute()
    {
     if(currentPos<Path.Length-1)
        {
            if (transform.position != Path[currentPos])
            {
                Vector3 pos = Vector3.MoveTowards(transform.position, Path[currentPos], speed * Time.deltaTime);
                rb.MovePosition(pos);
            }
            else
            {
                bezier.DisableDot(currentPos);
                currentPos++;
            }
        } 
    }
    void Fail()
    {
        
        hit = true;
        rb.useGravity = true;
        Enemy.isDefence = true;
        Debug.Log("Fail");
        gameController.Lose();

    }
    private void OnCollisionEnter(Collision collision)
    {

        ContactPoint[] contact = collision.contacts;
        Vector3 dir = (transform.position - contact[0].point).normalized;
        rb.AddForce(dir * 15, ForceMode.Impulse);
        Fail();
    }
    void Win()
    {
        Debug.Log("Win");
        gameController.Goal();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            Win();
         }
    }
    /*
public class Ball : MonoBehaviour
{
   public RouteCreator rc;
   private Rigidbody rb;
   public Animator animPlayer;
   public GameObject confetti;
   public Transform[] confettiPos;
   private int currentPos = 1;
   [SerializeField] private float speed;
   private bool hit = false;
   [HideInInspector] public bool isKick = false;

   private void Awake()
   {
       rb = GetComponent<Rigidbody>();
   }

   private void Update()
   {
       FollowToRoute();
   }

   public void FollowToRoute()
   {
       if (currentPos < 17 & isKick && !hit)
       {
           if (transform.position != rc.route[currentPos].transform.position)
           {
               Vector3 pos = Vector3.MoveTowards(transform.position, rc.route[currentPos].transform.position, speed * Time.deltaTime);
               rb.MovePosition(pos);
           }
           else
           {
               currentPos++;
           }
       }
   }

   private void OnCollisionEnter(Collision collision)
   {
       GameManager.instance.FailPanel();

       hit = true;
       rb.useGravity = true;
       Enemy.isDefence = true;

       ContactPoint[] contact = collision.contacts;
       Vector3 dir = (transform.position - contact[0].point).normalized;
       rb.AddForce(dir * 15, ForceMode.Impulse);
   }

   private void OnTriggerEnter(Collider other)
   {
       if (other.CompareTag("Goal"))
       {
           GameManager.instance.SuccessPanel();
           animPlayer.SetTrigger("Goal");
           for (int i = 0; i < 2; i++)
           {
               Instantiate(confetti, confettiPos[i].position, Quaternion.identity);
           }
       }
   }
}
    */
}
