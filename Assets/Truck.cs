using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Truck : MonoBehaviour
{
    [SerializeField] GameObject tire1, tire2, tire3, tire4;


    private void Start()
    {
        tire1.transform.DOLocalRotateQuaternion(Quaternion.Euler(90,0,0),2f) ;
    }
}
