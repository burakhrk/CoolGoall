using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forklift : MonoBehaviour
{
    private void Start()
    {
        MoveFork();
    }


    public void MoveFork()
    {
        this.transform.DOMoveY(2.7f, 1.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            MoveFork2();

        });
    }
    public void MoveFork2()
    {
        this.transform.DOMoveY(0, 1.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            MoveFork();

        });
    }
}
