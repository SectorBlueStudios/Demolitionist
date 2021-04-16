using UnityEngine;
using UnityEngine.UI;

public class LevelImageSwitcher : MonoBehaviour
{
    public GameObject player;
    public GameObject levelParent;

    void Update()
    {
        GetComponent<Image>().sprite = levelParent.transform.GetChild(player.GetComponent<Player>().playerLevel - 1).GetComponent<Image>().sprite;        
    }
}