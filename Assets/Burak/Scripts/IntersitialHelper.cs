using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersitialHelper : MonoBehaviour
{
    [SerializeField] AdManager adManager;


    private void Awake()
    {
        adManager.Init();
    }

    public void ShowIntersitial()
    {
        if(adManager.InterstatialAdManager.IsInterstatialAdReady())
        {
            adManager.InterstatialAdManager.ShowAd();
        }
    }
}
