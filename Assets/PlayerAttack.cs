using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public StorageTrigger st;
    public GameObject AttackText;
    public InputHandler ih;
    public BossHandler bh;
    private float timeDest;
    public float damageDelay;
    public GameObject beamPrefab;
    public Main game;
    public int bossID;
    public LineHandlerFunc lhf;
    public GameObject beamObject;
    public LineRenderer beamLR;
    public GameObject beamParticleObject;

    public void Start()
    {
        timeDest = 0;
    }

    public void Update()
    {
        if (st.energy >= 100)
        {
            AttackText.SetActive(true);
        }
        else
        {
            AttackText.SetActive(false);
        }

        BeamAttack();
    }

    private void FixedUpdate()
    {
        //Damage Tick
        if (Tick.tick % 0.02f == 0.0f)
        {
            if (beamParticleObject.activeSelf)
            {
                st.energy -= 2;
                bh.bossHealth -= 2;
            }
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void BeamAttack()
    {
        timeDest -= Time.deltaTime;
        if (timeDest <= 0)
        {
            if (ih.spacebarCheck && game.playerEnabled)
            {
                if (st.energy >= 100)
                {
                    if (bossID == 0)
                    {
                        GameObject beam = Instantiate(beamPrefab, gameObject.transform);
                        beam.GetComponent<GlideController>().SetDestination(bh.boss.transform.position);
                        beam.transform.parent = gameObject.transform.parent;
                        beam.tag = "Weapon";
                        timeDest = Time.deltaTime + damageDelay;
                        st.energy -= 100;
                    }
                }

                if (st.energy >= 1)
                {
                    if (bossID == 1)
                    {
                        beamParticleObject.SetActive(true);

                        if (bh)
                        {
                            lhf.RenderPersistentLine(beamObject, bh.boss);
                        }

                        if (bh)
                        {
                            lhf.RenderPersistentLine(beamObject, bh.boss);
                        }
                        // Get the direction vector from the beamParticleObject's position to the bossObject's position
                        Vector3 directionToBoss = bh.boss.transform.position - beamParticleObject.transform.position;

                        // Calculate the rotation to look at the bossObject
                        Quaternion targetRotation = Quaternion.LookRotation(directionToBoss, Vector3.up);

                        // Apply the rotation to the beamParticleObject
                        beamParticleObject.transform.rotation = targetRotation;

                        // Optionally, you can log the angle between the forward direction of beamParticleObject and the direction to the bossObject.
                        float angle = Vector3.Angle(beamParticleObject.transform.forward, directionToBoss);
                        StartCoroutine(despawn());
                    }
                }
                else
                {
                    beamParticleObject.SetActive(false);
                }
            }
            else
            {
                beamParticleObject.SetActive(false);
            }
        }

        IEnumerator despawn()
        {
            yield return new WaitForSeconds(2);
            lhf.RemovePersistentLine();
        }
    }
}

