using System.Collections;
using System.Collections.Generic;
using Prefabs.Events;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Data", menuName = "Tutorial/TutorialPanel", order = 1)]
public class TutorialPanelSO : ScriptableObject
{
    public EventTypes type;
    public string dialogue;
    public int panelNumber;
    public GameObject customPanel;
}