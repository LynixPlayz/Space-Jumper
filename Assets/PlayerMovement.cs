using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;
    public Camera mainCamera;
    public GameObject borderArea;
    public Main game;
    private bool pauseMovement;
    public float timeLeft;
    public GameObject DashEffectTimer;
    public TMP_Text DashEffectTimerText;
    public InputHandler ih;
    public bool countDown;

    void Update()
    {
        if(timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        else
        {
            timeLeft = 0;
        }
    }
    public void FixedUpdate()
    {
        if(ih.eCheck && timeLeft <= 0)
        {
            Dash();
        }
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        if(game.playerEnabled && !pauseMovement)
        {
            player.transform.position = HandleParams(worldPosition, borderArea);
        }
        DashEffectTimerText.text = ((Mathf.Round(timeLeft * 100)) / 10).ToString();
    }

    public static Vector2 HandleParams(Vector2 pos, GameObject area)
    {
        if (pos.y >= area.transform.GetChild(0).transform.position.y)
        {
            pos.y = area.transform.GetChild(0).transform.position.y;
        }
        if (pos.y <= area.transform.GetChild(1).transform.position.y)
        {
            pos.y = area.transform.GetChild(1).transform.position.y;
        }
        if (pos.x <= area.transform.GetChild(0).transform.position.x)
        {
            pos.x = area.transform.GetChild(0).transform.position.x;
        }
        if (pos.x >= area.transform.GetChild(1).transform.position.x)
        {
            pos.x = area.transform.GetChild(1).transform.position.x;
        }
        return pos;
    }

    public void Dash()
    {
        timeLeft = 1;
        DashEffectTimer.SetActive(true);
        StartCoroutine(movementPause());
    }

    IEnumerator movementPause()
    {
        pauseMovement = true;
        yield return new WaitForSeconds(1);
        pauseMovement = false;
    }
}
