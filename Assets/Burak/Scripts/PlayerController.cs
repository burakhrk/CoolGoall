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
    private void Awake()
    {
       bezierStartPos= bezierMove.transform.position;
        goalStartPos = bezierGoal.transform.position;
    }
    private void Update()
    {
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


        if (Mathf.Abs(b)>3)
        {
            bezierMove.transform.position = new Vector3(bezierMove.transform.position.x, bezierMove.transform.position.y, bezierMove.transform.position.z);
            MoveGoal();
        }

        if (Mathf.Abs(b) > 7f)
        {
             MoveGoal();
        }
        else
        {
            ResetGoalPos();
        }
       
    }
    void ResetGoalPos()
    {
        bezierGoal.transform.position = goalStartPos;
    }
    void MoveGoal()
    {
        Vector3 a = (Vector3)move;
         bezierGoal.transform.position = new Vector3(bezierGoal.transform.position.x-(a.x*sensivity),bezierGoal.transform.position.y,bezierGoal.transform.position.z);
    }
}
