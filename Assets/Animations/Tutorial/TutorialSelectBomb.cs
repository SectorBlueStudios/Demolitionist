using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialSelectBomb : MonoBehaviour
{
    public GameObject bombButton;
    public GameObject bomb;
    public GameObject dragController;
    private void OnEnable()
    {
        bombButton.GetComponent<EventTrigger>().enabled = true;
        dragController.GetComponent<Drag1>().GenerateExplosives();
    }
    public void TriggerNextText()
    {
        GetComponent<TutorialChangeText>().NextText();
    }

    public void SetBomb()
    {
        bomb.SetActive(true);
    }
}
