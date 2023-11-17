using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Random=UnityEngine.Random;

public class FireBossHandler : BossHandler
{
    public GameObject miniFirePrefab;
    public GameObject borderArea;
    public LineRenderer fireLine1;
    public LineRenderer fireLine2;
    public List<GameObject> lineFireList;
    public bool firstBossDeadRun = false;
    public bool tutorialPauseAttack;

    public void Start()
    {
        bossHealth = 100;
        attack1Ready = true;
        bossID = 0;
    }

    public override void BossAttack1()
    {
        if(debug){Debug.Log("attack1Called");}
        for (int i = 0; i < 4; i++)
        {
            GameObject miniFire = Instantiate(miniFirePrefab, gameObject.transform.GetChild(0).transform);
            miniFire.GetComponent<GlideController>().SetDestination(RandomPos(borderArea));
            ah.game.fireList.Add(miniFire);
        }

        
    }

    public override void BossAttack2()
    {
        gameObject.transform.GetChild(0).transform.position = new Vector3(gameObject.transform.GetChild(0).transform.position.x, 0, gameObject.transform.GetChild(0).transform.position.z);
        Vector3 firstPos =  gameObject.transform.GetChild(0).transform.position += new Vector3(0, Random.Range(-2, 4), 0);
        GameObject miniFire = Instantiate(miniFirePrefab, gameObject.transform.GetChild(0).transform);
        miniFire.transform.position = firstPos;
        miniFire.transform.eulerAngles += new Vector3(0, 0, 90);
        miniFire.transform.localScale *= 2;
        Vector3 secondPos =  gameObject.transform.GetChild(0).transform.position -= new Vector3(0, Random.Range(2, 8), 0);
        GameObject miniFire2 = Instantiate(miniFirePrefab, gameObject.transform.GetChild(0).transform);
        miniFire2.transform.position = firstPos;
        miniFire2.transform.eulerAngles += new Vector3(0, 0, 90);
        miniFire2.transform.localScale *= 2;
        miniFire.transform.parent = gameObject.transform.parent;
        miniFire2.transform.parent = gameObject.transform.parent;
        fireLine1 = miniFire.AddComponent<LineRenderer>();
        fireLine2 = miniFire2.AddComponent<LineRenderer>();
        miniFire.AddComponent<LineHandler>();
        miniFire2.AddComponent<LineHandler>();
        miniFire.GetComponent<GlideController>().SetDestination(miniFire.transform.position - new Vector3(28, 0, 0)); 
        miniFire2.GetComponent<GlideController>().SetDestination(miniFire2.transform.position - new Vector3(28, 0, 0)); 
        miniFire.GetComponent<GlideController>().speed = 12;
        miniFire2.GetComponent<GlideController>().speed = 12;
        lineFireList.Add(miniFire);
        lineFireList.Add(miniFire2);
        ah.game.fireList.Add(miniFire);
        ah.game.fireList.Add(miniFire2);
        StartCoroutine(despawn(lineFireList));
        
    }

    IEnumerator despawn(List<GameObject> list)
    {
        yield return new WaitForSeconds(10);
        foreach(GameObject miniFire in list)
        {
            Destroy(miniFire);
        }
    }
    
    public void instantDespawn(List<GameObject> list)
    {
        foreach(GameObject miniFire in list)
        {
            Destroy(miniFire);
        }
    }

    public static Vector3 RandomPos(GameObject area)
    {   float x;
        float y;
        float z;

        x = Random.Range(area.transform.GetChild(0).transform.position.x, area.transform.GetChild(1).transform.position.x);
        y = Random.Range(area.transform.GetChild(0).transform.position.y, area.transform.GetChild(1).transform.position.y);
        z = 0;

        Vector3 result = new Vector3(x, y, z);
        //Debug.Log(x.ToString() + y.ToString());
        return result;
    }

    public override void beforeAttack()
    {
        ah.ScaleBoss();
    }

    public override void extraUpdate()
    {
        if(GameObject.FindGameObjectsWithTag ("MiniFire").Length != 0 || tutorialPauseAttack)
        {
            attack1Ready = false;
        }
        else{
            attack1Ready = true;
        }
        
        if(tutorialPauseAttack)
        {
            attack2Ready = false;
        }
        else{
            attack2Ready = true;
        }

        if (isBossDead && firstBossDeadRun == false)
        {
            instantDespawn(lineFireList);
            instantDespawn(ah.game.fireList);
            BossDeathEvent.BOSS_DEATH.Invoke();
            firstBossDeadRun = true;
            tutorialPauseAttack = true;
        }
        //if(debug) Debug.Log(GameObject.FindGameObjectsWithTag ("MiniFire"));
    }
}