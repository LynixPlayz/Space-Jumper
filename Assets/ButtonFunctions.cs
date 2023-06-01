using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    public Vector3 normalSize;
    public Vector3 customSize;
    public void getBigger()
    {
        gameObject.transform.localScale = customSize;
    }

    public void getSmaller()
    {
        gameObject.transform.localScale = normalSize;
    }
}
