using System.Collections;
using enums;
using UnityEngine;

namespace TriggerScripts
{
    public class OtherCollisionTriggers : MonoBehaviour
    {
        public Main game;
        public GameObject coins;
        public bool treasureGiven = false;
        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Treasure"))
            {
                StartCoroutine(Wait(10, AnimationWait.OpenTreasure));
                game.bh.ah.BundleEnterAnimation();
            }
        }

        IEnumerator Wait(float milliseconds, AnimationWait waitFor)
        {
            yield return new WaitForSeconds(milliseconds / 1000);
            if (waitFor.Equals(AnimationWait.AddCoinsToBundle) && !treasureGiven)
            {
                coins.SetActive(true);
                game.bh.ah.CoinBundleAnimation();
                treasureGiven = true;
                StartCoroutine(Wait(5000, AnimationWait.CloseTreasure));
            }
            else if (waitFor.Equals(AnimationWait.ExitBundle))
            {
                game.bh.ah.treasureAnimator.gameObject.SetActive(false);
                //foreach (var item in game.gameah.uiList) item.gameObject.SetActive(true);
                game.bh.ah.BundleExitAnimation();
                StartCoroutine(Wait(1000, AnimationWait.Win));
            }
            else if (waitFor.Equals(AnimationWait.OpenTreasure))
            {
                game.bh.ah.TreasureOpenAnimation();
                StartCoroutine(Wait(1000, AnimationWait.AddCoinsToBundle));
            }
            else if (waitFor.Equals(AnimationWait.CloseTreasure))
            {
                game.bh.ah.TreasureCloseAnimation();
                StartCoroutine(Wait(1000, AnimationWait.ExitBundle));
            }
            else if (waitFor.Equals(AnimationWait.Win))
            {
                game.uiobject.addWinUI();
            }
        }
    }
}