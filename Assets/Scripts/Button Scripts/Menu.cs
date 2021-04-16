using UnityEngine;

public class Menu : MonoBehaviour {

    public GameObject menu;
    public GameObject settings;
    public GameObject pausePanel;

    public GameObject paused;

    public void ActivateMenu()
    {
        if (menu.activeSelf == false)
        {
            menu.SetActive(true);
            paused.GetComponent<Pause>().isPaused = true;
            pausePanel.SetActive(true);
        }
        else
        {
            menu.SetActive(false);
            paused.GetComponent<Pause>().isPaused = false;
            pausePanel.SetActive(false);
        }
    }

    public void Settings()
    {
        if(settings.activeSelf == false)
        {
            menu.SetActive(false);
            settings.SetActive(true);
        }
        else
        {
            menu.SetActive(true);
            settings.SetActive(false);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
