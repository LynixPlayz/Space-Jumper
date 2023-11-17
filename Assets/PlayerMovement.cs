using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
    public GameObject dashParticleSystemObj;
    public Vector2 lastMovePos;
    public bool challengeMode;

    private GameObject originalParent;
    public bool scriptDisabled;

    void Flap()
    {
        float x = player.transform.GetComponent<Rigidbody2D>().velocity.x;
        player.transform.GetComponent<Rigidbody2D>().velocity = new Vector2(x, 6);
        //Debug.Log("Flappedd");
    }
    
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex < 2) scriptDisabled = true;
        else scriptDisabled = false;
        if (scriptDisabled) return;
        if (player.Equals(null))
        {
            PlayerMovementVariables variables =
                GameObject.FindWithTag("PlayerVariables").transform.GetComponent<PlayerMovementVariables>();
            Debug.Log(variables);
            player = variables.player;
            mainCamera = variables.mainCamera;
            borderArea = variables.borderArea;
            timeLeft = variables.timeLeft;
            DashEffectTimer = variables.DashEffectTimer;
            DashEffectTimerText = variables.DashEffectTimerText;
            ih = variables.ih;
            dashParticleSystemObj = variables.dashParticleSystemObj;
            lastMovePos = variables.lastMovePos;
            game = variables.game;
        }
        if (!challengeMode) {player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;} else {player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;}
        Debug.Log(player.GetComponent<Rigidbody2D>().constraints);
        if (Input.GetKeyDown(KeyCode.Space) && challengeMode)
        {
            Flap();
        }
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
        if (scriptDisabled) return;
        if(ih.eCheck && timeLeft <= 0)
        {
            Dash();
        }
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        if(game.playerEnabled && !pauseMovement && !challengeMode)
        {
            player.transform.position = HandleParams(worldPosition, borderArea);
            Quaternion rot = player.transform.rotation;
            player.transform.rotation = Quaternion.Euler(rot.x, rot.y, (HandleParams(worldPosition, borderArea) - lastMovePos).y * 20);
            lastMovePos = HandleParams(worldPosition, borderArea);
        }
        if (game.playerEnabled && !pauseMovement)
        {
            player.transform.position = new Vector3(HandleParams(worldPosition, borderArea).x, player.transform.position.y, 0);
        }

        if (player.transform.position.y <= borderArea.transform.GetChild(1).transform.position.y)
        {
            player.transform.position = new Vector3(player.transform.position.x,
                borderArea.transform.GetChild(1).transform.position.y, 0);
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
        originalParent = player;
        ParentUtils.MoveToBlankObject("TempParticleParent", dashParticleSystemObj);
        game.pm.RunDashParticles();
        StartCoroutine(moveBackToOriginalParent());
    }

    IEnumerator moveBackToOriginalParent()
    {
        yield return new WaitForSeconds(1.1f);
        dashParticleSystemObj.transform.parent = originalParent.transform;
        dashParticleSystemObj.transform.position = originalParent.transform.position;
    }
}
