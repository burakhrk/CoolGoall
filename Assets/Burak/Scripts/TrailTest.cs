using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class TrailTest : MonoBehaviour
{
    public GameObject target;
   ParticleSystem particle;
    ParticleSystem.MainModule psmain;
    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
        if (!particle)
            Debug.LogError("adasdasd");

    }
    void Update()
    {
        if (target)
            transform.position = target.transform.position;
    }
    public void SetParticleLevel1()
    {
        psmain = particle.main;


        psmain.startSize = 0.06f;
    }
    public void SetParticleLevel2()
    {
        psmain = particle.main;

        psmain.startSize = 0.09f;
    }
    public void SetParticleLevel3()
    {
        psmain = particle.main;

        psmain.startSize = 0.12f;
    }
    public void SetParticleLevel4()
    {
        psmain = particle.main;


        psmain.startSize = 0.14f;
    }
}
