using UnityEngine;
using TMPro;
public class LevelMeter : MonoBehaviour {

    public GameObject player;

    public RectTransform gradient;
    public TextMeshProUGUI levelCompletion;
    public TextMeshProUGUI levelTEXT;

    public GameObject levelImage;

    public float percentComplete;
    
    int level1 = XPvalues.level1;
    int level2 = XPvalues.level2;
    int level3 = XPvalues.level3;
    int level4 = XPvalues.level4;
    int level5 = XPvalues.level5;
    int level6 = XPvalues.level6;
    int level7 = XPvalues.level7;
    int level8 = XPvalues.level8;
    int level9 = XPvalues.level9;
    int level10 = XPvalues.level10;

    int currentLevel = 0;

    int[] levels = new int[10];

    int totalXP;

    public void Start()
    {
        totalXP = player.GetComponent<Player>().totalXP;

        levels[0] = level1;
        levels[1] = level2;
        levels[2] = level3;
        levels[3] = level4;
        levels[4] = level5;
        levels[5] = level6;
        levels[6] = level7;
        levels[7] = level8;
        levels[8] = level9;
        levels[9] = level10;
    }

    public void Update()
    {
        int playerLevel = player.GetComponent<Player>().playerLevel; //Sets current player level and then proceeds with calculation

        totalXP = player.GetComponent<Player>().totalXP;

        CurrentLevel();
        player.GetComponent<Player>().playerLevel = currentLevel + 1;

        float threshold = levels[currentLevel + 1] - levels[currentLevel]; //Takes the next level minus the current level.
        float newXP = totalXP - levels[currentLevel]; //Subtracts the current level XP from the total XP earned.
        percentComplete = newXP / threshold; //Outputs the percentage of how far along the status bar should be. From the current level to the next.

        #region Debug functions
        //Debug.Log(percentComplete);
        //Debug.Log(threshold);
        //Debug.Log(totalXP);
        //Debug.Log(levels[currentLevel]);
        //Debug.Log(levels[currentLevel + 1]);
        //Debug.Log(currentLevel);
        //Debug.Log(newXP);
        //Debug.Log(percentComplete);
        #endregion

        gradient.offsetMax = new Vector2(Mathf.Lerp(-550, -120, percentComplete), gradient.offsetMax.y);
        levelCompletion.text = totalXP + " XP";
        levelTEXT.text =  "LEVEL " + (currentLevel + 1);

        if (playerLevel < player.GetComponent<Player>().playerLevel) //If the current player level is less than what was set before the calculation
                                                                     // then it is considered a level-up.
        {
            levelImage.GetComponent<Animator>().SetBool("triggerPulse", true); //Triggers the Animation to pulse the level image
            GetComponent<LoadUnlockExplosive>().LoadUnlockExplosiveScene();
        }


    }
    private void CurrentLevel()
    {
        int level = 0;
        int XP = totalXP;
        while(XP > levels[level + 1])
        {
            level++;
            //Debug.Log("LEVEL UP!");
            //GetComponent<LoadUnlockExplosive>().LoadUnlockExplosiveScene();
        }
        currentLevel = level;
    }
}