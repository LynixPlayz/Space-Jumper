using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AnimationHandler : MonoBehaviour
{
    public Animator bossAnimator;
    public Animator playerAnimator;
    public Animator waterGunAnimator;
    public Animator treasureAnimator;
    public Animator bundleAnimator;
    public Animator coinsAnimator;
    public Animator winUiAnimator;
    public Animator finishLineAnimator;
    public GameObject coins;
    public Main game;
    public List<Transform> uiList;

    void Start()
    {
        BossDeathEvent.BOSS_DEATH.AddListener(FinishLineAnim);
    }

    public void ScaleBoss()
    {
        if (bossAnimator.GetBool("playSizeUp") == false){
            bossAnimator.SetBool("playSizeUp", true);
            StartCoroutine(ToggleOff("playSizeUp", bossAnimator, 0.5f));
        }     
    }

    public void FinishLineAnim()
    {
        game.uiobject.hideAllUITimer(3495);
        game.bh.boss.SetActive(false);
        game.uiobject.finishLine.SetActive(true);
        if (finishLineAnimator.GetBool("EndGame") == false){
            finishLineAnimator.SetBool("EndGame", true);
            StartCoroutine(ToggleOff("EndGame", finishLineAnimator, 3f));
        }    
    }

    public void BossDeathAnim()
    {
        if (bossAnimator.GetBool("playBossDeath") == false){
            bossAnimator.SetBool("playBossDeath", true);
            StartCoroutine(ToggleOff("playBossDeath", bossAnimator, 3f));
        }    
    }

    IEnumerator ToggleOff(string animName, Animator animator, float afterAnimTime)
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool(animName, false);
        yield return new WaitForSeconds(afterAnimTime);
        afterAnimation(animName);
    }

    public void PlayerDeath()
    {
         if (playerAnimator.GetBool("playDeath") == false){
            playerAnimator.SetBool("playDeath", true);
            StartCoroutine(ToggleOff("playDeath", playerAnimator, 0.5f));
        }
    }

    public void afterAnimation(string animName)
    {
        if(animName == "playDeath")
        {
            game.uiobject.addRetryUI();
            game.playerEnabled = false;
            game.player.SetActive(false);
        }

        if (animName == "playBossDeath")
        {
            game.bh.boss.SetActive(false);
            
            TreasureAnimation();
            
        }

        if (animName == "addCoins")
        {
            coins.SetActive(false);
        }
        
    }

    public void TreasureAnimation()
    {
        if (treasureAnimator.GetBool("giveTreasure") == false){
            uiList = game.uiobject.hideAllUI();
            game.uiobject.bundle.SetActive(true);
            treasureAnimator.SetBool("giveTreasure", true);
            StartCoroutine(ToggleOff("giveTreasure", treasureAnimator, 0.5f));
        }
    }
    public void TreasureOpenAnimation()
    {
        if (treasureAnimator.GetBool("openTreasure") == false){
            treasureAnimator.SetBool("openTreasure", true);
            StartCoroutine(ToggleOff("openTreasure", treasureAnimator, 0.5f));
        }
    }
    public void TreasureCloseAnimation()
    {
        if (treasureAnimator.GetBool("closeTreasure") == false){
            treasureAnimator.SetBool("closeTreasure", true);
            StartCoroutine(ToggleOff("closeTreasure", treasureAnimator, 0.5f));
        }
    }

    public void BundleEnterAnimation()
    {
        if (bundleAnimator.GetBool("bundleEnter") == false){
            bundleAnimator.SetBool("bundleEnter", true);
            StartCoroutine(ToggleOff("bundleEnter", bundleAnimator, 0.5f));
        }
    }
    public void BundleExitAnimation()
    {
        if (bundleAnimator.GetBool("bundleExit") == false){
            bundleAnimator.SetBool("bundleExit", true);
            StartCoroutine(ToggleOff("bundleExit", bundleAnimator, 0.5f));
        }
    }

    public void CoinBundleAnimation()
    {
        /*if (coinsAnimator.GetBool("addCoins") == false){
            coinsAnimator.SetBool("addCoins", true);
            StartCoroutine(ToggleOff("addCoins", coinsAnimator, 0f));
        }*/
        StartCoroutine(childCoinsMove());
    }

    public void winUIAdd()
    {
        if (winUiAnimator.GetBool("winAdd") == false){
            winUiAnimator.SetBool("winAdd", true);
            StartCoroutine(ToggleOff("winAdd", winUiAnimator, 0.5f));
        }
    }

    IEnumerator childCoinsMove()
    {
        /*TODO make child have random position (offsets) and make the amount of coins customizable with code https://gamedev.stackexchange.com/questions/72976/playing-an-animation-relative-to-current-transform-in-unity*/
        for (int i = 0; i < coins.transform.childCount; i++)
        {
            GameObject child = coins.transform.GetChild(i).gameObject;
            Animator childAnimator = child.GetComponent<Animator>();
            child.SetActive(true);
            if (childAnimator.GetBool("addCoins") == false) childAnimator.SetBool("addCoins", true);
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(setActiveWait(0.5f, false, child));
            game.uiobject.coinCount += 1;
        }

        StartCoroutine(ToggleOff("addCoins", coinsAnimator, 0f));
    }

    IEnumerator setActiveWait(float waitTime, bool state, GameObject obj)
    {
        yield return new WaitForSeconds(waitTime);
        obj.SetActive(state);
    }

    void Update() {
        if(game.bh.bossID == 1)waterGunAnimator.SetFloat("Energy", game.pa.st.energy);
    }
}
