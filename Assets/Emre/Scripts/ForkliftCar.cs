using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkliftCar : MonoBehaviour
{

    public GameObject Wheel;
    public GameObject Wheel2;


    private void Start()
    {
        MoveFork();
       
    }


    public void MoveFork()
    {
        this.transform.DOMoveX(transform.position.x-5, 1.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            MoveFork2();

        });
    }
    public void MoveFork2()
    {
        this.transform.DOMoveX(transform.position.x + 5, 1.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            MoveFork();

        });
    }


}
