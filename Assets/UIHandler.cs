using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public Slider UIBar;
    public InputField input;
    public BossHandler bh;
    public StorageTrigger st;
    public Slider EnergyBar;
    public GameObject retryUI;
    public TMP_Text coinCounter;
    public int coinCount;
    public Main game;
    public GameObject winScreen;
    public GameObject uiCanvas;
    public GameObject uiTutorial;
    public GameObject finishLine;
    public GameObject bundle;

    private void Start()
    {
        bundle = game.gameah.bundleAnimator.gameObject.transform.parent.gameObject;
    }

    public List<Transform> hideAllUI()
    {
        List<Transform> uiList = new List<Transform>();
        foreach (Transform child in uiCanvas.transform)
        {
            child.gameObject.SetActive(false);
            if(child.gameObject.activeSelf) uiList.Add(child);
        }

        return uiList;
    }
    
    public void hideAllUITimer(float milliseconds)
    {
        List<Transform> uiList = new List<Transform>();
        foreach (Transform child in uiCanvas.transform)
        {
            if(child.gameObject.activeSelf == true) uiList.Add(child);
            child.gameObject.SetActive(false);
        }

        StartCoroutine(setAllActive(uiList, milliseconds));
    }

    public IEnumerator setAllActive(List<Transform> list, float milliseconds)
    {
        yield return new WaitForSeconds(milliseconds / 1000);
        foreach (var item in list)
        {
            item.gameObject.SetActive(true);
        }
    }
    
    public static float getValue(Slider slider)
    {
        float value = slider.value;
        return value;
    }

    public void increaseValue(Slider slider, float valueToAdd)
    {
        slider.value += valueToAdd / 100;
    }

    public void setValue(Slider slider, float valueToSet)
    {
        slider.value = valueToSet;
    }

    public void BossBarOnClick()
    {
        setValue(UIBar, float.Parse(input.text) / 100);
    }
    
    public void Update()
    {
        if(bh){setValue(UIBar, bh.bossHealth / 100);}
        setValue(EnergyBar, st.energy / 100);
        coinCounter.text = coinCount.ToString();
    }
    
    public void RestartGameFromButtonUIElement()
    {
        game.restart();
    }

    public void removeUI(GameObject ui) {
        ui.SetActive(false);
    }

    public void removeRetryUI()  {
        removeUI(retryUI);
    }

    public void addRetryUI()
    {
        retryUI.SetActive(true);
    }

    public void addWinUI()
    {
        winScreen.SetActive(true);
        game.gameah.winUIAdd();
    }

    public void removeWinUI()
    {
        removeUI(winScreen);
    }
}
