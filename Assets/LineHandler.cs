using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineHandler : MonoBehaviour
{
    public List<LineSO> lhList;

    void Start()
    {
        LineRenderer line = gameObject.GetComponent<LineRenderer>();
        line.material = GetComponent<Variables>().fireLineTexture;
        line.widthCurve = GetComponent<Variables>().fireLineCurve;
        line.sortingOrder = -10;
        line.SetPosition(0, gameObject.transform.position);
    }

    void Update()
    {
        LineRenderer line = gameObject.GetComponent<LineRenderer>();

        line.SetPosition(1, gameObject.transform.position);
        BoxCollider2D box = gameObject.GetComponents<BoxCollider2D>()[1];
        box.size = new Vector2(1.5f, Mathf.Abs(Mathf.Abs(line.GetPosition(1).x - line.GetPosition(0).x)  * (1.0f / gameObject.transform.localScale.x)));
        box.offset = new Vector2(2.5f, (line.GetPosition(1).x - line.GetPosition(0).x)  * (1.0f / gameObject.transform.localScale.x) / 2);
    }
}
