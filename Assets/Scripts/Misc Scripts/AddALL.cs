using UnityEngine;
using TMPro;

public class AddALL : MonoBehaviour {

    public GameObject levelProperties;

    int region;
    int level;

    public GameObject destructionMeter;
    public GameObject costMeter;

    public GameObject destructionValues; //Values defined by the user.
    public GameObject costValues;

    public GameObject player; //Player
    public GameObject dragController; //used for Statistics script

    public GameObject Stars;

    public GameObject destructionTEXT;
    public GameObject costTEXT;

    public float destructionPercentage;
    public float totalCost;

    private float costMax;

    bool activateBottomBar = false;

    float t = 0.0f;
    float s = 0.0f;

    bool bronzeDMG;
    bool silverDMG;
    bool goldDMG;

    bool bronzeCOST;
    bool silverCOST;
    bool goldCOST;

    GameObject bronzeStarDMG;
    GameObject silverStarDMG;
    GameObject goldStarDMG;
    GameObject bronzeStarCOST;
    GameObject silverStarCOST;
    GameObject goldStarCOST;

    int bufferDamage;
    int bufferCost;
    int bufferRound;

    int newXP;

    bool completed;

    int currentLevelXP;
    int dividedXP;

    float TransitionTime = 0.8f;

    public int textChoice = -1;

    bool round = false;

    bool isFinished = false;

    void OnEnable () {
        if (isFinished == false) //runs the level addition once
        {
            region = levelProperties.GetComponent<LevelProperties>().region;
            level = levelProperties.GetComponent<LevelProperties>().level;

            bufferDamage = 3;
            bufferCost = 3;
            bufferRound = 0;
            newXP = 0;

            player.GetComponent<Player>().addTotalExplosives(dragController.GetComponent<Statistics>().bufferExplosives); //Adds up and saves total explosives placed for that level. dragController contains the Statistics script which is where the total explosives buffer is held.

            #region Destruction
            destructionPercentage = destructionMeter.GetComponent<DamageMeter>().percent;
            player.GetComponent<Player>().addTotalDamage((int)((destructionMeter.GetComponent<DamageMeter>().totalDamage - destructionMeter.GetComponent<DamageMeter>().currentDamage)/10));
            //Debug.Log((int)destructionMeter.GetComponent<DamageMeter>().currentDamage);

            destructionTEXT.GetComponent<TextMeshProUGUI>().text = "" + destructionPercentage + "%";

            if (destructionPercentage < destructionValues.GetComponent<DestructionValues>().bronzeTier) //if the total percent is less than the user-set tier. (HIGHER IS BETTER)
            {
                bronzeDMG = true;
                bufferDamage--;
            }
            if (destructionPercentage < destructionValues.GetComponent<DestructionValues>().silverTier)
            {
                silverDMG = true;
                bufferDamage--;
            }
            if (destructionPercentage < destructionValues.GetComponent<DestructionValues>().goldTier)
            {
                goldDMG = true;
                bufferDamage--;
            }
            #endregion
            #region Cost
            totalCost = costMeter.GetComponent<CostMeter>().totalCost;
            costTEXT.GetComponent<TextMeshProUGUI>().text = "$" + 0;
            costMax = costValues.GetComponent<CostValues>().costMax;

            player.GetComponent<Player>().addTotalCash((int)costMeter.GetComponent<CostMeter>().totalCost);

            //Debug.Log("total Cost: " + totalCost);


            if (totalCost > ((costValues.GetComponent<CostValues>().goldTier) / 100) * costMax) //if the total cost percentage is higher than the tier's percent. (LOWER IS BETTER)
            {
                goldCOST = true;
                bufferCost--;
            }
            if (totalCost > ((costValues.GetComponent<CostValues>().silverTier) / 100) * costMax) //90
            {
                silverCOST = true;
                bufferCost--;
            }
            if (totalCost > ((costValues.GetComponent<CostValues>().bronzeTier) / 100) * costMax) //150
            {
                bronzeCOST = true;
                bufferCost--;
            }
            #endregion
            #region Star Assignment
            bronzeStarDMG = Stars.transform.GetChild(0).gameObject;
            silverStarDMG = Stars.transform.GetChild(1).gameObject;
            goldStarDMG = Stars.transform.GetChild(2).gameObject;
            bronzeStarCOST = Stars.transform.GetChild(3).gameObject;
            silverStarCOST = Stars.transform.GetChild(4).gameObject;
            goldStarCOST = Stars.transform.GetChild(5).gameObject;
            #endregion

            currentLevelXP = Checker();

            #region Debugging
            Debug.Log("Region: " + region);
            Debug.Log("Level: " + level);
            Debug.Log("XP for this level: " + currentLevelXP);
            //Debug.Log("Record XP earned for this level: " + player.GetComponent<Player>().levelXP[0, 0]);
            #endregion

            if (currentLevelXP > player.GetComponent<Player>().levelXP[region - 1, level - 1])   //If the new level's XP is higher than the previous record, run the reset algorithm.
            {
                newXP = currentLevelXP - player.GetComponent<Player>().levelXP[region - 1, level - 1];
                dividedXP = (newXP / (bufferCost + bufferDamage));
                player.GetComponent<Player>().levelXP[region - 1, level - 1] = currentLevelXP;  //Resets the level XP to be added.
                player.GetComponent<Player>().destructionStars[region - 1, level - 1] = 0;      //Resets the stars to be added.
                player.GetComponent<Player>().costStars[region - 1, level - 1] = 0;             //Resets the stars to be added.

                if (player.GetComponent<Player>().unlocked[region, level + 1] == false && currentLevelXP >= 100) //Unlocks the NEXT level
                {
                    //Debug.Log("Here");
                    player.GetComponent<Player>().Unlock(region, level + 1);
                    player.GetComponent<Player>().SavePlayer();
                }

                round = true;
            }
            isFinished = true;
        }
    }    
    int Checker()
    {
        int bufferValue;
        if (bufferDamage == 3 && bufferCost == 3)
            bufferValue = 500;
        else if (bufferDamage == 3 && bufferCost == 2)
            bufferValue = 410;
        else if (bufferDamage == 2 && bufferCost == 3)
            bufferValue = 400;
        else if (bufferDamage == 2 && bufferCost == 2)
            bufferValue = 250;
        else if (bufferDamage == 3 && bufferCost == 1)
            bufferValue = 240;
        else if (bufferDamage == 1 && bufferCost == 3)
            bufferValue = 230;
        else if (bufferDamage == 2 && bufferCost == 1)
            bufferValue = 200;
        else if (bufferDamage == 1 && bufferCost == 2)
            bufferValue = 160;
        else if (bufferDamage == 1 && bufferCost == 1)
            bufferValue = 100;
        else
        {
            bufferValue = 0;
        }
        return bufferValue;
    }
    private void Update()
    {
        destructionTEXT.GetComponent<TextMeshProUGUI>().text = "" + Mathf.RoundToInt(Mathf.Lerp(0, destructionPercentage, t)) + "%"; //Changes percentage text
        if (!bronzeDMG)
        {
            //Debug.Log("Bronze");
            if (t > 0.2)
            {
                bronzeDMG = true; //This is true because it will only trigger this once.
                bronzeStarDMG.SetActive(true); //Shows the star on screen. Triggers animation as well.

                if (player.GetComponent<Player>().destructionStars[region - 1, level - 1] == 0)
                {
                    player.GetComponent<Player>().AddXP(dividedXP);
                    player.GetComponent<Player>().AddDamageStar(region, level);
                    bufferRound += dividedXP;
                }
            }
        }
        if (!silverDMG)
        {
            //Debug.Log("Silver");
            if (t > 0.6)
            {
                silverDMG = true;
                silverStarDMG.SetActive(true);

                if (player.GetComponent<Player>().destructionStars[region - 1, level - 1] == 1)
                {
                    player.GetComponent<Player>().AddXP(dividedXP);
                    player.GetComponent<Player>().AddDamageStar(region, level);
                    bufferRound += dividedXP;
                }
            }
        }
        if (!goldDMG)
        {
            //Debug.Log("Gold");
            if (t > 0.9)
            {
                goldDMG = true;
                goldStarDMG.SetActive(true);

                if (player.GetComponent<Player>().destructionStars[region - 1, level - 1] == 2)
                {
                    player.GetComponent<Player>().AddXP(dividedXP);
                    player.GetComponent<Player>().AddDamageStar(region, level);
                    bufferRound += dividedXP;
                }
            }
        }

        t += TransitionTime * Time.deltaTime; //Sets the speed of how fast the transition occurs. The "TransitionTime" variable is set by the user.

        if (t > 1.0f)
        {
            t = 1.0f;
            textChoice = 1;
            activateBottomBar = true; //Activates the bottom bar's animations when the top is finished.
        }

        if (activateBottomBar == true)
        {
            costTEXT.GetComponent<TextMeshProUGUI>().text = "$" + Mathf.RoundToInt(Mathf.Lerp(0, totalCost, s)); //Changes the cost text in relation to time.
            if (!bronzeCOST && totalCost != 0) //totalCost >= bronze
            {
                //Debug.Log("Bronze");
                if (s >= .2)
                {
                    bronzeCOST = true;
                    bronzeStarCOST.SetActive(true);

                    if (player.GetComponent<Player>().costStars[region - 1, level - 1] == 0)
                    {
                        player.GetComponent<Player>().AddXP(dividedXP);
                        player.GetComponent<Player>().AddCostStar(region, level);
                        bufferRound += dividedXP;
                    }
                }
            }
            if (!silverCOST && totalCost != 0)
            {
                //Debug.Log("Silver");
                if (s >= .6)
                {
                    silverCOST = true;
                    silverStarCOST.SetActive(true);

                    if (player.GetComponent<Player>().costStars[region - 1, level - 1] == 1)
                    {
                        player.GetComponent<Player>().AddXP(dividedXP);
                        player.GetComponent<Player>().AddCostStar(region, level);
                        bufferRound += dividedXP;
                    }
                }
            }
            if (!goldCOST && totalCost != 0)
            {
                //Debug.Log("Gold");
                if (s >= .9)
                {
                    goldCOST = true;
                    goldStarCOST.SetActive(true);

                    if (player.GetComponent<Player>().costStars[region - 1, level - 1] == 2)
                    {
                        player.GetComponent<Player>().AddXP(dividedXP);
                        player.GetComponent<Player>().AddCostStar(region, level);
                        bufferRound += dividedXP;
                    }
                }
            }

            s += TransitionTime * Time.deltaTime; //Sets the speed of how fast the transition occurs. The "TransitionTime" variable is set by the user.

            if (s > 1.0f)
            {
                s = 1.0f;
                player.GetComponent<Player>().SavePlayer(); //Saves the player's date once everything is finished.
                if (!completed)
                {
                    //Debug.Log("Addition completed");
                    completed = true;
                    if (round == true)
                    {
                        //Debug.Log("Offset XP: " + (newXP - bufferRound));
                        player.GetComponent<Player>().AddXP(newXP - bufferRound); //Rounds the XP up due to the integer division errors.
                        player.GetComponent<Player>().SavePlayer();
                    }
                }
            }
        }
    }
}