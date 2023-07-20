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

            var a = goalKeeper.transform.position.x - other.gameObject.transform.position.x;
            var b = goalKeeper.transform.position.y - other.gameObject.transform.position.y;
            if (a>0)
            {
                if(b<0)
                goalKeeper.Jump(other.gameObject, 2,true);
                else
                    goalKeeper.Jump(other.gameObject, 2, true);

            }
            if (a < 0)
            {
                if(b<0)
                goalKeeper.Jump(other.gameObject, 1,true);
                else
                    goalKeeper.Jump(other.gameObject, 1,false);


            }


        }
    }
}

