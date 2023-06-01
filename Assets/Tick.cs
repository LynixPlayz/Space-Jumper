using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tick : MonoBehaviour
{
    public float tick;

    void Start()
    {
        tick = 0f;
    }

    void FixedUpdate()
    {
        if (tick >= 1.0f)
        {
            tick = 0.0f;
        }
        else
        {
            tick += 0.02f;
        }
    }
}
