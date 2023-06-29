using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropletBossHandler : MonoBehaviour
{
    public GameObject dropletMiniPrefab;
    public Main game;
    public List<GameObject> list;

    void Start()
    {
        
    }

    void Update()
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

    public void BossAttack1()
    {
        list = new List<GameObject>();
        StartCoroutine(WaitForRain(list));
        StartCoroutine(despawn(list));
    }
    
    void Rain(List<GameObject> list)
    {
        Debug.Log("test3");
        for(int i = 0; i < Random.Range(3, 10); i++)
        {
            Debug.Log("test4");
            Vector2 randomPos = new Vector3(Random.Range(0, 16) - 8, 6, 0);
            GameObject droplet = Instantiate(dropletMiniPrefab, randomPos, gameObject.transform.rotation);
            Vector3 pos = droplet.transform.position;
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
}
