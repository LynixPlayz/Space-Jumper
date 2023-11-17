using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Toggle challengeModeToggle;
    public PlayerMovement csv;

    private void Update()
    {
        csv.challengeMode = challengeModeToggle.isOn;
    }
}
