using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Line/PersistentLine", order = 1)]
public class LineSO : ScriptableObject {
    public LineRenderer lineRenderer;
    public GameObject positionObjectOne;
    public GameObject positionObjectTwo;
    
}
