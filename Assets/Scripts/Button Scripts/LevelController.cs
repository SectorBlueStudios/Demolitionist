using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Lean;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    public GameObject FadeController;

    public GameObject levelImage;

    public GameObject levelStartContainer;
    public GameObject loading;
    public GameObject play;
    public GameObject player;

    public GameObject levelNumber;

    public int region;
    public int level;

    public GameObject destructionBronze;
    public GameObject destructionSilver;
    public GameObject destructionGold;

    public GameObject costBronze;
    public GameObject costSilver;
    public GameObject costGold;

    public TextMeshProUGUI costStars;
    public TextMeshProUGUI destructionStars;

    public GameObject maincam;

    private Vector3 startPos;
    private Vector3 endPos;

    private bool triggerCameraMove = false;

    private float journey = 0f;

    public GameObject levelLine; 
    private int levelLineUnlockTemp = -1;
    private int levelLineUnlockAnimTemp = -1;
    private Vector3 startLinePos;
    private Vector3 endLinePos;
    private bool nextTrigger = true;
    private float t = 0f;
    private bool lineAnim = true;

    public Sprite[] levelImages = new Sprite[20];

    private bool playAfterLoading = false;

    public void Start()
    {

        player.GetComponent<Player>().LoadPlayer();

        /*Debug.Log("1");
        Debug.Log(player.GetComponent<Player>().unlocked[0, 0]);
        Debug.Log(player.GetComponent<Player>().unlockedAnim[0, 0]);
        Debug.Log("2");
        Debug.Log(player.GetComponent<Player>().unlocked[0, 1]);
        Debug.Log(player.GetComponent<Player>().unlockedAnim[0, 1]);
        Debug.Log("3");
        Debug.Log(player.GetComponent<Player>().unlocked[0, 2]);
        Debug.Log(player.GetComponent<Player>().unlockedAnim[0, 2]);
        Debug.Log("4");
        Debug.Log(player.GetComponent<Player>().unlocked[0, 3]);
        Debug.Log(player.GetComponent<Player>().unlockedAnim[0, 3]);*/


        //GENERATES LEVELS IF NOT PREVIOUSLY GENERATED
        for (int i = 0; i < 4; i++)
        {
            if (player.GetComponent<Player>().generatedLevels[i] == false)
            {
                for (int j = 0; j < player.GetComponent<Player>().unlocked.GetLength(1); j++)
                {
                    GameObject temp = GameObject.Find("Level " + (j + 1));
                    player.GetComponent<Player>().levelX[i, j] = temp.transform.position.x;
                    player.GetComponent<Player>().levelY[i, j] = temp.transform.position.y;
                }
                player.GetComponent<Player>().generatedLevels[i] = true;
                player.GetComponent<Player>().SavePlayer();
                break;
            }
        }

        //CREATES THE INITIAL LEVEL LINES
        for (int i = 0; i < player.GetComponent<Player>().unlocked.GetLength(1); i++)
        {

            if (player.GetComponent<Player>().unlocked[0, i] == true && player.GetComponent<Player>().unlockedAnim[0, i] == true)
                //If the level is unlocked and the animation has been run, remove the lock image.
            {
                //Debug.Log("YETTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT");
                GameObject.Find("Level " + (i + 1)).transform.GetChild(2).gameObject.GetComponent<Image>().enabled = false;
                if (levelLine.GetComponent<LineRenderer>().positionCount < i + 1) //If the position number is less than level number, create one
                    levelLine.GetComponent<LineRenderer>().positionCount++;
                levelLine.GetComponent<LineRenderer>().SetPosition(i, new Vector3(player.GetComponent<Player>().levelX[0, i], 
                                                                                  player.GetComponent<Player>().levelY[0, i],
                                                                                  -2));
            }
            else
            {
                //Debug.Log("HereHereHereHereHereHereHereHereHereHereHereHereHereHereHereHereHereHereHere!");
            }
        }

        //ACTIVATES SMOKE        
        for (int i = 0; i < player.GetComponent<Player>().unlocked.GetLength(1); i++)
        {

            if (player.GetComponent<Player>().unlocked[0, i] == true)
            {
                GameObject.Find("Level " + (i + 1)).gameObject.transform.GetChild(3).gameObject.SetActive(true);
            }
            else
            {
                GameObject.Find("Level " + (i)).gameObject.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
        Debug.Log(player.GetComponent<Player>().unlocked.GetLength(1));
        Debug.Log(player.GetComponent<Player>().unlocked.GetLength(1) - 1);
        

        //SETS THE MAIN CAMERA TO THE LAST UNLOCKED LEVEL
        maincam.transform.position = new Vector3(player.GetComponent<Player>().levelX[player.GetComponent<Player>().lastLevel[0], player.GetComponent<Player>().lastLevel[1]],
                                                 player.GetComponent<Player>().levelY[player.GetComponent<Player>().lastLevel[0], player.GetComponent<Player>().lastLevel[1]], 
                                                 maincam.transform.position.z);
    }

    //ACTIVATES THE LEVEL MENU
    public void StartControl(GameObject levelProp) //The level index is sent from the level.
    {
        DeactivateLevel();

        play.SetActive(true);

        region = levelProp.GetComponent<LevelProperties>().region;
        level = levelProp.GetComponent<LevelProperties>().level;
        levelImage.GetComponent<Image>().sprite = levelImage.GetComponent<LoadImages>().sprites[level - 1];
                     
        #region star activations
        /*
        if (player.GetComponent<Player>().destructionStars[region - 1, level - 1] >= 1)
            destructionBronze.SetActive(true);
        if (player.GetComponent<Player>().destructionStars[region - 1, level - 1] >= 2)
            destructionSilver.SetActive(true);
        if (player.GetComponent<Player>().destructionStars[region - 1, level - 1] == 3)
            destructionGold.SetActive(true);
        if (player.GetComponent<Player>().costStars[region - 1, level - 1] >= 1)
            costBronze.SetActive(true);
        if (player.GetComponent<Player>().costStars[region - 1, level - 1] >= 2)
            costSilver.SetActive(true);
        if (player.GetComponent<Player>().costStars[region - 1, level - 1] == 3)
            costGold.SetActive(true);
        */
        #endregion

        costStars.text = "" + player.GetComponent<Player>().costStars[region - 1, level - 1] + "/3";
        destructionStars.text = "" + player.GetComponent<Player>().destructionStars[region - 1, level - 1] + "/3";
        
        levelStartContainer.SetActive(true);
        levelNumber.GetComponent<LevelText>().SetLevelNumber(level);

        play.GetComponent<Button>().onClick.AddListener(PlayLevel); //Listens to when the "PLAY" button is pressed. The scene may then load and play.
    }

    public void PlayLevel()
    {
        //Set the last level so the camera can return to it.
        player.GetComponent<Player>().lastLevel[0] = region - 1;
        player.GetComponent<Player>().lastLevel[1] = level - 1;
        player.GetComponent<Player>().SavePlayer();

        FadeController.GetComponent<FadeOut>().ActivateFade("R" + region + "L" + level); //Takes the selected region and level and activates the fade to start the level.
        if (SceneManager.GetActiveScene().name != "Region Selection")
            GameObject.FindGameObjectWithTag("InitialMusic").SetActive(false);
        loading.SetActive(true);       
    }

    public void DeactivateLevel()
    {
        levelStartContainer.SetActive(false);
        loading.SetActive(false);
        play.SetActive(false);

        #region star deactivations
        destructionBronze.SetActive(false);
        destructionSilver.SetActive(false);
        destructionGold.SetActive(false);

        costBronze.SetActive(false);
        costSilver.SetActive(false);
        costGold.SetActive(false);
        #endregion
    }

    public void CheckForUnlock()
    {
        //player.GetComponent<Player>().Unlock(1, 1); //Unlock certain levels. Input ACTUAL Region/Level
        //player.GetComponent<Player>().Unlock(1, 4);
        for (int i = 0; i < player.GetComponent<Player>().unlocked.GetLength(1); i++)
        {
            if (player.GetComponent<Player>().unlocked[0, i] == true) //Is the level Unlocked?
            {
                if(player.GetComponent<Player>().unlockedAnim[0, i] == false) //Has the Unlock Animation been run before?
                {
                    MoveCamera(i);
                    StartCoroutine(UnlockAnimation(i));
                    player.GetComponent<Player>().UnlockAnim(1, i + 1); //Record that the Unlock Animation has been run
                }                
            }
            else
            {
                GameObject.Find("Level " + (i + 1)).GetComponent<Button>().enabled = false; //If level is locked, remove the ability to click its button.
                //Debug.Log("Level " + (i + 1) + " LOCKED"); //Which level is locked?
            }
        }
        player.GetComponent<Player>().SavePlayer();
    }

    public void MoveCamera(int i)
    {
        if (triggerCameraMove == false)
        {
            startPos = new Vector3(player.GetComponent<Player>().levelX[player.GetComponent<Player>().lastLevel[0], player.GetComponent<Player>().lastLevel[1]], 
                                   player.GetComponent<Player>().levelY[player.GetComponent<Player>().lastLevel[0], player.GetComponent<Player>().lastLevel[1]], 
                                   maincam.transform.position.z);
            Debug.Log(startPos);

            for (int j = 0; j < player.GetComponent<Player>().unlocked.GetLength(1); j++)
            {
                if (player.GetComponent<Player>().unlocked[0, j] == false) //Find the last unlocked level
                {
                    endPos = new Vector3(player.GetComponent<Player>().levelX[0, j - 1], player.GetComponent<Player>().levelY[0, j - 1], maincam.transform.position.z);
                    Debug.Log(endPos);
                    break;
                }
            }
            triggerCameraMove = true;
        }
    }

    public void Update()
    {
        //MOVES THE CAMERA TO LATEST UNLOCKED LEVEL
        if (triggerCameraMove == true)
        {
            //LERP BETWEEN POSITIONS
            maincam.transform.position = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0.0f, 1.0f, journey));
            maincam.GetComponent<Lean.Touch.LeanCameraZoom>().Zoom = Mathf.SmoothStep(maincam.GetComponent<Lean.Touch.LeanCameraZoom>().ZoomMax, 
                                                                                      maincam.GetComponent<Lean.Touch.LeanCameraZoom>().ZoomMin, journey);

            journey += 1f * Time.deltaTime;
            //Debug.Log(journey);
            if(journey >= 1f)
            {
                triggerCameraMove = false;
            }            
        }

        //Sets beginning line and end line -- Runs ONCE
        if (lineAnim == true)
        {
            for (int i = 0; i < player.GetComponent<Player>().unlocked.GetLength(1); i++) 
            {
                if (player.GetComponent<Player>().unlocked[0, i] == true && player.GetComponent<Player>().unlockedAnim[0, i] == false && levelLineUnlockAnimTemp == -1)//unlocked and the animation has not been run yet
                {
                    levelLineUnlockAnimTemp = i - 1; // START (level) OF LINE
                }
                if (player.GetComponent<Player>().unlocked[0, i] == false) //Search levels until the first locked one is found. Now, go back one.
                {
                    if (player.GetComponent<Player>().unlockedAnim[0, i - 1] == true)
                    {
                        //Debug.Log("XXXXXXXXXXXXXXXXXXXXXX");
                        break;
                    }
                    else
                    {
                        levelLineUnlockTemp = i - 1; // END (level) OF LINE
                        lineAnim = false; //Run this lineAnim if statement ONCE.^^^
                        nextTrigger = true; //Run the next if statement.
                        break;
                    }
                }
            }
        }

        if(levelLineUnlockAnimTemp < levelLineUnlockTemp && nextTrigger == true) //if START level is less than END level
        {
            //levelLine.GetComponent<LineRenderer>().positionCount++;
            startLinePos = new Vector3(player.GetComponent<Player>().levelX[0, levelLineUnlockAnimTemp], player.GetComponent<Player>().levelY[0, levelLineUnlockAnimTemp], -2);
            endLinePos = new Vector3(player.GetComponent<Player>().levelX[0, levelLineUnlockAnimTemp + 1], player.GetComponent<Player>().levelY[0, levelLineUnlockAnimTemp + 1], -2);
            levelLineUnlockAnimTemp++;
            nextTrigger = false;
            t = 0f;
        }
        if(nextTrigger == false && playAfterLoading == true)
        {
            if (levelLine.GetComponent<LineRenderer>().positionCount < levelLineUnlockAnimTemp + 1) //If the position number is less than level number, create one
            {
                    levelLine.GetComponent<LineRenderer>().positionCount++;
            }
            levelLine.GetComponent<LineRenderer>().SetPosition(levelLineUnlockAnimTemp - 1, startLinePos);
            levelLine.GetComponent<LineRenderer>().SetPosition(levelLineUnlockAnimTemp, Vector3.Lerp(startLinePos, endLinePos, t));
            Mathf.Clamp(t += 1f * Time.deltaTime, 0 , 1);
        }
        if (t >= 1f)
        {
            nextTrigger = true;
        }
    }

    IEnumerator UnlockAnimation(int i) //Place all animations for the Level Unlock Here!
    {
        yield return new WaitForSeconds(1); //How long until these run?
        GameObject.Find("Level " + (i + 1)).transform.GetChild(2).gameObject.GetComponent<Animator>().Play("Unlock"); //Run Unlock Animation
        playAfterLoading = true;
    }

    public void StoreLevelPictures()
    {

    }
}