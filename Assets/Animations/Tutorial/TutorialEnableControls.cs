using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnableControls : MonoBehaviour
{
    public GameObject freezePanel;
    public GameObject demolishButton;

    public GameObject explosiveMenuController;
    private void OnEnable()
    {
        freezePanel.SetActive(false);
    }

    public void DemolishTrigger()
    {
        explosiveMenuController.GetComponent<ExplosiveMenu>().ToggleMenuDown();
        freezePanel.SetActive(true);
        demolishButton.SetActive(false);

        StartCoroutine(WaitAfterExplosion());
    }

    IEnumerator WaitAfterExplosion()
    {
        yield return new WaitForSeconds(3);
        
        GetComponent<TutorialChangeText>().NextText();
    }
}
