using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public Animator bossAnimator;
    public Animator playerAnimator;
    public Main game;

    public void ScaleBoss()
    {
        if (bossAnimator.GetBool("playSizeUp") == false){
            bossAnimator.SetBool("playSizeUp", true);
            StartCoroutine(ToggleOff("playSizeUp", bossAnimator));
        }     
    }

    IEnumerator ToggleOff(string animName, Animator animator)
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool(animName, false);
        yield return new WaitForSeconds(0.5f);
        afterAnimation(animName);
    }

    public void PlayerDeath()
    {
         if (playerAnimator.GetBool("playDeath") == false){
            playerAnimator.SetBool("playDeath", true);
            StartCoroutine(ToggleOff("playDeath", playerAnimator));
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
    }
}
