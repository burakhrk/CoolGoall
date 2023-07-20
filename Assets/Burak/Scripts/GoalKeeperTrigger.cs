using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalKeeperTrigger : MonoBehaviour
{
    bool workOnce = false;
    GoalKeeper goalKeeper;
    private void Awake()
    {
        goalKeeper = GetComponentInParent<GoalKeeper>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (workOnce)
            return;

        if (other.gameObject.GetComponent<Ball>() != null)
        {
            workOnce = true;


            if (goalKeeper.transform.position.x-other.gameObject.transform.position.x>0)
                goalKeeper.Jump(other.gameObject,2);
            if (goalKeeper.transform.position.x - other.gameObject.transform.position.x < 0)
                goalKeeper.Jump(other.gameObject, 1);
        }
    }
}

