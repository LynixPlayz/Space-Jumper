using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageTrigger : MonoBehaviour
{
    public float energy;
    public AnimationHandler ah;

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Test");
        if(col.tag == "MiniFire" && ah.game.playerEnabled)
        {
            Destroy(col.gameObject);
            energy += 25;
            Debug.Log("Collider 1");
            
        }
        if (col.offset.y <= 0.0f)
        {
            ah.PlayerDeath();
        }
    }

    void Update()
    {
        if(energy >= 100)
        {
            energy = 100;
        }
    }
}
