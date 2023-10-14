
using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public int currentPanel = 0;
    public List<TutorialPanelSO> panels;
    public GameObject panelPrefab;
    
    /*private Dictionary<EventTypes, UnityEvent> types = new()
    {
        { EventTypes.BossDeathEvent, BossDeathEvent.BOSS_DEATH }
    };*/
    
    private Dictionary<EventTypes, UnityEvent> types = new();

    public void initPanelEventListeners(Main game)
    {
        types.Add(EventTypes.BossDeathEvent, BossDeathEvent.BOSS_DEATH);
        foreach (TutorialPanelSO panel in panels) 
        {
            UnityEvent e;
            Debug.Log(types.TryGetValue(panel.type, out e));
            Debug.Log("DEBUG:::" + e);
            e?.AddListener(() => game.tutorialManager.currentPanel = panel.panelNumber);
        }
        createPanels(game);
    }

    public void createPanels(Main game)
    {
        foreach (TutorialPanelSO panel in panels)
        {
            if (panel.customPanel == null)
            {
                GameObject createdPanel = Instantiate(panelPrefab, game.uiobject.uiTutorial.transform);
                createdPanel.transform.GetChild(0).transform.GetComponent<TMP_Text>().text = panel.dialogue;
                createdPanel.SetActive(false);
                createdPanel.name = panel.panelNumber.ToString();
            }
            else
            {
                Instantiate(panel.customPanel, game.uiobject.uiCanvas.transform).SetActive(false);
            }
        }
    }
    
    public void syncPanels(Main game)
    {
        foreach (Transform child in game.uiobject.uiTutorial.transform)
        {
            if(child.gameObject.name != currentPanel.ToString()) child.gameObject.SetActive(false);
        }

        Transform foundObject = game.uiobject.uiTutorial.transform.Find(currentPanel.ToString());
        Debug.Log("Searching for" + currentPanel + " Found: " + foundObject);
        if(foundObject) foundObject.gameObject.SetActive(true);
    }
}
