using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class BossHandler : MonoBehaviour
{
    public float bossHealth;
    public AnimationHandler ah;
    public GameObject boss;
    public Main game;
    public bool attack1Ready = true;
    public bool attack2Ready = true;
    public int bossID = 0;
    public bool isBossDead;
        
    //Random Attack Vars
    public bool isAttack1Waiting;
    public bool isAttack2Waiting;
    public Vector2 attack1TimeMinAndMax;
    public Vector2 attack2TimeMinAndMax;
    
    //Others
    public bool debug;
    public void Start()
    {
        boss = gameObject;
        bossHealth = 100;
    }

    public abstract void BossAttack1();

    public abstract void BossAttack2();

    public virtual void beforeAttack(){}
    
    public virtual void extraUpdate(){}
    
    public void Update()
    {
        
        extraUpdate();
        if (bossHealth <= 0 && isBossDead == false)
        {
            ah.BossDeathAnim();
            isBossDead = true;
        }
        if (!isAttack1Waiting)
        {
            if (debug) Debug.Log("coroutine1");
            StartCoroutine(runBossAttack1());
            isAttack1Waiting = true;
        }
        if (!isAttack2Waiting)
        {
            if(debug)Debug.Log("coroutine2");
            StartCoroutine(runBossAttack2());
            isAttack2Waiting = true;
        }
    }

    public void setHealth(float health)
    {
        bossHealth = health;
    }

    public IEnumerator runBossAttack1()
    {
        if(debug)Debug.Log("BossAttack1");
        float randomNum = Random.Range(attack1TimeMinAndMax.x, attack1TimeMinAndMax.y) ;
        if (debug) Debug.Log("waiting for " + randomNum);
        yield return new WaitForSeconds(randomNum - 1);
        beforeAttack();
        yield return new WaitForSeconds(1);
        if (debug) Debug.Log("finished waiting for " + randomNum + "will run if this is true " + attack1Ready);
        
        if(attack1Ready) BossAttack1();
        isAttack1Waiting = false;
    }

    public IEnumerator runBossAttack2()
    {
        if(debug)Debug.Log("BossAttack2");
        float randomNum = Random.Range(attack2TimeMinAndMax.x, attack2TimeMinAndMax.y) ;
        yield return new WaitForSeconds(randomNum - 1);
        beforeAttack();
        yield return new WaitForSeconds(1);
        if(attack2Ready) BossAttack2();
        isAttack2Waiting = false;
    }
}