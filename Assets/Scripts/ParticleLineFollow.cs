using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLineFollow : MonoBehaviour
{
    public LineRenderer lineRend;
    void Update()
    {        
        Vector3 endPos = lineRend.GetPosition(lineRend.positionCount - 1);
        transform.position = new Vector3(endPos.x, endPos.y, transform.position.z);
    }
}
