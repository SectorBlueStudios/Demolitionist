using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePulse : MonoBehaviour
{
    public void ActivatePulseTrigger()
    {
        GetComponent<Animator>().SetBool("StartPulse", true);
    }
}
