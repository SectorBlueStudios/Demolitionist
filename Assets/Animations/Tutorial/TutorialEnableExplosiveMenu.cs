using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class TutorialEnableExplosiveMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject explosiveMenu;

    public GameObject explosiveMenuController;
    public GameObject bottomBackgroundController;
    public GameObject rotateButtons;

    public GameObject showHealth;
    public GameObject menu;
    public GameObject endLevel;

    public GameObject damageMeter;
    public GameObject costMeter;

    public GameObject disableOriginalCost;
    public GameObject disableOriginalDamage;

    private void OnEnable()
    {
        pausePanel.SetActive(true);

        explosiveMenu.SetActive(true);
    }
    
    public void EnableNext()
    {
        StartCoroutine(WaitAndThenEnable());

        explosiveMenu.GetComponent<Button>().enabled = false;
        explosiveMenuController.SetActive(true);
        bottomBackgroundController.SetActive(true);
        rotateButtons.SetActive(true);
        //showHealth.SetActive(true);
        // menu.SetActive(true);
        //endLevel.SetActive(true);
        damageMeter.SetActive(true);
        costMeter.SetActive(true);

        pausePanel.SetActive(false);

        disableOriginalCost.SetActive(false);
        disableOriginalDamage.SetActive(false);
    }

    IEnumerator WaitAndThenEnable()
    {
        yield return new WaitForSeconds(2);

        //explosiveMenu.SetActive(true);
        GetComponent<TutorialChangeText>().NextText();
    }
}
