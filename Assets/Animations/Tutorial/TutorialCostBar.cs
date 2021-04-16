using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCostBar : MonoBehaviour
{
    public GameObject damageMeter;
    public GameObject costMeter;
    public GameObject originalDamageMeter;
    public GameObject pausePanel;

    public GameObject costController;
    public void OnEnable()
    {
        pausePanel.SetActive(true);
        //damageMeter.SetActive(false);
        //originalDamageMeter.SetActive(true);
        costController.GetComponent<Drag>().cost = 10;
        costMeter.SetActive(true);
    }

    public void DisableMeter()
    {
        pausePanel.SetActive(false);
        //costMeter.SetActive(false);
    }
}
