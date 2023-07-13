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
    [SerializeField] GameObject bezierMove;
    [SerializeField] float sensivity=1f;
    private void Awake()
    {
       bezierStartPos= bezierMove.transform.position;
    }
    private void Update()
    {
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
        
        bezierMove.transform.position = bezierStartPos;
    }
    void MoveBezier()
    { 

        bezierMove.transform.position += (Vector3)move * sensivity * sensivity;

        var a = bezierMove.transform.position.y ;
        if (a < 0)
            bezierMove.transform.position = new Vector3(bezierMove.transform.position.x,0,bezierMove.transform.position.z);

    }
}
