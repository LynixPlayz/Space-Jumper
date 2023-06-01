using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public ParticleSystem stars;
    public void RunAllParticles()
    {
        RunBackgroundParticles();
    }

    void RunBackgroundParticles()
    {
        stars.Play();
    }

    void FixedUpdate()
    {
        
    }
}
