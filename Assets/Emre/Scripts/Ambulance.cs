using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ambulance : MonoBehaviour
{

    public GameObject Wheel1;
    public GameObject Wheel2;


    

    private void Update()
    {
        AmbulanceMover();
    }

    void AmbulanceMover()
    {
        Wheel1.transform.Rotate(new Vector3(-10,transform.rotation.y,transform.rotation.z));
        Wheel2.transform.Rotate(new Vector3(-10,transform.rotation.y,transform.rotation.z));
        
        
        
        transform.DOMoveX(transform.position.x+35, 3.5f).SetEase(Ease.Linear).OnComplete(() =>
        {

            Destroy(gameObject);
        });
    }

}
