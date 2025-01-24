using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CollectableGold : MonoBehaviour
{
   [SerializeField] DribbleGameController controller;
    private void OnTriggerEnter(Collider other)
    {
        controller.GoldCollected(25);
        gameObject.SetActive(false);
    }
 }
