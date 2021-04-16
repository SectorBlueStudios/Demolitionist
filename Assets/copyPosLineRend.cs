using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class copyPosLineRend : MonoBehaviour
{
    public LineRenderer line1;
    public LineRenderer linenew;
    // Start is called before the first frame update
    void Start()
    {
        linenew = line1;
        //GetComponent<LineRenderer>() = line1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
