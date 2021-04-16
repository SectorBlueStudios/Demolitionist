using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRotateArrows : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject gameCam;

    public bool pressButtonRight;
    public bool pressButtonLeft;

    public void OnEnable()
    {
        if(pausePanel != null)
            pausePanel.SetActive(true);
    }

    public void FixedUpdate()
    {
        if(pressButtonRight == true)
        {
            pausePanel.SetActive(false);
            gameCam.GetComponent<RotateCamera>().RotateRight();
        }

        if (pressButtonLeft == true)
        {
            gameCam.GetComponent<RotateCamera>().RotateLeft();
        }
    }

    public void Update()
    {
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("TutorialCameraRotateStill"))
        {
            GetComponent<TutorialChangeText>().NextText();
        }
    }
}
