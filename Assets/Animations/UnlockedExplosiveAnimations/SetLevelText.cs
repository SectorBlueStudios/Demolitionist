using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetLevelText : MonoBehaviour
{
    public GameObject player;
    private void OnEnable()
    {
        GetComponent<TextMeshProUGUI>().text = "LEVEL " + (player.GetComponent<Player>().playerLevel);
    }
}
