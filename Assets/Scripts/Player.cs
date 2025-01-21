using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Player : MonoBehaviour
{
    Animator animator;
    float startCounter=1f;
    bool allowShoot=false;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (startCounter <= 0)
        {
            allowShoot = true;
            return;

        }

        startCounter =startCounter-Time.deltaTime;

    }
    public void Shoot()
    {
        if(allowShoot)
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
}
