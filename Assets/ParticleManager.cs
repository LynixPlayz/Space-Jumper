using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public ParticleSystem stars;
    public ParticleSystem dash;
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

    public void RunDashParticles()
    {
        dash.Play();
    }

    public static ParticleSystem GetParticleSystemTransform(ParticleSystem ps, Transform transform)
    {
        ParticleSystem newParticleSystem = ps;
        var sh = newParticleSystem.shape;
        sh.position = transform.position;
        sh.rotation = transform.rotation.eulerAngles;
        sh.scale = transform.localScale;
        return newParticleSystem;
    }
}
