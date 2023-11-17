using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropletBossHandler : BossHandler
{
    public GameObject dropletMiniPrefab;
    public List<GameObject> list;
    public GameObject waterGun;
    
    public float lastWaterGunChange;

    private new void Start()
    {
        bossID = 1;
    }
    public override void extraUpdate()
    {
        game.dropletList = list;
        
    }

    IEnumerator despawn(List<GameObject> list)
    {
        yield return new WaitForSeconds(10);
        foreach(GameObject miniDroplet in list)
        {
            Destroy(miniDroplet);
        }
    }
    public override void BossAttack1()
    {
        list = new List<GameObject>();
        StartCoroutine(WaitForRain(list));
        StartCoroutine(despawn(list));
        isAttack1Waiting = false;
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    void Rain(List<GameObject> list)
    {
        
        for(int i = 0; i < Random.Range(3, 10); i++)
        {
            Vector2 randomPos = new Vector3(Random.Range(0, 16) - 8, 6, 0);
            GameObject droplet = Instantiate(dropletMiniPrefab, randomPos, gameObject.transform.rotation);
            Vector3 pos = droplet.transform.position;
            droplet.transform.localScale /= 2;
            droplet.GetComponent<GlideController>().SetDestination(new Vector3(pos.x, -8, 0));
            list.Add(droplet);
        }
    }

    IEnumerator WaitForRain(List<GameObject> list)
    {
        for(int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(2);
            Rain(list);
        }  
    }

    public override void BossAttack2()
    {
        waterGun.SetActive(true);
        StartCoroutine(RainSemiDeadly());
        StartCoroutine(RemoveWaterGun(Time.deltaTime));
        lastWaterGunChange = Time.deltaTime;
        
    }

    IEnumerator RemoveWaterGun(float timeNow)
    {
        yield return new WaitForSeconds(9);
        if(lastWaterGunChange.Equals(timeNow)) waterGun.SetActive(false);
        isAttack2Waiting = false;
    }

    IEnumerator RainSemiDeadly()
    {
        for(int i = 0; i < Random.Range(3, 10); i++)
        {
            Vector2 randomPos = new Vector3(Random.Range(0, 16) - 8, 6, 0);
            GameObject droplet = Instantiate(dropletMiniPrefab, randomPos, gameObject.transform.rotation);
            int randomNum = Random.Range(1, 3);
            if(randomNum == 1)
            {
                droplet.GetComponent<SpriteRenderer>().color = Color.blue;
                droplet.tag = "MiniDropletCollectable";
            }
            Vector3 pos = droplet.transform.position + new Vector3(Random.Range(0, 16) - 8, 0, 0);
            droplet.GetComponent<GlideController>().SetDestination(new Vector3(pos.x, -8, 0));
            list.Add(droplet);
            yield return new WaitForSeconds(1);
        }
    }
}
