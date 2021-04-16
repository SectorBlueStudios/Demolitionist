using System.Collections;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.UI;

public class HoldButton : MonoBehaviour {
    
    public float timeEnd;
    public float time = 0.0f;
    bool active = false;
    public GameObject endMenu;
    public Slider slider;
    public Slider destructionSlider;
    public GameObject paused;
    public GameObject pausePanel;

    public GameObject ExplosiveMenuController;
    
    private void FixedUpdate()
    {
        if(active == true)
        {
            time += 0.1f;
            if (destructionSlider.gameObject.activeSelf == true && destructionSlider.value > 0) //If everything is destroyed, the HOLD TO END LEVEL button will not fill up.
            {
                slider.value = time / timeEnd;
            }
        }
        if(time >= timeEnd) //Still waits an extra 5 seconds. So, yield return new WaitForSeconds(1) + 5 = 6 seconds of wait time after level ends.
        {
            if (ExplosiveMenuController.GetComponent<Animator>().GetBool("toggle") == true) //If the ExplosiveMenuController is up, put it down!
                ExplosiveMenuController.GetComponent<Animator>().SetBool("toggle", false);

            endMenu.SetActive(true);
            paused.GetComponent<Pause>().isPaused = true;
            pausePanel.SetActive(true);
        }
    }

    private void Update()
    {
        if(destructionSlider.gameObject.activeSelf == true && destructionSlider.value <= 0) //destructionSlider.value starts at all of the object's durability combined. If this is 0, the level automatically ends.
        {
            StartCoroutine(EndLevel());
        }
    }

    public void StartHold()
    {
        active = true;
    }

    public void EndHold()
    {
        active = false;
        time = 0;
        slider.value = time / timeEnd;
    }

    IEnumerator EndLevel()
    {
        yield return new WaitForSeconds(1); //How long until the level ends?
        active = true; //THIS TRIGGERS THE LEVEL TO END!!!
    }
}