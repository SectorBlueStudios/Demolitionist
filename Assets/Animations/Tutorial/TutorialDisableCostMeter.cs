using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDisableCostMeter : MonoBehaviour
{
    public GameObject costMeter;
    public GameObject originalDamageMeter;
    public void DisableCostMeter()
    {
        costMeter.SetActive(false);
        originalDamageMeter.SetActive(true);
    }
}
