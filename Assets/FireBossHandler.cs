using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class FireBossHandler : MonoBehaviour
{
    public GameObject miniFirePrefab;
    public GameObject borderArea;
    public float bossHealth;
    public GameObject boss;
    public float startTime;
    public float randomNum;
    public float currentTime;
    public float startTime2;
    public float randomNum2;
    public float currentTime2;
    public AnimationHandler ah;
    public bool attack1ready;
    public LineRenderer fireLine1;
    public LineRenderer fireLine2;
    public List<GameObject> lineFireList;

    public void Start()
    {
        bossHealth = 100;
        startTime = 0;
        randomNum = Random.Range(2, 5);
        currentTime = 0.1f;
        startTime2 = 0;
        randomNum2 = Random.Range(5, 10);
        currentTime2 = 0.1f;
        attack1ready = true;
    }

    public void BossAttack1()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject miniFire = Instantiate(miniFirePrefab, gameObject.transform.GetChild(0).transform);
            miniFire.GetComponent<GlideController>().SetDestination(RandomPos(borderArea)); 
            ah.game.fireList.Add(miniFire);
        }
    }

    public void BossAttack2()
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

    void Update()
    {
        if(GameObject.FindGameObjectsWithTag ("MiniFire").Length != 0)
        {
            attack1ready = false;
        }
        else{
            attack1ready = true;
        }
        currentTime += Time.deltaTime / 2;
        currentTime2 += Time.deltaTime / 2;

        if (Mathf.Round(currentTime * 100.0f) / 100.0f == randomNum)
        {
            if(attack1ready){BossAttack1();}
            startTime = Time.deltaTime;
            Debug.Log("In If");
            randomNum = Random.Range(2, 5);
            currentTime = 0.1f;
        }
        if (Mathf.Round(currentTime * 100.0f) / 100.0f == randomNum - 1 && attack1ready)
        {
            //Play Animation
            ah.ScaleBoss();
        }
        if (Mathf.Round(currentTime2 * 100.0f) / 100.0f == randomNum2)
        {
            BossAttack2();
            startTime2 = Time.deltaTime;
            Debug.Log("In If");
            randomNum2 = Random.Range(10, 15);
            currentTime2 = 0.1f;
        }
        if (Mathf.Round(currentTime2 * 100.0f) / 100.0f == randomNum2 - 1)
        {
            //Play Animation
            ah.ScaleBoss();
        }
    }
}