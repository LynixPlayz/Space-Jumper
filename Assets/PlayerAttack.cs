using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public StorageTrigger st;
    public GameObject AttackText;
    public InputHandler ih;
    public FireBossHandler bh;
    private float timeDest;
    public float damageDelay;
    public GameObject beamPrefab;
    public Main game;

    public void Start()
    {
        timeDest = 0;
    }

    void Update(){
        if(st.energy >= 100)
        {
            AttackText.SetActive(true);
        }
        else
        {
            AttackText.SetActive(false);
        }
        BeamAttack();
    }

    void BeamAttack()
    {   
        timeDest -= Time.deltaTime;
        if(timeDest <= 0)
        {
            if(ih.spacebarCheck && st.energy >= 100 && game.playerEnabled){
                GameObject beam = Instantiate(beamPrefab, gameObject.transform);
                beam.GetComponent<GlideController>().SetDestination(bh.boss.transform.position);
                beam.transform.parent = gameObject.transform.parent;
                beam.tag = "Weapon";
                timeDest = Time.deltaTime + damageDelay;
                st.energy -= 100;
            } 
        }
    }
}

