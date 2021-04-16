using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public GameObject trashCan;
    public GameObject animationGameObject;
    public GameObject dragOffScreenGameObject;
    public GameObject dragOffScreenBomb;
    public GameObject mainBomb;
    public GameObject dragController;

    public GameObject explosiveMenuController;
    public GameObject bottomBackgroundController;
    public GameObject rotateButtons;

    public GameObject explosiveMenu;

    public GameObject panel;

    public GameObject pinchIn;
    public GameObject pinchOut;

    public void TriggerTrashAnimationEnable()
    {
        trashCan.SetActive(true);
        trashCan.GetComponent<TrashScript>().toggle = true;
    }

    public void TriggerTrashAnimationDisable()
    {
        trashCan.GetComponent<TrashScript>().toggle = false;
    }
    public void DisableTrashCan()
    {
        trashCan.SetActive(false);
    }

    public void TriggerNextText()
    {
        animationGameObject.GetComponent<TutorialChangeText>().NextText();
    }

    public void DisableDragOffScreenBomb()
    {
        dragOffScreenGameObject.GetComponent<TutorialChangeText>().NextText();
        gameObject.SetActive(false);
    }

    public void EnableDragOffScreenBomb()
    {
        dragOffScreenBomb.SetActive(true);
    }

    public void DisableMainBomb()
    {
        mainBomb.SetActive(false);
    }

    public void DisableDragController()
    {
        dragController.SetActive(false);
    }

    public void DisableMenuButtons()
    {
        explosiveMenuController.SetActive(false);
        bottomBackgroundController.SetActive(false);
        rotateButtons.SetActive(false);
    }

    public void DisableExplosiveMenu()
    {
        explosiveMenu.SetActive(false);
    }

    public void DisablePanel()
    {
        panel.SetActive(false);
    }

    public void SwapPinch()
    {
        pinchIn.SetActive(false);
        pinchOut.SetActive(true);
    }
}
