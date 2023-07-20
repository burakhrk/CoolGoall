using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GoalKeeper : MonoBehaviour
{
    bool workOnce = false;
    private Animator anima;
    private void Awake()
    {
        anima = GetComponent<Animator>();
    }
   
   public void Jump(GameObject ball , int direct,bool up )
    {
        if(direct==1)
        {
            anima.SetBool("JumpLeft", true);
            Vector3 a = new Vector3(ball.transform.position.x, transform.position.y, transform.position.z);
            if (up)
               a= a + Vector3.up;
            transform.DOMove(a + Vector3.left , 0.2f);
        }
        if (direct == 2)
        {
            anima.SetBool("JumpRight", true);
            Vector3 a = new Vector3(ball.transform.position.x, transform.position.y, transform.position.z);
            if (up)
                a = a + Vector3.up;

            transform.DOMove(a + Vector3.right , 0.2f);
        }
        /*
        if (direct == 3)
        {
            anima.SetBool("JumpUp", true);
            Vector3 a = new Vector3(ball.transform.position.x, transform.position.y, transform.position.z);
            transform.DOMove(a + Vector3.up * 2, 0.2f);
        }
        */
    }
}
