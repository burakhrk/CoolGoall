using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class TrailTest : MonoBehaviour
{
    public GameObject target;
   [SerializeField] ParticleSystem particle;
  
    void Update()
    {
        if (target)
            transform.position = target.transform.position;
    }
    public void SetParticleLevel1()
    {
        particle.startSize = 0.06f;
    }
    public void SetParticleLevel2()
    {
        particle.startSize = 0.09f;
    }
    public void SetParticleLevel3()
    {
        particle.startSize = 0.12f;
    }

}
