using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnableCam : MonoBehaviour
{
    public Animator camAnimator;

    public GameObject cameraZoom;

    public bool camZoom = false;
    private void OnEnable()
    {
        if(camZoom == true)
        {
            cameraZoom.GetComponent<LeanCameraZoom>().enabled = true;
        }
        
        camAnimator.enabled = true;
    }

    public void Update()
    {
        if(camAnimator.GetCurrentAnimatorStateInfo(0).IsName("CamMoveStill"))
        {
            if (camZoom == true)
            {
                cameraZoom.GetComponent<LeanCameraZoom>().enabled = false;
            }
            GetComponent<TutorialChangeText>().NextText();
        }
    }
}
