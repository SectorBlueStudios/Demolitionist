using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public GameObject fadeControl;
    public GameObject LevelInfo;
    public GameObject player;

    public void ReloadScene()
    {
        fadeControl.GetComponent<FadeOut>().ActivateFade(SceneManager.GetActiveScene().name);
    }

    public void ToRegionSelection()
    {
        fadeControl.GetComponent<FadeOut>().ActivateFade("Region Selection");
    }

    public void ToStart()
    {
        fadeControl.GetComponent<FadeOut>().ActivateFade("Start");
    }

    public void ToLevel()
    {
        fadeControl.GetComponent<FadeOut>().ActivateFade("Level Selection");
    }

    public void NextLevel()
    {
        string name = SceneManager.GetActiveScene().name;
        int temp = name[3] - 47;
        Debug.Log(temp);

        player.GetComponent<Player>().LoadPlayer();

        if (Application.CanStreamedLevelBeLoaded("R1L" + (name[3] - 47)))
        {
            if(player.GetComponent<Player>().unlocked[0, temp - 1] != false)
                fadeControl.GetComponent<FadeOut>().ActivateFade("R1L" + (name[3] - 47)); //The (-47) is the ASCII translation.
            else
            {
                Debug.Log("LEVEL IS LOCKED");
            }
        }
        else
        {
            Debug.Log("LAST SCENE OPEN");
        }
    }

    public void ToRegion()
    {
        if(LevelInfo == null)
            fadeControl.GetComponent<FadeOut>().ActivateFadeToRegion(gameObject.name);
        else if(LevelInfo.GetComponent<LevelProperties>().region == 1)
        {
            fadeControl.GetComponent<FadeOut>().ActivateFadeToRegion("Fallen Forest");
        }
        else if (LevelInfo.GetComponent<LevelProperties>().region == 2)
        {
            fadeControl.GetComponent<FadeOut>().ActivateFadeToRegion("Shattered Sea");
        }
        else if (LevelInfo.GetComponent<LevelProperties>().region == 3)
        {
            fadeControl.GetComponent<FadeOut>().ActivateFadeToRegion("Scorched City");
        }
        else if (LevelInfo.GetComponent<LevelProperties>().region == 4)
        {
            fadeControl.GetComponent<FadeOut>().ActivateFadeToRegion("Crumbled Cliffs");
        }
        else if (LevelInfo.GetComponent<LevelProperties>().region == 5)
        {
            fadeControl.GetComponent<FadeOut>().ActivateFadeToRegion("Desolate Desert");
        }
        else if (LevelInfo.GetComponent<LevelProperties>().region == 6)
        {
            fadeControl.GetComponent<FadeOut>().ActivateFadeToRegion("Trampled Tundra");
        }
        else
        {
            Debug.Log("ERROR - The region is not valid.");
        }
    }

    public void EnableLoading()
    {




    }
}
