using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public bool spacebarCheck;
    public bool eCheck;

    void Update(){
        spacebarCheck = CheckKey("space");
        eCheck = CheckKey("e");
    }
    public static bool CheckKey(string key)
    {
        if (Input.GetKey(key))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
