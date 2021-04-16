using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialClearExplosives2 : MonoBehaviour
{
    public GameObject pausePanel;

    public GameObject clearExplosivesButton;

    public GameObject bomb;

    public GameObject dragController;

    public GameObject bomb2;

    public GameObject costMeter;
    public GameObject originalCostMeter;
    public GameObject damageMeter;
    public GameObject originalDamageMeter;

    private void OnEnable()
    {
        costMeter.SetActive(true);
        originalCostMeter.SetActive(false);
        damageMeter.SetActive(true);
        originalDamageMeter.SetActive(false);

        pausePanel.SetActive(false);
        bomb.SetActive(true);

        //dragController.GetComponent<Drag>().cost += bomb.GetComponent<Explosion>().cost;

        clearExplosivesButton.GetComponent<Button>().interactable = true;
        clearExplosivesButton.GetComponent<Button>().transition = Selectable.Transition.ColorTint;
    }

    public void ClearExplosiveBuffer()
    {
        StartCoroutine(ClearExplosiveWait());

        clearExplosivesButton.SetActive(false);
    }

    IEnumerator ClearExplosiveWait()
    {
        yield return new WaitForSeconds(1);

        GetComponent<TutorialChangeText>().NextText();
        bomb2.SetActive(true);
    }
}
