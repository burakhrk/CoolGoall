using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalKeeperTrigger : MonoBehaviour
{
    bool workOnce = false;
    private void OnTriggerEnter(Collider other)
    {
        if (workOnce)
            return;

        if (other.gameObject.GetComponent<Ball>() != null)
        {
            workOnce = true;
            GetComponentInParent<GoalKeeper>().Jump(other.gameObject);
        }
    }
}

