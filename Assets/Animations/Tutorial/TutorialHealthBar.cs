using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialHealthBar : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject healthBar;

    private bool triggerHealthBar = false;
    private void OnEnable()
    {
        pausePanel.SetActive(true);
        healthBar.SetActive(true);
        healthBar.GetComponent<Button>().enabled = true;
    }

    public void PlayerTouches()
    {
        pausePanel.SetActive(false);
        GetComponent<Image>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        triggerHealthBar = true;
        StartCoroutine(TriggerNextText());
    }

    public void Update()
    {
        if(triggerHealthBar == true)
        {
            healthBar.GetComponent<ShowHealth>().ShowHealthBars();
        }
    }

    IEnumerator TriggerNextText()
    {
        yield return new WaitForSeconds(2);
        triggerHealthBar = false;
        healthBar.GetComponent<ShowHealth>().HideHealthBars();
        healthBar.SetActive(false);
        GetComponent<TutorialChangeText>().NextText();
    }
}
