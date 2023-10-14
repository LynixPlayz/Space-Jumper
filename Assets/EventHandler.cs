using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public void playBossDeathEvent()
    {
        Debug.Log(BossDeathEvent.BOSS_DEATH);
        BossDeathEvent.BOSS_DEATH.Invoke();
    }
}
