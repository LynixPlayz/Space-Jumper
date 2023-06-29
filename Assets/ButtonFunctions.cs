using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    public Vector3 normalSize;
    public Vector3 customSize;
    public void getBigger()
    {
        gameObject.transform.localScale += customSize - normalSize;
        gameObject.GetComponent<Animator>().SetBool("doAnimation", false);
    }

    public void getSmaller()
    {
        gameObject.transform.localScale -= customSize - normalSize;
        gameObject.GetComponent<Animator>().SetBool("doAnimation", true);
    }
}
