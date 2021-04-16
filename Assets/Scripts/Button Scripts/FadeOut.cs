using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeOut : MonoBehaviour {

    public GameObject loading;
    public bool active;
    public GameObject fadeObject;
    Color fade;
    float journey = 0f;
    public bool initiate = false;
    public float speed;

    private string scene;
    private string scene2;


    public void Start()
    {
        fadeObject.SetActive(true);
        fadeObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
    }

    void Update ()
    {
        if (active)
        {
            if (initiate == true)
            {
                if (journey <= 1)
                {
                    journey = journey + (speed) * Time.deltaTime;
                    fadeObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, journey);
                }
                else if(scene2 == null)
                {
                    SceneManager.LoadScene(scene); //Loads the new scene.

                }
                else
                {
                    initiate = false;
                    loading.SetActive(true);
                    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene2);
                }
            }
        }
    }

    public void ActivateFade(string tempScene) //Takes the scene sent from another script and activates it AFTER the Fade transition.
    {
        scene = tempScene;
        initiate = true;
    }

    public void ActivateFadeToRegion(string tempScene2)
    {
        scene2 = tempScene2;
        initiate = true;
    }
}