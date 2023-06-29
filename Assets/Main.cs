using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public ParticleManager pm;
    public UIHandler uiobject;
    public FireBossHandler bh;
    public PlayerAttack pa;
    public List<GameObject> fireList;
    public List<GameObject> dropletList;
    public bool playerEnabled;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<ParticleManager>();
        pm.RunAllParticles();
        playerEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void restart()
    {
        uiobject.removeRetryUI();
        bh.Start();
        pa.Start();
        pa.st.energy = 0;
        destroyAllMiniFire();
        destroyAllMiniDroplet();
        playerEnabled = true;
        player.GetComponent<SpriteRenderer>().color = Color.white;
        player.SetActive(true);
    }

    public void destroyAllMiniFire()
    {
        foreach(GameObject miniFire in fireList)
        {
            Destroy(miniFire);
        }
    }

    public void destroyAllMiniDroplet()
    {
        foreach(GameObject miniDroplet in dropletList)
        {
            Destroy(miniDroplet);
        }
    }
}
