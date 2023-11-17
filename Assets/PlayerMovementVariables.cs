using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovementVariables : MonoBehaviour
{
    public GameObject player;
    public Camera mainCamera;
    public GameObject borderArea;
    public Main game;
    public float timeLeft;
    public GameObject DashEffectTimer;
    public TMP_Text DashEffectTimerText;
    public InputHandler ih;
    public GameObject dashParticleSystemObj;
    public Vector2 lastMovePos;
    public bool challengeMode;

    public bool scriptDisabled;
}
