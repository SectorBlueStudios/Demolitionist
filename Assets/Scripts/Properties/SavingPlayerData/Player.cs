using UnityEngine;

public class Player : MonoBehaviour {

    public int totalXP;

    //[region number,level number]
    public int[,] destructionStars = new int[4, 25]; //Total damage stars earned on the level.
    public int[,] costStars = new int[4, 25];   //Total cost stars earned on the level.
    public int[,] levelXP = new int[4, 25];   //Total XP for the level.
    public bool[,] unlocked = new bool[4, 25];  //Is the level unlocked?
    public bool[,] unlockedAnim = new bool[4, 25];  //Has the Unlock Animation been run?
    public float[,] levelX = new float[4, 25];
    public float[,] levelY = new float[4, 25];
    public bool[] generatedLevels = new bool[4];
    public int[] lastLevel = new int[2]; // 0 = Region, 1 = Level

    public int playerLevel;

    public int explosivesPlaced;
    public int totalCashSpent;
    public int totalDamageDealt;

    public bool musicStatus = true;
    public bool soundFXStatus = true;

    public void Start()
    {        
        for (int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 25; j++)
            {
                destructionStars[i, j] = 0;
                costStars[i, j] = 0;
                levelXP[i, j] = 0;
                levelX[i, j] = 0;
                levelY[i, j] = 0;
            }
            generatedLevels[i] = false;
        }
        for(int i = 0; i < 2; i++)
        {
            lastLevel[i] = 0;
        }
        playerLevel = 1;
        LoadPlayer();
    }

    public void SavePlayer ()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer(this);
        totalXP = data.totalXP;

        destructionStars = data.destructionStars;
        costStars = data.costStars;
        unlocked = data.unlocked;
        unlockedAnim = data.unlockedAnim;
        levelXP = data.levelXP;
        playerLevel = data.playerLevel;
        levelX = data.levelX;
        levelY = data.levelY;
        generatedLevels = data.generatedLevels;
        lastLevel = data.lastLevel;
        explosivesPlaced = data.explosivesPlaced;
        totalCashSpent = data.totalCashSpent;
        totalDamageDealt = data.totalDamageDealt;
        musicStatus = data.musicStatus;
        soundFXStatus = data.soundFXStatus;
    }

    public void Reset()
    {
        totalXP = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 24; j++)
            {
                destructionStars[i, j] = 0;
                costStars[i, j] = 0;
                levelXP[i, j] = 0;
                unlocked[i, j] = false;
                unlockedAnim[i, j] = false;
                levelX[i, j] = 0;
                levelY[i, j] = 0;
            }
            generatedLevels[i] = false;
        }
        for (int i = 0; i < 2; i++)
        {
            lastLevel[i] = 0;
        }
        unlocked[0,0] = true; //Unlock certain levels. Input ACTUAL Region/Level
        unlockedAnim[0,0] = true;
        playerLevel = 1;
        explosivesPlaced = 0;
        totalCashSpent = 0;
        totalDamageDealt = 0;
        musicStatus = true;
        soundFXStatus = true;

        SavePlayer();
    }

    public void AddXP(int XP)
    {
        totalXP += XP;
    }

    public int LevelXP(int region, int level)
    {
        if (destructionStars[region - 1, level - 1] == 3 && costStars[region - 1, level - 1] == 3)      //3:3
            levelXP[region - 1, level - 1] = 500;
        else if (destructionStars[region - 1, level - 1] == 3 && costStars[region - 1, level - 1] == 2) //3:2
            levelXP[region - 1, level - 1] = 410;
        else if (destructionStars[region - 1, level - 1] == 2 && costStars[region - 1, level - 1] == 3) //2:3
            levelXP[region - 1, level - 1] = 400;
        else if (destructionStars[region - 1, level - 1] == 2 && costStars[region - 1, level - 1] == 2) //2:2
            levelXP[region - 1, level - 1] = 250;
        else if (destructionStars[region - 1, level - 1] == 3 && costStars[region - 1, level - 1] == 1) //3:1
            levelXP[region - 1, level - 1] = 240;
        else if (destructionStars[region - 1, level - 1] == 1 && costStars[region - 1, level - 1] == 3) //1:3
            levelXP[region - 1, level - 1] = 230;
        else if (destructionStars[region - 1, level - 1] == 2 && costStars[region - 1, level - 1] == 1) //2:1
            levelXP[region - 1, level - 1] = 200;
        else if (destructionStars[region - 1, level - 1] == 1 && costStars[region - 1, level - 1] == 2) //1:2
            levelXP[region - 1, level - 1] = 160;
        else if (destructionStars[region - 1, level - 1] == 1 && costStars[region - 1, level - 1] == 1) //1:1
            levelXP[region - 1, level - 1] = 100;
        else
            levelXP[region - 1, level - 1] = 0; //0:X X:0

        return levelXP[region - 1, level - 1];
    }

    public void AddCostStar(int region, int level)
    {
        costStars[region - 1, level - 1]++;
        //Debug.Log("Cost stars: " + (costStars[region - 1, level - 1]));
    }
    public void AddDamageStar(int region, int level) //ACTUAL
    {
        destructionStars[region - 1, level - 1]++;
        //Debug.Log("Destruction stars: " + (destructionStars[region - 1, level - 1]));
    }
    public void Unlock(int region, int level) //ACTUAL
    {
        unlocked[region - 1, level - 1] = true;
    }

    public void UnlockAnim(int region, int level) //ACTUAL
    {
        unlockedAnim[region - 1, level - 1] = true;
    }

    public void GeneratedLevels(int region) //ACTUAL
    {
        generatedLevels[region - 1] = true;
    }

    public void addTotalExplosives(int totalExplosives)
    {
        explosivesPlaced += totalExplosives;
    }

    public void addTotalCash(int totalCash)
    {
        totalCashSpent += totalCash;
    }

    public void addTotalDamage(int totalDamage)
    {
        totalDamageDealt += totalDamage;
    }

    public void ToggleMusic()
    {
        musicStatus = !musicStatus;
    }

    public void ToggleSFX()
    {
        soundFXStatus = !soundFXStatus;
    }
}

//Things to save:

    //Total XP - (Level is calculated off of XP).
    //Region,Level
    //Total Stars - (Anything one star and over is complete).
    //Unlocked?
    //Has the Unlock Animation been run?