using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineHandlerFunc : MonoBehaviour
{
    public bool listClearQueue = false;
    public List<LineSO> lhList;
    public Main game;

    //FixedUpdate to keep the line tied to the positions
    void FixedUpdate() {
        lhList.ForEach(delegate (LineSO so)
        {
            LineRenderer lr = so.lineRenderer;
            lr.SetPosition(0, so.positionObjectOne.transform.position);
            lr.SetPosition(1, so.positionObjectTwo.transform.position); 
        });
        if(listClearQueue){lhList.Clear();listClearQueue=false;}
    }
    //Renders a lne that will always be drawn
    public void RenderPersistentLine(GameObject one, GameObject two) {
        LineRenderer lr = null;
        if(one.GetComponent<LineRenderer>())
        {
            lr = one.GetComponent<LineRenderer>();
        }
        else {
            lr = one.AddComponent<LineRenderer>();
        }
        lr.SetPosition(0, one.transform.position);
        lr.SetPosition(1, two.transform.position);
        LineSO lso = new LineSO();
        lso.lineRenderer = lr;
        lso.positionObjectOne = one;
        lso.positionObjectTwo = two;
        lhList.Add(lso);
    }

    /*public static ParticleSystem PersistentLineParticles(ParticleSystem particleSystem, LineRenderer lr)
    {
        Transform lineTransform = new Transform();
        lineTransform.position = lr.GetPosition(0);
        lineTransform.scale = lr.GetPosition(lr.positionCount - 1) - lr.GetPosition(0);
        ParticleSystem ps = game.player.st.GetParticleSystemTransform(particleSystem, lineTransform);
        return ps;
    }*/

    
    public void RemovePersistentLine()
    {
        listClearQueue = true;
    }
}
