using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GoalKeeper : MonoBehaviour
{
    bool workOnce = false;
    private Animator anima;

    bool doNotMove=false;
    float startx=0f;
    private void Awake()
    {
        anima = GetComponent<Animator>();
    }
    private void Start()
    {
        startx = transform.position.x;
        if (doNotMove)
            return;

                StartCoroutine(kaleciMove());
    }
    Tween mySequence;
    Tween tween;
    IEnumerator kaleciMove()
    {
        while (!doNotMove)
        {
           

            anima.SetBool("L",true);
            anima.SetBool("R", false);

           mySequence = transform.DOMoveX(startx + 3f, 2.5f).SetEase(Ease.Linear);
             

            
            yield return new WaitForSeconds(2.5f);
            
            anima.SetBool("L", false);
            anima.SetBool("R", true);

          tween=  transform.DOMoveX(startx - 3f, 2.5f).SetEase(Ease.Linear);
                
           

            yield return new WaitForSeconds(2.5f);
            
        }
    }
   
   public void Jump(GameObject ball , int direct,bool up )
    {
        doNotMove = true;
        StopCoroutine(kaleciMove());
        mySequence.Kill();
        tween.Kill();
        DOTween.Kill(gameObject);
        if (direct==1)
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
