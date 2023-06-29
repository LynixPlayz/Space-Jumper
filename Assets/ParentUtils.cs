using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentUtils : MonoBehaviour
{
    public static void MoveToBlankObject(string name, GameObject obj)
    {
        GameObject findObj = GameObject.Find(name);
        if(findObj)
        {
            obj.transform.parent = findObj.transform.parent;
        }
        else
        {
            findObj = Instantiate(new GameObject());
            findObj.name = name;
            obj.transform.parent = findObj.transform.parent;
        }
    }
}
