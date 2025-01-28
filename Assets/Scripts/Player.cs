using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Player : MonoBehaviour
{
    Animator animator;
     bool allowShoot=false;
  [SerializeField]  GameController gameController;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    } 
    public void Shoot()
    {
        if(gameController.CanShoot)
        animator.SetBool("Shoot",true);
    }
    public void Goal()
    {
        animator.SetTrigger("Goal");
    }
    public void Fail()
    {
        animator.SetTrigger("Fail");

    }
    public void AnimationEnd()
    {

    }
}
