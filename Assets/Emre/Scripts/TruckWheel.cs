using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckWheel : MonoBehaviour
{
   
    private void Start()
    {
        float random = Random.RandomRange(0, 1f);
        RotateWheel();
      
    }


    public void RotateWheel()
    {

        this.transform.DOLocalRotate(new Vector3(90, 0, 0), 0.75f).SetEase(Ease.Linear).OnComplete(() =>
        {
            RotateWheel2();

        });
    }
    public void RotateWheel2()
    {
        this.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.75f).SetEase(Ease.Linear).OnComplete(() =>
        {
            RotateWheel();

        });
    }
}
