using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialClearExplosives : MonoBehaviour
{
    public GameObject pausePanel;

    public GameObject clearExplosivesButton;

    public GameObject costMeter;
    public GameObject originalCostMeter;
    public GameObject damageMeter;
    public GameObject originalDamageMeter;

    public GameObject costController;

    private void OnEnable()
    {
        pausePanel.SetActive(true);
        clearExplosivesButton.SetActive(true);

        costMeter.SetActive(false);
        originalCostMeter.SetActive(true);
        damageMeter.SetActive(false);
        originalDamageMeter.SetActive(true);

        costController.GetComponent<Drag>().cost = 10;
    }
}
