using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDemolish : MonoBehaviour
{
    public GameObject pausePanel;

    public GameObject demolishButton;

    private void OnEnable()
    {
        pausePanel.SetActive(true);

        demolishButton.SetActive(true);
    }

    public void DisableDemolishButton()
    {
        pausePanel.SetActive(false);

        demolishButton.GetComponent<Button>().transition = Selectable.Transition.ColorTint;
        demolishButton.GetComponent<Button>().interactable = true;
    }
}
