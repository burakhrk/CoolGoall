using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Player : MonoBehaviour
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Shoot()
    {
        animator.SetBool("Shoot",true);
    }
    public void Goal()
    {
        animator.SetTrigger("Goal");
    }
 }
