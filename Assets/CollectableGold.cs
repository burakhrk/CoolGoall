using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CollectableGold : MonoBehaviour
{
    [SerializeField] AudioClip clip;

    [SerializeField] DribbleGameController controller;
     private void OnTriggerEnter(Collider other)
    {
        controller.GoldCollected(25);
        gameObject.SetActive(false);
        controller.soundController.PlayAudio(clip);
    }
       
}
