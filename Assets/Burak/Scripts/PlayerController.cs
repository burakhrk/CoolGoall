using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
 public class PlayerController : MonoBehaviour
{
  [SerializeField]  PlayerInput player;
   [SerializeField] Vector2 move;
    Vector2 lastPos;
    Vector3 bezierStartPos;
    Vector3 goalStartPos;
    [SerializeField] GameObject bezierMove;
    [SerializeField] GameObject bezierGoal;
    [SerializeField] float sensivity=1f;
    [SerializeField] float boundx=4f;
    GameController gameController;
    bool shoot= false;
    bool isOnBoarding = false;
  [SerializeField]  OnBoardingController onBoardingController;
    private void Awake()
    {
        bezierStartPos = bezierMove.transform.position;
        goalStartPos = bezierGoal.transform.position;
        gameController = FindFirstObjectByType<GameController>();
        // onboarding
        // Upgrade sistemi
// futbolcu top statları
//  şut hızı vs  

        



        /*
   cheer  text ekle   
       */
    }
    public void OnBoarding()
    {
        isOnBoarding=true;
    }
    public void OnBoardingDOne()
    {
        isOnBoarding = false;

    }
    private void Shoot()
    {
        Debug.Log("Shoot");
        shoot = true;
        Bezier bezier= FindFirstObjectByType<Bezier>();
        bezier.isShoot = true;
        bezier.DisableDotVisuals();
        FindFirstObjectByType<Ball>().Shoot(bezier.GetPath());
        FindFirstObjectByType<Player>().Shoot();
    }
    float onboardingTimer = 3f;
    bool waitForSeconds=false;
    int counterBoarding=0;
    void OnBoardingNext()
    { 
        onBoardingController.NextStep();
    }
        private void Update()
    {
        if (isOnBoarding)
        {
            onboardingTimer -= Time.deltaTime;
            if (onboardingTimer <= 0)
                waitForSeconds = true;
        }

        if (shoot)
            return;

        if (Input.GetMouseButtonUp(0) && gameController.CanShoot&&!isOnBoarding)
        {
            Shoot();
            return;
        }
        if (Input.GetMouseButtonUp(0)&&isOnBoarding)
        {
            if (waitForSeconds)
            {
                waitForSeconds = false;
                onboardingTimer = 3f;
                OnBoardingNext();
            }
            else
            {
               counterBoarding++;
                if(counterBoarding>=2)
                {
                    waitForSeconds = false;
                    onboardingTimer = 3f;
                    OnBoardingNext();
                    counterBoarding = 0;
                }
            }
        }
        
        Vector2 startPos;
        startPos= player.actions["Move"].ReadValue<Vector2>();
        Vector2 go;
        go = player.actions["Move"].ReadValue<Vector2>();
       move= go - lastPos;
         if(go==Vector2.zero)
        {
            ResetBezier();
         }
        else
        {
            MoveBezier();

        }
        lastPos = go;
    }
     void ResetBezier()
    {
        
       // bezierMove.transform.position = bezierStartPos;
    }
    [SerializeField] float x;
    [SerializeField] float y;

    void MoveBezier()
    { 

        bezierMove.transform.position += (Vector3)move * sensivity ;
       
       y = bezierMove.transform.position.y ;
        x = bezierMove.transform.position.x;

        if (y >= 3.5f)
        {
            y = 3.5f;
        }
 
        if (y < 0)
            bezierMove.transform.position = new Vector3(bezierMove.transform.position.x,0.32f,bezierMove.transform.position.z);


        if (x>4.5f)
        {
            MoveGoal();
         }
        if(x<-4.5f)
        {
            MoveGoal();
         } 
             MoveGoal(); 
    }
    void ResetGoalPos()
    {
        bezierGoal.transform.position = goalStartPos;
    }
    void MoveGoal()
    {
        
        Vector3 a = (Vector3)move;
        if(a==Vector3.zero)
        {
            if(bezierMove.transform.position.x>5.5f)
            {
                bezierGoal.transform.position = bezierGoal.transform.position + (Vector3.right/3);
            }
            if (bezierMove.transform.position.x < -5.5f)
            {
                bezierGoal.transform.position = bezierGoal.transform.position +Vector3.left/3;
            }
        }
        else
        {
            bezierGoal.transform.position = new Vector3(bezierGoal.transform.position.x + (a.x * sensivity / 2), bezierGoal.transform.position.y + (a.y * sensivity / 2), bezierGoal.transform.position.z);

        }



        if (bezierGoal.transform.position.x> boundx)
            bezierGoal.transform.position = new Vector3(boundx, bezierGoal.transform.position.y, bezierGoal.transform.position.z);

        if (bezierGoal.transform.position.x < -boundx)
            bezierGoal.transform.position = new Vector3(-boundx, bezierGoal.transform.position.y, bezierGoal.transform.position.z);

        
 

        if ((bezierGoal.transform.position.y) > 3.5f)
        {
 
            bezierGoal.transform.position = new Vector3(bezierGoal.transform.position.x, 3.5f, bezierGoal.transform.position.z);

        }
        if ((bezierGoal.transform.position.y) < 0.1f)
        {
 
            bezierGoal.transform.position = new Vector3(bezierGoal.transform.position.x, 0.1f, bezierGoal.transform.position.z);

        }
    }
}
