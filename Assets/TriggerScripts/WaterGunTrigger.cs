using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGunTrigger : MonoBehaviour
{
    public StorageTrigger st;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "MiniDropletCollectable")
        {
            st.energy += 50;
            if(st.energy > 100){ st.energy = 100; }
            Destroy(col.gameObject);
        }
    }
}
