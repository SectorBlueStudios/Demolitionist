using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {

    public bool active;
    public GameObject fadeObject;
    Color fade;
    float journey = 1;
    public bool initiate = false;
    public float speed;
    private bool unlockLevelTrigger = false;
    public GameObject levelControl;

    public void Start()
    {
        fadeObject.SetActive(true);
        fadeObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
    }

    void Update ()
    {
        if (active)
        {
            if (journey >= 0)
            {
                journey = journey - (speed) * Time.deltaTime;
                fadeObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, journey);
            }
            else if(unlockLevelTrigger == false)
            {
                if(levelControl != null)
                    levelControl.GetComponent<LevelController>().CheckForUnlock();
                unlockLevelTrigger = true;
            }
        }
    }
}
