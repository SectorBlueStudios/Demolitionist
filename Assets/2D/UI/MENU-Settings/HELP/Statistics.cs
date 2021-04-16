using UnityEngine;
using TMPro;

public class Statistics : MonoBehaviour
{
    public int bufferExplosives;
    
    public GameObject player;

    public TextMeshProUGUI playerLevel;
    public TextMeshProUGUI totalXP;
    public TextMeshProUGUI cashSpent;
    public TextMeshProUGUI damageDealt;
    public TextMeshProUGUI totalExplosives;

    public void UpdateStats()
    {
        playerLevel.text = "" + player.GetComponent<Player>().playerLevel;
        totalXP.text = "" + player.GetComponent<Player>().totalXP;
        cashSpent.text = "" + player.GetComponent<Player>().totalCashSpent;
        damageDealt.text = "" + player.GetComponent<Player>().totalDamageDealt;
        totalExplosives.text = "" + player.GetComponent<Player>().explosivesPlaced;
    }

    public void BufferAddExplosive()
    {
        bufferExplosives++;
    }

    public void BufferRemoveExplosive()
    {
        bufferExplosives--;
    }
}
