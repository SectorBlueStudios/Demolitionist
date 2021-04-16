using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadUnlockExplosive : MonoBehaviour
{
    public GameObject pausePanel;
    public Camera mainCam;
    public GameObject mainSceneCanvas;
    public GameObject unlockExplosiveScene;

    public GameObject[] starAppearController = new GameObject[6]; //Controller that makes the stars appear
    public GameObject starStayController; //Controller that makes the stars still

    public void LoadUnlockExplosiveScene()
    {
        //pausePanel.SetActive(true);
        //mainCam.enabled = false;

        //Time.timeScale = 0;
        StartCoroutine(TriggerUnlockExplosiveSwitch());
    }

    public void UnloadUnlockExplosiveScene()
    {
        mainSceneCanvas.SetActive(true);
        unlockExplosiveScene.SetActive(false);
    }

    IEnumerator TriggerUnlockExplosiveSwitch()
    {
        yield return new WaitForSeconds(1);
        mainSceneCanvas.SetActive(false);
        unlockExplosiveScene.SetActive(true);
        
        for(int i = 0; i < 6; i++) //changes the AnimationController to a new one where the stars are still from the start.
        {
            starAppearController[i].GetComponent<Animator>().runtimeAnimatorController = starStayController.GetComponent<Animator>().runtimeAnimatorController;
        }
    }
}

