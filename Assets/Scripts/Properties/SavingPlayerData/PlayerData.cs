[System.Serializable]
public class PlayerData {

    public int totalXP;

    //[region number,level number]
    public int[,] destructionStars = new int[4, 25]; //Total stars earned on the level.
    public int[,] costStars = new int[4, 25];   //Total stars earned on the level.
    public int[,] levelXP = new int[4, 25];   //Total XP for the level.
    public bool[,] unlocked = new bool[4, 25];  //Is the level unlocked?
    public bool[,] unlockedAnim = new bool[4, 25];  //Has the animation played?
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


    public PlayerData(Player player)
    {
        totalXP = player.totalXP;

        destructionStars = player.destructionStars;
        costStars = player.costStars;
        unlocked = player.unlocked;
        unlockedAnim = player.unlockedAnim;
        levelXP = player.levelXP;
        playerLevel = player.playerLevel;
        levelX = player.levelX;
        levelY = player.levelY;
        generatedLevels = player.generatedLevels;
        lastLevel = player.lastLevel;
        explosivesPlaced = player.explosivesPlaced;
        totalCashSpent = player.totalCashSpent;
        totalDamageDealt = player.totalDamageDealt;
        musicStatus = player.musicStatus;
        soundFXStatus = player.soundFXStatus;
    }
}