using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAddingScript : MonoBehaviour
{
    public GameObject levelProperties;
    void Start()
    {
        levelProperties.GetComponent<AddALL>().enabled = true;
    }
}
