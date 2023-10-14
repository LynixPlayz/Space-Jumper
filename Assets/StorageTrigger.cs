using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageTrigger : MonoBehaviour
{
    public float energy;
    public AnimationHandler ah;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "MiniFire" && ah.game.playerEnabled)
        {
            Destroy(col.gameObject);
            energy += 25;

        }
        if (col.offset.y < 0.0f && col.tag != "Weapon")
        {
            ah.PlayerDeath();
        }
        if(col.tag == "MiniDropletDeadly")
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

    public void setEnergy(float energyFloat)
    {
        energy = energyFloat;
    }
}
