using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTriggers : MonoBehaviour
{
    public BossHandler bh;
    public bool needDamage;
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Recieved");
        if(col.tag == "Weapon")
        {
            Destroy(col.gameObject);
            //Making fixed update handle damage instead of OnTrigger
            //bh.bossHealth -= 20;
            needDamage = true;
        }
        else{
            Debug.Log(col.tag + " - " + col.name + " interacted with " + gameObject.tag + " - " + gameObject.name);
        }
    }

    void FixedUpdate()
    {
        if(needDamage)
        {
            needDamage = false;
            bh.bossHealth -= 20;
        }
    }
}
