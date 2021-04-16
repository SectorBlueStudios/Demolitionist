using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDisplayMeters : MonoBehaviour
{
    public GameObject costMeter;
    public GameObject damageMeter;
    public GameObject originalCostMeter;
    public GameObject originalDamageMeter;

    public GameObject pausePanel;

    private void OnEnable()
    {
        pausePanel.SetActive(true);

        originalCostMeter.SetActive(false);
        originalDamageMeter.SetActive(false);

        costMeter.SetActive(true);
        damageMeter.SetActive(true);

    }

    public void DisableMeters()
    {
        damageMeter.SetActive(false);
        originalDamageMeter.SetActive(true);
    }
}
