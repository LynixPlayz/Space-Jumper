using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public Slider UIBar;
    public InputField input;
    public FireBossHandler bh;
    public StorageTrigger st;
    public Slider EnergyBar;
    public GameObject retryUI;
    public Main game;

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
        setValue(UIBar, bh.bossHealth / 100);
        setValue(EnergyBar, st.energy / 100);
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
}
