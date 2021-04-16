using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnableDamageMeter : MonoBehaviour
{
    public GameObject damageMeter;
    public GameObject originalCostMeter;

    public GameObject pausePanel;

    private void OnEnable()
    {
        originalCostMeter.SetActive(true);
        damageMeter.SetActive(true);
    }

    public void DisableDamageBar()
    {
        damageMeter.SetActive(false);
        pausePanel.SetActive(false);
    }
}
