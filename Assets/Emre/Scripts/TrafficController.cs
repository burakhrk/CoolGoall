using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrafficController : MonoBehaviour
{
    public GameObject Ambulance;
    public GameObject Bus;
    GameController controller;
    bool stopSpawn;


    private void Awake()
    {
        controller = FindObjectOfType<GameController>();
    }


    private void OnEnable()
    {
        controller.OnGameEnd += SpawnController;
    }

    private void OnDisable()
    {
        controller.OnGameEnd -= SpawnController;
    }

    void Start()
    {
        
        Ambulance.transform.position = transform.position;
        StartCoroutine(CarTimer());
        
    }

    
  


    void SpawnController()
    {
        stopSpawn = true;
    }


    IEnumerator CarTimer()
    {

        while (!stopSpawn) 
        {
            Instantiate(Ambulance, new Vector3(-20, 0, 0), Quaternion.identity);
            yield return new WaitForSeconds(3f);
            Instantiate(Bus, new Vector3(45, 0, 0), Quaternion.identity);
            yield return new WaitForSeconds(4f);


        }

    }

   
}
