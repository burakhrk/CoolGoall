using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UIBall : MonoBehaviour
{
    DOTweenAnimation a;
    private void Awake()
    {
        a = GetComponent<DOTweenAnimation>();
    }
    public void Init()
    {
        Invoke("asd", 0.1f);
    }
    void asd()
    {
        a.DOPlay();

    }
}
