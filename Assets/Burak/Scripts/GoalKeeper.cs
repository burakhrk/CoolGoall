using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalKeeper : MonoBehaviour
{
    bool workOnce = false;
    private Animator anima;
    private void Awake()
    {
        anima = GetComponent<Animator>();
    }
   
   public void Jump(GameObject ball )
    {
        anima.SetBool("Jump",true);
    }
}
