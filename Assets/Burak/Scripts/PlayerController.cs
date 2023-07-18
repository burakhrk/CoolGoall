using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] float bound=3f;

    bool shoot=false;
    private void Awake()
    {
       bezierStartPos= bezierMove.transform.position;
        goalStartPos = bezierGoal.transform.position;
    }


    private void Shoot()
    {
        shoot = true;
        Bezier bezier= FindObjectOfType<Bezier>();
        bezier.isShoot = true;
        FindObjectOfType<Ball>().Shoot(bezier.GetPath());
        FindObjectOfType<Player>().Shoot();
    }
    private void Update()
    {
        if (shoot)
            return;

        if (Input.GetMouseButtonUp(0))
        {
            Shoot();
            return;

        }
       


        Vector2 go;
        go = player.actions["Move"].ReadValue<Vector2>();
       move= go - lastPos;
         if(go==Vector2.zero)
        {
            ResetBezier();
            ResetGoalPos();
        }
        else
        {
            MoveBezier();

        }
        lastPos = go;
    }
     void ResetBezier()
    {
        
        bezierMove.transform.position = bezierStartPos;
    }
    void MoveBezier()
    { 

        bezierMove.transform.position += (Vector3)move * sensivity ;
       
        
        var a = bezierMove.transform.position.y ;
        var b = bezierMove.transform.position.x;

       

        if (a < 0)
            bezierMove.transform.position = new Vector3(bezierMove.transform.position.x,0,bezierMove.transform.position.z);


        if (b>3)
        {
            MoveGoal();
         }
        if(b<-3f)
        {
            MoveGoal();
         }
        else
        {
           // bezierMove.transform.position = new Vector3(bezierMove.transform.position.x, bezierMove.transform.position.y, bezierMove.transform.position.z);
          //  Debug.Log(6);

        }
        if(a > 3f)
        {
             MoveGoal();
 
        }
        else
        {
            //bezierMove.transform.position = new Vector3(bezierMove.transform.position.x, bezierMove.transform.position.y, bezierMove.transform.position.z);
            //Debug.Log(8);

            //   ResetGoalPos();
        }

    }
    void ResetGoalPos()
    {
       // bezierGoal.transform.position = goalStartPos;
    }
    void MoveGoal()
    {
        Vector3 a = (Vector3)move;
         bezierGoal.transform.position = new Vector3(bezierGoal.transform.position.x+(a.x*sensivity/2),bezierGoal.transform.position.y+(a.y*sensivity/2),bezierGoal.transform.position.z);

        if(Mathf.Abs(bezierGoal.transform.position.x)>3.5f)
        {
            
            if(bezierGoal.transform.position.x> 3.5f)
            bezierGoal.transform.position = new Vector3(3.5f, bezierGoal.transform.position.y, bezierGoal.transform.position.z);

            else
                bezierGoal.transform.position = new Vector3(-3.5f, bezierGoal.transform.position.y, bezierGoal.transform.position.z);

        }
 

        if ((bezierGoal.transform.position.y) > 3f)
        {
 
            bezierGoal.transform.position = new Vector3(bezierGoal.transform.position.x, 3f, bezierGoal.transform.position.z);

        }
        if ((bezierGoal.transform.position.y) < 0.1f)
        {
 
            bezierGoal.transform.position = new Vector3(bezierGoal.transform.position.x, 0.1f, bezierGoal.transform.position.z);

        }
    }
}
