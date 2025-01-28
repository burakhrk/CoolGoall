using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailTest : MonoBehaviour
{
    public GameObject target;
     
    void Update()
    {
        if(target)
        transform.position = target.transform.position;
    }
}
