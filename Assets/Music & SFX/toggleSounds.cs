using UnityEngine;

public class toggleSounds : MonoBehaviour
{
    public enum type //Type of explosive defined using a dropdown menu.
    {
        music,
        sfx
    };
    public type audioType;

    public GameObject player;
    
    void Start()
    {
        player.GetComponent<Player>().LoadPlayer();
        if (audioType == type.music)
        {
            if (player.GetComponent<Player>().musicStatus == true)
                transform.GetChild(0).gameObject.SetActive(true);
            else
                transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (audioType == type.sfx)
        {

            if (player.GetComponent<Player>().soundFXStatus == true)
                transform.GetChild(0).gameObject.SetActive(true);
            else
                transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
