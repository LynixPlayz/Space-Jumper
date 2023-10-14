
using System;
using Prefabs.Events;
using UnityEngine;
using UnityEngine.Events;

public class BossDeathEvent : GameEvent
{
    public static UnityEvent BOSS_DEATH;

    private void Awake()
    {
        if (BOSS_DEATH == null)
            BOSS_DEATH = new UnityEvent();
    }
}
